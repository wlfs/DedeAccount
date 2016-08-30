using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace DedeAccount
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.panel1.Visible = false;
            this.panel3.Visible = false;
            this.panel2.Visible = false;
        }
        bool is_root = true;
        private void _getData(List<Site> sites, List<Category> cs, TreeNodeCollection nodes)
        {
            foreach (TreeNode item in nodes)
            {
                if (item.Tag is Category)
                {
                    if (item.Nodes.Count > 0)
                    {
                        var t = (Category)item.Tag;
                        t.Childs.Clear();
                        t.Sites.Clear();
                        _getData(t.Sites, t.Childs, item.Nodes);
                        cs.Add(t);
                    }
                    else
                    {
                        cs.Add((Category)item.Tag);
                    }
                }
                else
                {
                    sites.Add((Site)item.Tag);
                }
            }
        }
        public void saveData()
        {
            List<object> data = new List<object>();
            foreach (TreeNode item in treeView1.Nodes)
            {
                if (item.Nodes.Count > 0)
                {
                    var t = (Category)item.Tag;
                    t.Childs.Clear();
                    t.Sites.Clear();
                    _getData(t.Sites, t.Childs, item.Nodes);
                    data.Add(t);
                }
                else
                {
                    data.Add(item.Tag);
                }
            }

            FileStream fs = null;
            try
            {
                fs = new FileStream(@"data.dat", FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, data);
            }
            catch (Exception)
            {

            }
            finally
            {
                fs.Close();
            }

        }
        private void ReadData()
        {
            if (File.Exists("data.dat"))
            {
                FileStream fs = null;
                List<object> data = null;
                try
                {
                    fs = new FileStream(@"data.dat", FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    data = bf.Deserialize(fs) as List<object>;
                }
                catch (Exception)
                {

                }
                finally
                {
                    fs.Close();
                }
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        if (item is Site)
                        {
                            var site = (Site)item;
                            addNode2(treeView1.Nodes, site.Name, false, site);
                        }
                        else
                        {
                            var c = (Category)item;
                            var nod = addNode2(treeView1.Nodes, c.Name, true, c);
                            _bindNodes(c, nod);
                        }
                    }
                    treeView1.ExpandAll();
                }
            }
        }
        private void _bindNodes(Category c, TreeNode nodes)
        {
            foreach (var item in c.Sites)
            {
                addNode2(nodes.Nodes, item.Name, false, item);
            }
            foreach (var item in c.Childs)
            {
                var nod = addNode2(nodes.Nodes, item.Name, true, item);
                _bindNodes(item, nod);
            }
        }
        private Category currentCategory;
        private Site currentSite;

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //添加文件夹
            changePanel(2);
            if (currentCategory == null || !currentCategory.IsAdd)
            {
                currentCategory = new Category();
                currentCategory.IsAdd = true;
                BindCategory(true);
            }
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            is_root = true;
            toolStripButton1_Click(sender, e);
        }
        private void changePanel(int pannelIndex=0)
        {
            if (pannelIndex == 2)
            {
                if (!this.panel2.Visible)
                {
                    this.panel1.Visible = false;
                    this.panel3.Visible = false;
                    this.panel2.Visible = true;
                }
            }
            else if (pannelIndex == 1)
            {
                if (!this.panel1.Visible)
                {
                    this.panel1.Visible = true;
                    this.panel2.Visible = false;
                    this.panel3.Visible = false;
                }
            }
            else {
                if (!this.panel3.Visible)
                {
                    this.panel1.Visible = false;
                    this.panel2.Visible = false;
                    this.panel3.Visible = true;
                }
            }
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //添加站点
            changePanel(1);
            if (currentSite == null || !currentSite.IsAdd)
            {
                currentSite = new Site();
                currentSite.IsAdd = true;
                BindSite(true);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Tag is Site)
                {
                    currentSite = (Site)treeView1.SelectedNode.Tag;
                    changePanel(3);
                    BindSite();
                }
                else
                {
                    is_root = false;
                    currentCategory = (Category)treeView1.SelectedNode.Tag;
                    changePanel(2);
                    BindCategory();
                }
            }
            //点击事件
        }
        private void BindCategory(bool is_add = false)
        {
            textBox7.Text = currentCategory.Name;
            textBox8.Text = currentCategory.Remark;
            if (is_add)
            {
                button10.Text = "添加";
            }
            else
            {
                button10.Text = "保存";
            }
        }
        private void BindSite(bool is_add = false)
        {
            textBox15.Text = currentSite.AdminFolder;
           
            textBox1.Text = currentSite.AdminUrl;
            textBox2.Text = currentSite.Name;
            textBox3.Text = currentSite.Account;
            textBox4.Text = currentSite.Passowrd;
            textBox5.Text = currentSite.Url;
            textBox6.Text = currentSite.Remark;
            //绑定View
            textBox16.Text = currentSite.AdminFolder;
            textBox10.Text = currentSite.AdminUrl;
            textBox14.Text = currentSite.Name;
            textBox13.Text = currentSite.Account;
            textBox12.Text = currentSite.Passowrd;
            textBox11.Text = currentSite.Url;
            textBox9.Text = currentSite.Remark;
            if (is_add)
            {
                button7.Text = "添加";
            }
            else
            {
                button7.Text = "保存";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //保存
            currentSite.AdminUrl = textBox1.Text;
            currentSite.AdminFolder = textBox15.Text;
            currentSite.Name = textBox2.Text;
            currentSite.Account = textBox3.Text;
            currentSite.Passowrd = textBox4.Text;
            currentSite.Url = textBox5.Text;
            currentSite.Remark = textBox6.Text;
            if (currentSite.IsAdd)
            {
                currentSite.IsAdd = false;
                addNode(currentSite.Name, false, currentSite);
            }
            else
            {
                if (treeView1.SelectedNode != null)
                {
                    treeView1.SelectedNode.Tag = currentSite;
                    treeView1.SelectedNode.Text = currentSite.Name;
                }
            }
            MessageBox.Show("操作成功");
            saveData();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            currentCategory.Name = textBox7.Text;
            currentCategory.Remark = textBox8.Text;
            if (currentCategory.IsAdd)
            {
                currentCategory.IsAdd = false;
                addNode(currentCategory.Name, true, currentCategory);
            }
            else
            {
                if (treeView1.SelectedNode != null)
                {
                    treeView1.SelectedNode.Tag = currentCategory;
                    treeView1.SelectedNode.Text = currentCategory.Name;
                }
            }
            MessageBox.Show("操作成功");
            saveData();
        }
        private TreeNode addNode2(TreeNodeCollection nodes, String name, bool is_dir, object o)
        {
            TreeNode node = new TreeNode();
            node.Text = name;
            node.Tag = o;
            if (is_dir)
            {
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
            }
            else
            {
                node.ImageIndex = 1;
                node.SelectedImageIndex = 1;
            }
            nodes.Add(node);
            return node;
        }
        private TreeNode addNode(String name, bool is_dir, object o)
        {
            TreeNode node = new TreeNode();
            node.Text = name;
            node.Tag = o;
            if (is_dir)
            {
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
            }
            else
            {
                node.ImageIndex = 1;
                node.SelectedImageIndex = 1;
            }
            
            if (treeView1.SelectedNode != null&& !is_root)
            {
                if (treeView1.SelectedNode.Tag is Category)
                {
                    treeView1.SelectedNode.Nodes.Add(node);
                    return node;
                }
            }
            treeView1.Nodes.Add(node);
            return node;
        }

        private void btnGAccount_Click(object sender, EventArgs e)
        {
            //生成账号
            textBox3.Text = Util.RandomStr(30);
        }

        private void btnGPassword_Click(object sender, EventArgs e)
        {
            //生成密码
            textBox4.Text = Util.RandomStr(32);
        }
        private void btnGAdminFolder_Click(object sender, EventArgs e)
        {
            //生成管理地址
            textBox15.Text = Util.RandomStr(32);
            textBox1.Text = textBox5.Text.TrimEnd('/') + "/" + textBox15.Text;
        }
        private void btnViewUrl_Click(object sender, EventArgs e)
        {
            //浏览站点
            try
            {
                System.Diagnostics.Process.Start(textBox5.Text);
            }
            catch (Exception)
            {


            }
        }

        private void btnViewAdminUrl_Click(object sender, EventArgs e)
        {
            //浏览管理地址
            try
            {
                System.Diagnostics.Process.Start(textBox5.Text + "/" + textBox1.Text);
            }
            catch (Exception)
            {

            }
        }

        private void btnCopyAccount_Click(object sender, EventArgs e)
        {
            //复制账号
            Clipboard.SetDataObject(textBox3.Text);
        }

        private void btnCopyPassword_Click(object sender, EventArgs e)
        {
            //复制密码
            Clipboard.SetDataObject(textBox4.Text);
        }

        private void btnCopyUrl_Click(object sender, EventArgs e)
        {
            //复制站点地址
            Clipboard.SetDataObject(textBox5.Text);
        }

        private void btnCopayAdminUrl_Click(object sender, EventArgs e)
        {
            //复制管理地址
            Clipboard.SetDataObject(textBox1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
            ReadData();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null) {
                treeView1.SelectedNode.Remove();
                this.saveData();
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {
            //显示修改
            changePanel(1);
        }

        private void btnCopySite_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            WriteSite(treeView1.SelectedNode,sb);
            Clipboard.SetDataObject(sb.ToString());
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //导入
            FrmImportSite fm = new FrmImportSite(this);
            fm.ShowDialog();
        }
        private Point Position = new Point(0, 0);

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode myNode = null;
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                myNode = (TreeNode)(e.Data.GetData(typeof(TreeNode)));
            }
            else
            {
                MessageBox.Show("error");
            }
            Position.X = e.X;
            Position.Y = e.Y;
            Position = treeView1.PointToClient(Position);
            TreeNode DropNode = this.treeView1.GetNodeAt(Position);
            // 1.目标节点不是空。2.目标节点不是被拖拽接点的字节点。3.目标节点不是被拖拽节点本身
            if (DropNode != null && DropNode.Parent != myNode && DropNode != myNode)
            {
                if (DropNode.Tag is Site) {
                    MessageBox.Show("不能拖动到站点上！");
                    return;
                }
                TreeNode DragNode = myNode;
                // 将被拖拽节点从原来位置删除。
                myNode.Remove();
                // 在目标节点下增加被拖拽节点
                DropNode.Nodes.Add(DragNode);
            }
            // 如果目标节点不存在，即拖拽的位置不存在节点，那么就将被拖拽节点放在根节点之下
            if (DropNode == null)
            {
                TreeNode DragNode = myNode;
                myNode.Remove();
                treeView1.Nodes.Add(DragNode);
            }
        }

        private void 密码生成器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmStrG fsg = new FrmStrG();
            fsg.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            //设置文件类型 
            sfd.Filter = "数据库备份文件（*.bak）|*.bak|文本文件（*.txt）|*.txt|日志文件（*.log）|*.log";

            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;

            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;

            //点了保存按钮进入 
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = sfd.FileName.ToString(); //获得文件路径 
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径
                //导出
                StringBuilder sb = new StringBuilder();
                ReadSite(treeView1.Nodes, sb);
                System.IO.FileStream fs = (System.IO.FileStream)sfd.OpenFile();//输出文件
                StreamWriter wr = new StreamWriter(fs);
                wr.Write(sb.ToString());
                wr.Close();
                fs.Close();
                MessageBox.Show("导出成功");
            }
            
        }
        private void WriteSite(TreeNode node, StringBuilder sb) {
            var site = (Site)node.Tag;
            sb.AppendFormat("{0}{5}      账号：{1}{5}      密码：{2}{5}      站点地址：{3}{5}      管理地址：{4}{5}", node.FullPath, site.Account, site.Passowrd, site.Url, site.AdminUrl, Environment.NewLine);
            if (!string.IsNullOrEmpty(site.Remark)) {
                using (StringReader sr = new StringReader(site.Remark))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.AppendLine("      " + line);
                    }
                }
            }
        }
        private void ReadSite(TreeNodeCollection nodes, StringBuilder sb) {
            foreach (TreeNode item in nodes)
            {
                if (item.Tag is Site)
                {
                    WriteSite(item,sb);
                    sb.AppendLine("==============================================");
                }
                else {
                    ReadSite(item.Nodes, sb);
                }
            }
        }

        private void btnCopyAdminFolder(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(textBox15.Text);
        }
    }
}

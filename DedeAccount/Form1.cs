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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
        private void saveData()
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
            changePanel(true);
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
        private void changePanel(bool is_dir)
        {
            if (is_dir)
            {
                if (this.panel1.Visible)
                {
                    this.panel1.Visible = false;
                    this.panel2.Visible = true;
                    this.AcceptButton = button10;
                }
            }
            else
            {
                if (this.panel2.Visible)
                {
                    this.panel1.Visible = true;
                    this.panel2.Visible = false;
                    this.AcceptButton = button7;
                }
            }
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //添加站点
            changePanel(false);
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
                    changePanel(false);
                    BindSite();
                }
                else
                {
                    is_root = false;
                    currentCategory = (Category)treeView1.SelectedNode.Tag;
                    changePanel(true);
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
            textBox1.Text = currentSite.AdminUrl;
            textBox2.Text = currentSite.Name;
            textBox3.Text = currentSite.Account;
            textBox4.Text = currentSite.Passowrd;
            textBox5.Text = currentSite.Url;
            textBox6.Text = currentSite.Remark;
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

        private void button2_Click(object sender, EventArgs e)
        {
            //生成账号
            textBox3.Text = Util.RandomStr(30);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //生成密码
            textBox4.Text = Util.RandomStr(32);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //生成管理地址
            textBox1.Text = textBox5.Text + "/" + Util.RandomStr(32);
        }
        private void button8_Click(object sender, EventArgs e)
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

        private void button9_Click(object sender, EventArgs e)
        {
            //浏览管理地址
            try
            {
                System.Diagnostics.Process.Start(textBox1.Text);
            }
            catch (Exception)
            {


            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //复制账号
            Clipboard.SetDataObject(textBox3.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //复制密码
            Clipboard.SetDataObject(textBox4.Text);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //复制站点地址
            Clipboard.SetDataObject(textBox5.Text);
        }

        private void button6_Click(object sender, EventArgs e)
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
            }
        }

        
    }
}

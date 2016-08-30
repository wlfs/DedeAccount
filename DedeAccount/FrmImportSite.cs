using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DedeAccount
{
    public partial class FrmImportSite : Form
    {
        FrmMain mainForm;
        public FrmImportSite(FrmMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void FrmImportSite_Load(object sender, EventArgs e)
        {

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            //导入数据
            var content = txtContent.Text;
            if (!string.IsNullOrEmpty(content))
            {
                var itemCount = 0;
                List<Site> sites = new List<Site>();
                Site site = new Site();
                sites.Add(site);
                using (StringReader sr = new StringReader(content))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.IndexOf("===========") >= 0)
                        {
                            if (itemCount > 0)
                            {
                                if (!string.IsNullOrEmpty(site.Url)&& site.AdminUrl.Length>site.Url.Length&&site.AdminUrl.IndexOf(site.Url)>-1) {
                                    site.AdminFolder = site.AdminUrl.Substring(site.Url.Length).Trim('/');
                                }
                            }
                            itemCount = 0;
                            site = new Site();
                            sites.Add(site);
                            continue;
                        }
                        if (itemCount < 5)
                        {
                            if (!string.IsNullOrEmpty(line))
                            {
                                if (itemCount == 0)
                                {
                                    site.Name = line;
                                }
                                else if (line.IndexOf("账号") > -1)
                                {
                                    site.Account = GetValue(line);
                                }
                                else if (line.IndexOf("密码") > -1)
                                {
                                    site.Passowrd = GetValue(line);
                                }
                                else if (line.IndexOf("站点地址") > -1)
                                {
                                    site.Url = GetValue(line);
                                }
                                else if (line.IndexOf("管理地址") > -1)
                                {
                                    site.AdminUrl = GetValue(line);
                                }
                                itemCount++;

                            }
                        }
                        else
                        {
                            if (line.Length >= 6)
                            {
                                line = line.Substring(6);
                            }
                            if (string.IsNullOrEmpty(site.Remark))
                            {
                                site.Remark = line+ Environment.NewLine;
                            }
                            else
                            {
                                site.Remark += line+ Environment.NewLine;
                            }
                        }

                    }

                }

                foreach (var item in sites)
                {
                    if (!String.IsNullOrEmpty(item.Account))
                    {
                        var paths = item.Name.Split('-');
                        var nodes = mainForm.treeView1.Nodes;
                        TreeNode node = null;
                        for (int i = 0; i < paths.Length - 1; i++)
                        {
                            node = FindOrCreateNode(nodes, paths[i]);
                            nodes = node.Nodes;
                        }
                        CreateSiteNode(item, paths[paths.Length - 1], nodes, 0);
                    }
                }
                mainForm.saveData();
                MessageBox.Show("导入成功");
                this.Close();

            }
        }
        private string GetValue(string line)
        {
            if (line.IndexOf("：") > 0)
            {
                return line.Substring(line.IndexOf("：") + 1);
            }
            else if (line.IndexOf(":") > 0)
            {
                return line.Substring(line.IndexOf(":") + 1);
            }
            return line;
        }
        private void CreateSiteNode(Site site, string name, TreeNodeCollection nodes, int index)
        {
            if (index > 0)
            {
                name = name + "(" + index + ")";
            }
            var result = FindNodes(nodes, name);
            if (result != null)
            {
                CreateSiteNode(site, name, nodes, ++index);
            }
            else
            {
                site.Name = name;
                TreeNode node = new TreeNode();
                node.Text = name;
                node.Tag = site;
                node.ImageIndex = 1;
                node.SelectedImageIndex = 1;
                nodes.Add(node);
            }
        }
        public TreeNode FindNodes(TreeNodeCollection nodes, string name)
        {
            foreach (TreeNode item in nodes)
            {
                if (item.Text == name)
                {
                    return item;
                }
            }
            return null;
        }

        public TreeNode FindOrCreateNode(TreeNodeCollection nodes, string name)
        {
            var result = FindNodes(nodes, name);
            if (result != null)
            {
                return result;
            }
            else
            {
                TreeNode node = new TreeNode();
                node.Text = name;
                node.Tag = new Category() { Name = name, IsAdd = false };
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
                nodes.Add(node);
                return node;
            }
        }
    }
}

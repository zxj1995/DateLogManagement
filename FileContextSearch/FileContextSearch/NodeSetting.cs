using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FileContextSearch
{
    public partial class NodeSetting : Form 
    {
        public string ParentPath = "";
        private Form1 FM = new Form1();
        public string strType = "";
        public NodeSetting(string parentNode,Form1 FM1)
        {

            if (string.IsNullOrEmpty(parentNode))
            {
                ParentPath = Form1.ResearchPathDir;
            }
            else
            {
                if (parentNode == FileSearchHelper.GetInstance().ResearchDir)
                {
                    ParentPath = Form1.ResearchPathDir;
                }
                else
                {
                    if (!Directory.Exists(Path.Combine(Form1.ResearchPathDir, parentNode, "SubNodes")))
                    {
                        Directory.CreateDirectory(Path.Combine(Form1.ResearchPathDir, parentNode, "SubNodes"));
                    }
                    ParentPath = Path.Combine(Form1.ResearchPathDir, parentNode, "SubNodes");
                }
            }
            FM = FM1;
            InitializeComponent();
        }
        public NodeSetting(string _ParentPath, Form1 FM1, string _strType)
        {
            FM = FM1;
            ParentPath = _ParentPath;
            strType = _strType;
            InitializeComponent();
        }

        private void ResearchNodeCreate()
        {

            if (string.IsNullOrEmpty(NodeName.Text))
            {
                MessageBox.Show("请先填写添加节点名称！", "ERROR", MessageBoxButtons.OK);
                return;
            }
            var booltemp = false;
            var dirArr = new string[] { "Issues", "Progress", "Summary", "Demo", "SubNodes" };
            var dirtemp = Path.Combine(ParentPath, NodeName.Text);
            if (!Directory.Exists(dirtemp))
            {
                Directory.CreateDirectory(dirtemp);
                booltemp = true;
            }
            else
            {
                booltemp = false;
            }

            //创建基础文件及文件夹
            //创建文件夹
            var filePath = Path.Combine(ParentPath, NodeName.Text, "Progress", "Progress_Log.txt");
            if (booltemp)
            {
                var dirTempStr = "";
                foreach (var item in dirArr)
                {
                    dirTempStr = Path.Combine(ParentPath, NodeName.Text, item);
                    if (!Directory.Exists(dirTempStr))
                    {
                        Directory.CreateDirectory(dirTempStr);
                    }
                }
                dirTempStr = Path.Combine(ParentPath, NodeName.Text, "Summary", "ImportInfo");
                if (!Directory.Exists(dirTempStr))
                {
                    Directory.CreateDirectory(dirTempStr);
                }
                //创建文件
                if (!File.Exists(filePath))
                {
                    using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    {

                    }
                    var strTemp = "CurrentState:Conquering";
                    File.WriteAllText(filePath, strTemp);
                }
            }

            if (!checkBox1.Checked)
            {
                this.Close();
                return;
            }
            else
            {
                System.Diagnostics.Process.Start(dirtemp);
                System.Diagnostics.Process.Start(filePath);
                this.Close();
            }

        }

        private void CreateNewNode(string projectName)
        {
            var TN = new TreeNode();
            TN.Name = Path.Combine(ParentPath, projectName);
            TN.Text = projectName;
            if (Directory.Exists(TN.Name))
            {
                MessageBox.Show("该项目已存在！", "", MessageBoxButtons.OK);
            }
            else
            {
                if (!Directory.Exists(TN.Name))
                {
                    Directory.CreateDirectory(TN.Name);
                }
              
                var fileArr = new string[] { "Master", "Bakup", "Readme.txt" };
                if (!Directory.Exists(Path.Combine(TN.Name, fileArr[0])))
                {
                    Directory.CreateDirectory(Path.Combine(TN.Name, fileArr[0]));
                }
                if (!Directory.Exists(Path.Combine(TN.Name, fileArr[1])))
                {
                    Directory.CreateDirectory(Path.Combine(TN.Name, fileArr[1]));
                }
                if (!File.Exists(Path.Combine(TN.Name, fileArr[2])))
                {
                    using (var fs = new FileStream(Path.Combine(TN.Name, fileArr[2]), FileMode.OpenOrCreate))
                    {

                    }
                }
                //File.Create(Path.Combine(TN.Name, fileArr[2]));
                //导入源码
            }
            if (!checkBox1.Checked)
            {
                this.Close();
                return;
            }
            else
            {
                System.Diagnostics.Process.Start(TN.Name);
                var fileArr = new string[] { "Master", "Bakup", "Readme.txt" };
                System.Diagnostics.Process.Start(Path.Combine(TN.Name, fileArr[2]));
                this.Close();
            }
            //Directory.CreateDirectory(TN.Name);
        }

        private void ProjectNodeCreate()
        {
            if (!string.IsNullOrEmpty(ParentPath))
            {
                if (!string.IsNullOrEmpty(NodeName.Text))
                {
                    CreateNewNode(NodeName.Text);
                    //var tn = new TreeNode();
                    //tn.name
                }
                else
                {
                    MessageBox.Show("请先填写项目名称！", "Warning", MessageBoxButtons.OK);
                }
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(strType))
            {
                ResearchNodeCreate();
            }
            else
            {
                ProjectNodeCreate();
            }

        }

        private void NodeSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            FM.tabControl1_SelectedIndexChanged(sender,e);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(SourcePath.Text))
            {
                if (Directory.Exists(SourcePath.Text))
                {
                    //此处预留复制逻辑
                    //考虑直接用文件压缩来操作整个文件夹

                    //var di = new DirectoryInfo(SourcePath.Text);
                    //di.
                }
                else
                {
                    MessageBox.Show("源码地址不存在！", "Warning", MessageBoxButtons.OK);

                }
            }
            else
            {
                MessageBox.Show("请先填写项目地址！", "Warning", MessageBoxButtons.OK);
            }

        }
    }
}

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
        public NodeSetting(string parentNode)
        {
            if (string.IsNullOrEmpty(parentNode))
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
            InitializeComponent();
        }
        
        private void button13_Click(object sender, EventArgs e)
        {
            var dirArr = new string[]{"Issues","Progress","Summary","Demo","SubNodes"};
            var dirtemp = Path.Combine(ParentPath, NodeName.Text);
            if (!Directory.Exists(dirtemp))
            {
                Directory.CreateDirectory(dirtemp);
            }
            //创建基础文件及文件夹
            //创建文件夹
            var dirTempStr = "";
            foreach (var item in dirArr)
            {
                 dirTempStr = Path.Combine(ParentPath, NodeName.Text,item);

                if (!Directory.Exists(dirTempStr))
                {
                    Directory.CreateDirectory(dirTempStr);
                }
            }
            dirTempStr=Path.Combine(ParentPath, NodeName.Text, "Summary", "ImportInfo");
            if (!Directory.Exists(dirTempStr))
            {
                Directory.CreateDirectory(dirTempStr);
            }
            //创建文件
            var filePath = Path.Combine(ParentPath, NodeName.Text,"Progress", "Progress_Log.txt");
            if (!File.Exists(filePath))
            {
                using (var fs=new FileStream(filePath,FileMode.OpenOrCreate))
                {
                   
                }
                var strTemp = "CurrentState:Conquering";
                File.WriteAllText(filePath,strTemp);
            }
            System.Diagnostics.Process.Start(dirtemp);
            System.Diagnostics.Process.Start(filePath);
            this.Close();
        }
    }
}

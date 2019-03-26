using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace FileContextSearch
{
    public partial class setForm : Form
    {
        public setForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Configuration.ConfigurationManager.AppSettings.Set("fileDir", textBox4.Text);
            var config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings["fileDir"].Value = textBox4.Text;
            config.Save(ConfigurationSaveMode.Modified);
            var logPath = "DateLogDirPath";
            //var ResearchPath = "ResearchDirPath";
            registryHelper.GetInstance().AddKey(logPath, textBox4.Text);
            FileSearchHelper.GetInstance().DateLogDir = textBox4.Text;
        }

        private void setForm_Load(object sender, EventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            textBox4.Text = registryHelper.GetInstance().GetKeyValue("DateLogDirPath");// config.AppSettings.Settings["fileDir"].Value;
            textBox1.Text = registryHelper.GetInstance().GetKeyValue("ResearchDirPath");// config.AppSettings.Settings["fileDirResearch"].Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBox4.Text))
            {
                Directory.CreateDirectory(textBox4.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBox4.Text))
            {
                MessageBox.Show("请先创建文件夹！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                System.Diagnostics.Process.Start(textBox4.Text);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Configuration.ConfigurationManager.AppSettings.Set("fileDirResearch", textBox4.Text);
            var config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings["fileDirResearch"].Value = textBox1.Text;
            config.Save(ConfigurationSaveMode.Modified);
            //var logPath = "DateLogDirPath";
            var ResearchPath = "ResearchDirPath";
            registryHelper.GetInstance().AddKey(ResearchPath, textBox1.Text);
            FileSearchHelper.GetInstance().ResearchDir = textBox1.Text;
            Form1.ResearchPathDir = textBox1.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBox1.Text))
            {
                Directory.CreateDirectory(textBox1.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBox1.Text))
            {
                MessageBox.Show("请先创建文件夹！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                System.Diagnostics.Process.Start(textBox1.Text);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
                // 建立 OpenFileDialog 对象
                FolderBrowserDialog myFolderBrowserDialog = new FolderBrowserDialog();
                string settingpath="";
                // 设定 OpenFileDialog 对象的各个属性
                    var withBlock = myFolderBrowserDialog;
                withBlock.Description = "请选择一个文件夹";

            //是否显示对话框左下角 新建文件夹 按钮，默认为 true
            //dialog.ShowNewFolderButton = false;
            //withBlock.ShowDialog();
            //if (settingpath != "")
            //{
            //    withBlock.SelectedPath = settingpath;
            //}
            if (withBlock.ShowDialog() == DialogResult.OK)
            {
                //记录选中的目录
                settingpath = withBlock.SelectedPath;
                textBox4.Text = settingpath;
            }


        }

        private void button8_Click(object sender, EventArgs e)
        {
            // 建立 OpenFileDialog 对象
            FolderBrowserDialog myFolderBrowserDialog = new FolderBrowserDialog();
            string settingpath = "";
            // 设定 OpenFileDialog 对象的各个属性
            var withBlock = myFolderBrowserDialog;
            withBlock.Description = "请选择一个文件夹";

            //是否显示对话框左下角 新建文件夹 按钮，默认为 true
            //dialog.ShowNewFolderButton = false;
            //withBlock.ShowDialog();
            //if (settingpath != "")
            //{
            //    withBlock.SelectedPath = settingpath;
            //}
            if (withBlock.ShowDialog() == DialogResult.OK)
            {
                //记录选中的目录
                settingpath = withBlock.SelectedPath;
                textBox1.Text = settingpath;

            }
        }
    }
}

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
            FileSearchHelper.GetInstance().DateLogDir = textBox4.Text;
        }

        private void setForm_Load(object sender, EventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            textBox4.Text = config.AppSettings.Settings["fileDir"].Value;
            textBox1.Text = config.AppSettings.Settings["fileDirResearch"].Value;
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
    }
}

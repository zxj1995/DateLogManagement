using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Management;
using System.Threading;
namespace FileContextSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.textBox3.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            this.textBox2.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            //this.button5.Visible = false;
        }
        public static string startDate;
        public static string endDate;
        //public delegate void ChangeTextBoxValue(TextBox TB, string strTemp);

        //public void ChangeTextBoxMain()
        //{
        //    var deletemp = new ChangeTextBoxValue(ChangeTextBoxText);
        //    deletemp(textBox1, "success");
        //    deletemp(textBox2, "success");
        //}


        public void ChangeTextBoxText( string TB,string strTemp)
        {
            foreach (var item in this.DateLog.Controls)
            {
                if (item is TextBox)
                {
                    var itemSub = item as TextBox;
                    if (itemSub.Name==TB)
                    {
                        itemSub.Text = strTemp; 
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var form2 = new TimerShow("Start");

            //var form2 = new TimerShow();
            form2.EventTemp += new ChangeTextBoxValue(ChangeTextBoxText);
            form2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form2 = new TimerShow("End");

            //var form2=new TimerShow();
            form2.EventTemp += new ChangeTextBoxValue(ChangeTextBoxText);     
            form2.ShowDialog();

        }

        void form2_EventTemp(string ControlName, string strTemp)
        {
            throw new NotImplementedException();
        }

        public string FileDireoryPath;
        public List<string> fileWithSearchContentList=new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //var di = new DirectoryInfo(FileDireoryPath);
                fileWithSearchContentList.Clear();
                var startTime = DateTime.Parse(textBox3.Text);
                var endTime = DateTime.Parse(textBox2.Text);
                var fsh = FileSearchHelper.GetInstance();
                var dArr = fsh.getDate(startTime, endTime);
                //fsh.DateLogDir = textBox4.Text;
                foreach (var item in dArr)
                {
                    //if (item=="2018-09-03")
                    //{
                    //    string aa="";  
                    //}
                    var strtemp1 = fsh.GetFileContent(DateTime.Parse(item));
                    if (strtemp1 != "null")
                    {
                        if (fsh.SearchContentFromStr(strtemp1, textBox1.Text))
                        {
                            var fileNameTemp=fsh.ConvertDateToFileName(DateTime.Parse(item));
                            fileWithSearchContentList.Add(fileNameTemp);
                        }
                    }
                }
                StringBuilder sb = new StringBuilder();
                foreach (var item in fileWithSearchContentList)
                {
                    sb.AppendLine(item);
                }
                richTextBox1.Text = sb.ToString();

                if (checkBox1.Checked==true)
                {
                    foreach (var item in fileWithSearchContentList)
                    {
                        fsh.OpenListedFiles(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //var fsh = FileSearchHelper.GetInstance();
            //fsh.DateLogDir = textBox4.Text;
            //fsh.CreateNewDatelog();
            //fsh.GenerateFile();
            OpenFileByTemplate();
            //Thread th = new Thread(new ThreadStart(OpenFileByTemplate));
            //th.Start();
        }
        public void OpenFileByTemplate()
        {
            var dirPath = Path.Combine(FileSearchHelper.GetInstance().DateLogDir, "temp");

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string[] fileNames = new string[] { "Draft.txt", "DailyMission.txt", "Idea.txt" };
            for (int i = 0; i < fileNames.Length; i++)
            {
                var filePath = Path.Combine(FileSearchHelper.GetInstance().DateLogDir, "temp", fileNames[i]);
                if (!File.Exists(filePath))
                {
                    using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                    }
                }
                //System.Diagnostics.Process.Start(filePath);
                var a=(double)Screen.PrimaryScreen.Bounds.Width *0.3;
                int sw = (int)a;
                int sh = Screen.PrimaryScreen.Bounds.Height;
                int j = i;
                int x1 = (int)a * j;
                int y1 = 0;
                var temp = new TestFun();
                temp.OpenAndSetFileSize(filePath, x1, y1, sw, sh);
                //Thread.Sleep(1000);
                //OpenAndSetWindow(filePath, 200 * i, 200 * i, 800 + 20 * i, 800 + 20 * i);
            }
        }

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "MoveWindow")]
        public static extern bool MoveWindow(System.IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sf = new setForm();
            sf.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
        //注释于2019年1月18日12:08:48
            var fsh = FileSearchHelper.GetInstance();
            var dttemp = DateTime.Parse(this.textBox4.Text.ToString());
            //var fileNameTemp = fsh.ConvertDateToFileName(dttemp);
            fsh.CreateNewDatelogByDate(dttemp);
            //注释结束
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var form2 = new TimerShow("OpenFileByDate");

            //var form2 = new TimerShow();
            form2.EventTemp += new ChangeTextBoxValue(ChangeTextBoxText);
            form2.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var str = System.Configuration.ConfigurationManager.AppSettings["fileDir"];
            FileSearchHelper.GetInstance().DateLogDir = str;
            InitialDir();
            InitialUI();
        }
        private void InitialUI()
        {
            var dt1 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            var dt2 = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            var dt3 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            textBox4.Text = dt1;
            textBox3.Text = dt3;
            textBox2.Text = dt2;
        }
        private void button7_Click(object sender, EventArgs e)
        {
          OpenFileByTemplate();
        }
        public void InitialDir()
        {
            try
            {
                if (!Directory.Exists(FileSearchHelper.GetInstance().DateLogDir))
                {
                    Directory.CreateDirectory(FileSearchHelper.GetInstance().DateLogDir);
                }
                var tempDir = Path.Combine(FileSearchHelper.GetInstance().DateLogDir, "temp");
                if (!Directory.Exists(tempDir))
                {
                    Directory.CreateDirectory(tempDir);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }




        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            FileSearchHelper.GetInstance().GenerateFile();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FileSearchHelper.GetInstance().GenerateFile();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            FileSearchHelper.GetInstance().GenerateFile();
            var dttemp = DateTime.Parse(this.textBox4.Text.ToString());
            FileSearchHelper.GetInstance().CreateNewDatelogByDate(dttemp);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int x=int.Parse(textBox5.Text);
            int y = int.Parse(textBox6.Text);
            int w = int.Parse(textBox7.Text);
            int h = int.Parse(textBox8.Text);
            var temp =new  TestFun();
            var filePath=Path.Combine(FileSearchHelper.GetInstance().DateLogDir, "temp", "Draft.txt");
            temp.OpenAndSetFileSize(filePath, x, y, w, h);

        }


        //private void button5_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("aaa");
        //}
    }
}

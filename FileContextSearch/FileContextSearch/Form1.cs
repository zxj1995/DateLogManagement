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
using FileOperator;
using FileContextSearch.ResearchList;
using Newtonsoft.Json;


namespace FileContextSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //this.textBox3.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            //this.textBox2.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            //this.button5.Visible = false;
            Test.Parent = null;

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
            ContentChange();
            int index = 0;
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                if (tabControl1.TabPages[i].Name=="NewDateLog")
                {
                    index = i;
                    break;
                }
            }
            tabControl1.SelectedIndex = index;
            //査小杰屏蔽于2019-3-8 16:08:18
            //OpenFileByTemplate();
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
        public static string ResearchPathDir="";
        public System.Threading.Timer Tc;
        private void Form1_Load(object sender, EventArgs e)
        {
            //var str = System.Configuration.ConfigurationManager.AppSettings["fileDir"];
            var logPath = "DateLogDirPath";
            var str = registryHelper.GetInstance().GetKeyValue(logPath);
            FileSearchHelper.GetInstance().DateLogDir = str;
            var ResearchPath = "ResearchDirPath";
            str = registryHelper.GetInstance().GetKeyValue(logPath);
            //str = System.Configuration.ConfigurationManager.AppSettings["fileDirResearch"];
            FileSearchHelper.GetInstance().ResearchDir = str;
            ResearchPathDir = str;
            InitialDir();
            InitialUI();
            Tc = new System.Threading.Timer(new TimerCallback(autoSave), null, Timeout.Infinite, 3600000);
            Tc.Change(3600000, 3600000);
            //加载treeview
            this.WindowState =FormWindowState.Maximized;
            //检查注册表中是否存在日志保存地址信息
             logPath = "DateLogDirPath";
             ResearchPath = "ResearchDirPath";
            if (! registryHelper.GetInstance().IsRegisted (logPath))
            {
                MessageBox.Show("请设置日志保存文件夹！", "Warning", MessageBoxButtons.OK);
                var sf = new setForm();
                sf.ShowDialog();
            }
            if (!registryHelper.GetInstance().IsRegisted(ResearchPath))
            {
                MessageBox.Show("请设置Research保存文件夹！", "Warning", MessageBoxButtons.OK);
                var sf = new setForm();
                sf.ShowDialog();
            }
        }
        public void autoSave(object obj)
        {
            string[] fileNames = new string[] { "DailyMission.txt", "Idea.txt", "Draft.txt" };
            RichTextBox[] RTXArr = new RichTextBox[] { DailyMission, Idea, Draft };
            for (int i = 0; i < RTXArr.Length; i++)
            {
                FileSearchHelper.GetInstance().ReadTxtandWriteFile(RTXArr[i], fileNames[i]);
            }
            FileSearchHelper.GetInstance().GenerateFile(DateTime.Now);
        }
        private void InitialUI()
        {
            this.Text += Application.ProductVersion.ToString();
            var itemp = DateTime.Now.DayOfWeek - DayOfWeek.Monday;
            var dt1 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            var dt2 = DateTime.Now.AddDays(-7-itemp).ToString("yyyy-MM-dd");
            var dt3 = DateTime.Now.AddDays(-7 - itemp+4).ToString("yyyy-MM-dd");
            textBox4.Text = dt1;
            textBox3.Text = dt2;
            textBox2.Text = dt3;
            var strArr = new string[] { "Conquering", "Conquered", "Abandoned", "Urgent"};
            NodeState.Items.Clear();
            foreach (var item in strArr)
            {
                NodeState.Items.Add(item);
            }
            NodeState.SelectedIndex=0;
            {
                TV = new TreeView();
                TV.Name = "SearchTV";
                TV.Nodes.Clear();
                TV.Size = new Size(SearchName.Size.Width, SearchName.Size.Height - label6.Height);
                TV.Dock = DockStyle.Bottom;
                TV.DoubleClick += TreeNode_DoubleClick;
                ResearchTreeView.Nodes.Clear();
                ResearchTreeView.Name = "ResearchTreeView";
                ResearchTreeView.DoubleClick += TreeNode_DoubleClick;
                ResearchTreeView.Size = new Size(panel2.Size.Width, panel2.Size.Height - label1.Height);
                ResearchTreeView.Dock = DockStyle.Bottom;
                TV.Font = new Font("微软雅黑", 12, FontStyle.Bold);
                ResearchTreeView.Font = new Font("微软雅黑", 12, FontStyle.Bold);
                TV.MouseDown += MouseDown;
                ResearchTreeView.MouseDown += MouseDown;
                TV.BeforeCollapse += BeforeCollapse;
                ResearchTreeView.BeforeCollapse += BeforeCollapse;
                TV.BeforeExpand += BeforeExpand;
                ResearchTreeView.BeforeExpand += BeforeExpand;

            }

            InitNewModel();
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
                if (!Directory.Exists(FileSearchHelper.GetInstance().ResearchDir))
                {
                    Directory.CreateDirectory(FileSearchHelper.GetInstance().ResearchDir);
                    ResearchPathDir = FileSearchHelper.GetInstance().ResearchDir;
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
            autoSave(null);
            FileSearchHelper.GetInstance().GenerateFile(DateTime.Now);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            autoSave(null);
            if (IsByDateTime.Checked)
            {
                var fsh = FileSearchHelper.GetInstance();
                var dttemp = DateTime.Parse(this.textBox4.Text.ToString());
                //var filePath = Path.Combine(FileSearchHelper.GetInstance().DateLogDir, dttemp.Year.ToString(), dttemp.Month.ToString() + "月", dttemp.ToString("MM.dd") + ".txt");
                FileSearchHelper.GetInstance().GenerateFile(dttemp);
            }
            else
            {
                FileSearchHelper.GetInstance().GenerateFile(DateTime.Now);
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            FileSearchHelper.GetInstance().GenerateFile(DateTime.Now);
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

        //private void button10_Click(object sender, EventArgs e)
        //{
        //    var strArr = new List<string>();
        //    var ResearchList = Path.Combine(FileSearchHelper.GetInstance().ResearchDir, "MainList.txt");
        //    if (!File.Exists(ResearchList))
        //    {
        //        using (var fs = new FileStream(ResearchList, FileMode.OpenOrCreate))
        //        {

        //        }
        //        MessageBox.Show("ResearchList doesn't exist!", "Error", MessageBoxButtons.OK);
        //    }
        //    else
        //    {
        //        System.Diagnostics.Process.Start(ResearchList);
        //    }

        //}

        //public void OpenResearchList()
        //{
        //    //MessageBox.Show("假装这个模块已经做好了test");
        //    var strArr = new List<string>();
        //    var ResearchList = Path.Combine(FileSearchHelper.GetInstance().ResearchDir, "MainList.txt");
        //    if (!File.Exists(ResearchList))
        //    {
        //        using (var fs = new FileStream(ResearchList, FileMode.OpenOrCreate))
        //        {

        //        }
        //        MessageBox.Show("ResearchList doesn't exist!", "Error", MessageBoxButtons.OK);
        //    }
        //    else
        //    {
        //        var strtemp = "";
        //        strtemp = FileOperator.FileOperator.GetInstance().ReadFile(ResearchList);
        //        if (!string.IsNullOrEmpty(strtemp))
        //        {
        //            strArr = strtemp.Split(';').ToList();
        //        }
        //        foreach (var item in strArr)
        //        {
        //            //flowLayoutPanel1.Controls.Clear();
        //            var itemsub = new FlowLayoutPanel();
        //            var txtitem = new TextBox();
        //            txtitem.Text = item;
        //            itemsub.Controls.Add(txtitem);
        //            flowLayoutPanel1.Controls.Add(itemsub);
        //        }
        //    }

        //}

        private void button11_Click(object sender, EventArgs e)
        {
            var MainResearch = new ResearchItem();
            for (int i = 0; i < 4; i++)
            {
                var SubItem = new IssueItem();
                SubItem.Content = "测试"+i.ToString();
                SubItem.Name = "测试问题" + i.ToString();
                MainResearch.SubList.Add(SubItem);
            }
            var str=JsonConvert.SerializeObject(MainResearch);
            MessageBox.Show(str);
        }

        //private void button12_Click(object sender, EventArgs e)
        //{

        //    var FLOP_Item = new FlowLayoutPanel();
        //    FLOP_Item.Size = new Size(140, 82);
        //    FLOP_Item.BackColor = Color.Red;
        //    FLOP_Item.Margin = new Padding(10,10,10,0);
        //    //flowLayoutPanel1.Controls.Add(FLOP_Item);
        //}
        public TreeView ResearchTreeView = new TreeView();
        public TreeView TV = new TreeView();

        public void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //添加两个TreeView控件
            if (tabControl1.SelectedIndex==2)
            {
                foreach (Control item in panel2.Controls)
                {
                    if (item.Name == "ResearchTreeView")
                    {
                        panel2.Controls.Remove(item);
                        break;
                    }
                }
                foreach (Control item in SearchName.Controls)
                {
                    if (item.Name == "SearchTV")
                    {
                        SearchName.Controls.Remove(item);
                        break;
                    }
                }
                panel2.Controls.Add(ResearchTreeView);
                SearchName.Controls.Add(TV);
                LoadTreeView(ResearchTreeView, "");
                LoadTreeView(TV, "");
            }
            //ContentChange();
        }

        private void ResearchTreeView_DoubleClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllFiles(string DirPath, List<string> FIArr)
        {
            if (Directory.Exists(DirPath))
            {
                foreach (var item in Directory.GetFiles(DirPath))
                {
                    FIArr.Add(item);
                }
                foreach (var item in Directory.GetDirectories(DirPath))
                {
                    GetAllFiles(item, FIArr);
                }
            }
            return FIArr;
        }
        private void LoadTreeView(TreeView TV, string state)
        {
            TV.Nodes.Clear();
            var DI = new DirectoryInfo(ResearchPathDir);
            var TN = new TreeNode();
            TN.Name = ResearchPathDir;
            TN.Text = "ResearchList";
            TN.Expand();
            foreach (var item in DI.GetDirectories())
            {
                var booltemp = false;
                if (!string.IsNullOrEmpty(state))
                {
                    var olist = new List<string>();
                    olist.Clear();
                    foreach (var subitem in GetAllFiles(item.FullName, olist))
                    {
                        var content = "";
                        using (var fs = new StreamReader(subitem, Encoding.Default, false))
                        {
                            content = fs.ReadToEnd();
                        }
                        booltemp = FileSearchHelper.GetInstance().SearchContentFromStr(content, "CurrentState:" + state);
                    }
                }
                else
                {
                    booltemp = true;
                }
                var tntemp = new TreeNode();
                tntemp.Name = item.FullName;
                tntemp.Text = item.ToString();
                tntemp=SearchSubItem(tntemp.Name, tntemp, state);
                if (tntemp.Nodes.Count>0)
                {
                    booltemp = true;
                }
                if (booltemp)
                {
                    tntemp.Expand();
                    TN.Nodes.Add(tntemp);
                }
            }
            TN.Expand();
            TV.Nodes.Add(TN);
            //foreach (var itemstr in Directory.GetDirectories(ResearchPathDir))
            //{
            //    var item = new DirectoryInfo(itemstr);
            //    var TN = new TreeNode();
            //    TN.Name = itemstr;
            //    TN.Text = item.Name ;
            //    TN=SearchSubItem(TN.Name,TN, state);
            //    TV.Nodes.Add(TN);
            //}
        }
        public TreeNode SearchSubItem(string DirPath,TreeNode TN,string state)
        {
            var booltemp = false;
            var strList = new List<string>();
            if (Directory.Exists(DirPath))
            {
                var subItemArr = Directory.GetDirectories(DirPath);
                foreach (var Diritem in subItemArr)
                {
                    var DI = new DirectoryInfo(Diritem);
                    if (DI.Name == "SubNodes" && DI.GetDirectories().Length > 0)
                    {
                        foreach (var item in DI.GetDirectories())
                        {
                            
                            if (string.IsNullOrEmpty(state))
                            {
                                booltemp = true;
                            }
                            else
                            {
                                strList.Clear();
                                foreach (var subitem in GetAllFiles(item.FullName, strList))
                                {
                                    booltemp = false;
                                    var content = "";
                                    using (var fs = new StreamReader(subitem, Encoding.Default, false))
                                    {
                                        content = fs.ReadToEnd();
                                    }
                                    booltemp = FileSearchHelper.GetInstance().SearchContentFromStr(content, "CurrentState:" + state);
                                }

                            }
                            var tntemp = new TreeNode();
                            tntemp.Name = item.FullName;
                            tntemp.Text = item.Name;
                            SearchSubItem(tntemp.Name, tntemp, state);
                            if (booltemp)
                            {
                                tntemp.Expand();
                                TN.Nodes.Add(tntemp);
                            }
                        }
                    }
                }
            }
            return TN;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            var state = NodeState.SelectedItem.ToString();
            var TN = new TreeNode();

            TN.Name = ResearchPathDir;
            TN.Text = "ResearchList";
            TN.Expand();
            LoadTreeView(TV, state);
            //TV.Location = new Point(label6.Location.X, label6.Location.Y+label6.Height);
        }



        private void button13_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(Path.Combine(ResearchPathDir, ""));
            //ResearchTreeView.Nodes.Add(itemstr, item.Name);
            var strTemp = "";
            
            if (ResearchTreeView.SelectedNode is null)
            {
                strTemp = "";
            }
            else
            {
                strTemp = ResearchTreeView.SelectedNode.Name;
            }
            var NodeSettingTemp = new NodeSetting(strTemp,this);
            NodeSettingTemp.ShowDialog();
        }


        private void TreeNode_DoubleClick(object sender, EventArgs e)
        {
            TreeView TV = (TreeView)sender;
            if (!(TV.SelectedNode is null))
            {
                if (Directory.Exists(TV.SelectedNode.Name))
                {
                    System.Diagnostics.Process.Start(TV.SelectedNode.Name);
                }
                else
                {
                    MessageBox.Show("节点内容不存在！", "Error", MessageBoxButtons.OK);
                }
                var progressFileName = Path.Combine(TV.SelectedNode.Name, "Progress", "Progress_Log.txt");
                if (File.Exists(progressFileName))
                {
                    System.Diagnostics.Process.Start(progressFileName);
                }
                //else
                //{
                //    MessageBox.Show("节点内容不存在！", "Error", MessageBoxButtons.OK);
                //}
                
            }
        }

        //private void button18_Click(object sender, EventArgs e)
        //{

        //}
        //private void button5_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("aaa");
        //}
        public int m_MouseClicks = 0;
        private void MouseDown(object sender, MouseEventArgs e)
        {
            this.m_MouseClicks = e.Clicks;
        }
        private void BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (this.m_MouseClicks > 1)
            { //如果是鼠标双击则禁止结点折叠 
                e.Cancel = true;
            }
            else
            { //如果是鼠标单击则允许结点折叠 
                e.Cancel = false;
            }
        }
        //myTreeView控件节点展开之前判断鼠标按下的次数，并进行控制 
        private void BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (this.m_MouseClicks > 1)
            { //如果是鼠标双击则禁止结点展开 
                e.Cancel = true;
            }
            else
            { //如果是鼠标单击则允许结点展开 
                e.Cancel = false;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }
        //2019-3-8 12:04:10
        //初始化新日志模块
        public void InitNewModel()
        {
            //UI上等分contentContainer
            FlexLayOut();
            ContentChange();
        }
        public void FlexLayOut()
        {
            foreach (var item in ContentContainer.Controls)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.RichTextBox")
                {
                    var itemtemp = (RichTextBox)item;
                    itemtemp.Height  = ContentContainer.Height-85;
                    itemtemp.Width = ContentContainer.Width/3;
                }
                DailyMission.Location = new Point(0, 85);
                Idea.Location = new Point(ContentContainer.Width /3, 85);
                Draft.Location = new Point((ContentContainer.Width /3)*2, 85);
            }
            lblDailyMission.Location = new Point(20, 24);
            lblIdea .Location = new Point(Idea.Location.X+20, 24);
            lblDraft.Location = new Point(Draft.Location.X + 20, 24);
        }
        public void ContentChange()
        {
            //"DailyMission.txt", "Idea.txt", "Draft.txt"
            var strArr = FileSearchHelper.GetInstance().tempFileHandler();
            DailyMission.Text = strArr[0];
            Idea.Text = strArr[1];
            Draft.Text = strArr[2];
        }
        public void FillRTX()
        {
            var item = "";
        }

        private void ContentContainer_SizeChanged(object sender, EventArgs e)
        {
            FlexLayOut();
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            HeightInit();
        }
        public void HeightInit()
        {
            if (NewDateLog.Height > 180)
            {
                ContentContainer.Height = NewDateLog.Height - 180;
            }
        }



        private void NewDateLog_SizeChanged(object sender, EventArgs e)
        {
            HeightInit();
        }





        private void DailyMission_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var filePath = Path.Combine(FileSearchHelper.GetInstance().DateLogDir, "temp", "DailyMission.txt");
            if (File.Exists(filePath))
            {
                var a = (double)Screen.PrimaryScreen.Bounds.Width * 0.3;
                int sw = (int)a;
                int sh = Screen.PrimaryScreen.Bounds.Height;
                int j = 0;
                int x1 = (int)a * j;
                int y1 = 0;
                var temp = new TestFun();
                temp.OpenAndSetFileSize(filePath, x1, y1, sw, sh);
            }

        }

        private void Idea_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var filePath = Path.Combine(FileSearchHelper.GetInstance().DateLogDir, "temp", "Idea.txt");
            if (File.Exists(filePath))
            {
                var a = (double)Screen.PrimaryScreen.Bounds.Width * 0.3;
                int sw = (int)a;
                int sh = Screen.PrimaryScreen.Bounds.Height;
                int j = 1;
                int x1 = (int)a * j;
                int y1 = 0;
                var temp = new TestFun();
                temp.OpenAndSetFileSize(filePath, x1, y1, sw, sh);
            }

        }

        private void Draft_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var filePath = Path.Combine(FileSearchHelper.GetInstance().DateLogDir, "temp", "Draft.txt");
            if (File.Exists(filePath))
            {
                var a = (double)Screen.PrimaryScreen.Bounds.Width * 0.3;
                int sw = (int)a;
                int sh = Screen.PrimaryScreen.Bounds.Height;
                int j = 2;
                int x1 = (int)a * j;
                int y1 = 0;
                var temp = new TestFun();
                temp.OpenAndSetFileSize(filePath, x1, y1, sw, sh);
            }

        }

        private void DateLog_SizeChanged(object sender, EventArgs e)
        {
            richTextBox1.Height = DateLog.Height - 198;
        }

        private void splitContainer1_Panel2_SizeChanged(object sender, EventArgs e)
        {
            panel2.Height = splitContainer1.Panel2.Height-10;
            SearchName.Height= splitContainer1.Panel2.Height-10;
            TV.Size = new Size(SearchName.Size.Width, SearchName.Size.Height - label6.Height);
            ResearchTreeView.Size = new Size(panel2.Size.Width, panel2.Size.Height - label1.Height);
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoSave(null);
        }
        //2019-3-8 12:04:10

    }
}

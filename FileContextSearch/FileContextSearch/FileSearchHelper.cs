using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
namespace FileContextSearch
{
    public class FileSearchHelper
    {
        private FileSearchHelper()
        { 
        
        }
        private static FileSearchHelper Instance;
        private  static readonly object locker=new object();
        public string _DateLogDir = "";
        public string _ResearchDir = "";

        public string DateLogDir
        {
            get
            {
                return _DateLogDir;
            }
            set
            {
                _DateLogDir = value;
            }
        }

        public string ResearchDir
        {
            get
            {
                return _ResearchDir;
            }
            set
            {
                _ResearchDir = value;
            }
        }


        public static FileSearchHelper GetInstance()
        {
            if (Instance == null)
            {
                lock (locker)
                {
                    Instance = new FileSearchHelper();
                }
            }
            return Instance;
        }

        public List<string> getDate(DateTime startTime, DateTime endTime)
        {
            var dttemp =DateTime.Parse( startTime.ToString());
            var ld = new List<string>();
            while (DateTime.Compare(dttemp, endTime) <= 0)
            {
                dttemp = dttemp.AddDays(1);
                ld.Add(dttemp.ToString("yyyy-MM-dd"));
            }
            return ld;
        }

        public string GetFileContent(DateTime dt)
        {
            try
            {
                var strtemp = "";
                var yearID = dt.Year;
                var monthID = dt.Month;
                var dayID = dt.Day;

                var filePath = Path.Combine(DateLogDir, yearID.ToString(), monthID.ToString() + "月", dt.ToString("MM.dd") + ".txt");
                var filePath1 = Path.Combine(DateLogDir, yearID.ToString(), monthID.ToString() + "月", dt.ToString("yyyy-MM-dd") + ".txt");
                if (File.Exists(filePath))
                {
                    if (File.Exists(filePath))
                    {
                        using (var fs = new StreamReader(filePath,Encoding.Default, false))
                        {
                            strtemp = fs.ReadToEnd();
                        }
                    }
                }
                else
                {
                    if (File.Exists(filePath1))
                    {
                        using (var fs = new StreamReader(filePath1,Encoding.Default, false))
                        {
                            strtemp = fs.ReadToEnd();
                        }
                    }
                    else
                    {
                        strtemp = "null";
                    }
                }
                return strtemp;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void OpenListedFiles(string strtemp)
        {
            string[] strArr = new string[] { "---" };
            var filepath = strtemp.Split(strArr,StringSplitOptions.RemoveEmptyEntries);
            if (filepath.Length == 2)
            {
                var trueFileName = "";
                if (File.Exists(filepath[0]))
                {
                    trueFileName = filepath[0];
                }
                else
                {
                    if (File.Exists(filepath[1]))
                    {
                        trueFileName = filepath[1];
                    }
                    else
                    {
                        var fsh = FileSearchHelper.GetInstance();
                        //fsh.DateLogDir = textBox4.Text;
                        fsh.CreateNewDatelog();

                        //MessageBox.Show("文件不存在!/r","error");
                    }

                }
                System.Diagnostics.Process.Start(trueFileName);
            }
        }

        public bool SearchContentFromStr(string fileContent, string searchContent)
        {
            try
            {
                if (fileContent.IndexOf(searchContent,StringComparison.OrdinalIgnoreCase)>=0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public string ConvertDateToFileName(DateTime dt)
        {
            var strtemp = "";
            var yearID = dt.Year;
            var monthID = dt.Month;
            var dayID = dt.Day;
            var filePath = Path.Combine(DateLogDir, yearID.ToString(), monthID.ToString() + "月", dt.ToString("MM.dd") + ".txt");
            var filePath1 = Path.Combine(DateLogDir, yearID.ToString(), monthID.ToString() + "月", dt.ToString("yyyy-MM-dd") + ".txt");
            return filePath + "---" + filePath1;
        }


        //foreach (var item in fileNames)
        //{

        //    //OpenAndSetWindow(item,win)
        //}

        public void GenerateFile(DateTime dtTemp)
        {

            //1.检测日志是否存在
            //2.生成昨日日志文件
            var sb = new StringBuilder();
            var lineEx = new string[50];
            sb.Clear();
            var dt = dtTemp;
            var yearID = dt.Year.ToString();
            var monthID = dt.Month.ToString();
            var dayID = dt.Day.ToString();
            var filePath = "";
            var dirPath = "";
            filePath = Path.Combine(FileSearchHelper.GetInstance().DateLogDir, yearID, monthID + "月", dt.ToString("MM.dd") + ".txt");
            dirPath = Path.Combine(FileSearchHelper.GetInstance().DateLogDir, yearID, monthID + "月");
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string[] fileNames = new string[] { "DailyMission.txt", "Idea.txt", "Draft.txt" };
            int itemp = 0;
            bool[] booltemp = new bool[] { false, false, false };
            for (int i = 0; i < lineEx.Length; i++)
            {
                lineEx[i] = "--";
            }
            for (int i = 0; i < 3; i++)
            {
                var filePathTemp = Path.Combine(FileSearchHelper.GetInstance().DateLogDir, "temp", fileNames[i]);
                if (File.Exists(filePathTemp))
                {
                    booltemp[i] = false;
                }
                else
                {
                    booltemp[i] = true;
                }
            }

            if ((booltemp[0] || booltemp[1] || booltemp[2]))
            {
                return;
            }
            else
            {
                foreach (var item in fileNames)
                {
                    var filePathTemp = Path.Combine(FileSearchHelper.GetInstance().DateLogDir, "temp", item);
                    //var fi= new FileInfo(filePathTemp);
                    var fn = Path.GetFileNameWithoutExtension(filePathTemp);
                    sb.AppendLine(fn + string.Join("", lineEx));
                    var fileContentTemp = File.ReadAllLines(filePathTemp, Encoding.Default);
                    foreach (var subitem in fileContentTemp)
                    {
                        sb.AppendLine(subitem);
                    }
                    sb.AppendLine();
                }
                File.WriteAllText(filePath, sb.ToString());
            }

        }
        public void ReadTxtandWriteFile(RichTextBox RTB, string strFilePath)
        {
            var txt = RTB.Text;
            var booltemp = false;
            var filePathTemp = Path.Combine(FileSearchHelper.GetInstance().DateLogDir, "temp", strFilePath);
            if (File.Exists(filePathTemp))
            {
                booltemp = false;
            }
            else
            {
                booltemp = true;
            }
            using (StreamWriter fs=new StreamWriter(filePathTemp))
            {
                fs.Write(txt);
            }
        }
        public string[] tempFileHandler()
        {
            var sb = new StringBuilder();
            sb.Clear();

            string[] fileNames = new string[] { "DailyMission.txt", "Idea.txt", "Draft.txt" };
            int itemp = 0;
            var result = new string[3];
            bool[] booltemp = new bool[] { false, false, false };
            //for (int i = 0; i < lineEx.Length; i++)
            //{
            //    lineEx[i] = "--";
            //}
            for (int i = 0; i < 3; i++)
            {
                var filePathTemp = Path.Combine(FileSearchHelper.GetInstance().DateLogDir, "temp", fileNames[i]);
                if (File.Exists(filePathTemp))
                {
                    booltemp[i] = false;
                }
                else
                {
                    booltemp[i] = true;
                }
            }

            if ((booltemp[0] || booltemp[1] || booltemp[2]))
            {
                return result;
            }
            else
            {
                int i = 0;
                foreach (var item in fileNames)
                {
                    var filePathTemp = Path.Combine(FileSearchHelper.GetInstance().DateLogDir, "temp", item);
                    //var fi= new FileInfo(filePathTemp);
                    var fn = Path.GetFileNameWithoutExtension(filePathTemp);
                    var fileContentTemp = File.ReadAllLines(filePathTemp, Encoding.Default);
                    foreach (var subitem in fileContentTemp)
                    {
                        sb.AppendLine(subitem);
                    }
                    result[i] = sb.ToString();
                    i++;
                }
                return result;
            }

        }
        public bool CreateNewDatelog()
        {
            var dt = DateTime.Now;
            var yearID = dt.Year.ToString();
            var monthID = dt.Month.ToString();
            var dayID = dt.Day.ToString();
            var filePath = Path.Combine(DateLogDir, yearID, monthID+ "月", dt.ToString("MM.dd") + ".txt");
            var dirPath = Path.Combine(DateLogDir, yearID, monthID + "月");
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            using (var fs =new FileStream(filePath,FileMode.OpenOrCreate,FileAccess.Read))
            {
                
            }
            if (File.Exists(filePath))
            {
                System.Diagnostics.Process.Start(filePath);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CreateNewDatelogByDate(DateTime dt)
        {
            //var dt = DateTime.Now;
            var yearID = dt.Year.ToString();
            var monthID = dt.Month.ToString();
            var dayID = dt.Day.ToString();
            var filePath = Path.Combine(DateLogDir, yearID, monthID + "月", dt.ToString("MM.dd") + ".txt");
            var dirPath = Path.Combine(DateLogDir, yearID, monthID + "月");
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
            {

            }
            if (File.Exists(filePath))
            {
                System.Diagnostics.Process.Start(filePath);
                return true;
            }
            else
            {
                return false;
            }
        }
      
    }
}

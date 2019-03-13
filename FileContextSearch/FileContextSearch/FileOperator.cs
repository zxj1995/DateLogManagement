using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace FileOperator
{
    class FileOperator
    {
        private static readonly object locker=new object();
        private static FileOperator Instance;
        private FileOperator()
        { }
        public static FileOperator GetInstance()
        {
            lock (locker)
            {
                if (Instance==null)
                {
                    Instance = new FileOperator();
                }
            }
            return Instance;
        }


        public void OpenFile(string filePath)
        {
            try
            {
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ReadFile(string filePath)
        {
            try
            {
                string res;
                using (var fr=new StreamReader(filePath,Encoding.UTF8))
                {
                      res = fr.ReadToEnd();
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void WriteFile(string filePath,bool append,string value)
        {
            try
            {
                using (var fw = new StreamWriter(filePath,append))
                {
                   fw.Write(value);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateFile(string filePath)
        {
            try
            {
                using (var fs=new FileStream(filePath,FileMode.OpenOrCreate))
                {
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void DeleteFile(string filePath)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Truncate))
                {
                    
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

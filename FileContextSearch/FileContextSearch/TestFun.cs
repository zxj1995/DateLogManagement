using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace FileContextSearch
{
   public class TestFun
    {
       public void OpenAndSetFileSize(string filePath, int x, int y, int w, int h)
       {
           //Thread.Sleep(1000);
           //调用API
           //MessageBox.Show(x.ToString());
           Process p = new Process();
           var fileName = Path.GetFileName(filePath);
           p.StartInfo.FileName = filePath;
           p.StartInfo.CreateNoWindow = false;
           p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
           p.Start();
           new Thread(() =>
           {
               Thread.Sleep(1000);
               if (p != null && p.MainWindowHandle != IntPtr.Zero)
               {
                   MoveWindow(p.MainWindowHandle, x, y, w, h, true);
               }
           }).Start();
           //MoveWindow(p.MainWindowHandle, x, y, w, h, true);
           //MessageBox.Show(x.ToString());
       }
       //[System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "FindWindow")]
       //public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
       [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "MoveWindow")]
       public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);

       //[DllImport("user32.dll", CharSet = CharSet.Auto)]
       //public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileContextSearch
{
    public delegate void ChangeTextBoxValue(string ControlName, string strTemp);
    public partial class TimerShow : Form
    {
        public TimerShow()
        {
            InitializeComponent();
        }
        public string StartOrEnd;

        public TimerShow(string strtemp)
        {
            StartOrEnd = strtemp;
            InitializeComponent();
        }
        private void TimerShow_Load(object sender, EventArgs e)
        {
            this.Size = new Size(monthCalendar1.Size.Width + 20, monthCalendar1.Size.Height + 40);
            this.FormBorderStyle = FormBorderStyle.None;
        }
        public event ChangeTextBoxValue EventTemp;
        private void button1_Click(object sender, EventArgs e)
        {
            string dateTemp;
            switch (StartOrEnd)
            {
                case "Start":
                    dateTemp = this.monthCalendar1.SelectionStart.ToString("yyyy-MM-dd");
                    EventTemp("textBox3", dateTemp);
                    break;
                case "End":
                    dateTemp = this.monthCalendar1.SelectionStart.ToString("yyyy-MM-dd");
                    EventTemp("textBox2", dateTemp);
                    break;
                case "OpenFileByDate":
                    dateTemp = this.monthCalendar1.SelectionStart.ToString("yyyy-MM-dd");
                    EventTemp("textBox4", dateTemp);
                    break;

                default:
                    break;
            }
            this.Close();
        }
    }
}

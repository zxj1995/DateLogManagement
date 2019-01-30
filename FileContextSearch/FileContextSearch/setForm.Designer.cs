namespace FileContextSearch
{
    partial class setForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.date_log = new System.Windows.Forms.TabPage();
            this.research = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.date_log.SuspendLayout();
            this.research.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(128, 11);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(312, 21);
            this.textBox4.TabIndex = 15;
            this.textBox4.Text = "E:\\zxj\\personal\\zxj work info\\datelog";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "datelog文件夹地址";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(467, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 25);
            this.button1.TabIndex = 16;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(576, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 25);
            this.button2.TabIndex = 17;
            this.button2.Text = "创建文件夹路径";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(467, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 25);
            this.button3.TabIndex = 18;
            this.button3.Text = "打开文件夹";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.date_log);
            this.tabControl1.Controls.Add(this.research);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(747, 175);
            this.tabControl1.TabIndex = 19;
            // 
            // date_log
            // 
            this.date_log.Controls.Add(this.label1);
            this.date_log.Controls.Add(this.button3);
            this.date_log.Controls.Add(this.textBox4);
            this.date_log.Controls.Add(this.button2);
            this.date_log.Controls.Add(this.button1);
            this.date_log.Location = new System.Drawing.Point(4, 22);
            this.date_log.Name = "date_log";
            this.date_log.Padding = new System.Windows.Forms.Padding(3);
            this.date_log.Size = new System.Drawing.Size(739, 149);
            this.date_log.TabIndex = 0;
            this.date_log.Text = "date_log";
            this.date_log.UseVisualStyleBackColor = true;
            // 
            // research
            // 
            this.research.Controls.Add(this.button4);
            this.research.Controls.Add(this.button5);
            this.research.Controls.Add(this.button6);
            this.research.Controls.Add(this.label2);
            this.research.Controls.Add(this.textBox1);
            this.research.Location = new System.Drawing.Point(4, 22);
            this.research.Name = "research";
            this.research.Padding = new System.Windows.Forms.Padding(3);
            this.research.Size = new System.Drawing.Size(739, 149);
            this.research.TabIndex = 1;
            this.research.Text = "research";
            this.research.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(483, 48);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(101, 25);
            this.button4.TabIndex = 21;
            this.button4.Text = "打开文件夹";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(592, 16);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(101, 25);
            this.button5.TabIndex = 20;
            this.button5.Text = "创建文件夹路径";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(483, 16);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(101, 25);
            this.button6.TabIndex = 19;
            this.button6.Text = "保存";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "Research文件夹地址";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(130, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(312, 21);
            this.textBox1.TabIndex = 17;
            this.textBox1.Text = "C:\\ZXJ\\Research";
            // 
            // setForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 175);
            this.Controls.Add(this.tabControl1);
            this.Name = "setForm";
            this.Text = "setForm";
            this.Load += new System.EventHandler(this.setForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.date_log.ResumeLayout(false);
            this.date_log.PerformLayout();
            this.research.ResumeLayout(false);
            this.research.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage date_log;
        private System.Windows.Forms.TabPage research;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
    }
}
namespace FileContextSearch
{
    partial class NodeSetting
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
            this.button13 = new System.Windows.Forms.Button();
            this.NodeName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button13
            // 
            this.button13.Font = new System.Drawing.Font("Buxton Sketch", 18F, System.Drawing.FontStyle.Bold);
            this.button13.Location = new System.Drawing.Point(43, 38);
            this.button13.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(154, 35);
            this.button13.TabIndex = 19;
            this.button13.Text = "Node Create";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // NodeName
            // 
            this.NodeName.Location = new System.Drawing.Point(37, 12);
            this.NodeName.Name = "NodeName";
            this.NodeName.Size = new System.Drawing.Size(160, 21);
            this.NodeName.TabIndex = 20;
            // 
            // NodeSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 102);
            this.Controls.Add(this.NodeName);
            this.Controls.Add(this.button13);
            this.Name = "NodeSetting";
            this.Text = "InitialResearchDir";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NodeSetting_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.TextBox NodeName;
    }
}
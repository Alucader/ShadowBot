namespace WebService
{
    partial class WbeService
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textIP = new System.Windows.Forms.TextBox();
            this.IPlabel = new System.Windows.Forms.Label();
            this.Portlabel = new System.Windows.Forms.Label();
            this.textPort = new System.Windows.Forms.TextBox();
            this.Startbtn = new System.Windows.Forms.Button();
            this.textLog = new System.Windows.Forms.TextBox();
            this.btnclear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textIP
            // 
            this.textIP.Location = new System.Drawing.Point(86, 45);
            this.textIP.Name = "textIP";
            this.textIP.Size = new System.Drawing.Size(198, 25);
            this.textIP.TabIndex = 0;
            this.textIP.Text = "127.0.0.1";
            // 
            // IPlabel
            // 
            this.IPlabel.AutoSize = true;
            this.IPlabel.Font = new System.Drawing.Font("楷体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IPlabel.Location = new System.Drawing.Point(30, 48);
            this.IPlabel.Name = "IPlabel";
            this.IPlabel.Size = new System.Drawing.Size(25, 15);
            this.IPlabel.TabIndex = 1;
            this.IPlabel.Text = "IP";
            // 
            // Portlabel
            // 
            this.Portlabel.AutoSize = true;
            this.Portlabel.Font = new System.Drawing.Font("楷体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Portlabel.Location = new System.Drawing.Point(362, 51);
            this.Portlabel.Name = "Portlabel";
            this.Portlabel.Size = new System.Drawing.Size(43, 15);
            this.Portlabel.TabIndex = 2;
            this.Portlabel.Text = "Port";
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(442, 48);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(108, 25);
            this.textPort.TabIndex = 3;
            this.textPort.Text = "5566";
            // 
            // Startbtn
            // 
            this.Startbtn.AutoSize = true;
            this.Startbtn.Font = new System.Drawing.Font("楷体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Startbtn.Location = new System.Drawing.Point(599, 48);
            this.Startbtn.Name = "Startbtn";
            this.Startbtn.Size = new System.Drawing.Size(120, 27);
            this.Startbtn.TabIndex = 4;
            this.Startbtn.Text = "Start";
            this.Startbtn.UseVisualStyleBackColor = true;
            this.Startbtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // textLog
            // 
            this.textLog.BackColor = System.Drawing.SystemColors.Window;
            this.textLog.Location = new System.Drawing.Point(33, 93);
            this.textLog.Multiline = true;
            this.textLog.Name = "textLog";
            this.textLog.Size = new System.Drawing.Size(517, 384);
            this.textLog.TabIndex = 5;
            this.textLog.Text = " ";
            // 
            // btnclear
            // 
            this.btnclear.Location = new System.Drawing.Point(571, 448);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(97, 29);
            this.btnclear.TabIndex = 6;
            this.btnclear.Text = "清空";
            this.btnclear.UseVisualStyleBackColor = true;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // WbeService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(790, 503);
            this.Controls.Add(this.btnclear);
            this.Controls.Add(this.textLog);
            this.Controls.Add(this.Startbtn);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.Portlabel);
            this.Controls.Add(this.IPlabel);
            this.Controls.Add(this.textIP);
            this.MaximizeBox = false;
            this.Name = "WbeService";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TabText = "WebService";
            this.Text = "Start";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textIP;
        private System.Windows.Forms.Label IPlabel;
        private System.Windows.Forms.Label Portlabel;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.Button Startbtn;
        public  System.Windows.Forms.TextBox textLog;
        private System.Windows.Forms.Button btnclear;
    }
}


namespace WebService
{
    partial class CheckConn
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
            this.DGViewConn = new System.Windows.Forms.DataGridView();
            this.index23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGViewConn)).BeginInit();
            this.SuspendLayout();
            // 
            // DGViewConn
            // 
            this.DGViewConn.AllowDrop = true;
            this.DGViewConn.AllowUserToAddRows = false;
            this.DGViewConn.AllowUserToDeleteRows = false;
            this.DGViewConn.AllowUserToResizeColumns = false;
            this.DGViewConn.AllowUserToResizeRows = false;
            this.DGViewConn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGViewConn.BackgroundColor = System.Drawing.Color.White;
            this.DGViewConn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGViewConn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.index23,
            this.ip,
            this.port,
            this.time});
            this.DGViewConn.Location = new System.Drawing.Point(0, 0);
            this.DGViewConn.Name = "DGViewConn";
            this.DGViewConn.ReadOnly = true;
            this.DGViewConn.RowHeadersWidth = 51;
            this.DGViewConn.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DGViewConn.RowTemplate.Height = 27;
            this.DGViewConn.Size = new System.Drawing.Size(800, 450);
            this.DGViewConn.TabIndex = 0;
            this.DGViewConn.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGViewConn_CellContentClick);
            // 
            // index23
            // 
            this.index23.DataPropertyName = "index";
            this.index23.HeaderText = "索引";
            this.index23.MinimumWidth = 10;
            this.index23.Name = "index23";
            this.index23.ReadOnly = true;
            this.index23.Width = 125;
            // 
            // ip
            // 
            this.ip.DataPropertyName = "ip";
            this.ip.HeaderText = "地址";
            this.ip.MinimumWidth = 6;
            this.ip.Name = "ip";
            this.ip.ReadOnly = true;
            this.ip.Width = 125;
            // 
            // port
            // 
            this.port.DataPropertyName = "port";
            this.port.HeaderText = "端口号";
            this.port.MinimumWidth = 6;
            this.port.Name = "port";
            this.port.ReadOnly = true;
            this.port.Width = 125;
            // 
            // time
            // 
            this.time.DataPropertyName = "time";
            this.time.HeaderText = "登录时间";
            this.time.MinimumWidth = 6;
            this.time.Name = "time";
            this.time.ReadOnly = true;
            this.time.Width = 300;
            // 
            // CheckConn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DGViewConn);
            this.Name = "CheckConn";
            this.TabText = "查看连接";
            this.Text = "查看连接";
            this.Load += new System.EventHandler(this.CheckConn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGViewConn)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.DataGridView DGViewConn;
        private System.Windows.Forms.DataGridViewTextBoxColumn index23;
        private System.Windows.Forms.DataGridViewTextBoxColumn ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn port;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;

        #endregion
        //  public static string valueStr ="s";
    }
}
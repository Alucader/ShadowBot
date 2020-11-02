namespace WebService
{
    partial class Database
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
            this.DGViewRoom = new System.Windows.Forms.DataGridView();
            this.index23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.play1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.play2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.play3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.play4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGViewRoom)).BeginInit();
            this.SuspendLayout();
            // 
            // DGViewRoom
            // 
            this.DGViewRoom.AllowDrop = true;
            this.DGViewRoom.AllowUserToAddRows = false;
            this.DGViewRoom.AllowUserToDeleteRows = false;
            this.DGViewRoom.AllowUserToResizeColumns = false;
            this.DGViewRoom.AllowUserToResizeRows = false;
            this.DGViewRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGViewRoom.BackgroundColor = System.Drawing.Color.White;
            this.DGViewRoom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGViewRoom.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.index23,
            this.status,
            this.play1,
            this.play2,
            this.play3,
            this.play4});
            this.DGViewRoom.Location = new System.Drawing.Point(1, -1);
            this.DGViewRoom.Name = "DGViewRoom";
            this.DGViewRoom.ReadOnly = true;
            this.DGViewRoom.RowHeadersWidth = 51;
            this.DGViewRoom.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DGViewRoom.RowTemplate.Height = 27;
            this.DGViewRoom.Size = new System.Drawing.Size(799, 451);
            this.DGViewRoom.TabIndex = 1;
            // 
            // index23
            // 
            this.index23.DataPropertyName = "index";
            this.index23.HeaderText = "房间号";
            this.index23.MinimumWidth = 10;
            this.index23.Name = "index23";
            this.index23.ReadOnly = true;
            this.index23.Width = 125;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "状态";
            this.status.MinimumWidth = 6;
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 125;
            // 
            // play1
            // 
            this.play1.DataPropertyName = "play1";
            this.play1.HeaderText = "玩家1";
            this.play1.MinimumWidth = 6;
            this.play1.Name = "play1";
            this.play1.ReadOnly = true;
            this.play1.Width = 125;
            // 
            // play2
            // 
            this.play2.DataPropertyName = "play2";
            this.play2.HeaderText = "玩家2";
            this.play2.MinimumWidth = 6;
            this.play2.Name = "play2";
            this.play2.ReadOnly = true;
            this.play2.Width = 125;
            // 
            // play3
            // 
            this.play3.DataPropertyName = "play3";
            this.play3.HeaderText = "玩家3";
            this.play3.MinimumWidth = 6;
            this.play3.Name = "play3";
            this.play3.ReadOnly = true;
            this.play3.Width = 125;
            // 
            // play4
            // 
            this.play4.DataPropertyName = "play4";
            this.play4.HeaderText = "玩家4";
            this.play4.MinimumWidth = 6;
            this.play4.Name = "play4";
            this.play4.ReadOnly = true;
            this.play4.Width = 125;
            // 
            // Database
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DGViewRoom);
            this.Name = "Database";
            this.TabText = "房间管理";
            this.Text = "房间管理";
            this.Load += new System.EventHandler(this.Database_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGViewRoom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGViewRoom;
        private System.Windows.Forms.DataGridViewTextBoxColumn index23;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn play1;
        private System.Windows.Forms.DataGridViewTextBoxColumn play2;
        private System.Windows.Forms.DataGridViewTextBoxColumn play3;
        private System.Windows.Forms.DataGridViewTextBoxColumn play4;
    }
}
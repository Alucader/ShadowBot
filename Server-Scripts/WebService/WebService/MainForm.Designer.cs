namespace WebService
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.菜单1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.起始页ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemWin = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemStartServ = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemLook = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSQL = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemCo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemBS = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 468);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(918, 26);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(54, 20);
            this.toolStripStatusLabel1.Text = "状态栏";
            // 
            // dockPanel1
            // 
            this.dockPanel1.ActiveAutoHideContent = null;
            this.dockPanel1.BackColor = System.Drawing.Color.White;
            this.dockPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.Location = new System.Drawing.Point(0, 28);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.ShowPadIcon = false;
            this.dockPanel1.Size = new System.Drawing.Size(918, 440);
            this.dockPanel1.TabIndex = 4;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.Location = new System.Drawing.Point(718, 468);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 25);
            this.dateTimePicker1.TabIndex = 7;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.menuStrip1.BackgroundImage = global::WebService.Properties.Resources.d5;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.菜单1ToolStripMenuItem,
            this.MenuItemWin});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(918, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 菜单1ToolStripMenuItem
            // 
            this.菜单1ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.起始页ToolStripMenuItem,
            this.ToolStripMenuItem,
            this.MenuItemClose});
            this.菜单1ToolStripMenuItem.Name = "菜单1ToolStripMenuItem";
            this.菜单1ToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.菜单1ToolStripMenuItem.Text = "菜单1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripMenuItem2.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripMenuItem2.Image = global::WebService.Properties.Resources.d5;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem2.Text = "菜单栏";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripMenuItem3.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem3.Text = "窗口";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // 起始页ToolStripMenuItem
            // 
            this.起始页ToolStripMenuItem.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.起始页ToolStripMenuItem.Name = "起始页ToolStripMenuItem";
            this.起始页ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.起始页ToolStripMenuItem.Text = "起始页（E）";
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.ToolStripMenuItem.Text = "设置（S）";
            // 
            // MenuItemClose
            // 
            this.MenuItemClose.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MenuItemClose.Name = "MenuItemClose";
            this.MenuItemClose.Size = new System.Drawing.Size(224, 26);
            this.MenuItemClose.Text = "退出（Q）";
            this.MenuItemClose.Click += new System.EventHandler(this.MenuItemClose_Click);
            // 
            // MenuItemWin
            // 
            this.MenuItemWin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemStartServ,
            this.MenuItemLook,
            this.MenuItemSQL,
            this.MenuItemPlay,
            this.MenuItemCo,
            this.MenuItemBS});
            this.MenuItemWin.Name = "MenuItemWin";
            this.MenuItemWin.Size = new System.Drawing.Size(53, 24);
            this.MenuItemWin.Text = "窗口";
            // 
            // MenuItemStartServ
            // 
            this.MenuItemStartServ.Name = "MenuItemStartServ";
            this.MenuItemStartServ.Size = new System.Drawing.Size(224, 26);
            this.MenuItemStartServ.Text = "启动服务器";
            // 
            // MenuItemLook
            // 
            this.MenuItemLook.Name = "MenuItemLook";
            this.MenuItemLook.Size = new System.Drawing.Size(224, 26);
            this.MenuItemLook.Text = "查看连接";
            this.MenuItemLook.Click += new System.EventHandler(this.MenuItemLook_Click);
            // 
            // MenuItemSQL
            // 
            this.MenuItemSQL.Name = "MenuItemSQL";
            this.MenuItemSQL.Size = new System.Drawing.Size(224, 26);
            this.MenuItemSQL.Text = "房间管理";
            this.MenuItemSQL.Click += new System.EventHandler(this.MenuItemSQL_Click);
            // 
            // MenuItemPlay
            // 
            this.MenuItemPlay.Name = "MenuItemPlay";
            this.MenuItemPlay.Size = new System.Drawing.Size(224, 26);
            this.MenuItemPlay.Text = "玩家管理";
            // 
            // MenuItemCo
            // 
            this.MenuItemCo.Name = "MenuItemCo";
            this.MenuItemCo.Size = new System.Drawing.Size(224, 26);
            this.MenuItemCo.Text = "聊天室";
            // 
            // MenuItemBS
            // 
            this.MenuItemBS.Name = "MenuItemBS";
            this.MenuItemBS.Size = new System.Drawing.Size(224, 26);
            this.MenuItemBS.Text = "广播消息";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(918, 494);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TabText = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 菜单1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem MenuItemWin;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        public WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemClose;
        private System.Windows.Forms.ToolStripMenuItem 起始页ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemStartServ;
        private System.Windows.Forms.ToolStripMenuItem MenuItemLook;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSQL;
        private System.Windows.Forms.ToolStripMenuItem MenuItemPlay;
        private System.Windows.Forms.ToolStripMenuItem MenuItemCo;
        private System.Windows.Forms.ToolStripMenuItem MenuItemBS;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}
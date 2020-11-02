using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
namespace WebService
{
    public partial class MainForm : DockContent
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Menu1 menu1 = new Menu1() ;
            menu1.Show(dockPanel1);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Menu2 menu2 = new Menu2();
            menu2.Show(dockPanel1);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MenuForm menuForm = new MenuForm();
            menuForm.Show(dockPanel1, DockState.DockLeft);
            WbeService wbe = new WbeService();
            wbe.Show(dockPanel1);
           // FormHome home = new FormHome();
           // home.Show(dockPanel1);
        }

        private void MenuItemClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MenuItemLook_Click(object sender, EventArgs e)
        {
            CheckConn check = new CheckConn();
            check.Show(dockPanel1);
        }

        private void MenuItemSQL_Click(object sender, EventArgs e)
        {
            Database database = new Database();
            database.Show(dockPanel1);
        }
    }
}

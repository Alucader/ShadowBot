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
using WebService.Botton;

namespace WebService
{
    
    public partial class MenuForm : DockContent
    {

        public MenuForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            WbeService menu1 = new WbeService();
            OpenForm(menu1);
        }
        private void OpenForm(DockContent form)
        {
            MainForm main = (MainForm)this.Parent.Parent.Parent.Parent;

            string name = form.Name;
            foreach (Form  item in main.MdiChildren)
            {
                if (item .Name ==name)
                {
                    item.Activate();
                    return;
                }
            }

            main.IsMdiContainer = true;
            form.MdiParent = main;
            form.Show(main.dockPanel1);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            try
            {
             
                CheckConn check = new CheckConn();
                OpenForm(check);
                check.SearchConn();
            }
            catch
            {
               
            }

            /* if (service != null)
             {

             }
             */
            //richTextBox1.Text = "Text";


        }
       

        private void label3_Click(object sender, EventArgs e)
        {
            Database database = new Database();
            OpenForm(database);
        }
    }
}

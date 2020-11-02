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
using WebService.Logic;
namespace WebService
{
    public partial class Database : DockContent
    {
        public Database()
        {
            InitializeComponent();
        }

        private void Database_Load(object sender, EventArgs e)
        {
            try
            {
                DGViewRoom.DataSource = RoomMgr.instance.dt.DefaultView;
            }
            catch
            {

            }
        }
    }
}

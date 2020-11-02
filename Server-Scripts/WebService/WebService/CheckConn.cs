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
    public partial class CheckConn : DockContent 
    {
        DataTable data;
        public DataTable DataValue
        {
            get { return data; }
            set { data = value; }
        }
        public CheckConn()
        {

            InitializeComponent();
           
            //data = DataValue;
        }

    
        private void CheckConn_Load(object sender, EventArgs e)
        {
            SearchConn();
        }

        public  void SearchConn()
        {
            try
            {
                data = ServiceNet.instance.dt;
                DGViewConn.DataSource = data.DefaultView;
            }
            catch
            {

            }
        }

        private void DGViewConn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using WebService.Botton;
using WebService.Botton.Protocol;
using WebService.Logic;
using WeifenLuo.WinFormsUI.Docking;
namespace WebService
{
    public partial class WbeService : DockContent
    {
        public bool isStart = false;
        public static WbeService instance;
        public WbeService()
        {
            instance = this;
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {        
            if(!isStart)
            {
                ServiceNet net = new ServiceNet();
                net.Start(textIP.Text .Trim (),Int32.Parse(textPort.Text));
                net.proto = new ProtocolBytes();
                isStart = true;
                textLog.Text = textLog.Text + "\r\nWinForm:服务器开启成功";
                RoomMgr roomMgr = new RoomMgr();
                DataMgr data = new DataMgr();
                return;
            }
            if (isStart)
            {
                textLog.Text = textLog.Text+"\r\n"+"服务器已经开启，无需再次开启";
                return;
            }
                
        }

        public void WriteLine(string str)
        {
            WbeService.instance.textLog.Text = WbeService.instance.textLog.Text + "\r\n" + str;
            if(textLog.Text.Length>1000)
                textLog.Text = "";
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            textLog.Text = "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using WebService.Botton.Protocol;
using WebService.Botton;
namespace WebService.Middle
{
    class Conn
    {
        public int index = 100;
        //常量
        private const int BUFFER_SIZE = 1024;
        //socket
        public Socket socket;
        //是否使用
        public bool isUse = false;
        //粘包分包
        public byte[] lenBytes = new byte [sizeof (UInt32)];
        public Int32  msgLength =0;
        //缓冲区
        public byte[] readBuff = new byte[BUFFER_SIZE];
        public int buffCount = 0;
        //心跳机制
        public long lastTickTime = long.MinValue;
        public bool isWriteDataTable = false;
        public string time = string.Empty;

        public Player player;
        /// <summary>
        /// 构造
        /// </summary>
        public Conn()
        {
            readBuff = new byte[BUFFER_SIZE];
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="socket"></param>
        public void Init(Socket socket )
        {
            this.socket = socket;
            isUse = true;
            buffCount = 0;
            time =  DateTime.Now.ToLocalTime().ToString();

        }
        /// <summary>
        /// 剩余的Buffer+
        /// </summary>
        /// <returns></returns>
        public int BufferRemain()
        {
            return BUFFER_SIZE - buffCount;
        }
        /// <summary>
        /// 获取客户端地址
        /// </summary>
        /// <returns></returns>
        public string GetAddress()
        {
            
            return socket.RemoteEndPoint.ToString();
            

        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            if (isUse == false)
                return;
            if (player != null)
            {            
                player.Logout();
                return;
            }
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            ServiceNet.instance.Remove(this);
            isWriteDataTable = false;
            isUse = false;
            

        } 
        /// <summary>
        /// 发送协议
        /// </summary>
        public void Send(ProtocolBase protocol)
	{
            ServiceNet.instance.Send (this, protocol);
	}
    }
}

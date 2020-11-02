using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Middle;
using System.Net;
using System.Net.Sockets;
using WebService.Botton.Protocol;
using WebService.Logic;
using System.Windows.Forms;
using WebService;
using System.Reflection;

namespace WebService.Botton
{
   
    class ServiceNet 
    {
        public string text = "测试单例模式";
        public  int te = 0;
        public Socket listenfd;
        //客户端连接
        public Conn[] conns;
        //最大连接数
        public int maxConn = 50;
        //协议
        public ProtocolBase  proto;

        //主定时器
        System.Timers.Timer timer = new System.Timers.Timer(1000);
        //心跳时间
        public long heartBeatTime = 180;

        public delegate void DataTableCon(DataTable dt);
        //public event DataTableCon DataTableEvent;
        public HandleConnMsg handleConnMsg = new HandleConnMsg();
        public HandlePlayerMsg handlePlayerMsg = new HandlePlayerMsg();
        public HandlePlayerEvent handlePlayerEvent = new HandlePlayerEvent();
        public DataTable dt =new DataTable ();
         /// <summary>
        /// 单例模式
        /// </summary>
        public static ServiceNet instance;
        
        public ServiceNet()
        {
            instance = this;           
           // MessageBox.Show("使用了构造");
            dt.Columns.Add("index", typeof(int));
            dt.Columns.Add("ip", typeof(string));
            dt.Columns.Add("port", typeof(string));
            dt.Columns.Add("time", typeof(string));
        }
        
        /// <summary>
        /// 获取连接池索引
        /// </summary>
        /// <returns></returns>
        public int NewIndex()
        {
            if(conns ==null )
                return -1;
            for (int i = 0; i < conns.Length ; i++)
            {
                if (conns[i]== null)
                {
                    conns[i] = new Conn();
                    return i;
                }
                else if (conns[i].isUse == false)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// 开启服务器
        /// </summary> 
        public void Start(string host,int port)
        {
            //连接池          
           conns = new Conn[maxConn];
           for (int i = 0; i < maxConn ; i++)
            {
                conns[i] = new Conn();
                conns[i].index = i;
            }       
            listenfd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAdr = IPAddress.Parse(host);
            IPEndPoint ipEp = new IPEndPoint(ipAdr, port);
            try
            {
                listenfd.Bind(ipEp);
                listenfd.Listen(maxConn);
                listenfd.BeginAccept(AcceptCb, null);
           
                WbeService.instance.textLog.Text = WbeService.instance.textLog.Text+ "\r\n" + "ServiceNet:开启服务器成功!";
               
            }
            catch (Exception ex)
            {

                WbeService.instance.WriteLine(ex.Message);
            }
           
        }
        /// <summary>
        /// Accept回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void AcceptCb(IAsyncResult ar)
        {
            try
            {
                Socket socket = listenfd.EndAccept(ar);
                int index = NewIndex();
                if(index < 0)
                {
                    WbeService.instance.WriteLine("连接已满");
                    socket.Close();
                }
                else
                {
                    Conn conn = conns[index];
                    conn.Init(socket);               
                    //异步接收客户端数据           
                    conn.socket.BeginReceive(conn.readBuff,conn.buffCount ,conn.BufferRemain(),SocketFlags.None,RecevieCb,conn);
                    WbeService.instance.WriteLine(conn.GetAddress() + "加入连接 " + index);
                    if (conn.isWriteDataTable == false)
                    {
                        string str = conn.GetAddress();
                        string[] strSplit = str.Split(':');
                        DataRow dr = dt.NewRow();
                        dr[0] = index;
                        dr[1] = strSplit[0];
                        dr[2] = strSplit[1];
                        dr[3] = conn.time;
                        dt.Rows.Add(dr);
                        conn.isWriteDataTable = true;
                    }
                }             
            }
            catch(Exception  ex)
            {
                WbeService.instance.WriteLine("AcceptCb失败" + ex.Message);
                Console.WriteLine("AcceptCb失败" + ex.Message);  
            }
            finally
            {
                //继续异步监听连接
                listenfd.BeginAccept(AcceptCb, null);
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            for (int i = 0; i < conns.Length ; i++)
            {
                Conn conn =conns[i];
                if (conn == null)
                    continue;
                if (conn.isUse == false)
                    continue;
                lock (conn)
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// Recevie的回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void RecevieCb(IAsyncResult ar)
        {           
            Conn conn = (Conn)ar.AsyncState;
            lock (conn)
            {
                try
                {
                    //获取接收的字节数
                    int count = conn.socket.EndReceive(ar);
                    //关闭信号
                    if (count <= 0)
                    {
                        WbeService.instance.WriteLine("收到 [" + conn.GetAddress() + "]退出连接");
                        conn.Close();
                        return;
                    }
                    //处理数据           
                    conn.buffCount += count;                
                    ProcessData(conn);           
                    //继续接收
                    conn.socket.BeginReceive(conn.readBuff, conn.buffCount, conn.BufferRemain(), SocketFlags.None, RecevieCb, conn);

                }
                catch(Exception ex)
                {
                   //MessageBox.Show("接收错误: "+ex.Message);
                    WbeService.instance.WriteLine("RecevieCb接收错误: " + ex.Message)  ;
                  //  Remove(conn);
                    conn.Close();                
                }
            }
     
          
        }
        /// <summary>
        /// 数据处理
        /// </summary>
        /// <param name="conn"></param>
        private void ProcessData(Conn conn)
        {
            //小于长度字节
            if (conn.buffCount < sizeof(Int32))
                return;
            //将包含消息长度的4个字节复制到lenbytes字节数组中
            Array.Copy(conn.readBuff, conn.lenBytes, sizeof(Int32));
            //将lenbytes转为int整数，得到消息长度
            conn.msgLength = BitConverter.ToInt32(conn.lenBytes, 0);
            if (conn.buffCount < conn.msgLength + sizeof(Int32))
            {
                return;
            }        
            //处理消息            
            ProtocolBase protocol = proto.Decode(conn.readBuff, sizeof(Int32), conn.msgLength);    
            HandleMsg(conn, protocol);
           	//清除已处理的消息
            int count = conn.buffCount - conn.msgLength - sizeof(Int32);
            Array.Copy(conn.readBuff, sizeof(Int32)+conn.msgLength + sizeof(Int32), conn.readBuff, 0, count);
            conn.buffCount = count;
            if (conn.buffCount > 0)
                ProcessData(conn);
          //  WbeService.instance.WriteLine("ProcessData结束");
        }
        /// <summary>
        /// 处理协议的方法
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="protoBase"></param>
        private void HandleMsg(Conn conn,ProtocolBase protoBase)
        {

            string name = protoBase.GetName();
            if (name !="UpdateUnitInfo")
                WbeService.instance.WriteLine(name);
            string methodName = "Msg" + name;
            //连接协议分发
            if (conn.player == null || name == "HeatBeat" || name == "Logout")
            {
                MethodInfo mm = handleConnMsg.GetType().GetMethod(methodName);
                if (mm == null)
                {
                    string str = "[警告]HandleMsg没有处理连接方法 ";
                    WbeService.instance.WriteLine(str + methodName);
                    return;
                }
                Object[] obj = new object[] { conn, protoBase };
                WbeService.instance.WriteLine("[处理链接消息]" + conn.GetAddress() + " :" + name);
                mm.Invoke(handleConnMsg, obj);
            }
            //角色协议分发
            else
            {
                MethodInfo mm = handlePlayerMsg.GetType().GetMethod(methodName);
                if (mm == null)
                {
                    string str = "[警告]HandleMsg没有处理玩家方法 ";
                    Console.WriteLine(str + methodName);
                    return;
                }
                Object[] obj = new object[] { conn.player, protoBase };
                if (name != "UpdateUnitInfo")
                    WbeService.instance.WriteLine("[处理玩家消息]" + conn.player.id + " :" + name);
                mm.Invoke(handlePlayerMsg, obj);
            }

        }
        public  void Send(Conn conn ,ProtocolBase protocol)
        {
            byte[] bytes = protocol.Encode();
            byte[] length = BitConverter.GetBytes(bytes.Length);
            byte[] sendbuff = length.Concat(bytes).ToArray();
            try
            {
                conn.socket.BeginSend(sendbuff, 0, sendbuff.Length, SocketFlags.None, null, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="protocol"></param>
        public void Broadcast(ProtocolBase protocol)
        {
            for (int i = 0; i < conns.Length; i++)
            {
                if (conns[i] == null)
                    continue;
                if (conns[i].isUse == false)
                    continue;
                Send(conns[i], protocol);
            }
        }

        /// <summary>
        /// 心跳机制
        /// </summary>
        //主定时器
        public void HandleMainTimer(object sender, System.Timers.ElapsedEventArgs e)
        {
            //处理心跳
            HeartBeat();
            timer.Start();
        }
        //心跳
        public void HeartBeat()
        {
            //Console.WriteLine ("[主定时器执行]");
            long timeNow = Sys.GetTimeStamp();
            for (int i = 0; i < conns.Length; i++)
            {
                Conn conn = conns[i];
                if (conn == null) continue;
                if (!conn.isUse) continue;

                if (conn.lastTickTime < timeNow - heartBeatTime)
                {
                    Console.WriteLine("[心跳引起断开连接]" + conn.GetAddress());
                    lock (conn)
                    {
                        conn.Close();
                        Remove(conn);
                    }
                       
                }
            }
        }

        public DataTable SearchConn()
        {
           
            for (int i = 0; i < conns.Length; i++)
            {
                Conn conn = conns[i];
                if (conn == null)
                    continue;
                if (conn.isUse == false)
                {
                    continue;
                }
                            
               if(conn.isWriteDataTable ==false)
                {
                    string str = conn.GetAddress();
                    string[] strSplit = str.Split(':');                                  
                    DataRow dr = dt.NewRow();
                    dr[0] = i;
                    dr[1] = strSplit[0];
                    dr[2] = strSplit[1];
                    dr[3] = conn.time;
                    dt.Rows.Add(dr);
                    conn.isWriteDataTable = true;
                }              
            }
            return dt ;
        }
        public void Remove(Conn conn)
        {
            //    DataTable dts;
            try
            {
                DataRow[] foundRow;
                string str = "index=" + conn.index;
                foundRow = dt.Select(str, "");
                foreach (DataRow row in foundRow)
                {
                    dt.Rows.Remove(row);
                }
            }
            catch
            {

            }
        }
       
    }
}

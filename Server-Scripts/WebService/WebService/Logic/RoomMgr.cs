using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Middle;
using WebService.Botton.Protocol;
using System.Data;

namespace WebService.Logic
{
    class RoomMgr
    {

        //单例
        public static RoomMgr instance;

        public DataTable dt = new DataTable();
        int index = 0;
        public RoomMgr()
        {
            instance = this;
            dt.Columns.Add("index", typeof(int));
            dt.Columns.Add("status", typeof(string));
            dt.Columns.Add("play1", typeof(string));
            dt.Columns.Add("play2", typeof(string));
            dt.Columns.Add("play3", typeof(string));
            dt.Columns.Add("play4", typeof(string));
        }

        //房间列表
        public List<Room> list = new List<Room>();

        //创建房间
        public void CreateRoom(Player player)
        {
            Room room = new Room();
            index++;
            room.index = index;

            DataRow dr = dt.NewRow();
            dr[0] = room.index;
            dr[1] = room.status.ToString();
            dt.Rows.Add(dr);

            lock (list)
            {
                list.Add(room);
                room.AddPlayer(player);
            }
                 
        }

        //玩家离开
        public void LeaveRoom(Player player)
        {
            PlayerTempData tempDate = player.tempData;
            if (tempDate.status == PlayerTempData.Status.None)
                return;

            Room room = tempDate.room;

            lock (list)
            {
                room.DelPlayer(player.id);
                if (room.list.Count == 0)
                {
                    try
                    {
                        DataRow[] foundRow;
                        string str = "index=" + room.index;
                        foundRow = dt.Select(str, "");
                        foreach (DataRow row in foundRow)
                        {
                            dt.Rows.Remove(row);
                          
                        }
                        index--;
                        list.Remove(room);
                    }
                    catch
                    {
                        index--;
                        list.Remove(room);
                    }
                }
                    
            }
        }

        //列表
        public ProtocolBytes GetRoomList()
        {
            ProtocolBytes protocol = new ProtocolBytes();
            protocol.AddString("GetRoomList");
            int count = list.Count;
            //房间数量
            protocol.AddInt(count);
            //每个房间信息
            for (int i = 0; i < count; i++)
            {
                Room room = list[i];
                protocol.AddInt(room.list.Count);
                protocol.AddInt((int)room.status);
            }
            return protocol;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Botton.Protocol;
using WebService.Middle;
using WebService.Botton;
using System.Data;

namespace WebService.Logic
{
    class Room
    {
        //状态
        public enum Status
        {
            Prepare = 1,
            Fight = 2,
        }
        public Status status = Status.Prepare;
        public int index;
        //玩家
        public int maxPlayers = 4;
        public Dictionary<string, Player> list = new Dictionary<string, Player>();
        public int playnum=0;

        //添加玩家
        public bool AddPlayer(Player player)
        {
            lock (list)
            {
                if (list.Count >= maxPlayers)
                    return false;
                PlayerTempData tempData = player.tempData;
                tempData.room = this;
              tempData.team = SwichTeam();
                tempData.status = PlayerTempData.Status.Room;

                if (list.Count == 0)
                    tempData.isOwner = true;
                else
                    tempData.isOwner = false;
                string id = player.id;
                list.Add(id, player);
                playnum++;
                player.num = playnum;
                try
                {                
                    RoomMgr.instance.dt.Rows[tempData.room.index - 1][playnum + 1] = id;
                }
                catch
                {

                }
            }
            return true;
        }

        //分配队伍
        public int SwichTeam()
        {
            int count1 = 0;
            int count2 = 0;
            foreach (Player player in list.Values)
            {
                if (player.tempData.team == 1) count1++;
                if (player.tempData.team == 2) count2++;
            }
            if (count1 <= count2)
                return 1;
            else
                return 2;
        }

        //删除玩家
        public void DelPlayer(string id)
        {
            lock (list)
            {
                if (!list.ContainsKey(id))
                    return;
                bool isOwner = list[id].tempData.isOwner;
                list[id].tempData.status = PlayerTempData.Status.None;
                try
                {
                    RoomMgr.instance.dt.Rows[list[id].tempData.room.index - 1][list[id].num + 1] = "";
                    playnum--;
                }
                catch
                {
                    playnum--;
                }

                list.Remove(id);
                if (isOwner)
                    UpdateOwner();
            }
        }

        //更换房主
        public void UpdateOwner()
        {
            lock (list)
            {
                if (list.Count <= 0)
                    return;

                foreach (Player player in list.Values)
                {
                    player.tempData.isOwner = false;
                }

                Player p = list.Values.First();
                p.tempData.isOwner = true;
            }
        }

        //广播
        public void Broadcast(ProtocolBase protocol)
        {
            foreach (Player player in list.Values)
            {
                player.Send(protocol);
            }
        }

        //房间信息
        public ProtocolBytes GetRoomInfo()
        {
            ProtocolBytes protocol = new ProtocolBytes();
            protocol.AddString("GetRoomInfo");
            //房间信息
            protocol.AddInt(list.Count);
            //每个玩家信息
            foreach (Player p in list.Values)
            {
                protocol.AddString(p.id);
                protocol.AddInt(p.tempData.team);
                protocol.AddInt(p.data.win);
                protocol.AddInt(p.data.fail);
                protocol.AddString(p.data.achi);
                int isOwner = p.tempData.isOwner ? 1 : 0;
                protocol.AddInt(isOwner);
            }
            return protocol;
        }

        //房间能否开战
        public bool CanStart()
        {
            if (status != Status.Prepare)
                return false;

            int count1 = 0;
            int count2 = 0;

            foreach (Player player in list.Values)
            {
                if (player.tempData.team == 1) count1++;
                if (player.tempData.team == 2) count2++;
            }

            if (count1 < 1 || count2 < 1)
                return false;

            return true;
        }


        public void StartFight()
        {
            ProtocolBytes protocol = new ProtocolBytes();
            protocol.AddString("Fight");
            status = Status.Fight;
            int teamPos1 = 1;
            int teamPos2 = 1;
            lock (list)
            {
                protocol.AddInt(list.Count);
                foreach (Player p in list.Values)
                {
                    p.tempData.hp = 100;
                    protocol.AddString(p.id);
                    protocol.AddInt(p.tempData.team);
                    if (p.tempData.team == 1)
                        protocol.AddInt(teamPos1++);
                    else
                        protocol.AddInt(teamPos2++);
                    p.tempData.status = PlayerTempData.Status.Fight;
                }
                Broadcast(protocol);
            }
        }

      

        //中途退出战斗
        public void ExitFight(Player player)
        {     
            if (list[player.id] != null)
                list.Remove(player.id);
            //广播消息
            ProtocolBytes protocolRet = new ProtocolBytes();
            protocolRet.AddString(" ExitFight");
            protocolRet.AddString(player.id);          
            Broadcast(protocolRet);
            
            player.data.fail++;
           
        }
    }
}

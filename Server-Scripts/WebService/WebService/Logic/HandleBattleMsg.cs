using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Middle;
using WebService.Botton.Protocol;
using WebService.Botton;
namespace WebService.Logic
{
        partial class HandlePlayerMsg
        {

            public void MsgStartFight(Player player, ProtocolBase protoBase)
            {
                ProtocolBytes protocol = new ProtocolBytes();
                protocol.AddString("StartFight");
                //条件判断
                if (player.tempData.status != PlayerTempData.Status.Room)
                {
                    WbeService.instance.WriteLine("MsgStartFight status err " + player.id);
                    protocol.AddInt(-1);
                    player.Send(protocol);
                    return;
                }

                if (!player.tempData.isOwner)
                {
                    Console.WriteLine("MsgStartFight owner err " + player.id);
                    protocol.AddInt(-1);
                    player.Send(protocol);
                    return;
                }

                Room room = player.tempData.room;
                if (!room.CanStart())
                {
                    Console.WriteLine("MsgStartFight CanStart err " + player.id);
                    protocol.AddInt(-1);
                    player.Send(protocol);
                    return;
                }
          
                room.status = Room.Status.Fight;
                //开始战斗
                protocol.AddInt(0);
                player.Send(protocol);
                room.StartFight();
                try
                {
                    RoomMgr.instance.dt.Rows[player.tempData.room.index - 1][1] = room.status;
                }
                catch
                {
                }
            }

            //同步玩家单元
            public void MsgUpdateUnitInfo(Player player, ProtocolBase protoBase)
            {
                //获取数值
                int start = 0;
                ProtocolBytes protocol = (ProtocolBytes)protoBase;
                string protoName = protocol.GetString(start, ref start);
                float posX = protocol.GetFloat(start, ref start);
                float posY = protocol.GetFloat(start, ref start);
                float posZ = protocol.GetFloat(start, ref start);
                float rotX = protocol.GetFloat(start, ref start);
                float rotY = protocol.GetFloat(start, ref start);
                float rotZ = protocol.GetFloat(start, ref start);
                float speed = protocol.GetFloat(start, ref start);
                // WbeService.instance.WriteLine(posX+", "+posY + ", "+posZ+ rotX + ", " + rotY + ", " + rotZ);
                //获取房间
                if (player.tempData.status != PlayerTempData.Status.Fight)
                    return;
                Room room = player.tempData.room;           
                player.tempData.posX = posX;
                player.tempData.posY = posY;
                player.tempData.posZ = posZ;
                player.tempData.lastUpdateTime = Sys.GetTimeStamp();
                //广播
                ProtocolBytes protocolRet = new ProtocolBytes();
                protocolRet.AddString("UpdateUnitInfo");
                protocolRet.AddString(player.id);
                protocolRet.AddFloat(posX);
                protocolRet.AddFloat(posY);
                protocolRet.AddFloat(posZ);
                protocolRet.AddFloat(rotX);
                protocolRet.AddFloat(rotY);
                protocolRet.AddFloat(rotZ);
                protocolRet.AddFloat(speed);
                room.Broadcast(protocolRet);
            }

            public void MsgShooting(Player player, ProtocolBase protoBase)
            {
                //获取数值
                int start = 0;
                ProtocolBytes protocol = (ProtocolBytes)protoBase;
                string protoName = protocol.GetString(start, ref start);
    
                //获取房间
                if (player.tempData.status != PlayerTempData.Status.Fight)
                    return;
                Room room = player.tempData.room;
                //广播
                ProtocolBytes protocolRet = new ProtocolBytes();
                protocolRet.AddString("Shooting");
                protocolRet.AddString(player.id);
       
                room.Broadcast(protocolRet);
            }
            public void MsgGrenade(Player player, ProtocolBase protoBase)
            {
                //获取数值
                int start = 0;
                ProtocolBytes protocol = (ProtocolBytes)protoBase;
                string protoName = protocol.GetString(start, ref start);

                //获取房间
                if (player.tempData.status != PlayerTempData.Status.Fight)
                    return; 
                Room room = player.tempData.room;
                //广播
                ProtocolBytes protocolRet = new ProtocolBytes();
                protocolRet.AddString("Grenade");
                protocolRet.AddString(player.id);

                room.Broadcast(protocolRet);
            }   
            //伤害处理
            public void MsgHit(Player player, ProtocolBase protoBase)
            {
                //解析协议
                int start = 0;
                ProtocolBytes protocol = (ProtocolBytes)protoBase;
                string protoName = protocol.GetString(start, ref start);
                string enemyName = protocol.GetString(start, ref start);
                int damage = protocol.GetInt(start, ref start);
                    
                //获取房间
                if (player.tempData.status != PlayerTempData.Status.Fight)
                    return;
                Room room = player.tempData.room;
                //扣除生命值
                if (!room.list.ContainsKey(enemyName))
                {
                    WbeService.instance.WriteLine("MsgHit not Contains enemy " + enemyName);
                    return;
                }
                Player enemy = room.list[enemyName];
                if (enemy == null)
                    return;
                if (enemy.tempData.hp <= 0)
                    return;
                enemy.tempData.hp -= damage;
            
                ProtocolBytes protocolRet = new ProtocolBytes();
                protocolRet.AddString("Hit");
                protocolRet.AddString(enemy.id);
                protocolRet.AddInt(damage);
                room.Broadcast(protocolRet);
                if (enemy.tempData.hp <= 0)
                {
                    ProtocolBytes protocolkill = new ProtocolBytes();
                    protocolkill.AddString("KillInfo");
                    protocolkill.AddString(player.id);
                    protocolkill.AddString(enemy.id);
                    room.Broadcast(protocolkill);
                }

            }

            public void MsgRevive(Player player, ProtocolBase protoBase)
        {                       
            int start = 0;
            ProtocolBytes protocol = (ProtocolBytes)protoBase;
            string protoName = protocol.GetString(start, ref start);
            string reviveid = protocol.GetString(start, ref start);
            Room room = player.tempData.room;
            if (!room.list.ContainsKey(reviveid))
            {
                WbeService.instance.WriteLine("键值对不存在" + reviveid);
                return;
            }
            Player revive = room.list[reviveid];
            if (revive == null)
                return;
            revive.tempData.hp = 100;
            WbeService.instance.WriteLine(reviveid + "复活");
        }

        public void MsgResule(Player player, ProtocolBase protoBase)
        {
            int start = 0;
            ProtocolBytes protocol = (ProtocolBytes)protoBase;
            string protoName = protocol.GetString(start, ref start);
            int camp = protocol.GetInt(start, ref start);
            Room room = player.tempData.room;
            foreach (Player player1 in room.list.Values)
            {
                if (player1.tempData.team == camp)
                    player1.data.win += 1;
                else
                    player1.data.fail += 1;
                player1.data.achi = Ache(player1.data.win);
            }
        }

        string  Ache(int i)
        {
            if (i < 10)
                return "新兵";
            else if(i>10 &&i<20)
                return "下士";
            else if (i > 20 && i < 30)
                return "上士";
            else if (i > 40 && i < 60)
                return "上校";
            else if (i > 60 && i < 100)
                return "中尉";
            else
                return "上尉";
        }
    }
}

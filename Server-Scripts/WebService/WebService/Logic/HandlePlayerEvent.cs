using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Middle;

namespace WebService.Logic
{
    class HandlePlayerEvent
    {
        public void OnLogin(Player player)
        {

        }
        //下线
        public void OnLogout(Player player)
        {
            //房间中
            if (player.tempData.status == PlayerTempData.Status.Room)
            {
                Room room = player.tempData.room;
                RoomMgr.instance.LeaveRoom(player);
                if (room != null)
                    room.Broadcast(room.GetRoomInfo());
            }
            //战斗中
            if (player.tempData.status == PlayerTempData.Status.Fight)
            {
                Room room = player.tempData.room;
                room.ExitFight(player);
                RoomMgr.instance.LeaveRoom(player);
            }
        }
    }
}

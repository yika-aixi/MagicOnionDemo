//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-02:23
//MagicOnionTestService

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicOnion.Server;
using MagicOnion.Server.Hubs;
using MagicOnionTestService.LobbyMessageTest.Room;

namespace MagicOnionTestService.LobbyMessageTest.Impls
{
    public static class RoomManager
    {
        private static List<IGroup> _rooms;
        
        static RoomManager()
        {
            _rooms = new List<IGroup>();
            _roomInfos = new List<RoomInfo>();
        }

        public static bool HasRoom(string roomName)
        {
            return _rooms.Count(x => x.GroupName == roomName) > 0;
        }

        public static void AddRoom(IGroup room)
        {
            if (_rooms.Contains(room))
            {
                return;
            }
            
            _rooms.Add(room);
        }

        public static bool GetRoom(string roomName, out IGroup room)
        {
            for (var index = 0; index < _rooms.Count; index++)
            {
                room = _rooms[index];

                if (room.GroupName == roomName)
                {
                    return true;
                }
            }

            room = null;

            return false;
        }

        /// <summary>
        /// 没有找到返回null
        /// </summary>
        /// <param name="roomName"></param>
        /// <returns></returns>
        public static IGroup GetRoom(string roomName)
        {
            IGroup room;

            GetRoom(roomName, out room);

            return room;
        }

        private static List<RoomInfo> _roomInfos;
        
        /// <summary>
        /// 获取房间信息
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<RoomInfo>> GetRoomInfos()
        {
            for (var i = _roomInfos.Count; i < _rooms.Count; i++)
            {
                _roomInfos.Add(null);
            }

            for (var i = 0; i < _rooms.Count; i++)
            {
                var room = _rooms[i];

                var playerCount = await room.GetMemberCountAsync();
                
                var roomInfo = _roomInfos[i];

                if (roomInfo == null)
                {
                    _roomInfos[i] = new RoomInfo(room.GroupName);

                    roomInfo = _roomInfos[i];
                }

                roomInfo.PlayerCount = playerCount;
            }

            return _roomInfos;
        }
        
        /// <summary>
        /// 加入房间
        /// </summary>
        /// <param name="room"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task JoinRoom(IGroup room, ServiceContext context)
        {
            try
            {
                await room.AddAsync(context);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        } 

        /// <summary>
        /// 离开房间
        /// </summary>
        /// <param name="room"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<bool> LeaveRoom(IGroup room, ServiceContext context)
        {
            try
            {
                var result = await room.RemoveAsync(context);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-01:21
//Assembly-CSharp

using System.Collections.Generic;
using System.Threading.Tasks;
using MagicOnion;

namespace MagicOnionTestService.LobbyMessageTest.Room
{
    /// <summary>
    /// 房间服务 - > 客户端调用 
    /// </summary>
    public interface IGetRoomService:IService<IGetRoomService>
    {
        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <returns></returns>
        UnaryResult<RoomInfo[]>  GetRoomList();
    }
}
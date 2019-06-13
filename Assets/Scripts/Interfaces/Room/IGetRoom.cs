//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-01:21
//Assembly-CSharp

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagicOnionTestService.LobbyMessageTest.Room
{
    public interface IGetRoom
    {
        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetRoomList();
    }
}
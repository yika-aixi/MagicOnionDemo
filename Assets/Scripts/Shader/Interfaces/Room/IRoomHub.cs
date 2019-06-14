//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-17:33
//MagicOnionTestService

using System.Threading.Tasks;
using MagicOnion;

namespace MagicOnionTestService.LobbyMessageTest.Room
{
    public interface IRoomHub:IStreamingHub<IRoomHub,IRoomHubReceiver>
    {
        Task CreateRoom(string roomName);
        
        Task JoinOrCreateRoom(string roomName,string playerName);

        Task LeaveRoom();
    }
}
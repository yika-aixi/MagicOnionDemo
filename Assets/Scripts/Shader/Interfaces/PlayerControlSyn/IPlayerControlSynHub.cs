//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-17:07
//Assembly-CSharp

using System.Threading.Tasks;
using MagicOnion;
using Shader.MessageObjects;

namespace MagicOnionTestService.LobbyMessageTest.PlayerControlSyn
{
    /// <summary>
    /// 玩家操作同步
    /// </summary>
    public interface IPlayerControlSynHub:IStreamingHub<IPlayerControlSynHub,IPlayerControlSynReceiver>
    {
        Task Init(PlayerControlInitMesg mesg);
        
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="mesg"></param>
        /// <returns></returns>
        Task Move(PlayerMoveControlMesg mesg);
    }
}
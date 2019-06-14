//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-17:07
//Assembly-CSharp

using Shader.MessageObjects;

namespace MagicOnionTestService.LobbyMessageTest.PlayerControlSyn
{
    public interface IPlayerControlSynReceiver
    {
        void OnMove(PlayerMoveControlMesg mesg);
    }
}
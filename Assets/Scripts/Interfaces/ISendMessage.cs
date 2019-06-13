//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2019年01月15日01:32:14
//MagicOnionTestService

namespace MagicOnionTestService.LobbyMessageTest
{
    public interface ISendMessage
    {
        /// <summary>
        /// 发消息
        /// </summary>
        /// <param name="name">発言した人の名前</param>
        /// <param name="message">メッセージ</param>
        void OnSendMessage(string name, string message);
    }
}
//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-02:34
//MagicOnionTestService

using MessagePack;

namespace Shader.MessageObjects
{
    /// <summary>
    /// 发送消息返回
    /// </summary>
    [MessagePackObject()]
    public class SendMesgResponses
    {
        /// <summary>
        /// 发送者
        /// </summary>
        [Key(0)]
        public string UserName;

        /// <summary>
        /// 发送的消息
        /// </summary>
        [Key(1)]
        public SendMesg Messg;

        public SendMesgResponses(string userName, SendMesg messg)
        {
            UserName = userName;
            Messg = messg;
        }
    }
}
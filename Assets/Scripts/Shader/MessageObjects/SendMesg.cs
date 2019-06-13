//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-02:13
//Assembly-CSharp

using MessagePack;

namespace Shader.MessageObjects
{
    /// <summary>
    /// 发送的消息
    /// </summary>
    [MessagePackObject]
    public class SendMesg
    {
        /// <summary>
        /// 消息
        /// </summary>
        [Key(0)]
        public string Message;

        public SendMesg(string message)
        {
            Message = message;
        }
    }
}
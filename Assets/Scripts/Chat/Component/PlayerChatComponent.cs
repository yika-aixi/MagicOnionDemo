//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-03:01
//Assembly-CSharp

using System;
using Shader.MessageObjects;
using UnityEngine;

namespace Chat.Component
{
    public class PlayerChatComponent : MonoBehaviour
    {
        [SerializeField] 
        private string _userName;
        
        private ShowHeadMesgComponent _showHead;
        
        private void Awake()
        {
            _showHead = GetComponent<ShowHeadMesgComponent>();
        }

        public void Init(string userName)
        {
            _userName = userName;
            
            ChatComponent.SendMessageEve += ChatEventsOnSendMessage;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            ChatComponent.SendMessageEve -= ChatEventsOnSendMessage;
        }
        
        private void ChatEventsOnSendMessage(SendMesgResponses obj)
        {
            if (_userName == obj.UserName)
            {
                try
                {
                    _showHead.ShowMesg(obj.Messg.Message);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
    }
}
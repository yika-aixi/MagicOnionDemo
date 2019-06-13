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

        public void Init(string userName)
        {
            _userName = userName;
            
            ChatEvents.SendMessage += ChatEventsOnSendMessage;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            ChatEvents.SendMessage -= ChatEventsOnSendMessage;
        }

        private void OnDestroy()
        {
            ChatEvents.SendMessage -= ChatEventsOnSendMessage;
        }

        private void ChatEventsOnSendMessage(SendMesgResponses obj)
        {
            if (_userName == obj.UserName)
            {
                ShowHeadMesgComponent.Instance.ShowMesg(gameObject,obj.Messg.Message);
            }
        }
    }
}
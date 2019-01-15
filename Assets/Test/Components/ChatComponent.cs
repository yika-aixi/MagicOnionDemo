//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2019年01月15日02:02:17
//Assembly-CSharp

using System;
using Grpc.Core;
using MagicOnion.Client;
using MagicOnionTestService.LobbyMessageTest;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Components
{
    public class ChatComponent : MonoBehaviour, IChat
    {
        private IChatHub _chatHub;
        private bool _isJoin;

        public InputField UserNameInputField;
        
        //显示消息
        public Text ChatText;

        //入室・退室UI
        public Button JoinOrLeaveButton;
        public Text JoinOrLeaveButtonText;

        //发送消息块
        public Button SendMessageButton;
        public InputField MessageInputField;

        // Start is called before the first frame update
        void Start()
        {
            this._isJoin = false;

            //初始化
            var channel = new Channel("localhost:12345", ChannelCredentials.Insecure);
            this._chatHub = StreamingHubClient.Connect<IChatHub, IChat>(channel, this);

            //默认隐藏发送消息按钮
            this.SendMessageButton.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnDestroy()
        {
            //被销毁,调用离开
            this._chatHub.LeaveAsync();
            this._chatHub = null;
        }

        private void OnApplicationQuit()
        {
            if (_chatHub != null)
            {
                //游戏结束,调用离开
                this._chatHub.LeaveAsync();
            }
        }

        #region Client -> Server

        /// <summary>
        /// 加入房间或退出房间
        /// </summary>
        public async void JoinOrLeave()
        {
            if (this._isJoin)
            {
                await this._chatHub.LeaveAsync();
                this._isJoin = false;
                this.JoinOrLeaveButtonText.text = "加入房间";
                this.SendMessageButton.gameObject.SetActive(false);
                UserNameInputField.gameObject.SetActive(true);
            }
            else
            {
                if (string.IsNullOrEmpty(UserNameInputField.text))
                {
                    this.ChatText.text += $"<color=\"red\">用户名不能为空..</color>";
                    return;
                }
                await this._chatHub.JoinAsync(this.UserNameInputField.text);
                this._isJoin = true;
                this.JoinOrLeaveButtonText.text = "离开房间";
                this.SendMessageButton.gameObject.SetActive(true);
                UserNameInputField.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public async void SendMessage()
        {
            //如果没有加入房间不能发送消息
            if (!this._isJoin)
                return;

            await this._chatHub.SendMessageAsync(this.MessageInputField.text);
        }

        #endregion

        #region Client <- Server

        public void OnJoin(string name)
        {
            this.ChatText.text += $"\n{name}加入房间 - 时间:{DateTime.Now:s}";
        }

        public void OnLeave(string name)
        {
            this.ChatText.text += $"\n{name}离开房间 - 时间:{DateTime.Now:s}";
        }

        public void OnSendMessage(string name, string message)
        {
            this.ChatText.text += $"\n时间:{DateTime.Now:s} : {name}：{message}";
        }

        #endregion
    }
}
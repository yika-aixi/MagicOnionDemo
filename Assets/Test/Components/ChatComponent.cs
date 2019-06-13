//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2019年01月15日02:02:17
//Assembly-CSharp

using System;
using System.Threading.Tasks;
using Grpc.Core;
using MagicOnion.Client;
using MagicOnionTestService.LobbyMessageTest;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Components
{
    public class ChatComponent : MonoBehaviour, IChat
    {
        string _playerName;
        private IChatHub _chatHub;
        private bool _isJoin;

        public InputField UserNameInputField;
        public InputField RoomName;
        
        //显示消息
        public Text ChatText;

        //入室・退室UI
        public Text JoinOrLeaveButtonText;

        //发送消息块
        public Button SendMessageButton;
        public InputField MessageInputField;

        [Header("服务器连接相关")] 
        public InputField ip;

        public InputField port;

        [Header("ui范围")]
        public GameObject chat;
        public GameObject server;
        private Channel _channel;


        // Start is called before the first frame update
        void Start()
        {
            chat.SetActive(false);
            server.SetActive(true);
            SendMessageButton.enabled = false;
            
            this._isJoin = false;
        }

        public void Connect()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ip.text))
                {
                    this.ChatText.text += $"<color=\"red\">\nIP 不能为空!</color>"; 
                    return;
                }
                
                if (string.IsNullOrWhiteSpace(port.text))
                {
                    this.ChatText.text += $"<color=\"red\">\n端口 不能为空!</color>"; 
                    return;
                }
                
                _channel = new Channel($"{ip.text}:{port.text}", ChannelCredentials.Insecure);
                
                this._chatHub = StreamingHubClient.Connect<IChatHub, IChat>(_channel, this);
                
                chat.SetActive(true);
                server.SetActive(false);
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                this.ChatText.text += $"<color=\"red\">\n服务器连接失败!</color>";
            }
        }

        // Update is called once per frame
        async void Update()
        {
        }

        private async void OnDestroy()
        {
            await _leave();
        }

        private async Task _leave()
        {
            if (_chatHub == null)
            {
                return;
            }
            
            await this._chatHub.LeaveAsync();
            
            await _des();
        }

        private async Task _des()
        {
            if (_chatHub == null)
            {
                return;
            }
            
            await _chatHub.DisposeAsync();
            await _channel.ShutdownAsync();
            this._chatHub = null;
            _channel = null;
        }

        private async void OnApplicationQuit()
        {
            if (_chatHub != null)
            {
                await _leave();
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
                this.SendMessageButton.enabled = false;
                UserNameInputField.enabled = true;
            }
            else
            {
                if (string.IsNullOrEmpty(UserNameInputField.text))
                {
                    this.ChatText.text += $"<color=\"red\">\n用户名不能为空..</color>";
                    return;
                }
                
                if (string.IsNullOrEmpty(RoomName.text))
                {
                    this.ChatText.text += $"<color=\"red\">\n房间名名不能为空..</color>";
                    return;
                }

                _playerName = UserNameInputField.text;

                try
                {
                    await this._chatHub.JoinAsync(this.UserNameInputField.text, RoomName.text);
                    this._isJoin = true;
                    this.JoinOrLeaveButtonText.text = "离开房间";
                    this.SendMessageButton.enabled = true;
                    UserNameInputField.enabled = false;
                    RoomName.enabled = false;
                }
                catch (RpcException re)
                {
                    this.ChatText.text += $"<color=\"red\">\n连接服务器失败..</color>";
                    chat.SetActive(false);

                    await _des();
                    
                    server.SetActive(true);
                    
                }
                catch (Exception e)
                {
                    Debug.LogError(_chatHub);

                    
                    Debug.LogException(e);
                    this.ChatText.text += $"<color=\"red\">\n" +
                                          $"加入{RoomName.text}房间失败..\n" +
                                          $"{e.Message}" +
                                          $"</color>";
                }
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
            if (name == _playerName)
            {
                return;
            }
            
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
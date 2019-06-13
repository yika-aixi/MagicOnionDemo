//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-02:51
//Assembly-CSharp

using System;
using System.Threading.Tasks;
using MagicOnion.Client;
using MagicOnionTestService.LobbyMessageTest;
using Shader.MessageObjects;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Chat.Component
{
    public class ChatComponent:MonoBehaviour,IChatHubReceiver
    {
        string _playerName;
        private IChatHub _chatHub;
        private bool _isJoin;

        public CanvasGroup RoomUI;
        
        [Header("Names")]
        public InputField RoomName;
        public InputField UserName;
        
        
        [Header("消息显示")]
        public Text ChatText;
        
        [Header("消息输入框")]
        public InputField MessageInputField;

        public static ChatComponent Instance;
        
        void Awake()
        {
            Instance = this;
        }

        public void OnJoinRoom(JoinOrCreateRoomMesg mesg)
        {
            ChatEvents.OnJoinRoom(mesg);

            var player = PlayerManager.Instance.CreatePlayer(mesg.UserName);
            
            var con = player.GetComponent<ThirdPersonUserControl>();
            
            if (mesg.UserName != _playerName)
            {
                _showMesg($"{mesg.UserName} Join Room.");
            
                con.enabled = false;
            }
            else
            {
                con.enabled = true;
            }
        }

        public void OnLeaveRoom(string playerName)
        {
            ChatEvents.OnLeaveRoom(playerName);
            
            PlayerManager.Instance.DestroyPlayer(playerName);
            
            if (playerName != _playerName)
            {
                _showMesg($"{playerName} Leave Room.");
            }
        }

        public void OnSendMessage(SendMesgResponses mesg)
        {
            if (mesg.UserName != _playerName)
            {
                _showMesg($"My: {mesg.Messg}");
            }
            else
            {
                _showMesg($"{mesg.UserName}: {mesg.Messg}");
            }
        }

        void _showMesg(string mesg)
        {
            ChatText.text += "\n" +
                             $"{DateTime.Now:yyyy-MM-dd;HH:mm}: {mesg}";
        }


        #region Call Server Function

        public async void JoinOrCreateRoom()
        {
            if (!RoomName | !UserName)
            {
                return;    
            }

            string roomName = RoomName.text;

            string userName = UserName.text;
            
            if (string.IsNullOrWhiteSpace(roomName))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                return;
            }

            var joinResult = await JoinOrCreateRoom(new JoinOrCreateRoomMesg(roomName, userName));

            if (joinResult)
            {
                RoomUI.alpha = 0;
            }
        }
        
        public async Task<bool> JoinOrCreateRoom(JoinOrCreateRoomMesg mesg)
        {
            try
            {
                if (!MagicOnionManager.Instance.IsConnect)
                {
                    return false;
                }
            
                this._chatHub = StreamingHubClient.Connect<IChatHub, IChatHubReceiver>(MagicOnionManager.Instance.Channel, this);
                
                await _chatHub.JoinOrCreateRoom(mesg);
                //todo 进入场景

                return true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return false;
        }

        public async void LeaveRoom()
        {
            try
            {
                await _chatHub.LeaveRoom();
                await _chatHub.DisposeAsync();
                await MagicOnionManager.Instance.Channel.ShutdownAsync();

                //todo 离开场景
                
                //
                RoomUI.alpha = 1;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void SendMessage()
        {
            if (!MessageInputField)
            {
                return;    
            }
            
            if (string.IsNullOrWhiteSpace(MessageInputField.text))
            {
                return;
            }
            
            SendMessage(new SendMesg(MessageInputField.text));
        }

        public async void SendMessage(SendMesg mesg)
        {
            await _chatHub.SendMessage(mesg);
        }

        #endregion
    }
}
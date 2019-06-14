//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-02:51
//Assembly-CSharp

using System;
using System.Linq;
using MagicOnion.Client;
using MagicOnionTestService.LobbyMessageTest;
using Shader.MessageObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Chat.Component
{
    public partial class ChatComponent:MonoBehaviour,IChatHubReceiver
    {
        private IChatHub _chatHub;
        [NonSerialized]
        public bool IsJoin;

        public CanvasGroup RoomUI;
        public CanvasGroup ChatUI;

        public static ChatComponent Instance;

        public InputField messageInputField;
        
        void Awake()
        {
            Instance = this;
            ChatUI.alpha = 0;
            
            RoomComponent.JoinRoomEve += JoinOrCreateRoom;
            RoomComponent.LeaveRoomEve += LeaveRoom;
        }
        public KeyCode[] activationKeys = {KeyCode.Return, KeyCode.KeypadEnter};
        bool eatActivation;
        void Start()
        {
            // end edit listener
            messageInputField.onEndEdit.AddListener((value) => {
                // submit key pressed? then submit and set new input text
                if (AnyKeyDown(activationKeys))
                {
                    SendMessage(new SendMesg(value));
                    
                    messageInputField.text = "";
                    messageInputField.MoveTextEnd(false);
                    eatActivation = true;
                }

                // unfocus the whole chat in any case. otherwise we would scroll or
                // activate the chat window when doing wsad movement afterwards
                DeselectCarefully();
            });
        }

        private void Update()
        {
            if (AnyKeyDown(activationKeys) && !eatActivation)
                messageInputField.Select();
            eatActivation = false;
        }

        // deselect any UI element carefully
        // (it throws an error when doing it while clicking somewhere, so we have to
        //  double check)
        public static void DeselectCarefully()
        {
            if (!Input.GetMouseButton(0) &&
                !Input.GetMouseButton(1) &&
                !Input.GetMouseButton(2))
                EventSystem.current.SetSelectedGameObject(null);
        }
        
        public static bool AnyKeyDown(KeyCode[] keys)
        {
            return keys.Any(k => Input.GetKeyDown(k));
        }


        private void OnDestroy()
        {
            _shutdown();
        }

        public async void OnJoinChat(JoinOrCreateRoomMesg mesg)
        {
            //todo 进入场景 -- 游戏场景
            SceneManager.LoadScene(2);
            
            var localPlayer = mesg.UserName == LocalPlayer.PlayerName;

            var player = PlayerManager.Instance.CreatePlayer(localPlayer,mesg.UserName);

            ChatUI.alpha = 1;
            RoomUI.alpha = 0;
            if (!localPlayer)
            {
                _showMesg($"{mesg.UserName} Join Room.");
            }
        }

        public async void OnLeaveChat(string playerName)
        {
            //todo 进入场景 -- 主场景
            SceneManager.LoadScene(1);
            
            PlayerManager.Instance.DestroyPlayer(playerName);
            
            ChatUI.alpha = 0;
            
            if (playerName != LocalPlayer.PlayerName)
            {
                _showMesg($"{playerName} Leave Room.");
            }
        }

        public void OnSendMessage(SendMesgResponses mesg)
        {
            SendMessageEve?.Invoke(mesg);
            
            if (mesg.UserName == LocalPlayer.PlayerName)
            {
                _showMesg($"My: {mesg.Messg.Message}");
            }
            else
            {
                _showMesg($"{mesg.UserName}: {mesg.Messg.Message}");
            }
        }

        void _showMesg(string mesg)
        {
           LogComponentManager.Instance.ShowLog(mesg);
        }
        
        private async void _shutdown()
        {
            try
            {
                if (IsJoin)
                {
                    await _chatHub.LeaveChat();
                }
            }
            catch
            {
                // ignored
            }

            try
            {
                await _chatHub.DisposeAsync();
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        #region Call Server Function
        
        public async void JoinOrCreateRoom()
        {
            try
            {
                this._chatHub = StreamingHubClient.Connect<IChatHub, IChatHubReceiver>(MagicOnionManager.Instance.Channel, this);
                
                await _chatHub.JoinChat(new JoinOrCreateRoomMesg(LocalPlayer.RoomName,LocalPlayer.PlayerName));

                _waitDisConnect();
                
                IsJoin = true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private async void _waitDisConnect()
        {
            try
            {
                await _chatHub.WaitForDisconnect();
            }
            catch (Exception e)
            {
                _shutdown();
            }
        }

        public async void LeaveRoom()
        {
            try
            {
                await _chatHub.LeaveChat();

                IsJoin = false;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public async void SendMessage(SendMesg mesg)
        {
            await _chatHub.SendMessage(mesg);
        }

        #endregion
    }
}
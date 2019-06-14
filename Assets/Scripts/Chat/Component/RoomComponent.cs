//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-19:26
//Assembly-CSharp

using System;
using System.Threading.Tasks;
using MagicOnion.Client;
using MagicOnionTestService.LobbyMessageTest.Room;
using Shader.MessageObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Chat.Component
{
    public class RoomComponent : MonoBehaviour,IRoomHubReceiver
    {
        private IRoomHub _room;

        public bool IsJoin;
        
        [Header("Names")]
        public InputField RoomName;
        public InputField UserName;
        
        [Header("UI")]
        public CanvasGroup RoomUI;

        public CanvasGroup QuitButton;

        public static event Action JoinRoomEve;

        public static event Action LeaveRoomEve;
        
        public static RoomComponent Instance;
        
        void Awake()
        {
            Instance = this;
            QuitButton.alpha = 0;
        }

        private async void OnDestroy()
        {
            try
            {
                if (IsJoin)
                {
                    await _room.LeaveRoom();
                }
            
               
            }
            catch (Exception e)
            {
                // ignored
            }
            
            _dispose();
        }

        public void OnJoinRoom(string playerName)
        {
            LogComponentManager.Instance.ShowLog($"{playerName} Join Room.");
            RoomUI.alpha = 0;
            QuitButton.alpha = 1;
        }

        public async void OnLeaveRoom(string playerName)
        {
            LogComponentManager.Instance.ShowLog($"{playerName} Leave Room.");
            
            RoomUI.alpha = 1;
            QuitButton.alpha = 0;
        }

        private async void _dispose()
        {
            try
            {
                await _room.DisposeAsync();
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        #region Call Server
        
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

            JoinOrCreateRoom(roomName, userName);
        }

        public async void JoinOrCreateRoom(string roomName, string playerName)
        {
            
            if (MagicOnionManager.Instance.IsConnect)
            {
                _room = StreamingHubClient.Connect<IRoomHub, IRoomHubReceiver>(MagicOnionManager.Instance.Channel, this);

                try
                {
                    await _room.JoinOrCreateRoom(roomName,playerName);
                    IsJoin = true;
                    LocalPlayer.RoomName = roomName;
                    LocalPlayer.PlayerName = playerName;
            
                    JoinRoomEve?.Invoke();
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        public async void LeaveRoom()
        {
            try
            {
                LeaveRoomEve?.Invoke();

                await _room.LeaveRoom();
                
                LocalPlayer.RoomName = string.Empty;
                LocalPlayer.PlayerName = string.Empty;
                IsJoin = false;
                
                _dispose();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        #endregion
    }
}
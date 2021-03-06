//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-02:40
//Assembly-CSharp

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using MagicOnion;
using MagicOnion.Client;
using MagicOnionTestService.LobbyMessageTest;
using MagicOnionTestService.LobbyMessageTest.Room;
using UnityEngine;
using UnityEngine.UI;

namespace Chat.Component
{
    public class MagicOnionManager : MonoBehaviour
    {
        public Channel Channel { get; private set; } 
        public IGetRoomService RoomService;

        [NonSerialized] 
        public bool IsConnect;

        [Header("服务器连接ui")]
        public InputField IPInputField;
        public InputField PortInputField;

        
        [Header("UI块")]
        public CanvasGroup ConnectServerUI;

        public List<CanvasGroup> Others;

        [Header("房间名按钮模块")]
        public Button RoomNameButtonTemplate;
        public RectTransform RoomNameButtonTemplateParent;
        
        private List<Button> _roomNameButtons = new List<Button>();
        
        public static MagicOnionManager Instance;
        private void Awake()
        {
            Instance = this;
            ConnectServerUI.alpha = 1;
            Others.ForEach(x=>x.alpha = 0);
            RoomComponent.LeaveRoomEve += RefreshRoomList;
        }

        private async void OnDestroy()
        {
            try
            {
                await Channel.ShutdownAsync();
            }
            catch (Exception e)
            {
            }
        }

        public void Connect()
        {
            if (!IPInputField | !PortInputField)
            {
                return;    
            }
            
            if (string.IsNullOrWhiteSpace(IPInputField.text))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(PortInputField.text))
            {
                return;
            }

            Connect($"{IPInputField.text}:{PortInputField.text}");
        }

        public void Connect(string url)
        {
            Connect(url,ChannelCredentials.Insecure);
        }

        public async void Connect(string url, ChannelCredentials channelCredentials)
        {
            try
            {
                Channel = new Channel(url,channelCredentials);
                
                RoomService = MagicOnionClient.Create<IGetRoomService>(Channel);
                
                IsConnect = true;
                
                ConnectServerUI.alpha = 0;
                Others.ForEach(x=>x.alpha = 1);
            }
            catch (Exception e)
            {
                
            }

            RefreshRoomList();
        }

        public async void RefreshRoomList()
        {
            try
            {
                var roomList = await GetRoomList();

                for (var i = _roomNameButtons.Count; i < roomList.Length; i++)
                {
                    _roomNameButtons.Add(null);
                }

                for (var index = 0; index < roomList.Length; index++)
                {
                    var roomInfo = roomList[index];

                    var button = _roomNameButtons[index];

                    if (!button)
                    {
                        _roomNameButtons[index] = Instantiate(RoomNameButtonTemplate, RoomNameButtonTemplateParent);

                        button = _roomNameButtons[index];

                        button.gameObject.SetActive(true);
                    }

                    button.transform.GetChild(0).GetComponent<Text>().text = $"{roomInfo.RoomName}-{roomInfo.PlayerCount}";
                }
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// 获取房间列表,如果没有连接返回null
        /// </summary>
        /// <returns></returns>
        public async UnaryResult<RoomInfo[]> GetRoomList()
        {
            if (!IsConnect)
            {
                return null;
            }

            return await RoomService.GetRoomList();
        }
    }
}
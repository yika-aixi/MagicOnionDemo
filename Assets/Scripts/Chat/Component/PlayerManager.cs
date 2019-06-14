//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-03:30
//Assembly-CSharp

using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Chat.Component
{
    public class PlayerManager : MonoBehaviour
    {
        public GameObject PlayerPrefbs;

        [HideInInspector]
        public List<GameObject> Players;
        
        public static PlayerManager Instance;

        void Awake()
        {
            Players = new List<GameObject>();

            Instance = this;
        }

        public GameObject GetPlayer(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
            {
                return null;
            }
            
            return _getPlayer(playerName);
        }

        public GameObject CreatePlayer(bool localPlayer,string playerName)
        {
            var player = _getPlayer();
            player.SetActive(true);
            var con = player.GetComponent<ThirdPersonUserControl>();
            if (localPlayer)
            {
                player.tag = "Player";
                con.enabled = true;
            }
            else
            {
                con.enabled = false;
                player.tag = "OtherPlayer";
            }
            
            player.name = playerName;
            
            var chatComponent = _getOrAdd(player);

            chatComponent.Init(playerName);

            return player;
        }

        public void DestroyAllPlayer()
        {
            for (var i = 0; i < Players.Count; i++)
            {
                var player = Players[i];
                
                player.SetActive(false);
                
                var chatComponent = _getOrAdd(player);
            
                chatComponent.Close(); 
            }
        }

        /// <summary>
        /// 不考虑重名问题
        /// </summary>
        /// <param name="playerName"></param>
        public void DestroyPlayer(string playerName)
        {
            var player = _getPlayer(playerName);

            if (player)
            {
                player.SetActive(false);
                
                var chatComponent = _getOrAdd(player);
            
                chatComponent.Close(); 
            }
        }

        GameObject _getPlayer(string getPlayerName = null)
        {
            GameObject player = null;
            
            for (var index = 0; index < Players.Count; index++)
            {
                var go = Players[index];
                
                if (go.activeInHierarchy)
                {
                    if (getPlayerName == go.name)
                    {
                        return go;
                    }
                    
                    continue;
                }

                player = go;
            }

            if (!string.IsNullOrWhiteSpace(getPlayerName))
            {
                return player;
            }

            if (!player)
            {
                player = Instantiate(PlayerPrefbs,transform);
                Players.Add(player);
            }

            return player;
        }

        PlayerChatComponent _getOrAdd(GameObject player)
        {
            var chatComponent = player.GetComponent<PlayerChatComponent>();

            if (!chatComponent)
            {
                player.AddComponent<PlayerChatComponent>();
            }

            return chatComponent;
        }
        
    }
}
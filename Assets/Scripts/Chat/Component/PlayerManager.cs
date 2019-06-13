//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-03:30
//Assembly-CSharp

using System.Collections.Generic;
using UnityEngine;

namespace Chat.Component
{
    public class PlayerManager : MonoBehaviour
    {
        public GameObject PlayerPrefbs;

        public List<GameObject> Players;
        
        public static PlayerManager Instance;

        void Awake()
        {
            if (Players == null)
            {
                Players = new List<GameObject>();
            }
            else
            {
                DestroyAllPlayer();
            }
            
            Instance = this;
        }

        public GameObject CreatePlayer(string playerName)
        {
            var player = _getPlayer();
            
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
                var chatComponent = _getOrAdd(player);
            
                chatComponent.Close(); 
            }
        }

        GameObject _getPlayer(string name = null)
        {
            GameObject player = null;
            
            for (var index = 0; index < Players.Count; index++)
            {
                var go = Players[index];
                
                if (go.activeInHierarchy)
                {
                    if (name == go.name)
                    {
                        return go;
                    }
                    
                    continue;
                }

                player = go;
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                return player;
            }

            if (!player)
            {
                player = Instantiate(PlayerPrefbs,transform);
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
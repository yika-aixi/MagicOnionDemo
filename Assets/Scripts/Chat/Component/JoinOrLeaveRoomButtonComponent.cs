//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-15:00
//Assembly-CSharp

using UnityEngine;
using UnityEngine.UI;

namespace Chat.Component
{
    [RequireComponent(typeof(Button))]
    public class JoinOrLeaveRoomButtonComponent : MonoBehaviour
    {
        private Button _button;
        void Start()
        {
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener((async () =>
            {
                if (RoomComponent.Instance.IsJoin)
                {
                    RoomComponent.Instance.LeaveRoom();
                }
                else
                {
                    RoomComponent.Instance.JoinOrCreateRoom();
                }
            }));
        }
    }
}
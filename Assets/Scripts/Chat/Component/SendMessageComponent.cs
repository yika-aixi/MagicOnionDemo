//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-15:07
//Assembly-CSharp

using Shader.MessageObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Chat.Component
{
    [RequireComponent(typeof(Button))]
    public class SendMessageComponent : MonoBehaviour
    {
        public InputField MessageInputField;

        private Button _button;
        
        void Start()
        {
            _button = GetComponent<Button>();
                
            _button.onClick.AddListener(() =>
            {
                if (string.IsNullOrWhiteSpace(MessageInputField.text))
                {
                    return;
                }
                
                ChatComponent.Instance.SendMessage(new SendMesg(MessageInputField.text));
            });
        }
    }
}
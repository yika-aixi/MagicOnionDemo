//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-01:17
//Assembly-CSharp

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Chat.Component
{
    [RequireComponent(typeof(InputField))]
    public class TextSetInputFieldTextComponent : MonoBehaviour
    {
        private InputField _inputField;
        
        private void Awake()
        {
            _inputField = GetComponent<InputField>();
        }

        public void SetText(Text text)
        {
            _inputField.text = text.text.Split('-')[0];
        }
    }
}
//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-14:55
//Assembly-CSharp

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Chat.Component
{
    public class LogComponentManager : MonoBehaviour
    {
        public static LogComponentManager Instance;

        public Text LogText;
        
        void Awake()
        {
            Instance = this;
        }

        public void ShowLog(string log)
        {
            LogText.text += "\n" +
                             $"{DateTime.Now:yyyy-MM-dd;HH:mm}: {log}";
        }
    }
}
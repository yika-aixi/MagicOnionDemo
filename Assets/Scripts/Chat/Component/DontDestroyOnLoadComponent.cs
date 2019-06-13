//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-03:31
//Assembly-CSharp

using System;
using UnityEngine;

namespace Chat.Component
{
    public class DontDestroyOnLoadComponent : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
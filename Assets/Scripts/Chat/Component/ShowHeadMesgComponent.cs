//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-03:08
//Assembly-CSharp

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chat.Component
{
    public class ShowHeadMesgComponent : MonoBehaviour
    {
        private List<Text> _mesgTextList;

        public float TextShowTime = 2;
        private WaitForSeconds _wait;
        
        public RectTransform Parent;
            
        public Text MesgTextTemplate;

        private Camera _mainCamera;
        
        public static ShowHeadMesgComponent Instance;
        private void Awake()
        {
            Instance = this;
            _mainCamera = Camera.main;
            
            _wait = new WaitForSeconds(TextShowTime);
        }

        public void ShowMesg(GameObject go, string mesg)
        {
            if (!go || string.IsNullOrWhiteSpace(mesg))
            {
                return;
            }
            
            var text = _getText();
            
            text.enabled = true;
            
            text.text = mesg;
            
            text.rectTransform.anchoredPosition3D = _mainCamera.WorldToViewportPoint(go.transform.position);

            text.StartCoroutine(_close(text));
        }

        private IEnumerator _close(Text text)
        {
            yield return _wait;

            text.enabled = false;
        }

        Text _getText()
        {
            Text result = null;
            
            for (var index = 0; index < _mesgTextList.Count; index++)
            {
                var text = _mesgTextList[index];

                if (text.enabled)
                {
                    continue;
                }

                result = text;
            }

            if (!result)
            {
                var go = Instantiate(MesgTextTemplate.gameObject, Parent);

                result = go.GetComponent<Text>();
                
                _mesgTextList.Add(result);
            }

            return result;
        }
    }
}
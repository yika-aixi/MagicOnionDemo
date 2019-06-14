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
        private List<Text> _mesgTextList = new List<Text>();

        public float TextShowTime = 2;
        private WaitForSeconds _wait;
        
        public RectTransform Canvas;
        
        public RectTransform Parent;
        public CanvasGroup _group;
            
        public Text MesgTextTemplate;

        private Camera _mainCamera;
        
        private void Awake()
        {
            _wait = new WaitForSeconds(TextShowTime);
            _group = Parent.GetComponent<CanvasGroup>();
            _group.alpha = 0;
        }

        public void ShowMesg(string mesg)
        {
            var text = _getText();
            _group.alpha = 1;
            text.enabled = true;
            
            text.text = mesg;

            if (!_mainCamera)
            {
                _mainCamera = Camera.main;
            }
            
            text.rectTransform.anchoredPosition = WorldToCanvasPosition();

            text.StartCoroutine(_close(text));
        }
        
        private Vector2 WorldToCanvasPosition() 
        {
            //Vector position (percentage from 0 to 1) considering camera size.
            //For example (0,0) is lower left, middle is (0.5,0.5)
            Vector2 temp = _mainCamera.WorldToViewportPoint(transform.position);
 
            //Calculate position considering our percentage, using our canvas size
            //So if canvas size is (1100,500), and percentage is (0.5,0.5), current value will be (550,250)
            temp.x *= Canvas.sizeDelta.x;
            temp.y *= Canvas.sizeDelta.y;
 
            //The result is ready, but, this result is correct if canvas recttransform pivot is 0,0 - left lower corner.
            //But in reality its middle (0.5,0.5) by default, so we remove the amount considering cavnas rectransform pivot.
            //We could multiply with constant 0.5, but we will actually read the value, so if custom rect transform is passed(with custom pivot) , 
            //returned value will still be correct.
 
            temp.x -= Canvas.sizeDelta.x * Canvas.pivot.x;
            temp.y -= Canvas.sizeDelta.y * Canvas.pivot.y;
 
            return temp;
        }

        private IEnumerator _close(Text text)
        {
            yield return _wait;

            text.enabled = false;
            _group.alpha = 0;
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
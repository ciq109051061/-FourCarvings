using UnityEngine;
using UnityEngine.EventSystems;

namespace FourCarvings
{
    /// <summary>
    /// 開始介面，按鈕點擊事件建立
    /// </summary>
    public class StartButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {
        //定義事件
        public static System.Action<int> OnDown;
        public static System.Action<int> OnUp;
        //存取按鈕編號
        int startButtonID;

        private void Start()
        {
            startButtonID = transform.GetSiblingIndex();    //取得按鈕編號
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (OnDown != null)
            {
                OnDown(startButtonID);
                Debug.Log($"按下按鈕名:{gameObject.name}，按鈕序號:{startButtonID}");
            }
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (OnUp != null)
            {
                OnUp(startButtonID);
                Debug.Log($"放開按鈕名:{gameObject.name}，按鈕序號:{startButtonID}");
            }
        }
    }
}

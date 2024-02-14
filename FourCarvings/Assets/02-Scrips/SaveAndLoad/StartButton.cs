using UnityEngine;
using UnityEngine.EventSystems;

namespace FourCarvings
{
    /// <summary>
    /// «ö¶s¹Ï¤ù¤Á´«
    /// </summary>
    public class StartButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {
       

        public static System.Action<int> OnDown;
        public static System.Action<int> OnUp;
        int startButtonID;

        private void Start()
        {
            startButtonID = transform.GetSiblingIndex();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (OnDown != null)
            {
                OnDown(startButtonID);
                Debug.Log($"«ö¤U«ö¶s¦W:{gameObject.name}¡A«ö¶s§Ç¸¹:{startButtonID}");
            }
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (OnUp != null)
            {
                OnUp(startButtonID);
            }
        }
    }
}

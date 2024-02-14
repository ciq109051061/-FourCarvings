using UnityEngine;
using UnityEngine.EventSystems;

namespace FourCarvings
{
    /// <summary>
    /// 卷軸面板開關
    /// </summary>
    public class ReelControl : MonoBehaviour,IPointerClickHandler
    {
        //卷軸狀態
        private bool reelState;

        public Animator reelAnimator;

        private void Start()
        {
            //一開始卷軸關閉
            reelState = false;

            reelAnimator = GetComponent<Animator>();

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                reelState = !reelState;
                if (reelState)
                {
                    print("面板是開的");

                    
                    reelAnimator.Play("reelOpen");
                    
                }
                else if (reelState==false)
                {
                    print("面板是關的");
                    reelAnimator.Play("reelClose");
                }
            }
        }

        
    }
}

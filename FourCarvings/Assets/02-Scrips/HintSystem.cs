using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FourCarvings
{
    public class HintSystem : MonoBehaviour
    {
        //public static HintSystem Instance;

        #region 資料區

        [SerializeField, Header("提示間隔"), Range(0, 0.5f)]
         float hintIntervalTime = 0.1f;
        /// <summary>
        /// 提示UI介面
        /// </summary>
        public CanvasGroup groupHint;
        /// <summary>
        /// 提示內容
        /// </summary>
        public TextMeshProUGUI hintContent_text;

        

        [SerializeField,Header("提示對話資料")]
        private HintData hint;

        private WaitForSeconds hintInterval => new WaitForSeconds(hintIntervalTime);
        private bool isFading = false;
        #endregion


        private void Start()
        {
            //清空文字內容
            hintContent_text.text = "";

            
        }


        #region 偵測區
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //如果偵測到玩家，提示框彈出
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("偵測提示成功");
                StartCoroutine(FadeGroup());
                StartCoroutine(ShowHint(hint));

            }
            
        }
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            //偵測到玩家離開，提示框消失
            if(collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(FadeGroup(false));
                
            }
        }

        #endregion

        #region 協同程式區

        /// <summary>
        /// 提示逐字出現
        /// 使用kid老師方法
        /// </summary>
        public IEnumerator ShowHint(HintData hintData)
        {
            Debug.Log("已呼叫ShowHint");
            //為什麼要清空第二次?不解
            hintContent_text.text = "";

            for (int i = 0; i < hintData.hintContent.Length; i++)
            {
                string hintText = hintData.hintContent[i];
                hintContent_text.text += hintText;

                yield return hintInterval;
            }
        }
        /// <summary>
        /// 提示介面淡入淡出
        /// </summary>
        public IEnumerator FadeGroup(bool fadeIn = true)
        {
            if (isFading)
            {
                yield break; // 如果正在淡入淡出過程中，不重複觸發
            }
            Debug.Log("已呼叫FadeGroup");
            isFading = true;
            float increase = fadeIn ? +0.1f : -0.1f;
            for (int i = 0; i < 10; i++)
            {
                groupHint.alpha += increase;
                yield return new WaitForSeconds(0.04f);
            }
            isFading = false;
        }

        #endregion

    }


}

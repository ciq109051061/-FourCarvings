using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FourCarvings
{
    /// <summary>
    /// 道具效果
    /// </summary>
    public class ItemEffect : MonoBehaviour
    {
        public static ItemEffect Instance;

        private bool sealIsClose = false;

        private void Awake()
        {
            #region 單例
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != null)
            {
                Destroy(gameObject);
            }
            #endregion
        }

        //物品效果ID對應
        public void ExecuteEffect(int item_ID)
        {

            switch (item_ID)
            {

                case 0:
                    //金幣
                    Debug.Log("金幣使用成功");
                    break;
                case 1:
                    //狐狸令牌???
                    Debug.Log("令牌使用成功");
                    break;
                case 2:
                    //通行證
                    Debug.Log("通行證使用成功");
                    GameObject.Find("封印處").gameObject.SetActive(false);
                    //GameObject.Find("封印燈光01").gameObject.SetActive(false);
                    //GameObject.Find("封印燈光02").gameObject.SetActive(false);
                    sealIsClose = true;
                    Debug.Log($"是否準備關閉封印門{sealIsClose}");
                    StartCoroutine(SealTurnOff());
                    Debug.Log("封印處刪除成功");
                    break;
            }


        }

        public IEnumerator SealTurnOff()
        {
            UnityEngine.Color originalColor01 = new UnityEngine.Color(0.5490f, 0.9137f, 0.6342f, 0.2980f);
            UnityEngine.Color originalColor02 = new UnityEngine.Color(0.6701f, 0.9811f, 0.8733f, 0.3843f);
            UnityEngine.Color targetColor01 = new UnityEngine.Color(0.5490f, 0.9137f, 0.6342f, 0f);
            UnityEngine.Color targetColor02 = new UnityEngine.Color(0.6701f, 0.9811f, 0.8733f, 0.6f);
            UnityEngine.Color finalColor = new UnityEngine.Color(0.6701f, 0.9811f, 0.8733f, 0f);
            if (sealIsClose)
            {
                Light2D light01 = GameObject.Find("封印燈光01")?.GetComponent<Light2D>();
                Light2D light02 = GameObject.Find("封印燈光02")?.GetComponent<Light2D>();

                if (light01 != null && light02 != null)
                {
                    // 封印門01過渡效果
                    float elapsedTime = 0f;
                    while (elapsedTime < 0.2f)
                    {
                        float t = elapsedTime / 0.5f;
                        light01.color = UnityEngine.Color.Lerp(originalColor01, targetColor01, t);
                        light02.color = UnityEngine.Color.Lerp(originalColor02, targetColor02, t);
                        elapsedTime += Time.deltaTime;
                        Debug.Log($"封印燈光01檢測{light01.color}");
                        Debug.Log($"封印燈光02檢測{light02.color}");
                        yield return null;
                    }

                    // 封印門02過渡效果
                    elapsedTime = 0f;
                    while (elapsedTime < 0.2f)
                    {
                        float t = elapsedTime / 0.5f;
                        light02.color = UnityEngine.Color.Lerp(targetColor02, finalColor, t);
                        Debug.Log($"封印燈光02二次檢測{light02.color}");
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                }
                else
                {
                    Debug.LogError("封印燈光物件未找到");
                }

                sealIsClose = false;
                Debug.Log($"封印門是否關閉{sealIsClose}");
            }
        }
    }
}

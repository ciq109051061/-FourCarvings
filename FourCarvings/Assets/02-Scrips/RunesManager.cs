using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FourCarvings
{

    /// <summary>
    /// 符文管理
    /// </summary>
    public class RunesManager : MonoBehaviour
    {
        #region 變數

        public GameObject[] runes = new GameObject[5];

        public HintSystem hintSystem;

        public HintData hintData_order;

        public float runesInterval = 0.2f;

        [HideInInspector]
        public bool allRunesClick = false;
        public bool isShowRunes;

        private int currentIndex = 0; // 當前需要點擊的物品索引

        #endregion

        public IEnumerator OrderRunes()
        {
            isShowRunes = true;
            Color originalColor = new Color(1,1,1,0);
            Color targetColor = new Color(0.2196f, 0.6901f, 1f,1f);
            Color finalColor = new Color(0, 0, 0, 1);

            for (int i = 0; i < runes.Length; i++)
            {

                               
                // 點亮符文
                runes[i].GetComponent<Light2D>().color = targetColor;
                runes[i].GetComponent<SpriteRenderer>().color = originalColor;
                //ebug.Log(runes[i].gameObject.GetComponent<Light2D>().color);

                // 等待一秒，使用Color.Lerp實現平滑過渡
                float elapsedTime = 0f;
                while (elapsedTime < 0.7f)
                {
                    float t = elapsedTime / 0.5f; // 正規化時間
                    runes[i].GetComponent<Light2D>().color = Color.Lerp(targetColor, originalColor, t);
                    runes[i].GetComponent<SpriteRenderer>().color = Color.Lerp(originalColor,finalColor, t);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }


                //runes[i].GetComponent<SpriteRenderer>().color = finalColor;
                // 等待一秒，用於符文之間的間隔
                yield return new WaitForSeconds(0.4f);
                
                
            }


            isShowRunes = false;
            
        }

        public IEnumerator RunesLight(int index)
        {
            Debug.Log("成功點亮");
            Color originalColor = new Color(1, 1, 1, 0);
            Color targetColor = new Color(0.858f, 0.409f, 1f, 1f);
            //Color finalColor = new Color(0, 0, 0, 1);

            // 點亮符文
            runes[index].GetComponent<Light2D>().color = targetColor;
            runes[index].GetComponent<SpriteRenderer>().color = originalColor;
            runes[index].GetComponent<Light2D>().intensity = 2;
            //ebug.Log(runes[i].gameObject.GetComponent<Light2D>().color);

            for (int i = 0; i < 10; i++)
            {
                float light = 0.5f;
                runes[index].GetComponent<Light2D>().intensity -= light;
                yield return new WaitForSeconds(0.04f);
            }

            yield return new WaitForSeconds(0.5f);

            runes[index].SetActive(false);
        }


        public IEnumerator OrderClickRunes(int id)
        {
            
            if (currentIndex != id)
            {
                
                StartCoroutine(hintSystem.ShowHint(hintData_order));
                StartCoroutine(hintSystem.FadeGroup());
                yield return new WaitForSeconds(1f);
                StartCoroutine(hintSystem.FadeGroup(false));
                Debug.Log($"點擊第 {currentIndex} 個物品，但不符合ID{id}");
            }

            if (currentIndex==id)
            {
                if (currentIndex < runes.Length)
                {
                    StartCoroutine(RunesLight(currentIndex));
                    //runes[currentIndex].SetActive(false);
                    
                    Debug.Log($"點擊第 {currentIndex} 個物品");

                    if (currentIndex==4)
                    {
                        Debug.Log("點擊完最後一個符文");
                        allRunesClick = true;
                        AudioManager.PlayBreakAudio();
                    }
                }
                else
                {
                    Debug.Log("所有物品都被點擊完畢");
                    
                }
                currentIndex++;
            }

            if (allRunesClick==true)
            {
                Debug.Log("所有符文都被點擊完畢");
            }
            


        }
    }
}

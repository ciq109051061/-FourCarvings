using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FourCarvings
{

    public class GPM_Level_0 : MonoBehaviour
    {
        #region 變數

        public SpriteRenderer worldLight;
        public Animator playerAnimator;
        public Rigidbody2D playerRd;
        public Transform target; // 主角的位置

        [Header("提示")]
        public HintData hintData_warn;
        public HintData hintData_forRunes;

        [Header("腳本")]
        public CameraShake cameraShake;      
        public RunesManager runesManager;        
        public HintSystem hintSystem;        
        public ScreenFade ScreenFade;


        private bool hasStartedOrderRunes = false;  //符文是否全部按完
        private bool onCollison = false;            //守護者是否接觸地面
        [HideInInspector]
        public bool okClick = false;                //是否可點擊符文

        #endregion

        private void Start()
        {
            cameraShake.enabled = true;     //開啟相機震動
            AudioManager.PlayShakeAudio();
        }
       
        private void Update()
        {
            //震動結束，按順序點亮符文
            if (cameraShake.isShake == false && hasStartedOrderRunes == false)
            {
                AudioManager.StopShakeAudio();
                AudioManager.PlayNoiseAudio();
                StartCoroutine(StartOrderRunes());      
                hasStartedOrderRunes = true;
            }

            //震動結束且符文亮完，可以點擊符文
            if (cameraShake.isShake == true && hasStartedOrderRunes == true)
            {
                okClick = true;
            }

            //符文收集完畢，能量罩消失
            if (runesManager.allRunesClick)
            {
                
                playerAnimator.SetBool("OpenNow", true);
                playerRd.gravityScale = 0.3f;
                if (onCollison==true)
                {
                    StartCoroutine(ScreenFade.FadeOut());
                    SceneManager.LoadScene(2);
                }

            }

            
        }

         private IEnumerator StartOrderRunes()
        {
            yield return StartCoroutine(runesManager.OrderRunes());
            Debug.Log(runesManager.isShowRunes);

            if (runesManager.isShowRunes == false)
            {
                
                StartCoroutine(hintSystem.ShowHint(hintData_warn));
                StartCoroutine(hintSystem.FadeGroup());
                yield return new WaitForSeconds(3f);

                StartCoroutine(hintSystem.ShowHint(hintData_forRunes));
                yield return new WaitForSeconds(3f);
                runesManager.isShowRunes = true;
            }

            if (runesManager.isShowRunes == true)
            {
                StartCoroutine(hintSystem.FadeGroup(false));
                StopCoroutine(hintSystem.ShowHint(hintData_warn));
                
            }
            cameraShake.isShake = true;
            Debug.Log($"已關閉{cameraShake.isShake}");
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag=="Player")
            {
                Debug.Log("碰撞成功");
                onCollison = true;
            }
        }
    }
}
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

namespace FourCarvings
{

    public class GPM_Level_2 : MonoBehaviour
    {
        private enum GameState
        {
            StartIext,
            KnockText,
            BearMove,
            SerchText,
            TimeOver,
            AllDown
        }
        private GameState currentState = GameState.StartIext;
        public static bool StartTextDone = false;
        public static bool KnockTextDone = false;
        public static bool BearMoveDone = false;
        public static bool SerchOrNot;
        public static bool SerchOrNotTextDonw = false;
        private bool timeOverDone = false;
        public static bool timeOverTextDone = false;
        public static bool move01 = true;
        public static bool move02 = false;
        public static bool move03 = false;
        public static bool BearMoveFinallyDone = false;
        private Vector2 destination01, destination02, destination03, destination04;
        private float explorationTimeLimit = 300.0f;
        private bool isSerching = false;
        private bool bearGoBeck = true;
        public BoxCollider2D[] textCollider = new BoxCollider2D[4];
        public BearControl bearControl;
        //public GameObject mainCharater;
        public GameObject bear;
        //private Vector2 main;

        public TextMeshProUGUI explorationTimeText;

        private void Start()
        {
            //DontDestroyOnLoad(gameObject);
            destination01 = new Vector2(-3.71f, 6.27f);
            destination02 = new Vector2(-1.72f, 6.54f);
            destination03 = new Vector2(-1.72f, 5.35f);
            destination04 = new Vector2(-3.32f, 9.1f);
            //main = new Vector2(10.62f, 0.9f);
            textCollider[0].enabled = false;
            textCollider[1].enabled = false;
            textCollider[2].enabled = false;
            textCollider[3].enabled = false;
            explorationTimeText.enabled = false;
        }

        private void Update()
        {
            switch (currentState)
            {
                case GameState.StartIext:
                    Debug.Log("流程到StartIext階段");
                    textCollider[0].enabled = true;
                    if (StartTextDone)                    
                        currentState = GameState.KnockText;
                    break;
                case GameState.KnockText:
                    Debug.Log("流程到KnockText階段");
                    
                    textCollider[1].enabled = true;
                    if (KnockTextDone)
                        currentState = GameState.BearMove;
                    break;
                case GameState.BearMove:
                    Debug.Log("流程到BearMove階段");
                    
                        bearControl.BearMovement(destination01);
                        //StartCoroutine(SmoothMove(destination01));
                        bearControl.MoveForwardAnim();
     
                    if (move02==true)
                    {
                        Debug.Log("準備轉彎");
                        //bearControl.BearMovement(destination02);
                        StartCoroutine(SmoothMove(destination02));
                        //bearControl.MoveRighrAnim();
                        
                        //bearControl.MoveDown();
                    }
                    if (move03)
                    {
                        //開門動畫&音效
                        StartCoroutine(SmoothMove(destination03));
                        //bearControl.MoveForwardAnim();
                        Debug.Log("準備出門");
                        
                    }
                    
                    if (BearMoveDone)
                        currentState = GameState.SerchText;
                    break;
                case GameState.SerchText:
                    Debug.Log("流程到SerchTex階段");
                    textCollider[2].enabled = true;
                    Debug.Log($"SerchTex為{SerchOrNot}");
                    if (SerchOrNot == true && SerchOrNotTextDonw==true)
                    {
                        textCollider[2].enabled = false;
                        Debug.Log("進入探索");
                        StartCoroutine(StartExploration());
                        if (timeOverDone == true)
                            currentState = GameState.TimeOver;
                        
                    }
                        
                    if(SerchOrNot==false && SerchOrNotTextDonw == true)
                    {
                        //等待幾秒
                        currentState = GameState.TimeOver;
                    }
                    break;
                case GameState.TimeOver:
                    Debug.Log("流程到TimeOver階段");
                    bear.SetActive(true);
                    if (bearGoBeck)
                    {
                        StartCoroutine(SmoothMove(destination03));
                        StartCoroutine(SmoothMove(destination02));
                        //StartCoroutine(SmoothMove(destination01));
                        StartCoroutine(SmoothMove(destination04));
                        bearGoBeck = false;
                        //BearMoveFinallyDone = true;
                    }
                    
                    if (BearMoveFinallyDone)
                    {
                        textCollider[3].enabled = true;
                    }
                    
                    if (timeOverTextDone == true)
                        currentState = GameState.AllDown;
                    break;
                case GameState.AllDown:
                    Debug.Log("流程到AllDown階段");
                    textCollider[3].enabled = false;
                    SceneManager.LoadScene(4);
                    //mainCharater.transform.position = main;
                    break;
                default:                   
                    break;
            }
        }

        private IEnumerator KnockCoroutine()
        {
            Debug.Log("协程开始");

            AudioManager.PlayKnockAudio();

            // 等待敲门音频播放完毕
            yield return new WaitForSeconds(3); // 使用实际的敲门音频片段持续时间替换 YourKnockAudioDuration

            Debug.Log("敲门音频播放完毕");

            // 在敲门音频播放完毕后继续执行代码
            KnockTextDone = true;
        }
        IEnumerator YourCoroutine()
        {
            Debug.Log("Coroutine started");

            yield return StartCoroutine(KnockCoroutine());
        }

        public IEnumerator WaitForSeconds(float seconds)
        {
            float elapsedTime = 0f;

            while (elapsedTime < seconds)
            {
                // 在這裡可以顯示剩餘的等待時間
                Debug.Log($"Remaining time: {seconds - elapsedTime}");

                elapsedTime += Time.deltaTime;
                yield return null; // 讓 Unity 等待下一個幀
            }
        }
        

        public IEnumerator StartExploration()
        {
            isSerching = true;
            explorationTimeText.enabled = true;
            yield return StartCoroutine(CountdownTimer(explorationTimeLimit));
        }

        public IEnumerator CountdownTimer(float _timelimit)
        {
            float currentTime = _timelimit;
            while (currentTime>0f)
            {
                currentTime -= Time.deltaTime;
                string formattedTime = string.Format("{0:00}:{1:00}", Mathf.Floor(currentTime / 60), Mathf.Floor(currentTime % 60));
                explorationTimeText.text = formattedTime;
                Debug.Log($"現在時間:{currentTime}");
                yield return null;
            }
            
                OnExplorationTimeEnd();

        }

        public void OnExplorationTimeEnd()
        {
            isSerching = false;
            timeOverDone = true;
            explorationTimeText.enabled = false;
            Debug.Log("探索時間結束了");
            
        }

        private IEnumerator SmoothMove(Vector2 targetPos)
        {
            float moveSpeed = 3.0f;
            float distanceThreshold = 0.01f;

            while (Vector2.Distance(bearControl.transform.position, targetPos) > distanceThreshold)
            {
                // 计算移动方向和距离
                Vector2 direction = (targetPos - (Vector2)bearControl.transform.position).normalized;
                float distance = moveSpeed * Time.deltaTime;

                // 计算下一个位置
                Vector2 nextPosition = (Vector2)bearControl.transform.position + direction * distance;

                // 更新位置
                bearControl.BearMovement(nextPosition);

                yield return null;
            }

            // 确保最终位置正确
            bearControl.BearMovement(targetPos);
        }

        
        

    }
}


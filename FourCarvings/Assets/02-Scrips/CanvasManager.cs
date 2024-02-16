using UnityEngine;
using UnityEngine.SceneManagement;

namespace FourCarvings
{

    public class CanvasManager : MonoBehaviour
    {
        public Canvas[] canvas;
        public SaveLoadManager manager;
        
        private bool saveLoad_active;
        private bool playerBag_active;
        private bool dialogue_active;
        //private bool main_active;

        private void Start()
        {
            //canvas[0]=存讀檔介面
            //canvas[1]=背包介面
            //canvas[2]=對話介面
            //canvas[3]=主介面
            canvas[0] = GameObject.Find("存讀檔介面").GetComponent<Canvas>();
            canvas[1] = GameObject.Find("背包介面").GetComponent<Canvas>();
            canvas[2] = GameObject.Find("對話介面").GetComponent<Canvas>();
            canvas[3] = GameObject.Find("主介面").GetComponent<Canvas>();

            saveLoad_active = false;
        }

        private void Update()
        {
            ShowCanvas();
        }

        private void ShowCanvas()
        {
            //開啟存讀面板，面板層級為3，關閉則為1。
            if (saveLoad_active)
            {
                canvas[0].GetComponent<Canvas>().sortingOrder = 3;
                Debug.Log($"存讀面板開啟，層級為{canvas[0].GetComponent<Canvas>().sortingOrder}");
                MainPanel(false);
            }
            else
            {
                canvas[0].GetComponent<Canvas>().sortingOrder = 1;
                Debug.Log($"存讀面板關閉，層級為{canvas[0].GetComponent<Canvas>().sortingOrder}");
            }

            if (playerBag_active)
            {
                canvas[1].GetComponent<Canvas>().sortingOrder = 3;
                Debug.Log($"背包面板開啟，層級為{canvas[1].GetComponent<Canvas>().sortingOrder}");
                MainPanel(false);
            }
            else
            {
                canvas[1].GetComponent<Canvas>().sortingOrder = 1;
                Debug.Log($"背包面板關閉，層級為{canvas[1].GetComponent<Canvas>().sortingOrder}");
            }
            if (dialogue_active)
            {
                canvas[2].GetComponent<Canvas>().sortingOrder = 3;
                Debug.Log($"對話面板開啟，層級為{canvas[2].GetComponent<Canvas>().sortingOrder}");
                MainPanel(false);
            }
            else
            {
                canvas[2].GetComponent<Canvas>().sortingOrder = 1;
                Debug.Log($"對話面板關閉，層級為{canvas[2].GetComponent<Canvas>().sortingOrder}");
            }

            if (saveLoad_active==false && playerBag_active==false&& dialogue_active==false)
            {
                MainPanel(true);
            }
        }

        public void BackToStart()
        {
            SceneManager.LoadScene(0);
        }
        //點擊存讀面板關閉按鈕關閉面板
        public void CloseSaveOrLoad()
        {
            canvas[0].gameObject.GetComponent<CanvasGroup>().alpha = 0;
            saveLoad_active = false;
        }

        //點擊存檔按鈕開啟存檔面板
        public void SavePanel()
        {
            saveLoad_active = true;
            manager.isLoad = false;
            canvas[0].gameObject.GetComponent<CanvasGroup>().alpha = 1;
            
        }
        //點擊讀檔按鈕開啟讀檔面板
        public void LoadPanel()
        {
            saveLoad_active = true;
            manager.isLoad = true;
            canvas[0].gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }
        //點擊背包按鈕開起背包
        public void PlayerBagPanel()
        {
            playerBag_active = true;
            canvas[1].gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }
        //點擊背包面板關閉按鈕關閉面板
        public void ClosePlayerBag()
        {
            playerBag_active = false;
            canvas[1].gameObject.GetComponent<CanvasGroup>().alpha = 0;
        }
        //非手動開啟面板顯示判斷
        public void ActiveOrNot()
        {
            if (canvas[2].GetComponent<CanvasGroup>().alpha == 1)
            {
                dialogue_active = true;
            }
            if (canvas[2].GetComponent<CanvasGroup>().alpha == 0)
            {
                dialogue_active = false;
            }
        }

        public void MainPanel(bool isActive)
        {
            if (isActive)
            {
                canvas[3].GetComponent<Canvas>().gameObject.SetActive(true);
            }
            else
            {
                canvas[3].GetComponent<Canvas>().gameObject.SetActive(false);
            }
        }

    }
}
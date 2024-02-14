using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.EventSystems;

namespace FourCarvings
{
    /// <summary>
    /// 開始介面
    /// </summary>
    public class StartGameUI : MonoBehaviour
    {
        public SaveLoadManager manager;
        //public GameObject saveloadPanel;

        /*
        [Header("開始面板按鈕")]
        public Button NewButton; //新遊戲
        public Button ContinueButton; //繼續遊戲
        public Button LoadButton; //讀取遊戲
        public Button ExitButton; //離開遊戲
        */

        public Button[] startScenceButton = new Button[4];
        public Sprite[] originalImage = new Sprite[4];
        public Sprite[] changeImage = new Sprite[4];

        private void Awake()
        {
            manager.InitializeInfo();

            #region 監聽
            StartButton.OnUp += theMouseUp;
            StartButton.OnDown += theMouseDown;
            startScenceButton[0].onClick.AddListener(NewGame);
            startScenceButton[1].onClick.AddListener(ContinueGame);
            startScenceButton[2].onClick.AddListener(LoadGame);
            startScenceButton[3].onClick.AddListener(ExitGame);
            #endregion
        }
        private void Start()
        {
            //saveloadPanel.GetComponent<CanvasGroup>().alpha = 0;
            manager.SaveCanvas(false);
            for (int i = 0; i < startScenceButton.Length; i++)
            {
                startScenceButton[i].image.sprite = originalImage[i];
            }
        }

        private void OnDestroy()
        {
            StartButton.OnUp -= theMouseUp;
            StartButton.OnDown -= theMouseDown;
        }

        void NewGame()
        {
            SceneManager.LoadScene(1);      //跳轉到序章
            manager.InitializeInfo();       //遊戲資料初始化
        }

        void ContinueGame()
        {
            CheckForAutoFile(Application.persistentDataPath, 0);
        }

        //讀檔-調用讀檔面板
        void LoadGame()
        {
            
            manager.SaveCanvas(true);
            manager.isLoad = true;
        }

        //退出遊戲
        void ExitGame()
        {
            manager.QuitGame();
        }

        //查詢自動檔
        private void CheckForAutoFile(string filepath, int id)
        {
            if (Directory.Exists(filepath))
            {
                string[] files = Directory.GetFiles(filepath);
                foreach (string filePath in files)
                {
                    if (Path.GetExtension(filePath) == ".auto" && id == 0)
                    {
                        manager.ForLoad(filePath);
                        Debug.Log($"成功繼續遊戲，檔案:{filePath}");
                    }
                    break;
                }
            }
            else
            {
                Debug.Log("找不到自動檔，開啟新遊戲");
                NewGame();
            }
        }

        public void theMouseDown(int id)
        {
            startScenceButton[id].image.sprite = changeImage[id];
        }

        public void theMouseUp(int id)
        {
            startScenceButton[id].image.sprite = originalImage[id];
        }


    }

}
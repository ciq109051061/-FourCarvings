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
        //引用腳本
        public SaveLoadManager manager;
        
        //物件-按鈕:開始0、繼續1、讀檔2、離開3
        public Button[] startScenceButton = new Button[4];
        //物件-替換圖片:原圖、替換圖片
        public Sprite[] originalImage = new Sprite[4];
        public Sprite[] changeImage = new Sprite[4];

        private void Awake()
        {
 
            //事件訂閱
            StartButton.OnUp += theMouseUp;
            StartButton.OnDown += theMouseDown;

            #region 按鈕監聽
            startScenceButton[0].onClick.AddListener(NewGame);
            startScenceButton[1].onClick.AddListener(ContinueGame);
            startScenceButton[2].onClick.AddListener(LoadGame);
            startScenceButton[3].onClick.AddListener(ExitGame);
            #endregion
        }
        private void Start()
        {
            
            manager.ShowSaveCanvas(false);  //存讀檔面板不顯示
            manager.InitializeInfo();   //遊戲初始化
            //指定初始圖片
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

        #region 點擊按鈕切換圖片

        public void theMouseDown(int id)
        {
            startScenceButton[id].image.sprite = changeImage[id];
        }

        public void theMouseUp(int id)
        {
            startScenceButton[id].image.sprite = originalImage[id];
        }
        #endregion

        void NewGame()
        {
            Debug.Log("開始新遊戲");
            SceneManager.LoadScene(1);      //跳轉到序章
            manager.InitializeInfo();       //遊戲資料初始化
        }

        //讀取自動存檔
        void ContinueGame()
        {
            Debug.Log("繼續遊戲");
            CheckForAutoFile(Application.persistentDataPath, 0);    
        }

        //讀檔-調用讀檔面板
        void LoadGame()
        {
            Debug.Log("讀取遊戲");
            manager.ShowSaveCanvas(true);   //存讀檔面板顯示
            manager.isLoad = true;
            manager.ChangeCanvasImage(true);
            Debug.Log($"isload為{manager.isLoad}");
            Debug.Log($"存讀面板的圖片為{manager.saveLoadPanel.sprite.name}");
        }

        //退出遊戲
        void ExitGame()
        {
            Debug.Log("離開遊戲");
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

       


    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

namespace FourCarvings
{
    /// <summary>
    /// 開始介面
    /// </summary>
    public class StartGameUI : MonoBehaviour
    {
        public SaveLoadManager manager;

        public GameObject saveloadPanel;

        [Header("開始面板按鈕")]
        public Button NewButton; //新遊戲
        public Button ContinueButton; //繼續遊戲
        public Button LoadButton; //讀取遊戲
        public Button ExitButton; //離開遊戲


        private void Awake()
        {
            manager.InitializeInfo();
            NewButton.onClick.AddListener(NewGame);
            ContinueButton.onClick.AddListener(ContinueGame);
            LoadButton.onClick.AddListener(LoadGame);
            ExitButton.onClick.AddListener(ExitGame);
        }
        private void Start()
        {
            saveloadPanel.SetActive(false);
        }

        void NewGame()
        {
            SceneManager.LoadScene(1);
            manager.InitializeInfo();
            //TO-DO 初始化資料方法
        }

        void ContinueGame()
        {
            CheckForAutoFile(Application.persistentDataPath, 0);
            //TO-DO 讀取自動檔，無則NewGame()
        }

        void LoadGame()
        {
            saveloadPanel.SetActive(true);
            manager.isLoad = true;
            //TO-DO 調用讀檔面板
        }

        void ExitGame()
        {

            manager.QuitGame();

        }

        private void CheckForAutoFile(string filepath, int id)
        {
            // 確保路徑存在
            if (Directory.Exists(filepath))
            {
                // 取得目錄中的所有檔案
                string[] files = Directory.GetFiles(filepath);

                // 遍歷所有檔案
                foreach (string filePath in files)
                {
                    // 檢查副檔名是否為 ".save"
                    if (Path.GetExtension(filePath) == ".auto" && id == 0)
                    {
                        // 在這裡處理找到 ".save" 檔案的邏輯
                        manager.ForLoad(filePath);
                        break;
                    }

                    
                }
            }
            else
            {
                Debug.LogWarning("指定的路徑不存在: " + filepath);
            }
        }
    }

}
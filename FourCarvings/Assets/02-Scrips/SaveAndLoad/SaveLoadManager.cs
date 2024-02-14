using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace FourCarvings
{

    public class SaveLoadManager : MonoBehaviour
    {

        #region 變數
        public GameObject player;

        [Header("腳本")]
        public SaveData autoSaveData; //自動檔資料更新器
        public SaveData receiveSaveData;
        public SaveLoadSlotButton saveLoadSlotButton;

        [Header("UI")]
        public Image saveLoadPanel;  //存讀檔面板 (用於切換面板圖片)
        public Sprite savePanel;     //存檔面板圖片
        public Sprite loadPanel;     //讀檔面板圖片
        public CanvasGroup saveloadCanvas;

        [Header("按鈕")]
        public Button isSaveButton;
        public Button isLoadButton;

        [HideInInspector]
        public bool isSave = false;
        [HideInInspector]
        public bool isLoad = false;
        [HideInInspector]
        public string savePath;          //存檔路徑
        public static string shotPath;   //截圖路徑
        string[] fileExtension = {".auto", ".save01", ".save02" }; //副檔名(用於判斷存檔欄)

        #endregion

        private void Awake()
        {
            //指定路徑
            savePath = Application.persistentDataPath; 
            shotPath = $"{Application.persistentDataPath}/shot";
            //建立目錄
            if (!Directory.Exists(savePath))
            {
               Directory.CreateDirectory(savePath); 
            }
        }

        private void Start()
        {
            
            //資料初始化
            InitializeInfo();

            //按鈕監聽
            isSaveButton.onClick.AddListener(() => SaveOrLoad(true));
            isLoadButton.onClick.AddListener(() => SaveOrLoad(false));

        }

        #region 面板及模式判斷

        /// <summary>
        /// 判斷存檔or讀檔
        /// </summary>
        /// <param name="OnSave">外部接收bool值</param>
        public void SaveOrLoad(bool OnSave)
        {
            //判斷存讀檔
            isSave = OnSave;
            isLoad = !OnSave;
            //切換面板
            
            if (isLoad == true)
            {
                Debug.Log("目前為讀檔模式");
                ChangeCanvasImage(isLoad);
            }
            else
            {
                Debug.Log("目前為存檔模式");
                ChangeCanvasImage(isLoad);
            }
            
            //開啟面板
            ShowSaveCanvas(true);

        }
        /// <summary>
        /// 更改存讀檔面板圖片
        /// </summary>
        /// <param name="_change"></param>
        public void ChangeCanvasImage(bool _change)
        {
            if (_change)
            {
                Debug.Log("更換為讀檔面板圖片");
                saveLoadPanel.sprite = loadPanel;
            }
            else if(_change==false)
            {
                Debug.Log("更換為存檔面板圖片");
                saveLoadPanel.sprite = savePanel;
            }
        }
        /// <summary>
        /// 是否顯示存讀面板
        /// </summary>
        /// <param name="_fade"></param>
        public void ShowSaveCanvas(bool _fade = true)
        {
            if (_fade==true)
            {
                saveloadCanvas.alpha = 1;
            }

            if (_fade==false)
            {
                saveloadCanvas.alpha = 0;
            }

        }

        #endregion

        #region 核心功能-存/讀/刪 檔

        #region 存檔

        /// <summary>
        /// 存檔_左鍵點擊存檔欄
        /// </summary>
        /// <param name="index"存檔欄編號</param>
        public void ForSave(int index)
        {
            if (index != 0)
            {
                CheckForSaveFile(savePath, index);   //判斷是否有檔案，如果已有檔案先刪除
            }
            
            string fileName = "";               //存檔名接收變量，初始化為空

            //將資料儲存至SaveData(依Savedata格式)
            SaveData saveData = new SaveData
            {
                gameTime = SaveData.Instance.gameTime,
                playerPosition = PlayerMovement.finalMovent,
                saveShot = saveLoadSlotButton.screenShot.sprite,
                scenceName = GetScenceName(),

            };

            Debug.Log($"點擊的存檔欄編號{index}");

            //依存檔欄編號給予不同後綴名(用於:判斷後綴名就可以知道是哪一個存檔欄)
            if (index == 0)
            {
                fileName = "Save_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension[0];
            }
            if (index == 1)
            {
                fileName = "Save_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension[1];
            }
            if (index == 2)
            {
                fileName = "Save_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension[2];
            }

            
            string filePath = Path.Combine(savePath, fileName);   //指定路徑
            
            string jsonData = JsonUtility.ToJson(saveData);       //Json轉換打包進類的資料成string
            
            File.WriteAllText(filePath, jsonData);                //寫入檔案

            
            if (File.Exists(filePath))
            {
                Debug.Log($"存檔成功，檔案名為{fileName}");
            }
        }

        #endregion

        #region 讀檔

        /// <summary>
        /// 讀檔_左鍵點擊存檔欄
        /// 將讀取的檔案返回成遊戲數據
        /// </summary>
        /// <param name="filePath">檔按路徑</param>
        public void ForLoad(string filePath)
        {
            //判斷檔案是否存在，如果存在就加載
            if (File.Exists(filePath))
            {
                // 讀取檔案內容並反序列化為SaveData
                string jsonData = File.ReadAllText(filePath);
                SaveData saveData = JsonUtility.FromJson<SaveData>(jsonData);

                //將儲存在SaveData裡的資料傳換為遊戲內實質數據
                SetGameDuration(saveData.gameTime);             //遊戲時長
                SetPlayerPosition(saveData.playerPosition);     //玩家位置
                SetSceneceName(saveData.scenceName);
 
                Debug.Log($"讀檔成功，檔案名為{filePath}");
            }
            else
            {
                Debug.Log("讀取的檔案不存在");
            }
        }

        #endregion

        #region 刪檔

        public void DeleteSave(string filePath,int id)
        {
            if (Directory.Exists(filePath))
            {
                // 取得目錄中的所有檔案
                string[] files = Directory.GetFiles(filePath);

                // 遍歷所有檔案
                foreach (string file in files)
                {
                    // 檢查副檔名是否為 ".save"
                    if (Path.GetExtension(file) == ".save01" && id == 1)
                    {
                        // 在這裡處理找到 ".save01" 檔案的邏輯
                        Debug.Log("已刪除 .save 檔案: " + file);
                        File.Delete(file);
                        break;
                    }

                    if (Path.GetExtension(file) == ".save02" && id == 2)
                    {
                        // 在這裡處理找到 ".save02" 檔案的邏輯
                        Debug.Log("已刪除 .save 檔案: " + file);
                        File.Delete(file);
                    }
                    Debug.Log("找不到可刪除的檔案");
                }
                
            }
            else
            {
                Debug.LogWarning("指定的路徑不存在: " + filePath);
            }
            
        }

        #endregion

        #endregion

        #region 遊戲資料

        //初始化遊戲資料 (用於:Start)
        public void InitializeInfo()
        {
            receiveSaveData.gameTime = 0f;
            receiveSaveData.playerPosition = new Vector2(-18.11f, 2.96f);
            SaveData.Instance.scenceName = "A-開始畫面";
            ShowSaveCanvas(false);
        }

        //將存的遊戲時長付於實質遊戲時長 (用於ForLoad)
        private void SetGameDuration(float duration)
        {
            SaveData.Instance.gameTime = duration;
        }

        //接收要儲存的玩家位置
        private string GetScenceName()
        {
            string nowScenceName = SceneManager.GetActiveScene().name;
            Debug.Log($"這是儲存的場景名字{nowScenceName}");
            // TO-DO 獲取遊戲中玩家位置並返回數據
            return nowScenceName;
        }

        /*
        private void GetPropsState()
        {

        }
        */

        //  將儲存的場景賦值於遊戲內場景
        private void SetSceneceName(string scenceName)
        {
            SaveData.Instance.scenceName = scenceName;
            Debug.Log($"這是讀取的場景名字{scenceName}");
        }

        // 將儲存的玩家位置賦值於遊戲內玩家位置
        private void SetPlayerPosition(Vector2 position)
        {
            player.gameObject.transform.position = position;
            Debug.Log($"這是讀取的玩家位置{PlayerMovement.finalMovent}");
        }

        #endregion

        

        //檢查是否存檔欄已有檔案，如果已有按編號刪除 (用於:ForSave)
        private void CheckForSaveFile(string filepath,int id)
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
                    if (Path.GetExtension(filePath) == ".save01" && id == 1)
                    {
                        // 在這裡處理找到 ".save01" 檔案的邏輯
                        Debug.Log("找到 .save 檔案: " + filePath);
                        File.Delete(filePath);
                        break;
                    }

                    if (Path.GetExtension(filePath) == ".save02" && id == 2)
                    {
                        // 在這裡處理找到 ".save02" 檔案的邏輯
                        Debug.Log("找到 .save 檔案: " + filePath);
                        File.Delete(filePath);
                    }
                    Debug.Log("找不到自動存檔的檔案");
                }
            }
            else
            {
                Debug.LogWarning("指定的路徑不存在: " + filepath);
            }
        }

        #region 截圖

        //實現截圖功能 (用於:ForSave)
        public static void CameraCapture(int i, Camera camera, Rect rect)
        {
            if (!Directory.Exists(shotPath))

                Directory.CreateDirectory(shotPath);
            string path = Path.Combine(shotPath, $"{i}.png");

            int w = (int)rect.width;
            int h = (int)rect.height;

            RenderTexture rt = new RenderTexture(w, h, 0);
            camera.targetTexture = rt;
            camera.Render();

            RenderTexture.active = rt;

            Texture2D t2D = new Texture2D(w, h, TextureFormat.RGB24, true);

            t2D.ReadPixels(rect, 0, 0);
            t2D.Apply();

            byte[] bytes = t2D.EncodeToPNG();
            File.WriteAllBytes(path, bytes);

            camera.targetTexture = null;
            RenderTexture.active = null;
            GameObject.Destroy(rt);
        }

        //獲得截圖圖片 (用於:ForSave)
        public static Sprite LoadShot(int i)
        {
            var path = Path.Combine(shotPath, $"{i}.png");
            Texture2D t = new Texture2D(411, 311);
            t.LoadImage(GetImgByte(path));
            return Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f));
        }

        //不知道功能
        static byte[] GetImgByte(string path)
        {
            FileStream s = new FileStream(path, FileMode.Open);
            byte[] imgByte = new byte[s.Length];
            s.Read(imgByte, 0, imgByte.Length);
            s.Close();
            return imgByte;
        }

        //刪除截圖 (T0-D0 用於:刪檔)
        public  void DeleteShot(int i)
        {
            var path = Path.Combine(shotPath, $"{i}.png");
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log($"刪除截圖{i}");

            }
        }

        #endregion

     
        public  void QuitGame()
        {
            int autoID = 0;
            savePath = Application.persistentDataPath;
            //如果已有自動檔，刪除
            if (Directory.Exists(savePath))
            {
                Debug.Log(savePath);
                // 取得目錄中的所有檔案
                string[] files = Directory.GetFiles(savePath);

                // 遍歷所有檔案
                foreach (string filePath in files)
                {
                    // 檢查副檔名是否為 ".save"
                    if (Path.GetExtension(filePath) == ".auto" && autoID == 0)
                    {
                        // 在這裡處理找到 ".save01" 檔案的邏輯
                        Debug.Log("刪除 .auto 檔案: " + filePath);
                        File.Delete(filePath);
                        
                    }

                }
            }
            else
            {
                Debug.LogWarning("指定的路徑不存在: " + savePath);
                
            }
            ForSave(0);

            Debug.Log("自動檔完成");


#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

        }
    }
}

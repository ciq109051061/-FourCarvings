using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

namespace FourCarvings
{
    /// <summary>
    /// 存讀檔格操作
    /// </summary>
    public class SaveLoadSlotButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        #region 變數

        [Header("腳本")]
        public SaveLoadManager manager;

        [Header("UI")]
        public TextMeshProUGUI text_gameDate;   //日期
        public TextMeshProUGUI text_gameTime;   //時間
        public TextMeshProUGUI astive_gameDate; //中文_日期
        public TextMeshProUGUI astive_gameTime; //中文_時間
        public Image screenShot;                //截圖
        public Image rect;                      //邊框

        [Header("資源")]
        public Color enterColor;                //鼠標進入存檔時的邊框顏色
        public Sprite saveOri;                  //存檔欄默認圖片
        
        [HideInInspector]
        public static System.Action<int> OnLeftClick;      //接口 
        public static System.Action<int> OnRightClick;
        public string saveFilePath;         //存檔路徑      //資料接收
        public int id;                      //檔位編號

        #endregion

        #region Unity生命週期

        private void Awake()
        {
            //指定路徑
            saveFilePath = Application.persistentDataPath;           
        }

        private void Start()
        {
            //UI初始化
            //時間隱藏
            astive_gameDate.gameObject.SetActive(false);
            astive_gameTime.gameObject.SetActive(false);

            id = transform.GetSiblingIndex();       //從Inspector裡的順序獲取編號

            TimeFormat.SetOriTime();                //時間開始
            
            //查找檔案，讀取檔案資料，將儲存面板復原
            if (Directory.Exists(saveFilePath))
            {
                string[] files = Directory.GetFiles(saveFilePath);

                foreach (string filePath in files)
                {

                    if (Path.GetExtension(filePath) == ".auto" && id == 0)
                    {
                        UpdateUIInfoForStart(id);          // 更新UI介面
                    }
                    if (Path.GetExtension(filePath) == ".save01" && id == 1)
                    {
                        UpdateUIInfoForStart(id);           // 更新UI介面
                    }

                    if (Path.GetExtension(filePath) == ".save02" && id == 2)
                    {
                        UpdateUIInfoForStart(id);           // 更新UI介面
                    }
                }
            }
            else
            {
                Debug.LogWarning("指定的路徑不存在: " + saveFilePath);
            }
            

        }

        private void Update()
        {
            TimeFormat.SetCurTime();        //時間結束
        }

        #endregion

        #region 接口

        public void OnPointerClick(PointerEventData eventData)
        {
            #region 左鍵點擊
    
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                //存檔
                if (manager.isSave==true)
                {
                    Debug.Log($"現在是存檔模式{manager.isSave}");
                    if(id !=0)
                    {
                        manager.ForSave(id);        //存檔
                        UpdateUIInfo(id);           //更新存檔欄UI
                    }
                    else
                    {
                        Debug.Log("自動存檔不可點擊");
                        return;
                    }
                  
                }
                //讀檔
                if (manager.isLoad == true)
                {
                    Debug.Log($"現在是讀檔模式{manager.isLoad}");
                    CheckForLoadFile(saveFilePath,id);      //讀檔
                    manager.ShowSaveCanvas(false);              //面板關閉

                    if (SceneManager.GetActiveScene().name != SaveData.Instance.scenceName)
                    {
                        SceneManager.LoadScene(SaveData.Instance.scenceName);
                        Debug.Log($"場景名不符合，現在場景名:{SceneManager.GetActiveScene().name}，儲存場景名:{SaveData.Instance.scenceName}");
                    }
                    else
                    {
                        Debug.Log($"場景名符合，無須跳轉場景");
                    }

                    TimeFormat.SetOriTime();                //時間開始(重新計算)
                }
            }

            #endregion

            #region 右鍵點擊

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                //刪除檔案
                if (id !=0)
                {
                    manager.DeleteSave(saveFilePath, id);       //刪儲存檔
                    manager.DeleteShot(id);                     //刪除截圖
                    UpdateUIInfoForDelete(id);                  //更新UI面板
                }
                else
                {
                    Debug.Log("自動檔不可刪");
                    return;
                }
    
            }

            #endregion
        }

        #region 滑鼠變色

        //滑鼠進入變色
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (id != 0)
                rect.color = enterColor;
        }

        //滑鼠離開變色
        public void OnPointerExit(PointerEventData eventData)
        {
            if (id != 0)
                rect.color = Color.white;
        }
        #endregion

        #endregion

        #region 更新存讀檔面板

        //更新存檔欄UI-帶截圖-用於存檔
        public void UpdateUIInfo(int i)
        {
            //UI激活
            astive_gameDate.gameObject.SetActive(true);
            astive_gameTime.gameObject.SetActive(true);

            SaveLoadManager.CameraCapture(i, Camera.main, new Rect(0, 0, Screen.width, Screen.height));
            screenShot.sprite = SaveLoadManager.LoadShot(i);
            
            text_gameTime.text = $"{TimeFormat.GetFormatTime((int)SaveData.Instance.gameTime)}";
            
            string full = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            string date = full.Substring(0, 8);
            TimeFormat.SetData(ref date);
            text_gameDate.text = date;


        }

        //更新存檔欄UI-不帶截圖-用於一開始加載UI面板
        public void UpdateUIInfoForStart(int i)
        {
            //UI激活
            astive_gameDate.gameObject.SetActive(true);
            astive_gameTime.gameObject.SetActive(true);

            string shotPath = $"{Application.persistentDataPath}/shot";
            string imageName = $"{i}.png";
            string imagePath = Path.Combine(shotPath, imageName);

            if (File.Exists(imagePath))
            {
                byte[] imageData = File.ReadAllBytes(imagePath);
                Texture2D texture = new Texture2D(411, 311);
                texture.LoadImage(imageData);
                screenShot.sprite= Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                Debug.Log("找到圖片且加載成功");
            }
            else
            {
                Debug.Log("找不到圖片");
                return;
            }

        }

        //更新存檔欄UI-用於刪除檔案
        public void UpdateUIInfoForDelete(int i)
        {
            if (i==id)
            {
                astive_gameDate.gameObject.SetActive(false);
                astive_gameTime.gameObject.SetActive(false);

                screenShot.sprite = saveOri;
                text_gameTime.text = "";
                text_gameDate.text = "";
            }
            
        }

        #endregion

        //按編號讀取檔案
        private void CheckForLoadFile(string filepath,int id)
        {
            // 確保路徑存在
            if (Directory.Exists(filepath))
            {
                Debug.Log("讀取檔案中");
                // 取得目錄中的所有檔案
                string[] files = Directory.GetFiles(filepath);

                // 遍歷所有檔案
                foreach (string filePath in files)
                {
                    Debug.Log($"點選欄位ID{id}");
                    if (Path.GetExtension(filePath) == ".save01" && id==1)
                    {
                        Debug.Log($"讀檔成功，檔案名為{filePath}");
                        manager.ForLoad(filePath);
                        break;
                    }

                    if (Path.GetExtension(filePath) == ".save02" && id == 2)
                    {
                        Debug.Log($"讀檔成功，檔案名為{filePath}");
                        manager.ForLoad(filePath);
                        break;
                    }
                    
                    Debug.Log("未讀取到相對應的檔案");
                    
                }
            }
            else
            {
                Debug.LogWarning("指定的路徑不存在: " + filepath);
            }
        }


    }
    
}


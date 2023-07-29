using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

namespace FourCarvings
{

    public class SaveLoadSlotButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        #region 變量

        //腳本
        public SaveLoadManager manager;

        //public GameObject saveAndLoadPanel;

        //UI
        public TextMeshProUGUI text_gameDate;   //日期
        public TextMeshProUGUI text_gameTime;   //時間
        public TextMeshProUGUI astive_gameDate; //中文_日期
        public TextMeshProUGUI astive_gameTime; //中文_時間
        public Image screenShot;                //截圖
        public Image rect;                      //邊框
        
        public Color enterColor;                //鼠標進入存檔時的邊框顏色
        public Sprite saveOri;                  //存檔欄默認圖片
        
        //資料接收
        public string saveFilePath;         //存檔路徑
        public int id;                      //檔位編號

        //接口
        public static System.Action<int> OnLeftClick;
        public static System.Action<int> OnRightClick;

        #endregion

        #region Unity生命週期

        private void Awake()
        {
            //指定路徑
            saveFilePath = Application.persistentDataPath;           
        }

        private void Start()
        {
            astive_gameDate.gameObject.SetActive(false);
            astive_gameTime.gameObject.SetActive(false);

            id = transform.GetSiblingIndex();       //從Inspector裡的順序獲取編號

            TimeFormat.SetOriTime();                //時間開始
            
            //查找檔案，讀取檔案資料，將儲存面板復原
            //這裡發現，讀取資料連同遊戲資料，不知道會不會出錯，如果出錯的話要寫一個專門讀取UI資料的方法
            if (Directory.Exists(saveFilePath))
            {
                // 取得目錄中的所有檔案
                string[] files = Directory.GetFiles(saveFilePath);

                // 遍歷所有檔案
                foreach (string filePath in files)
                {
                    // 檢查副檔名是否為 ".save"
                    if (Path.GetExtension(filePath) == ".save01" && id == 1)
                    {
                        //manager.ForLoad(filePath);  // 讀取存檔資料
                        UpdataUIInfo(id);           // 更新UI介面
                    }

                    if (Path.GetExtension(filePath) == ".save02" && id == 2)
                    {
                        //manager.ForLoad(filePath);   // 讀取存檔資料
                        UpdataUIInfo(id);            // 更新UI介面
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
            //左鍵點擊-存檔、讀檔
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Debug.Log($"現在是存檔模式{manager.isSave}");

                if (manager.isSave==true)
                {
                    manager.ForSave(id);        //存檔
                    UpdataUIInfo(id);           //更新存檔欄UI
                    
                }

                Debug.Log($"現在是讀檔模式{manager.isLoad}");

                
                if (manager.isLoad == true)
                {

                    CheckForLoadFile(saveFilePath,id);      //讀檔
                    TimeFormat.SetOriTime();                //時間開始

                    manager.saveloadCanvas.gameObject.SetActive(false);
                    if (SceneManager.GetActiveScene().name != SaveData.Instance.scenceName)
                    {
                        SceneManager.LoadScene(SaveData.Instance.scenceName);
                    }
                }
                

            }

            //右鍵點擊刪除檔案
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                manager.DeleteSave(saveFilePath, id);
                manager.DeleteShot(id);
                UpdataUIInfoForDelete(id);
                //TO-DO 刪除截圖
                
            }
        }

        //滑鼠進入變色
        public void OnPointerEnter(PointerEventData eventData)
        {
            rect.color = enterColor;
        }

        //滑鼠離開變色
        public void OnPointerExit(PointerEventData eventData)
        {
            rect.color = Color.white;
        }

        #endregion

        //更新存檔欄UI
        public void UpdataUIInfo(int i)
        {
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

        public void UpdataUIInfoForDelete(int i)
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

        //按編號讀取檔案
        private void CheckForLoadFile(string filepath,int id)
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
                    if (Path.GetExtension(filePath) == ".save01" && id==1)
                    {
                        // 在這裡處理找到 ".save" 檔案的邏輯
                        manager.ForLoad(filePath);
                        break;
                    }

                    if (Path.GetExtension(filePath) == ".save02" && id == 2)
                    {
                        manager.ForLoad(filePath);
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


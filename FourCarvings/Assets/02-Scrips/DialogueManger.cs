using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

namespace FourCarvings
{
    /// <summary>
    /// 對話系統
    /// 逐字對話?
    /// 索引值的部分不懂? 不知道數字為什麼會剛好符合當前
    /// </summary>
    public class DialogueManger : MonoBehaviour
    {
        #region 資料區

        public static DialogueManger Instance;

        /// <summary>
        /// 對話表格
        /// </summary>
        public TextAsset dialogDataFile;
        /// <summary>
        /// UI_角色名字
        /// </summary>
        public TextMeshProUGUI nameText;
        /// <summary>
        /// UI_對話內容
        /// </summary>
        public TextMeshProUGUI dialogText;
        /// <summary>
        /// 角色圖片列表
        /// </summary>
        public List<Sprite> sprites = new List<Sprite>();
        /// <summary>
        /// 角色名字對應圖片字典
        /// </summary>
        Dictionary<string, Sprite> ImageDic = new Dictionary<string, Sprite>();
        /// <summary>
        /// 當前對話索引值
        /// </summary>
        public int dialogIndex;
        /// <summary>
        /// 對話文本_以行分割
        /// </summary>
        public string[] dialogRows;       
        /// <summary>
        /// 選項按鈕預置物
        /// </summary>
        public GameObject optionButton;
        /// <summary>
        /// 選項按鈕父節點
        /// </summary>
        public Transform buttonGroup;
        /// <summary>
        /// 對話進行按鍵
        /// </summary>
        public KeyCode dialogKey = KeyCode.Space;
        /// <summary>
        /// 對話按鍵開關
        /// </summary>
        public bool OnSpace;
        /// <summary>
        /// UI_對話介面
        /// </summary>
        public CanvasGroup dialogGroup;
        /// <summary>
        /// 是否開始對話
        /// </summary>
        public static bool present;
        /// <summary>
        /// UI_角色立繪_左
        /// </summary>
        public Image ch_image_Left;
        /// <summary>
        /// UI_角色立繪_右
        /// </summary>
        public Image ch_image_Right;
        /// <summary>
        /// 對話偵測觸碰點
        /// </summary>
        public BoxCollider2D detectionPoint;

        public static bool speed=false;

        public bool dialogueDown = false;

        public CanvasGroup mainCanvas;

        #endregion

        private void Awake()
        {
            ImageDic["小精靈"] = sprites[0];
            ImageDic["守護者"] = sprites[1];
            ImageDic["貝爾"] = sprites[2];
            ImageDic["棕熊"] = sprites[2];

        }

        
 
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //如果偵測到玩家，開始對話
            if (collision.gameObject.CompareTag("Player"))
            {
                //StartCoroutine(CanvasFade());
                dialogGroup.alpha = 1;
                Debug.Log("對話偵測成功");
                ReadText(dialogDataFile);
                ShowDialogue();
                present = true;                
                //OnSpace = true;
                mainCanvas.alpha = 0;

            }
        }

        private void Update()
        {
            SwitchForPlayerMovement();
            OnClickNext();
        }
        /// <summary>
        /// 更新UI文字
        /// </summary>
        public void UpdataText(string _name, string _text)
        {
            nameText.text = _name;
            dialogText.text = _text;
        }
        /// <summary>
        /// 更新立繪
        /// 小問題:一開始只出現一位對話者，但立繪繪兩張一起出現
        /// </summary>        
        public void UpadataImage(string _name, string _position)
        {
            if (present == true)
            {               
                if (_position == "左")
                {
                    ch_image_Left.sprite = ImageDic[_name];

                    ch_image_Right.color = new Color32(94, 94, 94, 255);

                    ch_image_Left.color = new Color32(255, 255, 255, 255);
                }
                else if (_position == "右")
                {
                    ch_image_Right.sprite = ImageDic[_name];

                    ch_image_Left.color = new Color32(94, 94, 94, 255);

                    ch_image_Right.color = new Color32(255, 255, 255, 255);
                }
                else if (_position == null)
                {
                    ch_image_Left.color = new Color32(94, 94, 94, 255);
                    ch_image_Right.color = new Color32(94, 94, 94, 255);
                }
            }
        }
        /// <summary>
        /// 讀取對話文本(.csv)
        /// </summary>
        public void ReadText(TextAsset _textAsset)
        {
            dialogRows = _textAsset.text.Split('\n');
            Debug.Log("文本讀取成功");
        }
        /// <summary>
        /// 顯示對話
        /// </summary>
        public void ShowDialogue()
        {
            for (int i = 0; i < dialogRows.Length; i++)
            {
                Debug.Log($"Processing line {i + 1} of {dialogRows.Length}");
                dialogueDown = false;
                string[] cells = dialogRows[i].Split(',');
                if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
                {
                    OnSpace = false;
                    Debug.Log("Found matching dialogue!");
                    UpdataText(cells[2], cells[4]);
                    UpadataImage(cells[2], cells[3]);
                    //跳轉索引值
                    OnSpace = true;
                    dialogIndex = int.Parse(cells[5]);
                    dialogueDown = true;
                    break;
                }
                else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex)
                {
                    GenerateOption(i);
                    dialogueDown = true;
                }
                else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex)
                {
                    Debug.Log("劇情結束");
                    //StartCoroutine(CanvasFade(false));
                    dialogGroup.alpha = 0;
                    present = false;
                    OnSpace = false;
                    //關閉對話偵測點，避免重複對話
                    detectionPoint.enabled = false;
                    mainCanvas.alpha = 1;
                    if (dialogDataFile.name == "找到鑰匙")
                    {
                        Debug.Log("鑰匙劇情撥放完畢");
                        KeyPrep.keyDialogueIsFinish = true;
                        Debug.Log($"鑰匙劇情{KeyPrep.keyDialogueIsFinish}");
                        SceneManager.LoadScene(3);
                    }
                    if (dialogDataFile.name == "第二章開頭")
                    {
                        GPM_Level_2.StartTextDone = true;
                    }
                    if (dialogDataFile.name == "敲門聲響起後")
                    {
                        GPM_Level_2.KnockTextDone = true;
                    }
                    if (dialogDataFile.name == "熊熊出門後")
                    {
                        GPM_Level_2.SerchOrNotTextDonw = true;
                    }
                    if (dialogDataFile.name == "熊熊回來")
                    {
                        GPM_Level_2.timeOverTextDone = true;
                    }
                }
            }
        }
        /// <summary>
        /// 按下對話按鍵，進行下一段對話
        /// </summary>
        public void OnClickNext()
        {
            //這段邏輯不是很了解，當我按下空白鍵時，執行顯示對話方法，應該會再次執行對話方法裡的循環，為什麼是下一句對話?
            if (Input.GetKeyDown(dialogKey) && OnSpace == true)
            {
                Debug.Log("按下空白鍵");
                ShowDialogue();
            }
        }
        /// <summary>
        /// 生成選項按鍵
        /// </summary>
        /// <param name="_index"></param>
        public void GenerateOption(int _index)
        {
            string[] cells = dialogRows[_index].Split(',');
            if (cells[0] == "&")
            {
                GameObject button = Instantiate(optionButton, buttonGroup);

                button.GetComponentInChildren<TextMeshProUGUI>().text = cells[4];
                //當按下button，AddListener監聽到了，執行括號內的行動而括號內用委託調用別的函數
                button.GetComponent<Button>().onClick.AddListener(delegate { OnOptionClick(int.Parse(cells[5]));
                    if (cells[6] != "") { cells[7]=Regex.Replace(cells[7],@"[\r\n]","");
                        OptionEffect(cells[6], cells[7]); } });
                OnSpace = false;

                GenerateOption(_index + 1);
            }

        }
        /// <summary>
        /// 選項被點擊
        /// </summary>
        /// <param name="_id"></param>
        public void OnOptionClick(int _id)
        {
            dialogIndex = _id;
            ShowDialogue();

            for (int i = 0; i < buttonGroup.childCount; i++)
            {
                Destroy(buttonGroup.GetChild(i).gameObject);
            }
            OnSpace = true;
        }
        /// <summary>
        /// 對話介面淡入和淡出
        /// </summary>
        /// <param name="_fade"></param>
        /// <returns></returns>
        private IEnumerator CanvasFade(bool _fade = true)
        {
            float increase = _fade ? +0.1f : -0.1f;
            for (int i = 0; i < 10; i++)
            {
                dialogGroup.alpha += increase;
                yield return new WaitForSeconds(0.04f);
            }
        }
        /// <summary>
        /// 玩家行動控制
        /// 為了對話時，玩家不可以亂跑
        /// </summary>
        public void SwitchForPlayerMovement()
        {
            if(present==false)
            {
                 PlayerMovement._switch=false;
            }
            else
            {
                PlayerMovement._switch = true;
            }
        }
        /// <summary>
        /// 道具效果管理
        /// </summary>
        /// <param name="_effect"></param>
        /// <param name="_target"></param>
        public void OptionEffect(string _effect, string _target)
        {
           
            if(_effect=="消失")
            {
                if(_target=="小精靈")
                {
                    GameObject.Find("小精靈").gameObject.SetActive(false);
                    speed = true;
                    Debug.Log("傳送守護者加速度");
                }
            }
            if(_effect=="探索")
            {
                GPM_Level_2.SerchOrNot = true;
                Debug.Log($"選擇探索{GPM_Level_2.SerchOrNot}");
            }
            if(_effect=="不探索")
            {
                GPM_Level_2.SerchOrNot = false;
                Debug.Log("選擇不探索");
            }
        }

        IEnumerator WaitAndContinue(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            // 繼續執行相應的代碼
        }
    }
}
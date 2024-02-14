using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace FourCarvings
{

    /// <summary>
    /// 背包管理
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        //靜態腳本
        public static InventoryManager Instance;
        public CanvasManager canvasManager;
        public ItemEffect itemEffect;
        
        [Header("物品資訊")]
        public Image item_image;                    //物品圖片
        public TextMeshProUGUI item_Name;           //物品名字
        public TextMeshProUGUI itemInformation;     //物品資訊
        public TextMeshProUGUI item_ID;             //物品ID

        [Header("背包UI")]
        public Button useButton;                    //使用鍵        
        public Button openBag;                      //按鈕-打開面板
        public Button closeBag;                     //按鈕-關閉面板
        public CanvasGroup bagCanvasGroup;          //面板-透明度
        [Header("背包管理")]
        public Sprite image_null;                   //空背包使用圖片       
        public Inventory playerBag;                 //指定玩家背包
        public GameObject slotGrid;                         //slot生成父節點
        public Slot slotPrefab;                             //slot預製物

        private List<Slot> slotList = new List<Slot>();     //slot清單
        private int bagMaxSlots = 12;                       //背包最大格數
        private bool bagIsOpen;
        private bool isLastOne;
        private int slotID;

        private void Awake()
        {
            #region 單例

            if (Instance == null)
            {
                //DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != null)
            {
                Destroy(gameObject);
            }

            #endregion

        }

        private void Start()
        {
            //初始化
            Instance.InitializeDetailsUI();
            Instance.bagCanvasGroup.alpha = 0;
            bagIsOpen = false;
            HasItem();

            #region 監聽
            Instance.openBag.onClick.AddListener(() => Instance.OpenBagPanel());
            Instance.closeBag.onClick.AddListener(() => Instance.CloseBagPanel());
            Instance.useButton.onClick.AddListener(() => Instance.UseItem());
            #endregion
        }

        private void Update()
        {
            //O鍵控制背包
            if(Input.GetKeyDown(KeyCode.O))
            {
                if (bagIsOpen==false)
                {
                    Instance.OpenBagPanel();
                }
                else if(bagIsOpen==true)
                {
                    Instance.CloseBagPanel();
                }
            }
        }

        #region 獲得物品

        //添加物品至背包清單
        public bool AddItemToInventory(Item item)
        {

            //查找背包是否已有該物品，有的話更新數量
            foreach (Item inventoryItem in playerBag.itemList)
            {
                //id匹配成功，增加數量、更新持有數UI
                if (inventoryItem.itemID == item.itemID)
                {
                    item.itemHeld++;
                    Debug.Log($"物品現有持有數{item.itemHeld}");
                    slotPrefab.UpdateCount(item);
                    
                    return true;
                }
            }

            //如果背包有位置，那麼加進去
            if (playerBag.itemList.Count < bagMaxSlots)
            {
                item.itemHeld = 1;              //設定數量
                playerBag.AddItem(item);        //添加物品至清單
                Instance.CreatItemSlot(item);   //新物品添加邏輯
                Debug.Log($"物品新增至玩家背包成功");
                return true;
            }

            return false;
        }

        //將新物品添加至物品格
        public void CreatItemSlot(Item item)
        {
            Slot newItem = Instantiate(Instance.slotPrefab, Instance.slotGrid.transform);   //生成預製物
            newItem.gameObject.transform.SetParent(Instance.slotGrid.transform);            //指定生成位置

            newItem.sloyItem = item;

            newItem.slotImage.sprite = item.itemImage;          //更新預製物圖片
            newItem.slotNum.text = item.itemHeld.ToString();    //更新預製物數量文字

            newItem.name = item.itemName;

            slotList.Add(newItem);                              //添加至預製物清單

            Debug.Log("slot新增成功");

        }

        public void HasItem()
        {
            foreach (Item haveItem in playerBag.itemList)
            {
                if (haveItem !=null)
                {
                    CreatItemSlot(haveItem);

                    Debug.Log("新增預製體成功");
                }
                
            }
        }

        #endregion


        #region 使用物品

        //使用完物品計算數量
        public void AfterUseCount(Item item)
        {

            if (item.itemHeld > 1)
            {
                item.itemHeld--;
                slotPrefab.UpdateCount(item);
                isLastOne = false;
                Debug.Log($"使用成功，數量成功減一{item.itemHeld}");
            }
            else if (item.itemHeld == 1)
            {

                //playerBag.RemoveItem(item);
                isLastOne = true;

                Debug.Log($"使用成功，物品為最後一個");
            }


        }
        //使用物品
        public void UseItem()
        {
            slotID = int.Parse(item_ID.text);
            Debug.Log($"點擊的物品{slotID}");

            foreach (Item thisItem in playerBag.itemList)
            {
                if (thisItem.itemID==slotID)
                {
                    Debug.Log($"匹配成功:物品名:{thisItem.itemName}，物品ID:{thisItem.itemID}");

                    Slot slotObject = slotList.Find(slot => slot.sloyItem == thisItem);

                    Debug.Log($"尋找成功:物品名:{slotObject.sloyItem.itemName}，物品ID:{slotObject.sloyItem.itemID}");

                    itemEffect.ExecuteEffect(slotID);

                    AfterUseCount(thisItem);

                    if (isLastOne == true)
                    {
                        Debug.Log($"這是將要刪除的slot名字{slotObject.name}");
                        
                        Destroy(slotObject.gameObject);

                        playerBag.RemoveItem(thisItem);

                        InitializeDetailsUI();

                        Debug.Log($"物品最後一個刪除成功");
                    }

                    break;
                }

            }
                /*
                thisItem.itemName = item_Name.text;
                Debug.Log($"點擊的物品{thisItem.itemName}");


                
                Debug.Log($"尋找匹配方法:物品名:{thisItem.itemName}，物品ID:{thisItem.itemID}");
                //名字匹配成功，找到物品
                if (thisItem.itemName == item_Name.text)
                {
                    Debug.Log($"名字匹配{thisItem.itemName}");

                    Slot slotObject = slotList.Find(slot => slot.sloyItem == thisItem);     //找到slot

                    Debug.Log($"slot匹配{thisItem}");
                    Debug.Log($"使用前數量{thisItem.itemHeld}");

                    ItemEffect.Instance.ExecuteEffect(thisItem.itemID);

                    AfterUseCount(thisItem);

                    Debug.Log($"使用後數量{thisItem.itemHeld}");

                    if (isLastOne==true)
                    {
                        Destroy(slotObject.gameObject);

                        InitializeDetailsUI();

                        Debug.Log($"物品最後一個刪除成功");
                    }
                    

                    CloseBagPanel();

                
                }
                

        }
            */

        }

        #endregion

        #region 更新背包資訊頁UI

        //初始化背包資訊頁UI
        private void InitializeDetailsUI()
        {
            Instance.item_image.sprite = Instance.image_null;
            Instance.item_Name.text = "";
            Instance.itemInformation.text = "";

        }

        // 更新資訊頁物品圖片
        public void UpdateItemImage(Sprite itemImage)
        {
            
            Instance.item_image.sprite = itemImage;
        }

        // 更新資訊頁物品名稱
        public void UpdateItemName(string itemName)
        {
            
            Instance.item_Name.text = itemName;
        }

        // 更新資訊頁物品資訊
        public void UpdateItemInfo(string itemDescription)
        {
            Instance.itemInformation.text = itemDescription;
        }

        public void UpdateItemID(int id)
        {
            Instance.item_ID.text = id.ToString();
        }

        //顯示使用鍵
        public void ShowUseButton(bool isShow)
        {
            Instance.useButton.gameObject.SetActive(isShow);
        }

        #endregion

        #region 按鈕控制面板

        //開啟背包面板
        private void OpenBagPanel()
        {
            Instance.bagCanvasGroup.alpha = 1;
            Instance.InitializeDetailsUI();
            bagIsOpen = true;
            canvasManager.updateCanvas = true;
        }

        //關閉背包面板
        private void CloseBagPanel()
        {
            Instance.bagCanvasGroup.alpha = 0;
            bagIsOpen = false;
            canvasManager.updateCanvas = true;
        }


        #endregion

    }



}

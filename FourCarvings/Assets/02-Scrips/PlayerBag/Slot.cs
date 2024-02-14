using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace FourCarvings
{
    /// <summary>
    /// 物品格
    /// </summary>
    public class Slot : MonoBehaviour, IPointerClickHandler
    {
        #region 變數

        public Item sloyItem;

        [Header("預製物UI")]
        public Image slotImage;             //背包格_物品圖片
        public TextMeshProUGUI slotNum;     //背包格_物品持有數

        [HideInInspector]
        public static System.Action OnLeftClick;
        //public int slotID;

        #endregion

        #region 訂閱滑鼠點擊事件

        private void Start()
        {
            OnLeftClick += OnClick;
            
        }

        private void OnDestroy()
        {
            OnLeftClick -= OnClick;           
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (OnLeftClick != null)
                    OnClick();
            }
            
        }

        #endregion

        //點擊物品格查看物品
        private void OnClick()
        {
            //更新資訊頁及物品ID
            InventoryManager.Instance.UpdateItemImage(sloyItem.itemImage);
            InventoryManager.Instance.UpdateItemName(sloyItem.itemName);
            Debug.Log($"Slot中的物品名字{sloyItem.itemName}");
            InventoryManager.Instance.UpdateItemInfo(sloyItem.itemInfo);
            InventoryManager.Instance.UpdateItemID(sloyItem.itemID);
            Debug.Log($"Slot中的物品編號{sloyItem.itemID}");
        }

        //更新持有數UI
        public void UpdateCount(Item item)
        {
            GameObject newSlot = GameObject.Find(item.itemName);
            Debug.Log($"newSlot名字{newSlot.name}");
            Debug.Log("成功改變持有數UI");
            newSlot.GetComponent<Slot>().slotNum.text = item.itemHeld.ToString();
            //slotNum.text = item.itemHeld.ToString();

            
        }

        


    }
}
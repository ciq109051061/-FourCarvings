using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FourCarvings
{

    public class Slot : MonoBehaviour
    {
        public Item sloyItem;

        public Image slotImage;

       // public Button useButton;

        public TextMeshProUGUI slotNum;

        //public string slotName;

        //public TextAsset propsTextAsset;

       // public string[] propsRows;

        //public InventoryManager it;

        //public Button useButton;

        private void Start()
        {
           // useButton.gameObject.SetActive(false);
            //slotName = sloyItem.itemName;
        }
        

        public void ItemOnClick()
        {
            Debug.Log("物品被點擊");
            //Debug.Log(slotName);
            InventoryManager.UpdataItemInfo(sloyItem.itemInfo);

            InventoryManager.UpdataUI_Image(sloyItem.itemImage) ;

            InventoryManager.Updata_ItemName(sloyItem.itemName);

            //slotName = sloyItem.itemName;
            //useButton.gameObject.SetActive(true);
            
        }
        /*
        public void UseOnClick()
        {
            DestroyImmediate(this.gameObject, true);
        }
        */
        /*
        public void ReadPropsText(TextAsset _textAsset)
        {
            propsRows = _textAsset.text.Split('\n');
            Debug.Log("道具效用讀取成功");
            
        }

        public void UseProps()
        {
            ReadPropsText(propsTextAsset);
            for (int i = 0; i < propsRows.Length; i++)
            {
                string[] cells = propsRows[i].Split(',');
                if (cells[0] == slotName)
                {                    
                    Debug.Log("道具名字匹配成功");
                    if (cells[1] == "通關")
                    {
                        GameObject.Find("封印處").gameObject.SetActive(false);
                    }
                }
            }
        }
        */
    }
}

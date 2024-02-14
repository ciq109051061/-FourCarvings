using UnityEngine;

namespace FourCarvings
{
    [CreateAssetMenu(fileName ="New Item",menuName = "FourCarvings/New Item")]
    public class Item : ScriptableObject
    {
        public int itemID; //物品ID與物品效果搭配

        public string itemName; //物品名字

        public Sprite itemImage; //物品圖片

        public int itemHeld; //物品數量

        [TextArea]
        public string itemInfo; //物品資訊

    }
}

using System.Collections.Generic;
using UnityEngine;

namespace FourCarvings
{

    /// <summary>
    /// 創建背包資料
    /// 方法:添加物品到背包、從背包移除物品
    /// </summary>
    [CreateAssetMenu(fileName = "New Inentory", menuName = "FourCarvings/New Inentory")]
    public class Inventory : ScriptableObject
    {
        public List<Item> itemList = new List<Item>();

        // 添加物品到背包
         public void AddItem(Item item)
        {
            itemList.Add(item);
        }

        // 從背包移除物品
        public void RemoveItem(Item item)
        {
            itemList.Remove(item);
        }

    }

}
using UnityEngine;

namespace FourCarvings
{

    /// <summary>
    /// 點擊地圖上物品
    /// </summary>
    public class ItemOnWorld : MonoBehaviour
    {
        public Item thisItem;

        private void OnMouseDown()
        {
            InventoryManager.Instance.AddItemToInventory(thisItem);

            //Debug.Log($"找到slot{GameObject.Find($"{thisItem.itemName}").gameObject.name}");
            AudioManager.OnWorldAudio();

            Destroy(gameObject);
        }
     
    }
}

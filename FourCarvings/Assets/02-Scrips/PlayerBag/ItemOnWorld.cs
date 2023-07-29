using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourCarvings
{
    public class ItemOnWorld : MonoBehaviour
    {
        public Item thisItem;
        public Inventory playerInventory;
        public Slot slot;

       //public AudioClip slecet;

       // public AudioSource onWorldSource;

        private void Awake()
        {
           // onWorldSource = gameObject.AddComponent<AudioSource>();
        }

        private void OnMouseDown()
        {
            AddNewItem();

            //OnWorldAudio();

            AudioManager.OnWorldAudio();

            Destroy(gameObject);

        }

        public void AddNewItem()
        {
            if(!playerInventory.itemList.Contains(thisItem))
            {
                playerInventory.itemList.Add(thisItem);
                //InventoryManager.CreatNewItem(thisItem);

            }
            else
            {
                thisItem.itemHeld += 1;
                //slot.slotNum.text = thisItem.itemHeld.ToString();
                //Debug.Log(slot.slotNum);
            }
            InventoryManager.RefeshItem();
        }

        /*
        public void OnWorldAudio()
        {
            onWorldSource.clip = slecet;
            onWorldSource.Play();
            Debug.Log("音效播放成功");
        }
        */
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace FourCarvings
{
    [CreateAssetMenu(fileName ="New Item",menuName = "FourCarvings/New Item")]
    public class Item : ScriptableObject
    {
        public string itemName;

        public Sprite itemImage;

        public int itemHeld;

        [TextArea]
        public string itemInfo;
    }
}

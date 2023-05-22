using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourCarvings
{
    [CreateAssetMenu(fileName ="New Hint Data",menuName =("FourCarvings/Hint Data"))]
    public class HintData : ScriptableObject
    {
        [Header("提示者")]
        public string hintName;

        [Header("提示內容" +
            "")]
        public string[] hintContent;
    }
}

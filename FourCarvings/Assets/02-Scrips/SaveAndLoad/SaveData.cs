using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourCarvings
{
    /// <summary>
    /// 資料儲存
    /// 儲存:遊戲內資料、存讀檔介面
    /// </summary>
    [System.Serializable]
    public class SaveData
    {
        public static SaveData Instance = new SaveData();

        //存讀檔介面
        public float gameTime; //遊戲時長 
        public Sprite saveShot; //截圖

        //遊戲內資料
        public Vector2 playerPosition; //玩家位置
        public string scenceName;      //場景名
        

    }
}

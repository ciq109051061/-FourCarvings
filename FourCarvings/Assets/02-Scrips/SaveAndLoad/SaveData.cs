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

        //道具狀態-撿了就沒有
        public GameObject rePassCard;
        public GameObject reGold01;
        public GameObject reFox;
        //道具效果紀錄
        //對話選項效果紀錄
        //對話序號紀錄
        //觸發點狀態紀錄
        
        

    }
}

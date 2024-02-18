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

        //第一關遊戲資料
        // 道具
        //狐狸令牌_顯示
        //通行證_顯示
        //金幣01-04_顯示
        // 對話觸發點
        //對話01_顯示
        //對話02_顯示 如果對話點02回檔，選項兩個也要跟著回檔
        // 選項
        //小精靈_顯示
        //移速加成
        // 封印門
        //封印門的碰撞
        //封印門的偵測


    }
}

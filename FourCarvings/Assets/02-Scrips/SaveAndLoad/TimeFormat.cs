using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourCarvings
{

    public class TimeFormat : MonoBehaviour
    {
        public static float oriT;  //起始時間
        public static float cutT;  //終點時間

        #region 計算遊戲時長
        public static void SetOriTime()
        {
            float tempT = Time.realtimeSinceStartup;
            oriT = SaveData.Instance.gameTime - tempT;
            SetCurTime();
        }

        public static void SetCurTime()
        {
            cutT = Mathf.Max(oriT + Time.realtimeSinceStartup, 0);
            SaveData.Instance.gameTime = cutT;
        }

        //時間格式轉換:00:00:00
        public static string GetFormatTime(int seconds)
        {
            TimeSpan ts = new TimeSpan(0, 0, seconds);
            return $"{ts.Hours.ToString("00")}:{ts.Minutes.ToString("00")}:{ts.Seconds.ToString("00")}";

        }

        #endregion

        #region 轉換格式

        public static void SetData(ref string data)
        {
            data = data.Insert(4, "/");
            data = data.Insert(7, "/");
        }

        public static void SetTime(ref string time)
        {
            time = time.Insert(2, ":");
            time = time.Insert(5, ":");
        }

        #endregion
    }
}

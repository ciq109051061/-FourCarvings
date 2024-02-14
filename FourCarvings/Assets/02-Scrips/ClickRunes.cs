using UnityEngine;

namespace FourCarvings
{
    /// <summary>
    /// 符文預製物
    /// </summary>
    public class ClickRunes : MonoBehaviour
    {
        #region 變數

        private int runes_ID;

        [Header("腳本")]
        public RunesManager runesManager;
        public GPM_Level_0 level_0;

        [HideInInspector]
        public bool canClick = false;

        #endregion

        private void Start()
        {
            runes_ID= transform.GetSiblingIndex();
        }

        private void OnMouseDown()
        {
            if (level_0.okClick==true)
            {
                Debug.Log($"點擊福文{runes_ID}");
                StartCoroutine(runesManager.OrderClickRunes(runes_ID));

                AudioManager.PlayRunsAudio(runes_ID);

            }
        }
    }
}

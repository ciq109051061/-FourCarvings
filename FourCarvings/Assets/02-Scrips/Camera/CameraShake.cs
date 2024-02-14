using UnityEngine;

namespace FourCarvings
{
    /// <summary>
    /// 相機震動
    /// </summary>
    public class CameraShake : MonoBehaviour
    {
        #region 變數

        public SpriteRenderer worldLight;       //震動改變顏色的圖片
        [Header("震動數值")]
        public float shakeDuration = 3f;       //震動持續時間
        public float shakeAmount = 0.2f;       //震動時圖片位移量
        public float interval = 0.1f;          //震動之間的間隔時間
        private float currentInterval = 0f;
        private Vector3 originalPosition;       //圖片位置
        [HideInInspector]
        public bool isShake;                    //是否在震動

        #endregion

        private void Start()
        {
            isShake = true;
            originalPosition = transform.localPosition;

        }

        private void Update()
        {
            Shake();
        }

        public void Shake()
        {
            //如果震動時間大於0
            if (shakeDuration > 0)
            {
                //如果當前間隔小於等於0
                if (currentInterval <= 0f)
                {
                    worldLight.color = new Color(1, 0.7f, 0.7f);

                    Vector3 shakeOffset = Random.insideUnitSphere * shakeAmount;
                    shakeOffset.z = 0f;     // 震動只影響x和y軸，z軸保持不變
                    transform.localPosition = originalPosition + shakeOffset;

                    currentInterval = interval;     
                }
                else
                {
                    currentInterval -= Time.deltaTime;

                }

                shakeDuration -= Time.deltaTime;

            }
            else
            {
                shakeDuration = 0f;
                transform.localPosition = originalPosition;
                worldLight.color = Color.white;
                isShake = false;
            }


        }
    }
}

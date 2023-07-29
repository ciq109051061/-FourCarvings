using UnityEngine;

namespace FourCarvings
{
    /// <summary>
    /// 相機跟隨主角
    /// 目前還缺相機邊界
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField,Header("跟隨目標")]
        private Transform target;
        /// <summary>
        /// 相機位置向量
        /// </summary>
        private Vector3 offset;

        private void Start()
        {
            //距離差
            offset = target.position - this.transform.position;
        }

        private void Update()
        {
            //物件位置=目標位置-距離差 ?-20230604
            this.transform.position = target.position - offset;
        }
    }
}

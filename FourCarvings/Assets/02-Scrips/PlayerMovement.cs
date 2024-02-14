using UnityEngine;
using UnityEngine.UIElements;

namespace FourCarvings
{
    /// <summary>
    /// 玩家控制
    /// 移動、動畫、音效
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        #region 變量區

        /// <summary>
        /// 玩家移動速度
        /// </summary>
        [SerializeField,Header("移動速度")]
        private float moveSpeed = 3f;
        /// <summary>
        /// 玩家鋼體
        /// </summary>
        public Rigidbody2D rb;

        //public static Transform playerTransform;

        public Vector2 movement;
        /// <summary>
        /// 玩家Animator
        /// </summary>
        public Animator animator;
        
        /// <summary>
        /// 背包開關
        /// </summary>
        private bool isOpen;
        /// <summary>
        /// 玩家移動開關
        /// </summary>
        public static bool _switch;

       // public Position position;

        public static Vector2 finalMovent;

        #endregion

        private void Start()
        {
           
        }

        private void Update()
        {
            //如果正在對話，玩家不能移動、剛體切換為isKinematic
            if (_switch == false)
            {
                moveSpeed = 3.0f;
                if (DialogueManger.speed == true)
                {
                    moveSpeed = 5.0f;
                    
                }
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
                rb.isKinematic = false;
               
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.sqrMagnitude);


            }
            else
            {
                rb.isKinematic = true;
                moveSpeed = 0;
               
            }

            

        }

        

        private void FixedUpdate()
        {
            //Movement
             finalMovent = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
             rb.MovePosition(finalMovent);
           
            
        }

        

        /// <summary>
        /// 腳步音效
        /// </summary>
        public void StepAudio()
        {
            AudioManager.PlayFootstepAudio();
        }
      
    }
}

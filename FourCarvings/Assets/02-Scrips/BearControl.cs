using UnityEngine;

namespace FourCarvings
{

    public class BearControl : MonoBehaviour
    {
        public Rigidbody2D bearRB;
        public Animator bearAnimator;
        public BoxCollider2D[] moveTri = new BoxCollider2D[2];

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag=="Bear")
            {
                if (collision.name=="熊熊落腳點01")
                {
                    GPM_Level_2.move02 = true;
                    Debug.Log("踩到落腳點01");
                    GameObject.Find("熊熊落腳點01").SetActive(false);
                }
                if (collision.name == "熊熊落腳點02")
                {
                    GPM_Level_2.move03 = true;
                    //GPM_Level_2.move02 = false;
                    Debug.Log("踩到落腳點02");
                    GameObject.Find("熊熊落腳點02").SetActive(false);
                }
                if (collision.name == "熊熊落腳點03")
                {
                    //GPM_Level_2.move03 = false;
                    gameObject.SetActive(false);
                    MoveDown();
                    GameObject.Find("熊熊落腳點03").SetActive(false);
                }
                if (collision.name == "熊熊落腳點04")
                {

                    GPM_Level_2.BearMoveFinallyDone = true;
                }

            }
        }

        public void BearMovement(Vector2 targetPos)
        {
            float moveSpeed = 3.0f;
            bearRB.position = Vector2.MoveTowards(bearRB.position, targetPos, moveSpeed * Time.deltaTime);
            
        }

        public void MoveForwardAnim()
        {
            bearAnimator.Play("bear_moveForward");
        }

        public void MoveRighrAnim()
        {
            bearAnimator.Play("bear_moveRight");
        }



        public void MoveDown()
        {
            GPM_Level_2.BearMoveDone = true;
        }


    }
}

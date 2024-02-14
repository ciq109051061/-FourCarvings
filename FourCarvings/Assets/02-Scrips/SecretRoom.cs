using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourCarvings
{

    public class SecretRoom : MonoBehaviour
    {
        public Animator secretAnim;
        public GameObject wall;
        public GameObject paper01, paper02;
        public BoxCollider2D wallCollider;
        private bool isClick = true;
        private void Start()
        {
            paper01.SetActive(false);
            paper02.SetActive(false);
        }

        private void OnMouseDown()
        {
            if (isClick==true)
            {
                secretAnim.Play("secretRoomDoor");
                isClick = false;
                wall.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                paper01.SetActive(true);
                paper02.SetActive(true);
                wallCollider.enabled = false;

            }
            
            
        }
    }
}

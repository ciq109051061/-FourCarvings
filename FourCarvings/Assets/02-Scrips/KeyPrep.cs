using UnityEngine;
using UnityEngine.SceneManagement;

namespace FourCarvings
{

    public class KeyPrep : MonoBehaviour
    {
        public GameObject keyDialogue;

        public static bool keyDialogueIsFinish = false;

        private void Start()
        {
            keyDialogue.gameObject.SetActive(false);
       
        }

        private void OnMouseDown()
        {
            Debug.Log("�ߨ��_�ͤF");
            gameObject.SetActive(false);
            keyDialogue.gameObject.SetActive(true);
            
        }

    }
}

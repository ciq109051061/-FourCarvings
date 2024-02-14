using UnityEngine;
using UnityEngine.SceneManagement;

namespace FourCarvings
{

    public class JumpToFrost : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag=="Player")
            {
                SceneManager.LoadScene(4);
            }
        }
    }
}

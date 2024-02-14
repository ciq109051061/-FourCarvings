using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FourCarvings
{

    public class Rainning : MonoBehaviour
    {
        public GameObject globalLight;
        public BoxCollider2D rainTrigge;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag=="Player")
            {
                StartCoroutine(rainLight());
                AudioManager.PlayRainAudio();
            }
        }

        public IEnumerator rainLight()
        {
            Color originalColor = new Color(0.60f, 0.70f, 0.65f, 0);
            //Color targetColor = new Color(0.59f, 0.68f, 0.73f, 1f);
            Color finalColor = new Color(0.31f, 0.45f, 0.52f, 1);

            for (int i = 0; i < 1; i++)
            {
                float elapsedTime = 0f;
                while (elapsedTime < 0.5f)
                {
                    float t = elapsedTime / 0.25f;
                    globalLight.GetComponent<Light2D>().color= Color.Lerp(originalColor, finalColor, t);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
            }

            rainTrigge.enabled = false;
        }
    }
}

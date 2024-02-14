using UnityEngine;

namespace FourCarvings
{

    public class OkRain : MonoBehaviour
    {
        public ParticleSystem rainParticle;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag=="Player")
            {
                rainParticle.Play();
            }
        }
    }
}

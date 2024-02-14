using UnityEngine;

namespace FourCarvings
{

    public class GPM_Level_1 : MonoBehaviour
    {
        public ParticleSystem[] fireFly =new ParticleSystem [3];

        private void Start()
        {
            for (int i = 0; i < fireFly.Length; i++)
            {
                fireFly[i].Play();
            }
        }

        
    }
}

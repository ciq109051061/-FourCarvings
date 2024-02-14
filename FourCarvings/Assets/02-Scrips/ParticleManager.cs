using UnityEngine;

namespace FourCarvings
{
    /// <summary>
    /// 粒子系統管理
    /// </summary>
    public class ParticleManager : MonoBehaviour
    {
        public SpriteRenderer energySheild;     //能量罩

        [Header("粒子")]
        public ParticleSystem particleA;        //能量團
        public ParticleSystem particleB;        //能量團爆炸
       
        [Header("腳本")]
        public RunesManager runesManager;

        private bool playParticleA = true;
        private bool playParticleB = false;

        private void Update()
        {
            #region 按順序播放粒子

            if (particleA != null && particleB != null)
            {
                if (playParticleA)
                {
                    PlayParticleA();
                }
                else if (playParticleB &&runesManager.allRunesClick)
                {
                    PlayParticleB();
                }
            }
            else
            {
                UnityEngine.Debug.LogError("particleA or particleB is not assigned!");
            }

            #endregion
        }
     
        private void PlayParticleA()
        {
            particleA.Play();

            if (runesManager.allRunesClick)
            {
                playParticleA = false;
                playParticleB = true;
            }
        }

        private void PlayParticleB()
        {          
            particleA.Stop();

            particleA.gameObject.SetActive(false);
            energySheild.gameObject.SetActive(false);

            particleB.Play();
            playParticleB = false;

        }
        
    }
}

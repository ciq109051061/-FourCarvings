using UnityEngine;
using UnityEngine.SceneManagement;

namespace FourCarvings
{
    /// <summary>
    /// 音樂、音效管理
    /// 使用:方法，外部呼叫
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        #region 資料區

        static AudioManager current;

        [Header("腳步聲")]
        public AudioClip walk_audioClip;

        [Header("小精靈")]
        public AudioClip elf_audioClip;

        public AudioClip rain_audioClip;

        public AudioClip[] background_audioClip;

        public AudioClip[] runs_audioClip;

        public AudioClip shake_audioClip;

        public AudioClip break_audioClip;

        public AudioClip knock_audioClip;
 
        [Header("選取聲")]
        public AudioClip slecet;
        /// <summary>
        /// 玩家音效播放器
        /// </summary>
        private AudioSource playerSource;
        /// <summary>
        /// 道具音效播放器
        /// </summary>
        private AudioSource onWorldSource;
        /// <summary>
        /// 小精靈播放器
        /// </summary>
        private AudioSource ElfSource;

        private AudioSource rainSource;

        private AudioSource backGroundSource;

        private AudioSource runsSource;

        private AudioSource shakeSource;

        private AudioSource breakSorce;

        private AudioSource knockSorce;

        public Slot thisSlot;



        #endregion

        private void Awake()
        {
            //current = this;
            //DontDestroyOnLoad(gameObject);

            #region 單例

            if (current == null)
            {
                //DontDestroyOnLoad(gameObject);
                current = this;
            }
            else if (current != null)
            {
                Destroy(gameObject);
            }

            #endregion

            playerSource = gameObject.AddComponent<AudioSource>();
            onWorldSource = gameObject.AddComponent<AudioSource>();
            ElfSource = gameObject.AddComponent<AudioSource>();
            rainSource = gameObject.AddComponent<AudioSource>();
            backGroundSource = gameObject.AddComponent<AudioSource>();
            runsSource = gameObject.AddComponent<AudioSource>();
            shakeSource = gameObject.AddComponent<AudioSource>();
            breakSorce = gameObject.AddComponent<AudioSource>();
            knockSorce = gameObject.AddComponent<AudioSource>();

            if (SceneManager.GetActiveScene().name=="C-第一關_山洞")
            {
                Debug.Log($"音樂匹配1{SceneManager.GetActiveScene().name}");
                current.backGroundSource.clip = current.background_audioClip[0];
                current.backGroundSource.Play();
                current.backGroundSource.loop = true;
            }
            else if(SceneManager.GetActiveScene().name == "B-序章_符文")
            {
                current.backGroundSource.clip = current.background_audioClip[2];
                
            }
            else
            {
                Debug.Log($"音樂匹配2{SceneManager.GetActiveScene().name}");
                current.backGroundSource.clip = current.background_audioClip[1];
                current.backGroundSource.Play();
                current.backGroundSource.loop = true;
            }


            
        }

        #region 方法區

        /// <summary>
        /// 播放腳步聲
        /// </summary>
        public static void PlayFootstepAudio()
        {
            //int index = Random.Range(0, current.walk_audioClip.Length);

            current.playerSource.clip = current.walk_audioClip;

            current.playerSource.Play();

            current.playerSource.volume = 0.02f;

           
        }

        public static void PlayRainAudio()
        {
            current.rainSource.clip = current.rain_audioClip;
            current.rainSource.Play();
            current.rainSource.volume = 0.5f;
        }

        /// <summary>
        /// 播放選取音效
        /// </summary>
        public static void OnWorldAudio()
        {
            current.onWorldSource.clip = current.slecet;
            current.onWorldSource.Play();
            //選取音量
            current.onWorldSource.volume = 0.5f;
        }

        public static void ElfAudio()
        {
            current.ElfSource.clip = current.elf_audioClip;
            current.ElfSource.Play();

            current.ElfSource.volume = 0.1f;
        }

        public static void PlayRunsAudio(int runsID)
        {
            current.runsSource.clip = current.runs_audioClip[runsID];
            current.runsSource.Play();


        
        }

        public static void PlayShakeAudio()
        {
            current.shakeSource.clip = current.shake_audioClip;
            current.shakeSource.Play();
        }

        public static void StopShakeAudio()
        {
            current.shakeSource.Stop();
        }

        public static void PlayNoiseAudio()
        {
            current.backGroundSource.Play();
            current.backGroundSource.loop = true;
        }

        public static void PlayBreakAudio()
        {
            current.breakSorce.clip = current.break_audioClip;
            current.breakSorce.Play();
        }

        public static void PlayKnockAudio()
        {
            //current.knockSorce.clip = current.knock_audioClip;
            //current.knockSorce.Play();
            current.knockSorce.PlayOneShot(current.knock_audioClip);
        }
        #endregion

    }

}
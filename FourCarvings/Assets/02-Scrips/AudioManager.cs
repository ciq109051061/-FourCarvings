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

        //音源音效
        public AudioClip walk_audioClip;    //腳步聲
        public AudioClip elf_audioClip;     //小精靈
        public AudioClip rain_audioClip;    //下雨聲
        public AudioClip[] background_audioClip;    //背景音樂
        public AudioClip[] runs_audioClip;  //符文聲音
        public AudioClip shake_audioClip;   
        public AudioClip break_audioClip;   //防護罩破碎音效
        public AudioClip knock_audioClip;   //敲門聲
        public AudioClip slecet;            //道具選取聲
        
        //聲音播放器
        private AudioSource playerSource;   //玩家音效播放器 
        private AudioSource onWorldSource;  //道具音效播放器
        private AudioSource ElfSource;      //小精靈音效播放器
        private AudioSource rainSource;     //下雨音效播放器
        private AudioSource backGroundSource;   //背景音樂播放器
        private AudioSource runsSource;     //符文音效播放器
        private AudioSource shakeSource;
        private AudioSource breakSorce;     //道具音效播放器   
        private AudioSource knockSorce;     //防護罩破碎音效播放器

        public Slot thisSlot;



        #endregion

        private void Awake()
        {
           
            #region 單例

            if (current == null)
            {
                DontDestroyOnLoad(gameObject);
                current = this;

                // 添加 AudioSource
                playerSource = gameObject.AddComponent<AudioSource>();
                onWorldSource = gameObject.AddComponent<AudioSource>();
                ElfSource = gameObject.AddComponent<AudioSource>();
                rainSource = gameObject.AddComponent<AudioSource>();
                backGroundSource = gameObject.AddComponent<AudioSource>();
                runsSource = gameObject.AddComponent<AudioSource>();
                shakeSource = gameObject.AddComponent<AudioSource>();
                breakSorce = gameObject.AddComponent<AudioSource>();
                knockSorce = gameObject.AddComponent<AudioSource>();

                string sceneName = SceneManager.GetActiveScene().name;
                switch (sceneName)
                {
                    case "C-第一關_山洞":
                        current.backGroundSource.clip = current.background_audioClip[0];
                        Debug.Log($"山洞音樂匹配成功");
                        break;
                    case "B-序章_符文":
                        current.backGroundSource.clip = current.background_audioClip[2];
                        Debug.Log($"音樂匹配成功{SceneManager.GetActiveScene().name}");
                        break;
                    default:
                        current.backGroundSource.clip = current.background_audioClip[1];
                        break;
                }
                current.backGroundSource.Play();
                current.backGroundSource.loop = true;
            }
            else if (current != null)
            {
                Destroy(gameObject);
            }

            #endregion

        }

        #region 方法區

        /// <summary>
        /// 播放腳步聲
        /// </summary>
        public static void PlayFootstepAudio()
        {            
            current.playerSource.clip = current.walk_audioClip;
            current.playerSource.Play();
            current.playerSource.volume = 0.02f;       
        }

        /// <summary>
        /// 播放下雨音效
        /// </summary>
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
            current.onWorldSource.volume = 0.5f;
        }

        /// <summary>
        /// 播放小精靈音效
        /// </summary>
        public static void ElfAudio()
        {
            current.ElfSource.clip = current.elf_audioClip;
            current.ElfSource.Play();
            current.ElfSource.volume = 0.1f;
        }
        /// <summary>
        /// 播放符文音效
        /// </summary>
        /// <param name="runsID"></param>
        public static void PlayRunsAudio(int runsID)
        {
            current.runsSource.clip = current.runs_audioClip[runsID];
            current.runsSource.Play();      
        }
        /// <summary>
        /// 播放序章震動音效
        /// </summary>
        public static void PlayShakeAudio()
        {
            current.shakeSource.clip = current.shake_audioClip;
            current.shakeSource.Play();
        }
        /// <summary>
        /// 停止播放序章震動音效
        /// </summary>
        public static void StopShakeAudio()
        {
            current.shakeSource.Stop();
        }

        public static void PlayNoiseAudio()
        {
            current.backGroundSource.Play();
            current.backGroundSource.loop = true;
        }
        /// <summary>
        /// 播放防護罩破碎音效
        /// </summary>
        public static void PlayBreakAudio()
        {
            current.breakSorce.clip = current.break_audioClip;
            current.breakSorce.Play();
        }
        /// <summary>
        /// 播放敲門聲
        /// </summary>
        public static void PlayKnockAudio()
        {
            current.knockSorce.PlayOneShot(current.knock_audioClip);
        }
        #endregion

    }

}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

        public Slot thisSlot;

        #endregion

        private void Awake()
        {
            current = this;
            DontDestroyOnLoad(gameObject);

            playerSource = gameObject.AddComponent<AudioSource>();
            onWorldSource = gameObject.AddComponent<AudioSource>();
            ElfSource = gameObject.AddComponent<AudioSource>();
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

        #endregion

    }

}
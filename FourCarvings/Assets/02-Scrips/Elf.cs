using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FourCarvings
{
    /// <summary>
    /// 插入小精靈聲音
    /// </summary>
    public class Elf : MonoBehaviour
    {
        public void ElfAudioPlay()
        {
            AudioManager.ElfAudio();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FourCarvings
{

    public class ScreenFade : MonoBehaviour
    {
        public Image maskImage;

        public float fadeDuration = 2.0f;


        public IEnumerator FadeOut()
        {

            Color startColor = maskImage.color;
            Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);

            float elapsedTime = 0.0f;
            while (elapsedTime < fadeDuration)
            {
                float normalizedTime = elapsedTime / fadeDuration;
                maskImage.color = Color.Lerp(startColor, endColor, normalizedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            maskImage.color = endColor;
        }



    }
}

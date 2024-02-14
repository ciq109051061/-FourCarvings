using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowIcon : MonoBehaviour
{
    public Image image;
    public float fadeDuration = 3.0f;

    private float currentAlpha = 0.0f;
    private float targetAlpha = 1.0f;
    private float timer = 0.0f;

    

    private void Start()
    {
        //image = GetComponent<Image>(); // 如果你没有在Inspector面板中指定Image对象，请取消这一行的注释
    }

    private void Update()
    {
        
            timer += Time.deltaTime;
            if (timer < fadeDuration)
            {
                currentAlpha = Mathf.Lerp(0.0f, 1.0f, timer / fadeDuration);
                Color newColor = image.color;
                newColor.a = currentAlpha;
                image.color = newColor;
                
            }
            else
            {
                Color newColor = image.color;
                newColor.a = targetAlpha;
                image.color = newColor;
            }
        

        StartCoroutine(ForShow());
    }


    public IEnumerator ForShow()
    {
        

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(3);
    }
}

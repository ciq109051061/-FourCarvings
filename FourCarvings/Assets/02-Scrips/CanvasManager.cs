using UnityEngine;
using UnityEngine.SceneManagement;

namespace FourCarvings
{

    public class CanvasManager : MonoBehaviour
    {
        public Canvas[] canvas;
        public bool updateCanvas = false;

        private void Update()
        {
            if (updateCanvas==true)
            {
                ShowCanvas();
            }
        }

        private void ShowCanvas()
        {
            for (int i = 0; i < canvas.Length; i++)
            {
                if (canvas[i].gameObject.GetComponent<CanvasGroup>().alpha==1)
                {
                    canvas[i].sortingOrder = 5;
                }
                else
                {
                    canvas[i].sortingOrder = 1;
                }

                Debug.Log($"這是{canvas[i].gameObject.name}，它的層級是{canvas[i].sortingOrder}");
                updateCanvas = false;
            }
        }

        public void BackToStart()
        {
            SceneManager.LoadScene(0);
        }

        public void OpenSaveOrLoad()
        {
            canvas[0].gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }
    }
}

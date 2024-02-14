using UnityEngine;

public class SceondDialogue : MonoBehaviour
{
    public static bool afterKnock = false;

    public GameObject sceondText;

    private void Start()
    {
        sceondText.gameObject.SetActive(false);

    }
    private void Update()
    {
        if (afterKnock == true)
        {
            sceondText.gameObject.SetActive(true);
        }
    }
}

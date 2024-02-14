using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SecretPaper : MonoBehaviour
{
    public CanvasGroup paperCanvas;
    public TextMeshProUGUI paperContent01;
    public TextMeshProUGUI paperContent02;

    private void Start()
    {
        paperContent01.gameObject.SetActive(false);
        paperContent02.gameObject.SetActive(false);
    }
    private void OnMouseDown()
    {
        // 獲取鼠標的螢幕座標
        Vector3 mousePosition = Input.mousePosition;

        // 使用攝影機將螢幕座標轉換為射線
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        // 創建一個 RaycastHit 來存儲射線碰到的物體的信息
        RaycastHit hit;

        // 判斷射線是否碰撞到某個物體
        if (Physics.Raycast(ray, out hit))
        {
            // 判斷碰撞到的物體的名字
            string collidedObjectName = hit.collider.gameObject.name;

            // 使用 if 來檢查碰撞到的物體的名字
            if (collidedObjectName == "信紙01")
            {
                Debug.Log("這是信紙01");
                paperCanvas.alpha = 1;
                paperContent01.gameObject.SetActive(true);
            }
            if (collidedObjectName == "信紙02")
            {
                Debug.Log("這是信紙02");
                paperCanvas.alpha = 1;
                paperContent02.gameObject.SetActive(true);
            }
        }
    }
}

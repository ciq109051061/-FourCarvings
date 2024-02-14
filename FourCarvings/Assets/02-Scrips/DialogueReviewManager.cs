using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FourCarvings
{

    public class DialogueReviewManager : MonoBehaviour
    {
        public static DialogueReviewManager Instance;
        public TextMeshProUGUI reviewText;  //顯示回顧對話文字
        public GameObject insReviewDialogue;
        public RectTransform reviewPos;
        public ScrollRect scrollView;       
        //public RectTransform content;
        //public string[] reviewDialogueRows;
        //public TextAsset forReviewText;     //接收最新文本

        private void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != null)
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (DialogueManger.Instance.dialogueDown==true)
            {
                AddCompletedDialogue();
                //UpdateReviewUI();
            }
        }

        /// <summary>
        /// 更新最新回顧對話
        /// </summary>
        /// <param name="dialogue"></param>
        public void AddCompletedDialogue()
        {
            GameObject review = Instantiate(insReviewDialogue, reviewPos);
            review.GetComponent<TextMeshProUGUI>().text = DialogueManger.Instance.dialogText.text;
            DialogueManger.Instance.dialogueDown = false;
            //reviewText.text = DialogueManger.Instance.dialogText.text;
            //UpdateReviewUI();
        }
        /*
        private void UpdateReviewUI()
        {
            
            // 讓 ScrollView 捲動到最下方，以顯示最新的內容
            StartCoroutine(ScrollToBottom());
        }

        private IEnumerator ScrollToBottom()
        {
            yield return new WaitForEndOfFrame();
            scrollView.verticalNormalizedPosition = 0f;
        }
        */
        
    }
}   

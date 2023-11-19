using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class FloatingTextSource : MonoBehaviour
    {
        [SerializeField] private float textDuration = 0.5f;

        private Animator animator;
        private TextMeshProUGUI textMesh;
        private static readonly int ReplayText = Animator.StringToHash("Replay");
        // Start is called before the first frame update
        private void Awake()
        {
            //textMesh = GetComponent<TextMeshPro>();
            textMesh = GetComponentInChildren<TextMeshProUGUI>();
            animator = GetComponentInChildren<Animator>();
        }
        void Start()
        {
            //Destroy(this, 0.5f);
            //StartCoroutine(FadeText());.
            
        }

        public void Activate(string text, Color textColor)
        {
            textMesh.text = text;
            textMesh.color = textColor;
            animator.SetBool(ReplayText, true);
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            animator.SetBool(ReplayText, true);
            gameObject.SetActive(false);
        }

        IEnumerator FadeText()
        {
            float timer = 0f;
            while (timer < textDuration)
            {
                timer += textDuration;
                textMesh.alpha = Mathf.Lerp(textMesh.color.a, 0f, timer / textDuration);
                textMesh.transform.position = Vector3.Lerp(textMesh.transform.position, 
                    textMesh.transform.position + (new Vector3(0,1,0) * 3f), 
                    timer / textDuration);
                yield return null;
            }

            textMesh.alpha = 0f;
            yield return null;
        }

    }
}


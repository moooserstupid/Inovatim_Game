using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.FloatingText
{
    public class FloatingTextManager : MonoBehaviour
    {
        public static FloatingTextManager Instance;
        [SerializeField] private FloatingTextSource[] floatingTextSources;

        private Queue<int> usageQueue;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(Instance);
            }


        }
        private void Start()
        {
            floatingTextSources = GetComponentsInChildren<FloatingTextSource>();
            usageQueue = new Queue<int>();
            for (int i = 0; i < floatingTextSources.Length; ++i)
            {
                floatingTextSources[i].Deactivate();
                usageQueue.Enqueue(i);
            }
        }

        public void ShowText(string text, Vector3 worldPosition, Color textColor)
        {
            //Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

            Debug.Log(text);
            if (usageQueue.Count > 0)
            {

                int textIndex = usageQueue.Dequeue();
                floatingTextSources[textIndex].Activate(text, textColor);
                floatingTextSources[textIndex].transform.position = worldPosition + new Vector3(0, 5f, 0);
                floatingTextSources[textIndex].transform.LookAt(Camera.main.transform.position);

                StartCoroutine(DeactivateTextRoutine(textIndex));
            }


        }

        IEnumerator DeactivateTextRoutine(int textIndex, float duration = 0.4f)
        {
            yield return new WaitForSeconds(duration);
            floatingTextSources[textIndex].Deactivate();
            usageQueue.Enqueue(textIndex);
        }
    }
}


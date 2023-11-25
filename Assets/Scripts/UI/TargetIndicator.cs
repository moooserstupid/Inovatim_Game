using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class TargetIndicator : MonoBehaviour
    {
        private Transform target;
        public float rotationSpeed;
        private bool isActive;
        void Start()
        {
            gameObject.SetActive(false);
            isActive = false;
        }

        public void SetTarget(Transform target)
        {
            gameObject.SetActive(true);
            this.target = target;
            isActive = true;
        }
        public void DeactivateTarget()
        {
            gameObject.SetActive(false);
            isActive = false;
        }
        void Update()
        {
            if (!isActive) return;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position),
                rotationSpeed * Time.deltaTime);
        }

    }
}
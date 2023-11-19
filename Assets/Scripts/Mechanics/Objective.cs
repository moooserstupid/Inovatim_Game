using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Mechanic
{
    public class Objective : MonoBehaviour
    {
        [SerializeField] private TextMeshPro textMesh;

        private void Start()
        {
            textMesh.text = gameObject.name;
        }
    }
}


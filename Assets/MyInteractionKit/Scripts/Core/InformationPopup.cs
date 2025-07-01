using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyInteractionKit
{
    public class InformationPopup : MonoBehaviour
    {
        public UnityEvent onActivation;
        public UnityEvent onDeactivation;

        [Header("Detection Settings")]
        public DetectionMethod detectionMethod = DetectionMethod.Both;

        [Header("Do Not Change")]
        public GameObject questionMark;
        public GameObject descriptionPlane;

        public enum DetectionMethod
        {
            OnTriggerEnter,
            OnMouseEnter,
            Both
        }

        private void Start()
        {
            if (descriptionPlane != null)
            {
                descriptionPlane.SetActive(false);
            }
            if (questionMark != null)
            {
                questionMark.SetActive(true);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((detectionMethod == DetectionMethod.OnTriggerEnter || detectionMethod == DetectionMethod.Both) && other.CompareTag("Player"))
            {
                InvokeDetection();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if ((detectionMethod == DetectionMethod.OnTriggerEnter || detectionMethod == DetectionMethod.Both) && other.CompareTag("Player"))
            {
                EndDetection();
            }
        }

        private void OnMouseEnter()
        {
            if (detectionMethod == DetectionMethod.OnMouseEnter || detectionMethod == DetectionMethod.Both)
            {
                InvokeDetection();
            }
        }

        private void OnMouseExit()
        {
            if (detectionMethod == DetectionMethod.OnMouseEnter || detectionMethod == DetectionMethod.Both)
            {
                EndDetection();
            }
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider == GetComponent<Collider>())
                    {
                        InvokeDetection();
                    }
                }
            }
        }

        private void InvokeDetection()
        {
            if (questionMark != null)
            {
                questionMark.SetActive(false);
            }
            if (descriptionPlane != null)
            {
                descriptionPlane.SetActive(true);
            }
            onActivation?.Invoke();
        }

        private void EndDetection()
        {
            if (questionMark != null)
            {
                questionMark.SetActive(true);
            }
            if (descriptionPlane != null)
            {
                descriptionPlane.SetActive(false);
            }
            onDeactivation?.Invoke();
        }
    }
}

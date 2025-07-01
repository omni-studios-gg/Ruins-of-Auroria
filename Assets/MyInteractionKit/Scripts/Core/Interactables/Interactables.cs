using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace MyInteractionKit
{
    public class Interactables : MonoBehaviour
    {
        private Renderer objectRenderer;

        public UnityEvent onInteract;

        bool isInteractable = false;
        public bool disableAfterInteraction = true;

        [Header("Settings")]
        public KeyCode keyToPress = KeyCode.E;
        public string descriptionText = "Open gate";

        [Header("Highlight Interactable Object")]
        public bool highlightInteractableObject = false;
        public GameObject objectToHighlight;
        public Color highlightColor = new Color(111, 108, 91, 255);
        private Material material;

        [Header("Detection Settings")]
        public DetectionMethod detectionMethod = DetectionMethod.Both;

        [Header("Do Not Change")]
        public GameObject descriptionPlane;
        public TMP_Text keyToPressTxt;
        public TMP_Text descriptionTxt;

        public enum DetectionMethod
        {
            OnTriggerEnter,
            OnMouseEnter,
            Both
        }

        private void Start()
        {
            descriptionPlane.SetActive(false);

            if (highlightInteractableObject)
            {
                if (objectToHighlight != null)
                {
                    objectRenderer = objectToHighlight.GetComponent<Renderer>();
                    material = objectRenderer.material;
                }
                else
                {
                    UnityEngine.Debug.LogError("Main Target Object is Not Assigned. Please assign a target object");
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((detectionMethod == DetectionMethod.OnTriggerEnter || detectionMethod == DetectionMethod.Both) && other.CompareTag("Player"))
            {
                ToggleInteractable(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if ((detectionMethod == DetectionMethod.OnTriggerEnter || detectionMethod == DetectionMethod.Both) && other.CompareTag("Player"))
            {
                ToggleInteractable(false);
            }
        }

        private void OnMouseEnter()
        {
            if (detectionMethod == DetectionMethod.OnMouseEnter || detectionMethod == DetectionMethod.Both)
            {
                ToggleInteractable(true);
            }
        }

        private void OnMouseExit()
        {
            if (detectionMethod == DetectionMethod.OnMouseEnter || detectionMethod == DetectionMethod.Both)
            {
                ToggleInteractable(false);
            }
        }

        public void Interact()
        {
            onInteract.Invoke();

            if (objectRenderer != null)
            {
                material.DisableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", Color.black);
            }

            Cleanup();
        }

        private void Cleanup()
        {
            if (disableAfterInteraction)
            {
                ToggleInteractable(false);

                if (TryGetComponent<Collider>(out var collider))
                {
                    collider.enabled = false;
                }

                this.enabled = false;
            }
        }

        public void ToggleInteractable(bool isEnabled)
        {
            if (isEnabled)
            {
                isInteractable = true;
                SetText(keyToPress, descriptionText);
                EnableCanvas();

                if (highlightInteractableObject)
                {
                    material.EnableKeyword("_EMISSION");
                    material.SetColor("_EmissionColor", highlightColor);
                }
            }
            else
            {
                isInteractable = false;
                SetText(KeyCode.None, "");
                DisableCanvas();

                if (highlightInteractableObject)
                {
                    material.DisableKeyword("_EMISSION");
                    material.SetColor("_EmissionColor", Color.black);
                }
            }
        }

        void SetText(KeyCode keyToPress, string descriptionText)
        {
            keyToPressTxt.text = keyToPress.ToString();
            descriptionTxt.text = descriptionText;
        }

        internal void DisableCanvas()
        {
            descriptionPlane.SetActive(false);
        }

        internal void EnableCanvas()
        {
            descriptionPlane.SetActive(true);
        }

        private void Update()
        {
            if (isInteractable)
            {
                if (Input.GetKeyDown(keyToPress))
                {
                    Interact();
                }

                // Mobile compatibility: Detect touch input
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        Interact();
                    }
                }
            }
        }

        private void OnValidate()
        {
            if (keyToPressTxt != null)
                keyToPressTxt.text = keyToPress.ToString();

            if (descriptionTxt != null)
                descriptionTxt.text = descriptionText;
        }
    }
}

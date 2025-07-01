using UnityEngine;

namespace MyInteractionKit
{
    public class Billboard : MonoBehaviour
    {
        public enum BillboardMode
        {
            LookAtCamera,
            AlignWithCameraForward
        }

        public BillboardMode mode = BillboardMode.LookAtCamera;
        private Camera mainCamera;

        void Start()
        {
            // Cache the main camera for performance.
            mainCamera = Camera.main;
        }

        void LateUpdate()
        {
            if (mainCamera == null) return;

            if (mode == BillboardMode.LookAtCamera)
            {
                // Rotate the object to always face the camera.
                transform.LookAt(mainCamera.transform);
            }
            else if (mode == BillboardMode.AlignWithCameraForward)
            {
                // Align the object's forward direction with the camera's forward direction.
                transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward);
            }
        }
    }
}


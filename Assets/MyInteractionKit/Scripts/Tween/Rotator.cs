using UnityEngine;

namespace MyInteractionKit
{
    public class Rotator : MonoBehaviour
    {
        public Vector3 rotationAxis = Vector3.up; // Axis around which the object will rotate
        public float rotationSpeed = 10f; // Speed of rotation in degrees per second
        public bool localSpace = true; // Rotate in local space (true) or world space (false)

        private bool isRotating = false; // Track whether rotation is active

        public bool startOnPlay = true;

        private void Start()
        {
            if (startOnPlay)
            {
                StartRotation();
            }
        }

        void Update()
        {
            if (isRotating)
            {
                RotateObject();
            }
        }

        void RotateObject()
        {
            if (localSpace)
            {
                // Rotate around the object's local axis
                transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
            }
            else
            {
                // Rotate around the world's axis
                transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.World);
            }
        }

        public void StartRotation()
        {
            isRotating = true;
        }

        public void StopRotation()
        {
            isRotating = false;
        }

        public void SetRotationSpeed(float newSpeed)
        {
            rotationSpeed = newSpeed;
        }

        public void SetRotationAxis(Vector3 newAxis)
        {
            rotationAxis = newAxis;
        }

        public void ToggleRotation(bool toggle)
        {
            isRotating = toggle;
        }
    }
}




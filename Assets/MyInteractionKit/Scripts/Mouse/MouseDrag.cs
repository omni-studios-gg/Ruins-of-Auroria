using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyInteractionKit
{
    public class MouseDrag : MonoBehaviour
    {
        private Camera m_Camera;
        private float cameraZDistance;


        // Start is called before the first frame update
        void Start()
        {
            m_Camera = Camera.main;
            cameraZDistance = m_Camera.WorldToScreenPoint(transform.position).z;
        }

        private void OnMouseDown()
        {
            Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraZDistance);
            Vector3 newWorldPosition = m_Camera.ScreenToWorldPoint(screenPosition);
            transform.position = newWorldPosition;
        }
       
    }
}

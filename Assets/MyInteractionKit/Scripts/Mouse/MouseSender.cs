using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyInteractionKit
{
    public class MouseSender : MonoBehaviour
    {
        Camera m_Camera;
        void Awake()
        {
            m_Camera = Camera.main;
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                Ray ray = m_Camera.ScreenPointToRay(mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    print(hit.collider.gameObject.name);
                    hit.collider.gameObject.SendMessage("LMBClick", SendMessageOptions.DontRequireReceiver);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Vector3 mousePosition = Input.mousePosition;
                Ray ray = m_Camera.ScreenPointToRay(mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    hit.collider.gameObject.SendMessage("RMBClick", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}

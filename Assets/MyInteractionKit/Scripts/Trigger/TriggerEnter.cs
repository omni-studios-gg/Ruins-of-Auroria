using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyInteractionKit
{
    [RequireComponent(typeof(Collider))]
    public class TriggerEnter : MonoBehaviour
    {
        public UnityEvent<Collider> onTriggerEnter;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != this && other.tag != "Interactor")
            {
                onTriggerEnter.Invoke(other);
            }
        }
    }
}



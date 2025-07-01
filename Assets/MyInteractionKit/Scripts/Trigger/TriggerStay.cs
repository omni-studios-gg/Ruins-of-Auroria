using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyInteractionKit
{
    [RequireComponent(typeof(Collider))]
    public class TriggerStay : MonoBehaviour
    {
        public UnityEvent<Collider> onTriggerStay;

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject != this && other.tag != "Interactor")
            {
                onTriggerStay.Invoke(other);
            }
        }
    }
}



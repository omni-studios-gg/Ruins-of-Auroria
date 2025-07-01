using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyInteractionKit{
    public class TriggerExit : MonoBehaviour
    {
        public UnityEvent<Collider> onTriggerExit;

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject != this && other.tag != "Interactor")
            {
                onTriggerExit.Invoke(other);
            }
        }
    }

}


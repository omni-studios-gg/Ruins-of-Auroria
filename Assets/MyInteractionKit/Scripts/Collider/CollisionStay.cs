using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyInteractionKit
{
    public class CollisionStay : MonoBehaviour
    {
        public UnityEvent<Collision> onCollisionStay;
        private void OnCollisionStay(Collision collision)
        {
            onCollisionStay.Invoke(collision);
        }
    }
}

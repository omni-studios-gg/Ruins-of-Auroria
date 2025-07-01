using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyInteractionKit
{
    public class CollisionEnter : MonoBehaviour
    {
        public UnityEvent<Collision> onCollisionEnter;
        private void OnCollisionEnter(Collision collision)
        {
            onCollisionEnter.Invoke(collision);
        }
    }
}

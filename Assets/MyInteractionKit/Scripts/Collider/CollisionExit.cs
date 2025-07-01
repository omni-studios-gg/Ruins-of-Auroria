using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyInteractionKit
{
    public class CollisionExit : MonoBehaviour
    {
        public UnityEvent<Collision> onCollisionExit;
        private void OnCollisionExit(Collision collision)
        {
            onCollisionExit.Invoke(collision);
        }
    }
}

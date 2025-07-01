using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyInteractionKit
{
    public class MouseReceiver : MonoBehaviour
    {
        public UnityEvent LeftMouseClicked;
        public UnityEvent RightMouseClicked;

        public void LMBClick()
        {
            LeftMouseClicked.Invoke();
        }

        public void RMBClick()
        {
            RightMouseClicked.Invoke(); 
        }

    }
}

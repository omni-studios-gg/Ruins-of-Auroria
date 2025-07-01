using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyInteractionKit
{
    public class Toggle : MonoBehaviour
    {
        public bool on = false;
        public UnityEvent isOn;
        public UnityEvent isOff;

        public void ToggleOnOff()
        {
            if (on)
            {
                on = false;
                isOff.Invoke();
            } else
            {
                on = true;
                isOn.Invoke();
            }
        }
    }
}

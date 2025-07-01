using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace MyInteractionKit
{
    public class CountdownTimer : MonoBehaviour
    {

        public bool startOnPlay = false;
        public float countdownTime = 10f; // Set the countdown time in seconds
        public float countdownInterval = 1f;

        public UnityEvent TimerUp;

        [Header("Output to text")]
        public bool outputToText; 
        public TMP_Text timerText;

        void Start()
        {
            if (outputToText)
            {
                if (timerText == null)
                {
                    UnityEngine.Debug.LogError("Output to text is enabled but text is not assigned");
                }
            }

            if (startOnPlay)
            {
                StartCoroutine(StartCountdown());
            }


        }

        public void StartTimer()
        {
            StartCoroutine(StartCountdown());
        }

        private IEnumerator StartCountdown()
        {
            float currentTime = countdownTime;

            while (currentTime > 0)
            {
                yield return new WaitForSeconds(countdownInterval);
                currentTime -= countdownInterval;
                if (outputToText)
                {
                    timerText.text = currentTime.ToString();
                }
               
            }

            TimerUp.Invoke();
        }
    }

}

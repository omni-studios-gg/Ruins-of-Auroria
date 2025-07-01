using System;
using UnityEngine;

namespace MyInteractionKit
{
    public class SimpleTween : MonoBehaviour
    {
        [Header("Start Values")]
        public Vector3 startPosition;
        public Vector3 startRotation;
        public Vector3 startScale = Vector3.one;

        [Header("End Values")]
        public Vector3 finishPosition;
        public Vector3 finishRotation;
        public Vector3 finishScale = Vector3.one;

        public float duration = 5.0f;
        public bool reversible;

        [Header("Tween Options")]
        public bool tweenPosition = true;
        public bool tweenRotation = true;
        public bool tweenScale = true;

        private float elapsedTime = 0.0f;
        private bool isTweening = false;
        private bool reverse = false;

        private void Start()
        {
            startPosition = transform.localPosition;
            startRotation = transform.localEulerAngles;
            startScale = transform.localScale;
        }

        void Update()
        {
            if (isTweening)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;

                if (tweenPosition)
                {
                    Vector3 startPos = reverse ? finishPosition : startPosition;
                    Vector3 endPos = reverse ? startPosition : finishPosition;
                    transform.localPosition = Vector3.Lerp(startPos, endPos, t);
                }

                if (tweenRotation)
                {
                    Quaternion startRot = Quaternion.Euler(reverse ? finishRotation : startRotation);
                    Quaternion endRot = Quaternion.Euler(reverse ? startRotation : finishRotation);
                    transform.localRotation = Quaternion.Lerp(startRot, endRot, t);
                }

                if (tweenScale)
                {
                    Vector3 startScl = reverse ? finishScale : startScale;
                    Vector3 endScl = reverse ? startScale : finishScale;
                    transform.localScale = Vector3.Lerp(startScl, endScl, t);
                }

                if (t >= 1.0f)
                {
                    TweenDone();
                }
            }
        }

        private void TweenDone()
        {
            elapsedTime = 0.0f;
            isTweening = false;

            if (reversible)
            {
                reverse = !reverse;
            }
        }

        public void StartTween()
        {
            elapsedTime = 0.0f;
            isTweening = true;
        }

        public void StopTween()
        {
            isTweening = false;
        }

        public void ResetToStart()
        {
            transform.localPosition = startPosition;
            transform.localRotation = Quaternion.Euler(startRotation);
            transform.localScale = startScale;

            elapsedTime = 0.0f;
            isTweening = false;
            reverse = false;
        }

        public void GoToStartPosition()
        {
            transform.localPosition = startPosition;
            transform.localRotation = Quaternion.Euler(startRotation);
            transform.localScale = startScale;
        }

        public void GoToEndPosition()
        {
            transform.localPosition = finishPosition;
            transform.localRotation = Quaternion.Euler(finishRotation);
            transform.localScale = finishScale;
        }
    }
}


using System;
using System.Collections;
using LaunchBad.ScriptableObjects;
using UnityEngine;

namespace LaunchBad.Core
{
    public class CountDownManager : MonoBehaviour
    {
        [SerializeField] private float frequency = 1f;
        public static event Action<float> OnCountDown;
        public static event Action OnCountDownFinished;
        
        private float _startTime;

        private void OnEnable()
        {
            GameManager.OnRocketChanged += StartCountDown;
            AbortButton.OnAbort += StopCountDown;
        }

        private void OnDisable()
        {
            GameManager.OnRocketChanged -= StartCountDown;
            AbortButton.OnAbort -= StopCountDown;
        }

        private void StopCountDown()
        {
            StopAllCoroutines();
        }

        private void StartCountDown(Rocket rocket)
        {
            var countDownDuration = rocket.CountDownDuration;
            OnCountDown?.Invoke(countDownDuration);
            _startTime = Time.time;
            StartCoroutine(CountDownCoroutine(countDownDuration));
        }

        private IEnumerator CountDownCoroutine(float countDownDuration)
        {
            var elapsedTime = 0f;
            while (elapsedTime < countDownDuration)
            {
                elapsedTime = Time.time - _startTime;
                OnCountDown?.Invoke(countDownDuration - elapsedTime);
                yield return new WaitForSeconds(frequency);
            }
            OnCountDownFinished?.Invoke();
        }
    }
}



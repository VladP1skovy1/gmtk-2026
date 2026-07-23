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
        
        private float _startTime;

        private void OnEnable()
        {
            GameManager.OnRocketChanged += StartCountDown;
        }
        
        private void OnDisable()
        {
            GameManager.OnRocketChanged -= StartCountDown;
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
            OnCountDown?.Invoke(0f);
        }
    }
}



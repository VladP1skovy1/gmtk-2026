using System;
using System.Collections;
using LaunchBad.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace LaunchBad.Core
{
    public class CountDownManager : MonoBehaviour
    {
        [FormerlySerializedAs("frequency")] [SerializeField] private float eventFrequency = 1f;
        [SerializeField] private TextMeshProUGUI countdownText;
        public static event Action<float> OnCountDown;
        public static event Action OnCountDownFinished;
        
        private float _startTime;
        private float _currentCountDownValue;
        private bool _isCountingDown;

        private void OnEnable()
        {
            GameManager.OnRocketChanged += StartCountDown;
            AbortButton.OnAbort += StopCountDown;
            SecurityManager.OnSecurityBreach += StopCountDown;
        }

        private void OnDisable()
        {
            GameManager.OnRocketChanged -= StartCountDown;
            AbortButton.OnAbort -= StopCountDown;
            SecurityManager.OnSecurityBreach -= StopCountDown;
        }

        private void StopCountDown(LaunchPadStatusInfo obj)
        {
            StopCountDown();
        }

        private void StopCountDown()
        {
            _isCountingDown = false;
            StopAllCoroutines();
        }

        private void Update()
        {
            if (!_isCountingDown) return;
            
            _currentCountDownValue = Mathf.Max(_currentCountDownValue - Time.deltaTime, 0f);
            countdownText.text = $"T-{_currentCountDownValue:F1}";
        }

        private void StartCountDown(Rocket rocket)
        {
            _currentCountDownValue = rocket.CountDownDuration;
            OnCountDown?.Invoke(_currentCountDownValue);
            _isCountingDown = true;
            StartCoroutine(CountDownCoroutine());
        }

        private IEnumerator CountDownCoroutine()
        {
            while (_currentCountDownValue > 0f)
            {
                OnCountDown?.Invoke(_currentCountDownValue);
                yield return new WaitForSeconds(eventFrequency);
            }
            
            OnCountDownFinished?.Invoke();
            _isCountingDown = false;
        }
    }
}



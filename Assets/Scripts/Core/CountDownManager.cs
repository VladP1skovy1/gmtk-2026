using System;
using System.Collections;
using LaunchBad.Buttons;
using LaunchBad.ScriptableObjects;
using LaunchBad.Utils;
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
        public float CurrentCountDownValue { get; private set; }

        private float _startTime;
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
            
            CurrentCountDownValue = Mathf.Max(CurrentCountDownValue - Time.deltaTime, 0f);
            countdownText.text = $"T-{CurrentCountDownValue:F1}";
        }

        private void StartCountDown(Rocket rocket)
        {
            CurrentCountDownValue = rocket.CountDownDuration;
            OnCountDown?.Invoke(CurrentCountDownValue);
            _isCountingDown = true;
            StartCoroutine(CountDownCoroutine());
        }

        private IEnumerator CountDownCoroutine()
        {
            while (CurrentCountDownValue > 0f)
            {
                OnCountDown?.Invoke(CurrentCountDownValue);
                yield return new WaitForSeconds(eventFrequency);
            }
            
            OnCountDownFinished?.Invoke();
            _isCountingDown = false;
        }
    }
}



using System;
using System.Collections;
using UnityEngine;

namespace LaunchBad.Core
{
    public class CountDownManager : MonoBehaviour
    {
        public static event Action<float> OnCountDown;
        private float _countDownDuration;
        
        public void StartCountDown(float countDownDuration)
        {
            OnCountDown?.Invoke(countDownDuration);
            StartCoroutine(CountDownCoroutine(_countDownDuration));;
        }

        private IEnumerator CountDownCoroutine(float countDownDuration)
        {
            var elapsedTime = 0f;
            while (elapsedTime < countDownDuration)
            {
                elapsedTime += Time.deltaTime;
                OnCountDown?.Invoke(countDownDuration - elapsedTime);
                yield return null;
            }
            OnCountDown?.Invoke(0f);
        }
    }
}



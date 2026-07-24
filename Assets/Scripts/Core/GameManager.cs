using System;
using LaunchBad.ScriptableObjects;
using UnityEngine;

namespace LaunchBad.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Rocket testRocket;
        public static event Action<Rocket> OnRocketChanged;
        public static event Action<Rocket, bool> OnChoiceMade;
        
        private Rocket _currentRocket;

        private void Start()
        {
            OnRocketChanged?.Invoke(testRocket);
            _currentRocket = testRocket;
        }

        private void OnEnable()
        {
            AbortButton.OnAbort += HandleAbort;
            CountDownManager.OnCountDownFinished += HandleLaunch;
        }

        private void OnDisable()
        {
            AbortButton.OnAbort -= HandleAbort;
            CountDownManager.OnCountDownFinished -= HandleLaunch;
        }

        private void HandleAbort() => MakeChoice(false);
        private void HandleLaunch() => MakeChoice(true);
        
        private void MakeChoice(bool wasLaunched)
        {
            OnChoiceMade?.Invoke(_currentRocket, wasLaunched);
        }
    }
}
using System;
using System.Collections.Generic;
using LaunchBad.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace LaunchBad.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Rocket testRocket;
        [SerializeField] private List<Rocket> rockets;

        [SerializeField] private List<Image> greenLights;
        [SerializeField] private List<Image> redLights;
        [SerializeField] private Sprite greenActiveLight;
        [SerializeField] private Sprite redActiveLight;

        [SerializeField] private int minRequiredGreenLights;
        [SerializeField] private int maxAmountRedLights;

        private int _currentGreenLightIndex;
        private int _currentRedLightIndex;

        private int _tpLaunches;
        private int _fpLaunches;


        public static event Action<Rocket> OnRocketChanged;
        public static event Action<Rocket, bool> OnChoiceMade;

        public static event Action<EndGameStates> OnGameFinished;

        private Rocket _currentRocket;
        private int _currentRocketIndex;

        private void Start()
        {
            _currentGreenLightIndex = 0;
            _currentRedLightIndex = 0;

            _currentRocketIndex = 0;
            _currentRocket = rockets[_currentRocketIndex];
            OnRocketChanged?.Invoke(_currentRocket);
        }

        private void HandleNextRocket()
        {
            OnRocketChanged?.Invoke(_currentRocket);
        }
        
        private void CheckFinishRequirements(bool wasLaunched)
        {
            if (_fpLaunches >= maxAmountRedLights)
            {
                OnGameFinished?.Invoke(EndGameStates.AllRedLights);
            }
            else if (_tpLaunches >= minRequiredGreenLights)
            {
                OnGameFinished?.Invoke(EndGameStates.AllGreenLights);
            }
            else if (_currentRocketIndex >= rockets.Count)
            {
                OnGameFinished?.Invoke(EndGameStates.NotEnoughGreenLights);
            }
            else
            {
                _currentRocket = rockets[_currentRocketIndex];
                OnChoiceMade?.Invoke(_currentRocket, wasLaunched);
            }
        }
        
        private void MakeChoice(bool wasLaunched)
        {
            switch (_currentRocket.ShouldBeLaunched)
            {
                case true when wasLaunched:
                    greenLights[_currentGreenLightIndex].sprite = greenActiveLight;
                    _currentGreenLightIndex++;
                    _tpLaunches++;
                    break;
                case false when wasLaunched:
                    redLights[_currentRedLightIndex].sprite = redActiveLight;
                    _currentRedLightIndex++;
                    _fpLaunches++;
                    break;
                case true when !wasLaunched:
                    _currentGreenLightIndex++;
                    break;
            }
            _currentRocketIndex++;
            CheckFinishRequirements(wasLaunched);
        }

        private void HandleAbort() => MakeChoice(false);
        private void HandleLaunch() => MakeChoice(true);
        
        private void OnEnable()
        {
            AbortButton.OnAbort += HandleAbort;
            CountDownManager.OnCountDownFinished += HandleLaunch;
            NextRocketButton.OnNextRocketClicked += HandleNextRocket;
        }

        private void OnDisable()
        {
            AbortButton.OnAbort -= HandleAbort;
            CountDownManager.OnCountDownFinished -= HandleLaunch;
            NextRocketButton.OnNextRocketClicked -= HandleNextRocket;
        }
    }
}
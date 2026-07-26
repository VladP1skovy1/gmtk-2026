using System;
using System.Collections;
using System.Collections.Generic;
using LaunchBad.Buttons;
using LaunchBad.ScriptableObjects;
using LaunchBad.Utils;
using LaunchBad.Windows;
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
        
        [SerializeField] private NewRulesWindow newRulesWindow;

        private int _currentGreenLightIndex;
        private int _currentRedLightIndex;

        private int _tpLaunches;
        private int _fpLaunches;
        
        public static event Action<Rocket> OnRocketChanged;
        public static event Action<Rocket, bool> OnChoiceMade;
        public static event Action<int> OnNewLaunch;
        public static event Action<EndGameStates> OnGameFinished;
        
        public static event Action<bool, Action> OnRocketAnimation;

        private Rocket _currentRocket;
        private int _currentRocketIndex;

        private void Start()
        {
            _currentGreenLightIndex = 0;
            _currentRedLightIndex = 0;

            _currentRocketIndex = 0;
            _currentRocket = rockets[_currentRocketIndex];
            HandleNextRocket();
        }

        private void HandleNextRocket()
        {
            OnNewLaunch?.Invoke(_currentRocketIndex);
            newRulesWindow.Show(() => OnRocketChanged?.Invoke(_currentRocket));
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
                OnChoiceMade?.Invoke(_currentRocket, wasLaunched);
                _currentRocket = rockets[_currentRocketIndex];
            }
        }
        
        private IEnumerator MakeChoice(bool wasLaunched)
        {
            bool animationFinished = false;

            switch (_currentRocket.ShouldBeLaunched)
            {
                case true when wasLaunched:
                    OnRocketAnimation?.Invoke(true, () => animationFinished = true);
                    yield return new WaitUntil(() => animationFinished);

                    greenLights[_currentGreenLightIndex].sprite = greenActiveLight;
                    _currentGreenLightIndex++;
                    _tpLaunches++;
                    break;

                case false when wasLaunched:
                    OnRocketAnimation?.Invoke(false, () => animationFinished = true);
                    yield return new WaitUntil(() => animationFinished);

                    redLights[_currentRedLightIndex].sprite = redActiveLight;
                    _currentRedLightIndex++;
                    _fpLaunches++;
                    break;
            }

            _currentRocketIndex++;
            CheckFinishRequirements(wasLaunched);
        }

        private void HandleAbort() => StartCoroutine(MakeChoice(false));
        private void HandleLaunch() => StartCoroutine(MakeChoice(true));
        
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
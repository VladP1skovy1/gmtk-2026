using System;
using LaunchBad.Core;
using LaunchBad.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace LaunchBad.Buttons
{
    public class AbortButton : MonoBehaviour
    {
        public static event Action OnAbort;
        
        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(Abort);
            GameManager.OnNewLaunch += HandleNewLaunch;
            GameManager.OnChoiceMade += HandleChoiceMade;
            GameManager.OnRocketChanged += HandleRocketChanged;
            GameManager.OnRocketAnimation += HandleRocketAnimation;
        }

        private void HandleRocketAnimation(bool arg1, Action arg2)
        {
            _button.interactable = false;
        }

        private void HandleRocketChanged(Rocket obj)
        {
            _button.interactable = true;
        }

        private void HandleChoiceMade(Rocket arg1, bool arg2)
        {
            _button.interactable = false;
        }

        private void HandleNewLaunch(int obj)
        {
            _button.interactable = false;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Abort);
            GameManager.OnNewLaunch -= HandleNewLaunch;
            GameManager.OnChoiceMade -= HandleChoiceMade;
            GameManager.OnRocketChanged -= HandleRocketChanged;
            GameManager.OnRocketAnimation -= HandleRocketAnimation;
        }

        private void Abort()
        {
            OnAbort?.Invoke();
        }
    }
}

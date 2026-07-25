using System;
using UnityEngine;
using UnityEngine.UI;

namespace LaunchBad.Rules
{
    public class RuleTabButton : MonoBehaviour
    {
        private Button _button;
        private int _tabIndex;
        
        public static event Action<int> OnTabSelected;
        
        public static void ResetTabSelection()
        {
            OnTabSelected?.Invoke(-1);
        }

        public void Initialize(int tabIndex)
        {
            _tabIndex = tabIndex;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OpenTab);
            OnTabSelected += HandleTabSelected;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OpenTab);
            OnTabSelected -= HandleTabSelected;
        }

        private void HandleTabSelected(int selectedTabIndex)
        {
            _button.interactable = selectedTabIndex != _tabIndex;
        }

        private void OpenTab()
        {
            OnTabSelected?.Invoke(_tabIndex);
        }
    }
}
using System;
using System.Collections.Generic;
using LaunchBad.Rules;
using LaunchBad.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LaunchBad.Windows
{
    public class NewRulesWindow : MonoBehaviour
    {
        [SerializeField] private Button okButton;
        [SerializeField] private GameObject ruleText;
        [SerializeField] private Transform rulesContainer;
        
        private Action _onConfirmAction;
        private Window _window;
        
        private void Awake()
        {
            okButton.onClick.AddListener(OnOkButtonClicked);
            _window = GetComponent<Window>();
        }
        
        public void Show(Action onConfirm)
        {
            _onConfirmAction = onConfirm;
            _window.Show();
        }

        private void OnOkButtonClicked()
        {
            _onConfirmAction?.Invoke();
            _window.Hide();
        }

        public void SetNewRules(List<Rule> newRules)
        {
            foreach (Transform child in rulesContainer)
            {
                Destroy(child.gameObject);
            }

            foreach (var rule in newRules)
            {
                var ruleTextObject = Instantiate(ruleText, rulesContainer);
                var textComponent = ruleTextObject.GetComponent<TextMeshProUGUI>();
                textComponent.text = rule.Text;
            }
        }
    }
}

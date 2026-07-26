using System.Collections.Generic;
using System.Linq;
using LaunchBad.Core;
using LaunchBad.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace LaunchBad.Rules
{
    public class RuleBook : MonoBehaviour
    {
        [FormerlySerializedAs("rules")] [SerializeField] private List<RuleTab> ruleTabs;
        [Header("Prefabs")]
        [SerializeField] private GameObject ruleTabPrefab;
        [SerializeField] private GameObject ruleTabButtonPrefab;
        [SerializeField] private GameObject ruleTextPrefab;
        [Header("References")]
        [SerializeField] private Transform ruleTabButtonsContainer;
        [SerializeField] private Transform ruleTabsContainer;
        [SerializeField] private NewRulesWindow newRulesWindow;
        [Header("Settings")]
        [SerializeField] private float tabButtonHeight;
        
        private float _tabButtonWidth;
        private List<Rule> _newRules = new List<Rule>();
        
        private void Awake()
        {
            _tabButtonWidth = GetComponent<RectTransform>().sizeDelta.x / ruleTabs.Count;
        }

        private void OnEnable()
        {
            GameManager.OnNewLaunch += CreateRulebook;
        }
        
        private void OnDisable()
        {
            GameManager.OnNewLaunch -= CreateRulebook;
        }

        private void CreateRulebook(int currentLaunchIndex)
        {
            _newRules.Clear();
            
            for (var i = 0; i < ruleTabs.Count; i++)
            {
                CreateRuleTab(ruleTabs[i], i, currentLaunchIndex);
            }

            newRulesWindow.SetNewRules(_newRules);
            RuleTabButton.ResetTabSelection();
        }

        private void CreateRuleTab(RuleTab ruleTab, int tabIndex, int currentLaunchIndex)
        {
            var tabObject = Instantiate(ruleTabPrefab, ruleTabsContainer);
            var tabButtonObject = Instantiate(ruleTabButtonPrefab, ruleTabButtonsContainer);
            
            var tabButton = tabButtonObject.GetComponent<RuleTabButton>();
            tabButton.Initialize(tabIndex);
            
            var tabPanel = tabObject.GetComponent<RuleTabPanel>();
            tabPanel.Initialize(tabIndex);
            
            tabButtonObject.GetComponentInChildren<TextMeshProUGUI>().text = ruleTab.TabName;
            
            SetButtonTransform(tabButtonObject.GetComponent<RectTransform>(), tabIndex);
            
            FillRuleTab(tabObject, ruleTab.Rules, currentLaunchIndex);
        }

        private void FillRuleTab(GameObject tab, List<Rule> rules, int currentLaunchIndex)
        {
            var contentTransform = tab.GetComponent<ScrollRect>().content;

            foreach (var rule in rules.Where(rule => rule.LaunchNumber <= currentLaunchIndex))
            {
                CreateRuleText(rule, contentTransform);
                if (rule.LaunchNumber != currentLaunchIndex) continue;
                _newRules.Add(rule);
            }
        }

        private void CreateRuleText(Rule rule, RectTransform contentTransform)
        {
            var ruleTextObject = Instantiate(ruleTextPrefab, contentTransform);
            var ruleTextComponent = ruleTextObject.GetComponent<TextMeshProUGUI>();
            ruleTextComponent.text = $"- {rule.Text}";
        }

        private void SetButtonTransform(RectTransform buttonTransform, int index)
        {
            buttonTransform.sizeDelta = new Vector2(_tabButtonWidth, tabButtonHeight);
            buttonTransform.anchoredPosition = new Vector2(index * _tabButtonWidth + _tabButtonWidth / 2, 0);
        }
    }
}

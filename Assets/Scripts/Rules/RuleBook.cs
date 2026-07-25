using System.Collections.Generic;
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
        
        private float _tabButtonWidth;
        
        private void Awake()
        {
            _tabButtonWidth = 1f / ruleTabs.Count;
            
            for (var i = 0; i < ruleTabs.Count; i++)
            {
                CreateRuleTab(ruleTabs[i], i);
            }
            
            RuleTabButton.ResetTabSelection();
        }

        private void CreateRuleTab(RuleTab ruleTab, int index)
        {
            var tabObject = Instantiate(ruleTabPrefab, ruleTabsContainer);
            var tabButtonObject = Instantiate(ruleTabButtonPrefab, ruleTabButtonsContainer);
            
            var tabButton = tabButtonObject.GetComponent<RuleTabButton>();
            tabButton.Initialize(index);
            
            var tabPanel = tabObject.GetComponent<RuleTabPanel>();
            tabPanel.Initialize(index);
            
            tabButtonObject.GetComponentInChildren<TextMeshProUGUI>().text = ruleTab.TabName;
            
            SetButtonTransform(tabButtonObject.GetComponent<RectTransform>(), index);
            
            FillRuleTab(tabObject, ruleTab.Rules);
        }

        private void FillRuleTab(GameObject tab, List<Rule> rules)
        {
            var contentTransform = tab.GetComponent<ScrollRect>().content;
            
            foreach (var rule in rules)
            {
                CreateRuleText(rule, contentTransform);
            }
        }

        private void CreateRuleText(Rule rule, RectTransform contentTransform)
        {
            var ruleTextObject = Instantiate(ruleTextPrefab, contentTransform);
            var ruleTextComponent = ruleTextObject.GetComponent<TextMeshProUGUI>();
            ruleTextComponent.text = rule.Text;
        }

        private void SetButtonTransform(RectTransform buttonTransform, int index)
        {
            var buttonAnchorsX = new Vector2(index * _tabButtonWidth, (index + 1) * _tabButtonWidth);
            buttonTransform.anchorMin = new Vector2(buttonAnchorsX.x, 0);
            buttonTransform.anchorMax = new Vector2(buttonAnchorsX.y, 1);
        }
    }
}

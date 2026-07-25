using UnityEngine;

namespace LaunchBad.Rules
{
    public class RuleTabPanel : MonoBehaviour
    {
        private int _tabIndex;
        
        public void Initialize(int tabIndex)
        {
            _tabIndex = tabIndex;
        }
        
        private void Awake()
        {
            RuleTabButton.OnTabSelected += HandleTabSelected;
        }

        private void OnDestroy()
        {
            RuleTabButton.OnTabSelected -= HandleTabSelected;
        }

        private void HandleTabSelected(int index)
        {
            gameObject.SetActive(index == _tabIndex);
        }
    }
}

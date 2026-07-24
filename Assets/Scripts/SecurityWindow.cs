using LaunchBad.UI;
using TMPro;
using UnityEngine;

namespace LaunchBad
{
    public class SecurityWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI securityMessage;
        
        private Window _window;
        
        private void Awake()
        {
            _window = GetComponent<Window>();
        }

        private void OnEnable()
        {
            SecurityManager.OnSecurityBreach += HandleSecurityBreach;
        }
        
        private void OnDisable()
        {
            SecurityManager.OnSecurityBreach -= HandleSecurityBreach;
        }

        private void HandleSecurityBreach(LaunchPadStatusInfo info)
        {
            securityMessage.text = info.message;
            _window.Show();   
        }
    }
}

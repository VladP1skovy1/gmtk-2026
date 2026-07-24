using UnityEngine;
using UnityEngine.UI;

namespace LaunchBad.UI
{
    public class WindowButton : MonoBehaviour
    {
        [SerializeField] private Canvas windowCanvas;
        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ToggleWindow);
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(ToggleWindow);
        }

        private void ToggleWindow()
        {
            windowCanvas.enabled = !windowCanvas.enabled;
        }
    }
}

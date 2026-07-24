using System;
using UnityEngine;
using UnityEngine.UI;

namespace LaunchBad.Core
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
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Abort);
        }

        private void Abort()
        {
            OnAbort?.Invoke();
        }
    }
}

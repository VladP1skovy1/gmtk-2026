using System;
using UnityEngine;

namespace LaunchBad
{
    public class NextRocketButton : MonoBehaviour
    {
        public static event Action OnNextRocketClicked;

        public void OnButtonClick()
        {
            
            OnNextRocketClicked?.Invoke();
        }
    }
}
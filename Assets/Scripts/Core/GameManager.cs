using System;
using LaunchBad.ScriptableObjects;
using UnityEngine;

namespace LaunchBad.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Rocket testRocket;

        public static event Action<Rocket> OnRocketChanged;

        private void Start()
        {
            OnRocketChanged?.Invoke(testRocket);
        }
    }
}
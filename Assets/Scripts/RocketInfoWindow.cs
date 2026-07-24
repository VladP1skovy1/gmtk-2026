using LaunchBad.Core;
using LaunchBad.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace LaunchBad
{
    public class RocketInfoWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI temperatureText;
        [SerializeField] private TextMeshProUGUI windText;
        [SerializeField] private TextMeshProUGUI specialInstructionsText;

        private void OnEnable()
        {
            GameManager.OnRocketChanged += HandleRocketChanged;
        }
        
        private void OnDisable()
        {
            GameManager.OnRocketChanged -= HandleRocketChanged;
        }

        private void HandleRocketChanged(Rocket rocket)
        {
            nameText.text = rocket.RocketName;
            temperatureText.text = $"Temperature Range: {rocket.TemperatureRange.x}-{rocket.TemperatureRange.y} °C";
            windText.text = $"Wind: {rocket.WindRange.y} m/s";
            specialInstructionsText.text = $"{rocket.SpecialInstructions}";
        }
    }
}

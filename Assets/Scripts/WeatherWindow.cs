using LaunchBad.UI;
using UnityEngine;
using UnityEngine.UI;

namespace LaunchBad
{
    public class WeatherWindow : MonoBehaviour
    {
        [SerializeField] private RocketSlider temperatureSlider;
        [SerializeField] private RocketSlider windSlider;
        [SerializeField] private Image skyImage;
        [SerializeField] private Sprite clearSkySprite;
        [SerializeField] private Sprite cloudySkySprite;
        [SerializeField] private Sprite stormSkySprite;
        
        [SerializeField] private float maxTemperature;
        [SerializeField] private float maxWindSpeed;

        private void SetSky(SkyStatus status)
        {
            skyImage.sprite = status switch
            {
                SkyStatus.Clear => clearSkySprite,
                SkyStatus.Cloudy => cloudySkySprite,
                SkyStatus.Storm => stormSkySprite,
                _ => skyImage.sprite
            };
        }
        
        private void SetWindTemperature(float wind, float temperature)
        {
            windSlider.SetValue(wind / maxWindSpeed);
            temperatureSlider.SetValue(temperature / maxTemperature);
        }

        private void OnEnable()
        {
            WeatherController.OnSkyStatusChanged += SetSky;
            WeatherController.OnWindTemperatureChanged += SetWindTemperature;
            SetSky(WeatherController.CurrentSkyStatus);
           
        }

        private void OnDisable()
        {
            WeatherController.OnSkyStatusChanged -= SetSky;
            WeatherController.OnWindTemperatureChanged -= SetWindTemperature;
        }
    }
}
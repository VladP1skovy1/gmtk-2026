using System;
using LaunchBad.Core;
using LaunchBad.ScriptableObjects;
using LaunchBad.Utils;
using UnityEngine;

namespace LaunchBad
{
    public class WeatherController : MonoBehaviour
    {
        public static event Action<SkyStatus> OnSkyStatusChanged;
        public static event Action<float, float> OnWindTemperatureChanged;
        public static SkyStatus CurrentSkyStatus { get; private set; }
        
        private Timetable<SkyStatus> _skyTimetable;
        private Timetable<float> _windTimetable;
        private Timetable<float> _temperatureTimetable;
        
        private float _startTime;
        private float _duration;
        private bool _isRunning;
        
        private void Update()
        {
            if (!_isRunning) return;

            var currentCountDownTime = Mathf.Max(_duration - (Time.time - _startTime), 0f);

            var wind = _windTimetable.GetValueAtTime(currentCountDownTime);
            var temperature = _temperatureTimetable.GetValueAtTime(currentCountDownTime);
            OnWindTemperatureChanged?.Invoke(wind, temperature);

            if (currentCountDownTime <= 0f) _isRunning = false;
        }

        private void OnRocketChange(Rocket rocket)
        {
            _skyTimetable = rocket.SkyTimetable;
            _windTimetable = rocket.WindTimetable;
            _temperatureTimetable = rocket.TemperatureTimetable;
            
            _duration = rocket.CountDownDuration;
            _startTime = Time.time;
            _isRunning = true;
        }

        private void OnCountDown(float time)
        {
            if (_skyTimetable == null) return;
            var newStatus = _skyTimetable.GetValueAtTime(time);
            if (newStatus == CurrentSkyStatus) return;
            
            CurrentSkyStatus = newStatus;
            OnSkyStatusChanged?.Invoke(CurrentSkyStatus);
        }

        private void OnEnable()
        {
            CountDownManager.OnCountDown += OnCountDown;
            GameManager.OnRocketChanged += OnRocketChange;
        }

        private void OnDisable()
        {
            CountDownManager.OnCountDown -= OnCountDown;
            GameManager.OnRocketChanged -= OnRocketChange;
        }
    }
}
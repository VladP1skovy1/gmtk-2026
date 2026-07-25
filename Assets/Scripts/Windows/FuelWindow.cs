using System.Collections.Generic;
using LaunchBad.Core;
using LaunchBad.ScriptableObjects;
using LaunchBad.UI;
using LaunchBad.Utils;
using UnityEngine;

namespace LaunchBad.Windows
{
    public class FuelWindow : MonoBehaviour
    {
        [SerializeField] private List<RocketSlider> sliders;
        [SerializeField] private CountDownManager countDownManager;
        private List<FuelTank> _fuelTanks;
        
        private void Update()
        {
            if (_fuelTanks == null) return;
            UpdateTanks();
        }

        private void SetTanks(Rocket rocket)
        {
            _fuelTanks = rocket.FuelTanks;
            for (var i = 0; i < sliders.Count; i++)
            {
                if (i >= _fuelTanks.Count)
                {
                    sliders[i].gameObject.SetActive(false);
                    continue;
                }
                
                sliders[i].gameObject.SetActive(true);
                sliders[i].SetPointer(rocket.FuelTanks[i].requiredFuelAmount);
            }
        }

        private void UpdateTanks()
        {
            for (var i = 0; i < _fuelTanks.Count; i++)
            {
                sliders[i].SetValue(_fuelTanks[i].fuelTimetable.GetValueAtTime(countDownManager.CurrentCountDownValue));
            }
        }
        
        private void OnRocketChange(Rocket rocket)
        {
            SetTanks(rocket);
        }

        private void OnEnable()
        {
            GameManager.OnRocketChanged += OnRocketChange;
        }

        private void OnDisable()
        {
            GameManager.OnRocketChanged -= OnRocketChange;
        }
    }
}
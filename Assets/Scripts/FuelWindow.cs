using System.Collections.Generic;
using LaunchBad.Core;
using LaunchBad.ScriptableObjects;
using LaunchBad.UI;
using UnityEngine;

namespace LaunchBad
{
    public class FuelWindow : MonoBehaviour
    {
        [SerializeField] private List<RocketSlider> sliders;
        private List<FuelTank> _fuelTanks;


        private void Update()
        {
            if (_fuelTanks == null) return;
            UpdateTanks();
        }

        private void SetTanks(Rocket rocket)
        {
            _fuelTanks = rocket.FuelTanks;
            for (var i = 0; i < rocket.FuelTanks.Count; i++)
            {
                _fuelTanks[i].CurrentFuelAmount = _fuelTanks[i].InitialFuelAmount;
                _fuelTanks[i].isLeaking = false;
                sliders[i].SetValue(rocket.FuelTanks[i].CurrentFuelAmount);
                sliders[i].SetPointer(rocket.FuelTanks[i].RequiredFuelAmount);
                sliders[i].gameObject.SetActive(true);
            }
        }

        private void UpdateTanks()
        {
            for (var i = 0; i < _fuelTanks.Count; i++)
            {
                if (!_fuelTanks[i].isLeaking) continue;
                _fuelTanks[i].CurrentFuelAmount -= _fuelTanks[i].FuelLeakSpeed * Time.deltaTime;
                sliders[i].SetValue(_fuelTanks[i].CurrentFuelAmount);
            }
        }

        private void CheckTanks(float time)
        {
            if (_fuelTanks == null) return;

            for (var i = 0; i < _fuelTanks.Count; i++)
            {
                if (time <= _fuelTanks[i].FuelLeakStartTime)
                {
                    _fuelTanks[i].isLeaking = true;
                }
            }
        }


        private void OnCountDown(float time)
        {
            CheckTanks(time);
        }

        private void OnRocketChange(Rocket rocket)
        {
            SetTanks(rocket);
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
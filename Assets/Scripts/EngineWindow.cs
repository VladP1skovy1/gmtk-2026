using System.Collections.Generic;
using LaunchBad.Core;
using LaunchBad.ScriptableObjects;
using LaunchBad.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace LaunchBad
{
    public class EngineWindow : MonoBehaviour
    {
        [SerializeField] private Image engineImage;

        private EngineStatus _currentStatus = EngineStatus.Off;
        private List<EngineSpriteMapping> _engineSpriteMappings;
        private DiscreteTimetable<EngineStatus> _engineTimetable;

        private void OnEnable()
        {
            GameManager.OnRocketChanged += OnRocketChanged;
            CountDownManager.OnCountDown += OnCountDown;
        }
        
        private void OnDisable()
        {
            GameManager.OnRocketChanged -= OnRocketChanged;
            CountDownManager.OnCountDown -= OnCountDown;
        }

        private void OnCountDown(float time)
        {
            if (_engineTimetable == null) return;
            var status = _engineTimetable.GetValueAtTime(time);
            if (status == _currentStatus) return;
            _currentStatus = status;
            engineImage.sprite = _engineSpriteMappings.Find(mapping => mapping.status == status).sprite;
        }

        private void OnRocketChanged(Rocket rocket)
        {
            _engineSpriteMappings = rocket.EngineSpriteMappings;
            _engineTimetable = rocket.EngineTimetable;
        }
        
        
    }
}

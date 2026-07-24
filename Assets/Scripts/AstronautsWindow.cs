using System.Collections.Generic;
using LaunchBad.Core;
using LaunchBad.ScriptableObjects;
using LaunchBad.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace LaunchBad
{
    public class AstronautsWindow : MonoBehaviour
    {
        [SerializeField] private List<Image> astronautsImages;
        private List<AstronautSpriteMapping> _astronautsSpriteMappings;
        private List<DiscreteTimetable<AstronautStatus>> _astronautsTimetables;
        private List<AstronautStatus?> _currentStatuses;
        
        private void OnCountDown(float time)
        {
           
            if (_astronautsTimetables == null) return;
            for (int i = 0; i < astronautsImages.Count; i++)
            {
                var status = _astronautsTimetables[i].GetValueAtTime(time);
                if (status == _currentStatuses[i]) continue;
                _currentStatuses[i] = status;
                astronautsImages[i].sprite = _astronautsSpriteMappings.Find(mapping => mapping.status == status).sprite;
            }
        }
        
        
        private void OnRocketChange(Rocket rocket)
        {
            
            _astronautsSpriteMappings = rocket.AstronautSpriteMappings;
            _astronautsTimetables = rocket.AstronautsTimetables;
            
            _currentStatuses = new List<AstronautStatus?>();
            for (int i = 0; i < astronautsImages.Count; i++)
            {
                _currentStatuses.Add(null);
            }
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

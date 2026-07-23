using UnityEngine;
using UnityEngine.UI;

namespace LaunchBad.UI
{
    public class RocketSlider : MonoBehaviour
    {
        [SerializeField] private RectTransform pointer;
        private Slider _slider;
        private Vector2 _anchorX;
        
        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _anchorX = new Vector2(pointer.anchorMin.x, pointer.anchorMax.x);
        }

        public void SetValue(float value)
        {
            _slider.value = value;
        }

        public void SetPointer(float value)
        {
            pointer.anchorMin = new Vector2(_anchorX.x, value);
            pointer.anchorMax = new Vector2(_anchorX.y, value);
        }
    }
}
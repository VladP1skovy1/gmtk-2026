using UnityEngine;
using UnityEngine.EventSystems;

namespace LaunchBad.UI
{
    public class Window : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler
    {
        private RectTransform _rectTransform;
        private Vector3 _offset;
        private Canvas _canvas;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
        }

        public void Hide()
        {
            _canvas.enabled = false;
        }

        public void ToggleVisibility()
        {
            _canvas.enabled = !_canvas.enabled;
            if (!_canvas.enabled) return;
            transform.SetAsLastSibling();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, eventData.position,
                eventData.pressEventCamera, out var globalMousePos);

            _offset = _rectTransform.position - globalMousePos;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, eventData.position,
                    eventData.pressEventCamera, out var globalMousePos))
            {
                _rectTransform.position = globalMousePos + _offset;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.SetAsLastSibling();
        }
    }
}
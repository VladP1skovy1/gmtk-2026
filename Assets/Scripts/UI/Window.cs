using UnityEngine;
using UnityEngine.EventSystems;

namespace LaunchBad.UI
{
    public class Window : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        private RectTransform _rectTransform;
        private Vector3 _offset;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
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
    }
}
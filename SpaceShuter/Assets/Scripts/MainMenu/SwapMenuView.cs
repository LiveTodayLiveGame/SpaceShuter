using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LiveToday
{
    public class SwapMenuView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        public event Action<bool, bool> OnEndDragEvent;
        public void OnBeginDrag(PointerEventData eventData)
        {
            OnEndDragEvent?.Invoke(true, false);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnEndDragEvent?.Invoke(false, true);
        }
    }
}

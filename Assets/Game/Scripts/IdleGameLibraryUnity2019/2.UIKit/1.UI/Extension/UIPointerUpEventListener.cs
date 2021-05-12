namespace QFramework
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;

    public class UIPointerUpEventListener : MonoBehaviour, IPointerUpHandler
    {
        public UnityAction<PointerEventData> OnPointerUpEvent;

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (OnPointerUpEvent != null) OnPointerUpEvent(eventData);
        }

        public static UIPointerUpEventListener CheckAndAddListener(GameObject obj)
        {
            UIPointerUpEventListener listener = obj.GetComponent<UIPointerUpEventListener>();
            if (listener == null) listener = obj.AddComponent<UIPointerUpEventListener>();

            return listener;
        }

        public static UIPointerUpEventListener Get(GameObject obj)
        {
            return CheckAndAddListener(obj);
        }

        void OnDestroy()
        {
            OnPointerUpEvent = null;
        }
    }
}
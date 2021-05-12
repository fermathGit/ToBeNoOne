namespace QFramework
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    
    public class UIPointerDownEventListener : MonoBehaviour,IPointerDownHandler
    {
        public UnityAction<PointerEventData> OnPointerDownEvent;

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (OnPointerDownEvent != null) OnPointerDownEvent(eventData);
        }

        public static UIPointerDownEventListener CheckAndAddListener(GameObject obj)
        {
            UIPointerDownEventListener listener = obj.GetComponent<UIPointerDownEventListener>();
            if (listener == null) listener = obj.AddComponent<UIPointerDownEventListener>();

            return listener;
        }
        public static  UIPointerDownEventListener Get(GameObject obj)
        {
            return CheckAndAddListener (obj);
        }
        
        void OnDestroy()
        {
            OnPointerDownEvent = null;
        }
    }
}
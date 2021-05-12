namespace QFramework
{
    using UnityEngine;
    
	public static class UIRectTransformExtension
    {
        public static Vector2 GetLocalPosInRect(this RectTransform selfRectTrans, Camera camera = null)
        {
            Vector2 retLocalPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(selfRectTrans, Input.mousePosition,
                camera, out retLocalPos);
            return retLocalPos;
        }

        public static bool InRect(this RectTransform selfRectTrans, Camera camera = null)
        {
            if (null == camera)
                camera = UIManager.Instance.RootCanvas.worldCamera;
 
            return RectTransformUtility.RectangleContainsScreenPoint(selfRectTrans, Input.mousePosition, camera);
        }
 
        public static Vector2 ToScreenPoint(this RectTransform selfRectTrans)
        {
            return RectTransformUtility.WorldToScreenPoint(UIManager.Instance.RootCanvas.worldCamera, selfRectTrans.position);
        }
 
        public static Vector2 ToScreenPoint(this RectTransform selfRectTrans, Vector2 worldPos)
        {
            return RectTransformUtility.WorldToScreenPoint(UIManager.Instance.RootCanvas.worldCamera, worldPos);
        }
 
        public static bool InRootTransRect(this RectTransform selfRectTrans, RectTransform rootTrans, Camera camera = null)
        {
            if (null == camera)
                camera = UIManager.Instance.RootCanvas.worldCamera;
            return RectTransformUtility.RectangleContainsScreenPoint(rootTrans, selfRectTrans.ToScreenPoint(), camera);
        }
 
        public static Vector2 ConvertWorldPosToLocalPosInSelf(this RectTransform selfRectTrans, Vector2 worldPos)
        {
            var screenPos = RectTransformUtility.WorldToScreenPoint(QUICameraUtil.UICamera, worldPos);
            Vector2 retLocalPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(selfRectTrans, screenPos, QUICameraUtil.UICamera,
                out retLocalPos);
            return retLocalPos;
        }
	}
}
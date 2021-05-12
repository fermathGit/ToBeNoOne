using UnityEngine;

namespace QFramework
{
	public static class QUICameraUtil 
	{
		public static Camera UICamera
		{
			get { return UIManager.Instance.UICamera; }
		}
		
		public static void SetPerspectiveMode()
		{
			UICamera.orthographic = false;
		}

		public static void SetOrthographicMode()
		{
			UICamera.orthographic = true;
		}

		public static Texture2D CaptureCamera(Rect rect)
		{
			var camera = UICamera;
			RenderTexture rt = new RenderTexture(Screen.width,Screen.height,0);
			camera.targetTexture = rt;
			camera.Render();

			RenderTexture.active = rt;

			Texture2D screenShot = new Texture2D((int) rect.width, (int) rect.height, TextureFormat.RGB24, false);
			screenShot.ReadPixels(rect,0,0);
			screenShot.Apply();

			camera.targetTexture = null;
			RenderTexture.active = null;
			rt.Release();
			Object.Destroy(rt);

			return screenShot;
		}
	}
}

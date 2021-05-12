using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void Handler( object param );

public class RayRawImage : RawImage, IPointerClickHandler {
    public Handler onClick;
    private Camera uiCamera;
    // 预览映射相机
    private Camera PreviewCamera;

    public void ResetData( Camera PreviewCamera, Camera uiCamera ) {
        this.uiCamera = uiCamera;
        this.PreviewCamera = PreviewCamera;
    }
    //protected override void Start()
    //{
    //    // 初始获取预览映射相机
    //    if (PreviewCamera == null)
    //    {
    //        PreviewCamera = GameObject.Find("Camera").transform.GetComponent<Camera>();
    //    }
    //}

    void IPointerClickHandler.OnPointerClick( PointerEventData eventData ) {
        //GetRawImageObj(eventData, PreviewCamera);
        CheckDrawRayLine( canvas, eventData.position, PreviewCamera, uiCamera );
    }

    /// <summary>
    /// 射线投射
    /// </summary>
    /// <param name="canvas">画布</param>
    /// <param name="mousePosition">当前Canvas下点击的鼠标位置</param>
    /// <param name="previewImage">预览图</param>
    /// <param name="previewCamera">预览映射图的摄像机</param>
    private void CheckDrawRayLine( Canvas canvas, Vector3 mousePosition, Camera previewCamera, Camera UiCamera ) {
        Vector2 ClickPosInRawImg;
        // 将UI相机下点击的UI坐标转为相对RawImage的坐标
        if ( RectTransformUtility.ScreenPointToLocalPointInRectangle( canvas.transform as RectTransform, mousePosition, UiCamera, out ClickPosInRawImg ) ) {
            //获取预览图的长宽
            float imageWidth = rectTransform.rect.width;
            float imageHeight = rectTransform.rect.height;
            //获取预览图的坐标，此处RawImage的Pivot需为(0,0)，不然自己再换算下
            float localPositionX = rectTransform.localPosition.x;
            float localPositionY = rectTransform.localPosition.y;

            //获取在预览映射相机viewport内的坐标（坐标比例）
            float p_x = ( ClickPosInRawImg.x - localPositionX ) / imageWidth;
            float p_y = ( ClickPosInRawImg.y - localPositionY ) / imageHeight;

            //从视口坐标发射线
            Ray ray = previewCamera.ViewportPointToRay( new Vector2( p_x, p_y ) );

            var hitInfo2D = Physics2D.Raycast( ray.origin, ray.direction );

            if ( hitInfo2D.transform ) {
                onClick?.Invoke( hitInfo2D.transform.gameObject );
                Debug.Log( "点放到" );
            }
                
            //else
            //    Debug.LogErrorFormat("hitInfo2D.transform is null !");
        }
    }
    /// <summary>
    /// 通过点击RawImage中映射的RenderTexture画面，对应的相机发射射线，得到物体
    /// </summary>
    /// <param name="data">rawimage点击的数据</param>
    /// <param name="rawImgRectTransform">rawimage的recttransfotm</param>
    /// <param name="previewCamera">生成rendertexture中画面的相机</param>
    /// <returns>返回射线碰撞到的物体</returns>
    private GameObject GetRawImageObj( PointerEventData data, Camera previewCamera ) {
        if ( rectTransform == null || previewCamera == null )
            return null;

        GameObject obj = null;
        var pos = ( data.position - (Vector2)rectTransform.position ) / rectTransform.lossyScale - rectTransform.rect.position;
        var rate = pos / rectTransform.rect.size;
        var ray = previewCamera.ViewportPointToRay( rate );
        RaycastHit raycastHit;

        if ( Physics.Raycast( ray, out raycastHit ) ) {
            //Debug.Log(raycastHit.transform.name);
            obj = raycastHit.transform.gameObject;

            onClick?.Invoke( obj );
        } else {
            Vector3 dir = new Vector3( ray.origin.x, ray.origin.y, ray.origin.z * -500 );
            Debug.DrawLine( ray.origin, dir );
            Debug.LogErrorFormat( "ray origin = [{0}]   dir = [{1}]", ray.origin.ToString(), dir.ToString() );
        }
        return obj;
    }
}




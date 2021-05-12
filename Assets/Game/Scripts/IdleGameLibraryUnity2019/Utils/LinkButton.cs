using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LinkButton : MonoBehaviour {

    private Text linkText;

    void Awake() {
        linkText = transform.Find( "Text" ).GetComponent<Text>();

    }
    void Start() {
        CreateLink( linkText );
    }

    public void CreateLink( Text text ) {
        if ( text == null )
            return;

        //克隆Text，获得相同的属性  
        Text underline = Instantiate( text ) as Text;
        underline.name = "Underline";

        underline.transform.SetParent( text.transform );
        underline.transform.localScale = Vector3.one;

        RectTransform rt = underline.rectTransform;

        //设置下划线坐标和位置  
        rt.anchoredPosition3D = Vector3.zero;
        rt.offsetMax = Vector2.zero;
        rt.offsetMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.anchorMin = Vector2.zero;

        underline.text = "_";
        float perlineWidth = underline.preferredWidth;      //单个下划线宽度  

        float width = text.preferredWidth;

        int lineCount = (int)Mathf.Round( width / perlineWidth );

        for ( int i = 1; i < lineCount; i++ ) {
            underline.text += "_";
        }
    }
}
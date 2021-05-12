using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager {
    public delegate void GameVoidDelegate();
    public static GameVoidDelegate OnLocalize;

    public const string kUILayerName = "UI";

    public static void InitValue( LocalizationText txt ) {
        txt.color = new Color( 50f / 255f, 50f / 255f, 50f / 255f );
        RectTransform contentRT = txt.GetComponent<RectTransform>();
        contentRT.sizeDelta = new Vector2( 160f, 100f );
        txt.gameObject.layer = LayerMask.NameToLayer( kUILayerName );
    }
}

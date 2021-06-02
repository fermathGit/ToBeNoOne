//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QFramework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;
    
    
    public class UIUserDataPanelData : QFramework.UIPanelData
    {
    }
    
    public partial class UIUserDataPanel : QFramework.UIPanel
    {
        
        protected override void ProcessMsg(int eventId, QFramework.QMsg msg)
        {
            throw new System.NotImplementedException ();
        }
        
        protected override void OnInit(QFramework.IUIData uiData)
        {
            mData = uiData as UIUserDataPanelData ?? new UIUserDataPanelData();
            // please add init code here
        }
        
        protected override void OnOpen(QFramework.IUIData uiData)
        {
        }

        protected override void RegisterUIEvent() {
            base.RegisterUIEvent();
            BtnClear.onClick.AddListener( () => {
                UserDataManager.instance.MyDatas = new List<WzData>();
                UserDataManager.instance.SaveUserData();

                UITipsPanelData data = new UITipsPanelData();
                data.contentString = "清档成功，请重启程序";
                UIMgr.OpenPanel<UITipsPanel>( data );
            } );
            BtnClose.onClick.AddListener( () => {
                CloseSelf();
            } );

            //将存档数据的部分序列化成json字段
            BtnSeData.onClick.AddListener( () => {
                string userJsonStr = LitJson.JsonMapper.ToJson( UserDataManager.instance.MyDatas );
                GUIUtility.systemCopyBuffer = userJsonStr;
                InputField.text = userJsonStr;

                UITipsPanelData data = new UITipsPanelData();
                data.contentString = "已复制到剪切板。。";
                UIMgr.OpenPanel<UITipsPanel>( data );
            } );
            //json字段反序列化成存档数据，并覆盖MyDatas
            BtnDeData.onClick.AddListener( () => {
                var jsonStr = GUIUtility.systemCopyBuffer;
                if ( !string.IsNullOrEmpty( jsonStr ) ) {
                    UserDataManager.instance.MyDatas = LitJson.JsonMapper.ToObject<List<WzData>>( jsonStr );

                    UserDataManager.instance.SaveUserData();

                    UITipsPanelData data = new UITipsPanelData();
                    data.contentString = "覆盖成功（也许，maybe）";
                    UIMgr.OpenPanel<UITipsPanel>( data );
                }
            } );
        }

        protected override void OnShow()
        {
        }
        
        protected override void OnHide()
        {
        }
        
        protected override void OnClose()
        {
        }
    }
}

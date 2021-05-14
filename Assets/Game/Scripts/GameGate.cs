using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class GameGate : MonoBehaviour {
    // Use this for initialization
#if GAME_DEBUG
    public static bool isDebug = true;
#else
    public static bool isDebug = false;
#endif

    public static bool isLoad = false;

    void OnEnable() {

        if ( !isLoad ) {

            //屏幕适配
            if ( Screen.width * 1.0f / Screen.height <= 1080f / 2400 ) {
                UIMgr.SetResolution( 1080, 2400, 0 );
                Camera.main.orthographicSize = 11f;
            } else {
                UIMgr.SetResolution( 1080, 2400, 1 );
                Camera.main.orthographicSize = 11f;
            }

            //初始化配置表信息
            //ConfigManager.LoadData();

            //初始化数据存储系统
            UserDataManager.instance.Start();

            //声音管理器
            //gameObject.AddComponent<AudioManager>();

            //初始化Timer
            Timer.Init();
            
            isLoad = true;

            Input.multiTouchEnabled = true;
            
            //初始化场景和UI
            //GlobalManager.instance.GameMap.Init();
            
            UIMgr.OpenPanel<UITestPanel>();
            
            ////第一次进游戏
            //if ( UserDataManager.instance.IsFirstEnterGame ) {

                
            //} else {
                
            //}
            
        }
    }

    private void Update() {
        EventMgr.Notify( EventUtil.onApplicationUpdate );
        
    }

    private void OnApplicationQuit() {
        UserDataManager.instance.SaveUserData();
    }

    private void OnApplicationFocus( bool focus ) {
        Debug.Log( "OnApplicationFocus :" + focus );

        if ( focus ) {
            EventMgr.Notify( EventUtil.onApplicationFocus_True );
        } else {
            UserDataManager.instance.SaveUserData();
        }
    }

    private void OnApplicationPause( bool pause ) {
        Debug.Log( "OnApplicationPause :" + pause );
        if ( pause ) {
            //UserDataManager.instance.SaveUserData();
        }
    }
}

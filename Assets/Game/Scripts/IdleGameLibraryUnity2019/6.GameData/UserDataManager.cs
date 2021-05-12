using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using QFramework;
using LitJson;

public class UserDataManager : BaseSingleton<UserDataManager> {
    string key_userData = "myUserData211";

    public UserData MyUserData {
        get { return m_userData; }
        set {
            m_userData = value;
            SaveUserData();
        }
    }

    private UserData m_userData = null;

    public void Start() {
        Debuger.Log( "===========初始化（用户数据）系统=============" );
        m_userData = new UserData();
        m_userData.SetDefaultData();

        //从存档中加载数据
        LoadData();

        Debuger.Log( "----------（用户数据）系统初始化完成-------------" );
    }

    void LoadData() {
        var decryptJsonStr = PlayerPrefs.GetString( key_userData );
        if ( !string.IsNullOrEmpty( decryptJsonStr ) ) {

            string jsonStr = decryptJsonStr;
            if ( PlayerPrefs.GetInt( "encrypted" ) == 1 ) {
                jsonStr = decryptJsonStr;
            }

            m_userData = JsonMapper.ToObject<UserData>( jsonStr );
        }

        //初始化 使数据可用
        InitData();
    }

    void InitData() {
        
    }

    public void SaveUserData() {
        UnInitData();

        string userJsonStr = JsonMapper.ToJson( m_userData );
        string encryptUserJsonStr = userJsonStr;
        PlayerPrefs.SetString( key_userData, encryptUserJsonStr );
        PlayerPrefs.SetInt( "encrypted", 1 );
    }

    void UnInitData() {
       
    }
    
    public void ClearData() {
        m_userData.SetDefaultData();
    }
    
    #region get set
    public List<WzData> MyDatas { get { return m_userData.myDatas; } set { m_userData.myDatas = value; } }
    public List<string> InvestmentDisplays { get { return m_userData.investmentDisplays; }set { m_userData.investmentDisplays = value; } }
    public List<string> ConsumptionDisplays { get { return m_userData.consumptionDisplays; } set { m_userData.consumptionDisplays = value; } }

    #endregion

}


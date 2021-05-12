using System;
using UnityEngine;
using System.Collections.Generic;

public static class Localization {
    public enum LanguageType {
        Chinese,
        English,
    }


    public static LanguageType GetCurrentLangageType() {
        LanguageType lType = LanguageType.Chinese;
        switch ( language ) {
            case "ch":
                lType = LanguageType.Chinese;
                break;
            case "en":
                lType = LanguageType.English;
                break;
            default:
                lType =  LanguageType.Chinese ;
                break;

        }
        return lType;
    }

    static string mLanguage;
    static public string language {
        get {
            if ( string.IsNullOrEmpty( mLanguage ) ) {
                mLanguage = PlayerPrefs.GetString( "Language", string.Empty );
            }
            return mLanguage;
        }
        set {

        }
    }

    public static void SwitchLanguage( string lan ) {
        language = lan;
        mLanguage = lan;

        PlayerPrefs.SetString( "Language", mLanguage );
        if ( LocalizationManager.OnLocalize != null ) {
            LocalizationManager.OnLocalize();
        }
    }

    static public string Get( string key ) {
        //var data = StringTemplate.Tem( key );
        //if ( data == null ) { return "请配置string-key"; }
        //switch ( GetCurrentLangageType() ) {
        //    case LanguageType.Chinese: return data.chinese;
        //    case LanguageType.English: return data.english;
        //}
        return key;
    }
}

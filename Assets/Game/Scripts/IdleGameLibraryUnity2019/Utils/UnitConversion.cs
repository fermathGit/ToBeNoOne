using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// k、m、b、t、q、Q、s、S、o、n、d
/// </summary>
public class UnitConversion {


    /// <summary>
    /// 要显示的字符串
    /// </summary>
    public const string suffix = "kmbtqQsSondEFGHIJKLMNOBQRST";
    public static string StringDisplay( string ogi ) {
        string outstringValue = ConvertString( ogi );
        return outstringValue;
    }

    //保留0位小数
    public static string ConvertString_NotDecimals( string value ) {
        string[] stringNews = LasetIndexOf( value );
        string stringNew_1 = stringNews[0];
        int length = stringNew_1.Length;
        string stringNew = null;
        if ( length > 3 ) {
            stringNew = stringNew_1.Substring( 0, length % 3 == 0 ? 3 : length % 3 ) + suffix[( length - 4 ) / 3];
        } else {
            int decimal_New = value.LastIndexOf( "." );
            if ( decimal_New != -1 ) {
                stringNew = value.Substring( 0, decimal_New + 3 <= value.Length ? decimal_New + 3 : value.Length );
            } else
                stringNew = value;
        }
        return stringNew;

    }

    public static string ConvertString( string value ) {
        value = GameCommon.ScientificNotationChangeToD( value );

        string[] stringNews = LasetIndexOf( value );
        string stringNew_1 = stringNews[0];
        int length = stringNew_1.Length;
        string stringNew = null;
        if ( length > 3 ) {
            stringNew = stringNew_1.Substring( 0, length % 3 == 0 ? 3 : length % 3 ) + "." + stringNew_1.Substring( length % 3 == 0 ? 3 : length % 3, 2 ) + suffix[( length - 4 ) / 3];
        } else {
            int decimal_New = value.LastIndexOf( "." );
            if ( decimal_New != -1 ) {
                stringNew = value.Substring( 0, decimal_New + 3 <= value.Length ? decimal_New + 3 : value.Length );
            } else
                stringNew = value;
        }
        return stringNew;
    }

    /// <summary>
    /// 取整
    /// </summary>
    public static string ConvertString_integer( string value ) {
        value = GameCommon.ScientificNotationChangeToD( value );

        string[] stringNews = LasetIndexOf( value );
        string stringNew_1 = stringNews[0];
        int length = stringNew_1.Length;
        string stringNew = null;
        if ( length > 3 ) {
            stringNew = stringNew_1.Substring( 0, length % 3 == 0 ? 3 : length % 3 ) + "." + stringNew_1.Substring( length % 3 == 0 ? 3 : length % 3, 2 ) + suffix[( length - 4 ) / 3];
        } else {
            int decimal_New = value.LastIndexOf( "." );
            if ( decimal_New != -1 ) {
                stringNew = stringNew_1;
            } else
                stringNew = value;
        }
        return stringNew;
    }

    /// <summary>
    /// 摘取字符串，以小数点为分隔，区分小数点前和小数点后的字符。  【0】小数点前；【1】小数点后
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private static string[] LasetIndexOf( string value ) {
        int lenght = value.Length;
        int decimal_New = value.LastIndexOf( "." );
        //含有小数点
        if ( decimal_New != -1 ) {
            string decimal_L = value.Substring( 0, decimal_New );
            string decimal_R = value.Substring( decimal_New + 1, lenght - decimal_New - 1 );
            return new string[] { decimal_L, decimal_R };
        }
        //没有小数点
        else {
            return new string[] { value };
        }
    }
}

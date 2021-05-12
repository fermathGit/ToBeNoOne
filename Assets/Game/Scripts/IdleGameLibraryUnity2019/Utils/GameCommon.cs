using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.GZip;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Spine.Unity;

using System.Reflection;

/// <summary>
/// 游戏常用方法
/// </summary>
public static class GameCommon {
    public const float GAME_DATA_VERSION = 0.0f;//

    public static float LOAD_DATA_VERSION = 0.0f;

    //base64字符串解码
    public static string UnBase64String(string value) {
        if (string.IsNullOrEmpty(value)) {
            return "";
        }
        byte[] bytes = Convert.FromBase64String(value);
        return Encoding.UTF8.GetString(bytes);
    }

    //base64字符串编码
    public static string ToBase64String(string value) {
        if (string.IsNullOrEmpty(value)) {
            return "";
        }
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// 
    /// 压缩   Compresses the G zip.
    /// </summary>
    /// <returns>The G zip.</returns>
    /// <param name="rawData">Raw data.</param>
    public static byte[] CompressGZip(byte[] rawData) {
        MemoryStream ms = new MemoryStream();
        GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true);
        compressedzipStream.Write(rawData, 0, rawData.Length);
        compressedzipStream.Close();
        return ms.ToArray();
    }

    /// <summary>
    /// 解压  Uns the G zip.
    /// </summary>
    /// <returns>The G zip.</returns>
    /// <param name="byteArray">Byte array.</param>
    public static byte[] UnGZip(byte[] byteArray) {
        GZipInputStream gzi = new GZipInputStream(new MemoryStream(byteArray));

        //包数据解大小上限为50000
        MemoryStream re = new MemoryStream(50000);
        int count;
        byte[] data = new byte[50000];
        while ((count = gzi.Read(data, 0, data.Length)) != 0) {
            re.Write(data, 0, count);
        }
        byte[] overarr = re.ToArray();
        return overarr;
    }

    /// <summary>  
    /// 把对象序列化为字节数组  
    /// </summary>  
    public static string SerializeObject(object obj) {
        IFormatter formatter = new BinaryFormatter();
        string result = string.Empty;
        using ( MemoryStream stream = new MemoryStream() ) {
            formatter.Serialize( stream, obj );
            byte[] byt = new byte[stream.Length];
            byt = stream.ToArray();
            //result = Encoding.UTF8.GetString(byt, 0, byt.Length);
            result = Convert.ToBase64String( byt );
            stream.Flush();
        }
        return result;
    }

    /// <summary>  
    /// 把字节数组反序列化成对象  
    /// UserDataMode userdata = (UserDataMode)GameCommon.DeserializeObject(data);
    /// </summary>  
    public static object DeserializeObject( string str ) {
        IFormatter formatter = new BinaryFormatter();
        //byte[] byt = Encoding.UTF8.GetBytes(str);
        byte[] byt = Convert.FromBase64String( str );
        object obj = null;
        using ( Stream stream = new MemoryStream( byt, 0, byt.Length ) ) {
            obj = formatter.Deserialize( stream );
        }
        return obj;
    }
    /// <summary>
    /// 把字典序列化
    /// </summary>
    /// <param name="dic"></param>
    /// <returns></returns>
    public static byte[] SerializeDic<T>(Dictionary<string, T> dic) {
        if (dic.Count == 0)
            return null;
        MemoryStream ms = new MemoryStream();
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(ms, dic);//把字典序列化成流
        byte[] bytes = ms.GetBuffer();

        return bytes;
    }
    /// <summary>
    /// 反序列化返回字典
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static Dictionary<string, T> DeserializeDic<T>(byte[] bytes) {
        Dictionary<string, T> dic = null;
        if (bytes == null)
            return dic;
        //利用传来的byte[]创建一个内存流
        MemoryStream ms = new MemoryStream(bytes);
        BinaryFormatter formatter = new BinaryFormatter();
        //把流中转换为Dictionary
        dic = (Dictionary<string, T>)formatter.Deserialize(ms);
        return dic;
    }

    /// <summary>
    /// 把字List序列化
    /// </summary>
    /// <param name="dic"></param>
    /// <returns></returns>
    public static byte[] SerializeList<T>(List<T> dic) {
        if (dic.Count == 0)
            return null;
        MemoryStream ms = new MemoryStream();
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(ms, dic);//把字典序列化成流
        byte[] bytes = ms.GetBuffer();

        return bytes;
    }
    /// <summary>
    /// 反序列化返回List
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static List<T> DeserializeList<T>(byte[] bytes) {
        List<T> list = null;
        if (bytes == null)
            return list;
        //利用传来的byte[]创建一个内存流
        MemoryStream ms = new MemoryStream(bytes);
        BinaryFormatter formatter = new BinaryFormatter();
        //把流中转换为List
        list = (List<T>)formatter.Deserialize(ms);
        return list;
    }

    /// <summary>
    /// 二进制数据写入文件 Writes the byte to file.
    /// </summary>
    /// <param name="data">Data.</param>
    /// <param name="tablename">path.</param>
    public static void WriteByteToFile(byte[] data, string path) {

        FileStream fs = new FileStream(path, FileMode.Create);
        fs.Write(data, 0, data.Length);
        fs.Close();
    }

    /// <summary>
    /// 读取文件二进制数据 Reads the byte to file.
    /// </summary>
    /// <returns>The byte to file.</returns>
    /// <param name="path">Path.</param>
    public static byte[] ReadByteToFile(string path) {
        //如果文件不存在，就提示错误
        if (!File.Exists(path)) {
            Debug.Log("读取失败！不存在此文件");
            return null;
        }
        FileStream fs = new FileStream(path, FileMode.Open);
        BinaryReader br = new BinaryReader(fs);
        byte[] data = br.ReadBytes((int)br.BaseStream.Length);

        fs.Close();
        return data;
    }

    public static UnityEngine.Object GetResource(string path) {
        return Resources.Load(path);
    }

    //解析配置表中配置的坐标数组
    public static List<List<float>> ParseConfigPositionList(String val) {
        List<List<float>> retlist = new List<List<float>>();
        if (val != null && !("" == val)) {
            try {
                String[] ps = val.Split('|');
                for (int i = 0; i < ps.Length; i++) {
                    if ("" == (ps[i])) continue;
                    String[] points = ps[i].Split(',');
                    if (points.Length < 2) continue;
                    List<float> pointlist = new List<float>();
                    pointlist.Add(float.Parse(points[0]));
                    pointlist.Add(float.Parse(points[1]));
                    retlist.Add(pointlist);
                }
            }
            catch (Exception e) {

                Debug.Log(e.ToString());
            }
        }
        return retlist;
    }


    /// <summary>
    /// 获取当前时间戳
    /// </summary>
    /// <param name="bflag">为真时获取10位时间戳,为假时获取13位时间戳.</param>
    /// <returns></returns>
    public static long GetTimeStamp(bool bflag = true) {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        long ret;
        if (bflag)
            ret = Convert.ToInt64(ts.TotalSeconds);
        else
            ret = Convert.ToInt64(ts.TotalMilliseconds);
        return ret;
    }

    public static DateTime GetDateTime(long totalMilliseconds) {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        DateTime dt = startTime.AddMilliseconds(totalMilliseconds);
        return dt;
    }


    /// <summary>
    /// 坐标点是否在屏幕内
    /// </summary>
    /// <returns><c>true</c> if is position in screen; otherwise, <c>false</c>.</returns>
    public static bool IsPositionInScreen(Vector3 pos) {
        int cutDistance = 100;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);
        if (screenPos.x < -cutDistance || screenPos.y < -cutDistance || screenPos.x > Screen.width + cutDistance || screenPos.y > Screen.height + cutDistance) {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 世界坐标转NGUI坐标
    /// </summary>
    /// <returns>The position to NGUI position.</returns>
    /// <param name="camera">Camera.</param>
    public static Vector3 WorldPosToNGUIPos(Camera worldCamera, Camera uiCamera, Vector3 pos) {
        Vector3 screenpos = worldCamera.WorldToScreenPoint(pos);
        screenpos.z = 0f;   //把z设置为0
        Vector3 uipos = uiCamera.ScreenToWorldPoint(screenpos);
        return uipos;
    }


    /// <summary>
    /// 数字单位数组
    /// </summary>
    public static string[] unitStr = { "","K", "M", "B", "T", "aa", "bb", "cc", "dd", "ee", "ff"
    , "gg", "hh", "ii", "jj", "kk", "ll", "mm","nn","oo","pp","qq","rr","ss","tt","uu","vv","ww","xx","yy","zz","AA","BB","CC","DD","EE","FF"};

    /// <summary>
    /// 将数字换算成有单位的简洁形式
    /// </summary>
    /// <returns>The unit string.</returns>
    /// <param name="d">D.</param>
    public static string Double2UnitString(double d) {
        string dStr = ((decimal)Math.Round(d)).ToString();
        int len = dStr.Length;

        int index = 0;
        while (len > 3) {
            index++;
            len -= 3;
        }
        if (index == 0)
            return dStr;
        return string.Format("{0:f2} {1}", d / Math.Pow(10, index * 3), unitStr[index]);
    }

    /// <summary>
    /// 将有单位的简洁形式换算回数字
    /// </summary>
    /// <returns>The string2 double.</returns>
    /// <param name="unitString">Unit string.</param>
    public static double UnitString2Double(string unitString) {
        var strArray = unitString.Split(',');
        if (strArray.Length == 2) {
            string unit = strArray[1];
            int index = Array.IndexOf(unitStr, unit);
            return Convert.ToDouble(strArray[0]) * Math.Pow(10, index * 3);
        }
        else {
            Debug.LogError("将有单位的简洁形式换算回数字，出现问题");
        }
        return 0;
    }

    /// <summary>
    /// 放置类玩法的金币表现形式
    /// </summary>
    /// <returns>The double idle gun.</returns>
    /// <param name="str">String.</param>
    public static double ToDouble_IdleGun(this string str) {
        return UnitString2Double(str);
    }

    /// <summary>
    /// 放置类玩法的金币表现形式
    /// </summary>
    /// <returns>The double idle gun.</returns>
    /// <param name="str">String.</param>
    public static string ToString_IdleGun(this string str) {
        return str.Replace(',', ' ');
    }

    /// <summary>
    /// 放置类玩法的金币表现形式
    /// </summary>
    /// <returns>The string idle gun.</returns>
    /// <param name="d">D.</param>
    public static string ToString_IdleGun(this double d) {
        return Double2UnitString(d);
    }

    /// <summary>
    /// 播放UGUI的spine
    /// </summary>
    /// <param name="spine">Spine.</param>
    /// <param name="animationName">Animation name.</param>
    /// <param name="isLoop">If set to <c>true</c> is loop.</param>
    /// <param name="trackIndex">Track index.</param>
    public static void ShowUGUISpine(SkeletonGraphic spine, string animationName, bool isLoop = true, int trackIndex = 0, float timeScale = 1) {
        spine.gameObject.SetActive(true);
        spine.timeScale = timeScale;
        spine.Skeleton.SetToSetupPose();
        spine.AnimationState.ClearTracks();
        spine.AnimationState.SetAnimation(trackIndex, animationName, isLoop);
    }

    public static void ShowUGUISpine(RectTransform spine, string animationName, bool isLoop = true, int trackIndex = 0, float timeScale = 1) {
        ShowUGUISpine(spine.GetComponent<SkeletonGraphic>(), animationName, isLoop, trackIndex, timeScale);
    }

    public static void ShowSkeletonAnimation( SkeletonAnimation skeletonAnimation, string animationName, bool isLoop = true ) {
        Spine.AnimationState spineAnimationState = skeletonAnimation.state;

        skeletonAnimation.skeleton.SetToSetupPose();
        spineAnimationState.ClearTracks();
        spineAnimationState.SetAnimation( 0, animationName, isLoop );

    }

    public static void ShowSkeletonAnimation( Transform trs, string animationName, bool isLoop = true ) {
        SkeletonAnimation sa = trs.GetComponent<SkeletonAnimation>();
        if ( sa != null ) {
            ShowSkeletonAnimation( sa, animationName, isLoop );
        } else {
            Debug.LogError( "没有SkeletonAnimation组件" );
        }
    }

    /// <summary>
    /// “,”把string分割成List
    /// </summary>
    public static List<string> StringToList_comma( string s ) {
        return new List<string>( s.Split( ',' ) );
    }

    /// <summary>
    /// “:”把string分割成List
    /// </summary>
    public static List<string> StringToList_colon( string s ) {
        return new List<string>( s.Split( ':' ) );
    }
    /// <summary>
    /// “, ”把string分割成List
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static List<string> StringToList_commaSpace( string s ) {
        return new List<string>( s.Split( new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries ) );
    }

    /// <summary>
    /// 科学计数法转化为正常数值
    /// </summary>
    /// <param name="strData"></param>
    /// <returns></returns>
    public static string ScientificNotationChangeToD( string strData ) {
        Decimal dData = 0.0M;
        if ( strData.Contains( "E" ) ) {
            dData = Convert.ToDecimal( Decimal.Parse( strData.ToString(), System.Globalization.NumberStyles.Float ) );
        } else {
            return strData;
        }
        return dData.ToString();

    }

    /// <summary>
    /// 每隔n个字符插入一个字符
    /// </summary>
    /// <param name="input">源字符串</param>
    /// <param name="interval">间隔字符数</param>
    /// <param name="value">待插入值</param>
    /// <returns>返回新生成字符串</returns>
    public static string InsertFormat( string input, int interval, string value ) {
        for ( int i = interval; i < input.Length; i += interval + 1 )
            input = input.Insert( i, value );
        return input;
    }


    /// <summary>
    /// 把时间秒数转化成字符串00:00:00
    /// </summary>
    /// <param name="second">时间秒数</param>
    /// <returns>字符串00:00:00</returns>
    public static string TransTimeSecondIntToString( float second ,bool isShowHour=true) {
        string str = "";
        try {
            float hour = second / 3600;
            float min = second % 3600 / 60;
            float sec = second % 60;
            if ( hour < 10 ) {
                str += "0" + ( (int)hour ).ToString();
            } else {
                str += ( (int)hour ).ToString();
            }

            str += ":";

            if ( !isShowHour ) str = "";

            if ( min < 10 ) {
                str += "0" + ( (int)min ).ToString();
            } else {
                str += ( (int)min ).ToString();
            }
            str += ":";
            if ( sec < 10 ) {
                str += "0" + ( (int)sec ).ToString();
            } else {
                str += ( (int)sec ).ToString();
            }
        }
        catch ( System.Exception ex ) {
            Debug.LogWarning( "Catch:" + ex.Message );
        }
        return str;
    }

    public static int GetRandom( int[] arr ) {
        int n = UnityEngine.Random.Range( 0, arr.Length );
        return arr[n];
    }

    public static string GetRandom( string[] arr ) {
        int n = UnityEngine.Random.Range( 0, arr.Length );
        return arr[n];
    }

}


/// <summary>
/// 测试类
/// </summary>
public class GameCommonTest {

    public static void Base64Test() {
        string base64string = GameCommon.ToBase64String("aaaa11233Base64编码和解码");

        string unbase64string = GameCommon.UnBase64String(base64string);

        Debug.Log("base64string : " + base64string);
        Debug.Log("unbase64string : " + unbase64string);
    }

    public static void SerializeDicTest() {

        Dictionary<string, int> test = new Dictionary<string, int>();

        test.Add("1", 1);
        test.Add("2", 2);
        test.Add("3", 4);

        byte[] testbyte = GameCommon.SerializeDic<int>(test);

        Dictionary<string, int> testdic = GameCommon.DeserializeDic<int>(testbyte);

        Debug.Log("[SerializeDicTest]  : " + testdic["3"]);
    }

    public static void GZipTest() {
        string testdata = "aaaa11233GZip压缩和解压";

        byte[] gzipdata = GameCommon.CompressGZip(Encoding.UTF8.GetBytes(testdata));
        byte[] undata = GameCommon.UnGZip(gzipdata);

        Debug.Log("[GZipTest]  : data" + Encoding.UTF8.GetString(undata));
    }

}
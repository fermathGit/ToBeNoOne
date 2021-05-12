using System;
using System.Numerics;

public class BigDecimal {
    string value = "0";

    public BigDecimal( string _value ) {
        value = _value;
    }

    /// <summary>
    /// 赋值
    /// </summary>
    /// <param name="_value"></param>
    public void SetValue( string _value ) { 
        value = _value;
    }

    public string GetValue() {
        return value;
    }

    public static BigDecimal operator +( BigDecimal left, BigDecimal right ) {
        string decimal1 = left.GetValue();
        string decimal2 = right.GetValue();

        int decimalpartlength1 = 0;
        int decimalpartlength2 = 0;
        int integerpartlength1 = 0;
        int integerpartlength2 = 0;

        //int maxscale = 0;
        BigInteger bi1;
        BigInteger bi2;

        //检查小数部分长度
        int pointIdx1 = decimal1.IndexOf( '.' );
        if ( pointIdx1 >= 0 ) {
            decimalpartlength1 = decimal1.Length - pointIdx1 - 1;      //得到小数部分长度
            integerpartlength1 = pointIdx1 == 0 ? 1 : pointIdx1;       //得到整数部分长度,考虑小数点在第一位的情况
        } else {
            integerpartlength1 = decimal1.Length;                      //得到整数部分长度
        }

        //检查小数部分长度
        int pointIdx2 = decimal2.IndexOf( '.' );
        if ( pointIdx2 >= 0 ) {
            decimalpartlength2 = decimal2.Length - pointIdx2 - 1;      //得到小数部分长度
            integerpartlength2 = pointIdx2 == 0 ? 1 : pointIdx2;       //得到整数部分长度,考虑小数点在第一位的情况
        } else {
            integerpartlength2 = decimal2.Length;                      //得到整数部分长度
        }

        decimal1 = decimal1.Replace( ".", "" );
        decimal2 = decimal2.Replace( ".", "" );

        //对齐小数部分
        if ( decimalpartlength1 < decimalpartlength2 ) {
            decimal1 = decimal1 + new string( '0', decimalpartlength2 - decimalpartlength1 );
        }
        if ( decimalpartlength2 < decimalpartlength1 ) {
            decimal2 = decimal2 + new string( '0', decimalpartlength1 - decimalpartlength2 );
        }

        bi1 = BigInteger.Parse( decimal1 );
        bi2 = BigInteger.Parse( decimal2 );

        //确定小数位的精度
        int maxscale = Math.Max( decimalpartlength1, decimalpartlength2 );

        string result = ( bi1 + bi2 ).ToString();
        if ( maxscale > 0 ) {
            if ( maxscale > result.Length ) {
                result = "0." + new string( '0', maxscale - result.Length ) + result;    //小数点后面的0补上，再还原小数点
            } else {
                result = result.Insert( result.Length - maxscale, "." );                 //还原小数点
                if ( result.StartsWith( "." ) ) result = "0" + result;                     //补上个位的0
            }
        }

        BigDecimal value = new BigDecimal( result );
        return value;
    }

    public static BigDecimal operator -( BigDecimal left, BigDecimal right ) {
        string decimal1 = left.GetValue();
        string decimal2 = right.GetValue();

        int decimalpartlength1 = 0;
        int decimalpartlength2 = 0;
        int integerpartlength1 = 0;
        int integerpartlength2 = 0;

        BigInteger bi1;
        BigInteger bi2;

        //检查小数部分长度
        int pointIdx1 = decimal1.IndexOf( '.' );
        if ( pointIdx1 >= 0 ) {
            decimalpartlength1 = decimal1.Length - pointIdx1 - 1;      //得到小数部分长度
            integerpartlength1 = pointIdx1 == 0 ? 1 : pointIdx1;       //得到整数部分长度,考虑小数点在第一位的情况
        } else {
            integerpartlength1 = decimal1.Length;                      //得到整数部分长度
        }

        //检查小数部分长度
        int pointIdx2 = decimal2.IndexOf( '.' );
        if ( pointIdx2 >= 0 ) {
            decimalpartlength2 = decimal2.Length - pointIdx2 - 1;      //得到小数部分长度
            integerpartlength2 = pointIdx2 == 0 ? 1 : pointIdx2;       //得到整数部分长度,考虑小数点在第一位的情况
        } else {
            integerpartlength2 = decimal2.Length;                      //得到整数部分长度
        }

        decimal1 = decimal1.Replace( ".", "" );
        decimal2 = decimal2.Replace( ".", "" );

        //对齐小数部分
        if ( decimalpartlength1 < decimalpartlength2 ) {
            decimal1 = decimal1 + new string( '0', decimalpartlength2 - decimalpartlength1 );
        }
        if ( decimalpartlength2 < decimalpartlength1 ) {
            decimal2 = decimal2 + new string( '0', decimalpartlength1 - decimalpartlength2 );
        }

        bi1 = BigInteger.Parse( decimal1 );
        bi2 = BigInteger.Parse( decimal2 );


        //确定小数位的精度
        int maxscale = Math.Max( decimalpartlength1, decimalpartlength2 );

        string result = ( bi1 - bi2 ).ToString();
        if ( maxscale > 0 ) {
            if ( maxscale > result.Length ) {
                result = "0." + new string( '0', maxscale - result.Length ) + result;    //小数点后面的0补上，再还原小数点
            } else {
                result = result.Insert( result.Length - maxscale, "." );                 //还原小数点
                if ( result.StartsWith( "." ) ) result = "0" + result;                     //补上个位的0
            }
        }

        BigDecimal value = new BigDecimal( result );
        return value;
    }

    public static BigDecimal operator /( BigDecimal left, BigDecimal right ) {
        ////算法：把两个参数的小数部分补0对齐，整数部分确保被除数长度大于除数，
        ////      放大被除数的要求精度的倍数，确保整数计算能保留希望的小数部分
        ////      还原小数点，输出要求的精度，多余部分截断
        //if (!isNumeric(decimal1)) throw new ArgumentException("Invalid argument");
        //if (!isNumeric(decimal2)) throw new ArgumentException("Invalid argument");

        string decimal1 = left.GetValue();
        string decimal2 = right.GetValue();
        //判断负号
        int s1 = decimal1.IndexOf( '-' );
        if ( s1 >= 0 ) decimal1 = decimal1.Replace( "-", "" );

        //判断负号
        int s2 = decimal2.IndexOf( '-' );
        if ( s2 >= 0 ) decimal2 = decimal2.Replace( "-", "" );

        int sign = s1 + s2;     //=-2都是正数；=-1一正一负；=0都是负数；>0非法数字

        int decimalpartlength1 = 0;
        int decimalpartlength2 = 0;
        int integerpartlength1 = 0;
        int integerpartlength2 = 0;

        int maxscale = 0;
        BigInteger bi1;
        BigInteger bi2;

        //检查小数部分长度
        int pointIdx1 = decimal1.IndexOf( '.' );
        if ( pointIdx1 >= 0 ) {
            decimalpartlength1 = decimal1.Length - pointIdx1 - 1;      //得到小数部分长度
            integerpartlength1 = pointIdx1 == 0 ? 1 : pointIdx1;       //得到整数部分长度,考虑小数点在第一位的情况
        } else {
            integerpartlength1 = decimal1.Length;                      //得到整数部分长度
        }

        //检查小数部分长度
        int pointIdx2 = decimal2.IndexOf( '.' );
        if ( pointIdx2 >= 0 ) {
            decimalpartlength2 = decimal2.Length - pointIdx2 - 1;      //得到小数部分长度
            integerpartlength2 = pointIdx2 == 0 ? 1 : pointIdx2;       //得到整数部分长度,考虑小数点在第一位的情况
        } else {
            integerpartlength2 = decimal2.Length;                      //得到整数部分长度
        }

        decimal1 = decimal1.Replace( ".", "" );
        decimal2 = decimal2.Replace( ".", "" );

        //对齐小数部分
        if ( decimalpartlength1 < decimalpartlength2 ) {
            decimal1 = decimal1 + new string( '0', decimalpartlength2 - decimalpartlength1 );
        }
        if ( decimalpartlength2 < decimalpartlength1 ) {
            decimal2 = decimal2 + new string( '0', decimalpartlength1 - decimalpartlength2 );
        }

        bi1 = BigInteger.Parse( decimal1 );
        bi2 = BigInteger.Parse( decimal2 );

        if ( bi2.ToString() == "0" ) throw new DivideByZeroException( "DivideByZeroError" );  //throw new DivideByZeroException("DivideByZeroError")


        int rightpos = 0;                                               //计算从右边数小数点的位置，用于还原小数点
        int pows = integerpartlength2 - integerpartlength1;
        if ( pows >= 0 ) {
            bi1 = bi1 * BigInteger.Pow( 10, pows + 1 );                   //放大被除数，确保大于除数
            rightpos += pows + 1;
        }

        //确定小数位的精度
        maxscale = Math.Max( decimalpartlength1, decimalpartlength2 );
        if ( maxscale < 2 ) maxscale = 2;
        //if (CustomScale < 0)
        //{
        //    CustomScale = maxscale;                                     //CustomScale<0，表示精度由参数决定
        //}
        //else
        //{
        //    maxscale = Math.Max(maxscale, CustomScale);                 //得到最大的小数位数
        //}

        bi1 = bi1 * BigInteger.Pow( 10, maxscale );             //放大被除数，确保整数除法之后，能保留小数部分
        rightpos += maxscale;

        BigInteger d = bi1 / bi2;                                       //注意整数除法的特点：会丢掉小数部分
        string result = d.ToString();

        if ( rightpos > result.Length ) {
            result = "0." + new string( '0', rightpos - result.Length ) + result;    //小数点后面的0补上，再还原小数点
        } else {
            result = result.Insert( result.Length - rightpos, "." );                 //还原小数点
            if ( result.StartsWith( "." ) ) result = "0" + result;                     //补上个位的0
        }

        //超出精度截断
        if ( rightpos > maxscale ) result = result.Substring( 0, result.Length - ( rightpos - maxscale ) );
        if ( result.EndsWith( "." ) ) result = result.Substring( 0, result.Length - 1 ); //去掉末尾小数点
        //还原正负号
        if ( sign == -1 ) result = "-" + result;
        BigDecimal value = new BigDecimal( result );
        return value;
    }
    
    public static BigDecimal operator *( BigDecimal left, BigDecimal right ) {
        string decimal1 = left.GetValue();
        string decimal2 = right.GetValue();
        //判断负号
        int s1 = decimal1.IndexOf( '-' );
        if ( s1 >= 0 ) decimal1 = decimal1.Replace( "-", "" );

        //判断负号
        int s2 = decimal2.IndexOf( '-' );
        if ( s2 >= 0 ) decimal2 = decimal2.Replace( "-", "" );

        int sign = s1 + s2;     //=-2都是正数；=-1一正一负；=0都是负数；>0非法数字

        int decimalpartlength1 = 0;
        int decimalpartlength2 = 0;
        int integerpartlength1 = 0;
        int integerpartlength2 = 0;

        BigInteger bi1;
        BigInteger bi2;

        //检查小数部分长度
        int pointIdx1 = decimal1.IndexOf( '.' );
        if ( pointIdx1 >= 0 ) {
            decimalpartlength1 = decimal1.Length - pointIdx1 - 1;      //得到小数部分长度
            integerpartlength1 = pointIdx1 == 0 ? 1 : pointIdx1;       //得到整数部分长度,考虑小数点在第一位的情况
        } else {
            integerpartlength1 = decimal1.Length;                      //得到整数部分长度
        }

        //检查小数部分长度
        int pointIdx2 = decimal2.IndexOf( '.' );
        if ( pointIdx2 >= 0 ) {
            decimalpartlength2 = decimal2.Length - pointIdx2 - 1;      //得到小数部分长度
            integerpartlength2 = pointIdx2 == 0 ? 1 : pointIdx2;       //得到整数部分长度,考虑小数点在第一位的情况
        } else {
            integerpartlength2 = decimal2.Length;                      //得到整数部分长度
        }

        decimal1 = decimal1.Replace( ".", "" );
        decimal2 = decimal2.Replace( ".", "" );

        int offdecimal1 = 0;
        int offdecimal2 = 0;
        //对齐小数部分
        if ( decimalpartlength1 < decimalpartlength2 ) {
            offdecimal1 = decimalpartlength2 - decimalpartlength1;
            decimal1 = decimal1 + new string( '0', offdecimal1 );
        }
        if ( decimalpartlength2 < decimalpartlength1 ) {
            offdecimal2 = decimalpartlength1 - decimalpartlength2;
            decimal2 = decimal2 + new string( '0', offdecimal2 );
        }

        bi1 = BigInteger.Parse( decimal1 );
        bi2 = BigInteger.Parse( decimal2 );

        BigInteger d = bi1 * bi2;                                       //注意整数除法的特点：会丢掉小数部分
        string result = d.ToString();
        int rightpos = ( decimalpartlength1 + offdecimal1 ) + ( decimalpartlength2 + offdecimal2 );
        if ( rightpos > 0 ) {
            if ( rightpos > result.Length ) {
                result = "0." + new string( '0', rightpos - result.Length ) + result;    //小数点后面的0补上，再还原小数点
            } else {
                result = result.Insert( result.Length - rightpos, "." );                 //还原小数点
                if ( result.StartsWith( "." ) ) result = "0" + result;                     //补上个位的0
            }
        }

        //还原正负号
        if ( sign == -1 ) result = "-" + result;
        BigDecimal value = new BigDecimal( result );
        return value;
    }

    public static bool operator <( BigDecimal left, BigDecimal right ) {
        string decimal1 = left.GetValue();
        string decimal2 = right.GetValue();

        int decimalpartlength1 = 0;
        int decimalpartlength2 = 0;
        int integerpartlength1 = 0;
        int integerpartlength2 = 0;

        //int maxscale = 0;
        BigInteger bi1;
        BigInteger bi2;

        //检查小数部分长度
        int pointIdx1 = decimal1.IndexOf( '.' );
        if ( pointIdx1 >= 0 ) {
            decimalpartlength1 = decimal1.Length - pointIdx1 - 1;      //得到小数部分长度
            integerpartlength1 = pointIdx1 == 0 ? 1 : pointIdx1;       //得到整数部分长度,考虑小数点在第一位的情况
        } else {
            integerpartlength1 = decimal1.Length;                      //得到整数部分长度
        }

        //检查小数部分长度
        int pointIdx2 = decimal2.IndexOf( '.' );
        if ( pointIdx2 >= 0 ) {
            decimalpartlength2 = decimal2.Length - pointIdx2 - 1;      //得到小数部分长度
            integerpartlength2 = pointIdx2 == 0 ? 1 : pointIdx2;       //得到整数部分长度,考虑小数点在第一位的情况
        } else {
            integerpartlength2 = decimal2.Length;                      //得到整数部分长度
        }

        decimal1 = decimal1.Replace( ".", "" );
        decimal2 = decimal2.Replace( ".", "" );

        //对齐小数部分
        if ( decimalpartlength1 < decimalpartlength2 ) {
            decimal1 = decimal1 + new string( '0', decimalpartlength2 - decimalpartlength1 );
        }
        if ( decimalpartlength2 < decimalpartlength1 ) {
            decimal2 = decimal2 + new string( '0', decimalpartlength1 - decimalpartlength2 );
        }

        bi1 = BigInteger.Parse( decimal1 );
        bi2 = BigInteger.Parse( decimal2 );

        return bi1 < bi2;
    }

    public static bool operator >( BigDecimal left, BigDecimal right ) {
        string decimal1 = left.GetValue();
        string decimal2 = right.GetValue();

        int decimalpartlength1 = 0;
        int decimalpartlength2 = 0;
        int integerpartlength1 = 0;
        int integerpartlength2 = 0;

        //int maxscale = 0;
        BigInteger bi1;
        BigInteger bi2;

        //检查小数部分长度
        int pointIdx1 = decimal1.IndexOf( '.' );
        if ( pointIdx1 >= 0 ) {
            decimalpartlength1 = decimal1.Length - pointIdx1 - 1;      //得到小数部分长度
            integerpartlength1 = pointIdx1 == 0 ? 1 : pointIdx1;       //得到整数部分长度,考虑小数点在第一位的情况
        } else {
            integerpartlength1 = decimal1.Length;                      //得到整数部分长度
        }

        //检查小数部分长度
        int pointIdx2 = decimal2.IndexOf( '.' );
        if ( pointIdx2 >= 0 ) {
            decimalpartlength2 = decimal2.Length - pointIdx2 - 1;      //得到小数部分长度
            integerpartlength2 = pointIdx2 == 0 ? 1 : pointIdx2;       //得到整数部分长度,考虑小数点在第一位的情况
        } else {
            integerpartlength2 = decimal2.Length;                      //得到整数部分长度
        }

        decimal1 = decimal1.Replace( ".", "" );
        decimal2 = decimal2.Replace( ".", "" );

        //对齐小数部分
        if ( decimalpartlength1 < decimalpartlength2 ) {
            decimal1 = decimal1 + new string( '0', decimalpartlength2 - decimalpartlength1 );
        }
        if ( decimalpartlength2 < decimalpartlength1 ) {
            decimal2 = decimal2 + new string( '0', decimalpartlength1 - decimalpartlength2 );
        }

        bi1 = BigInteger.Parse( decimal1 );
        bi2 = BigInteger.Parse( decimal2 );

        return bi1 > bi2;
    }

    public static bool operator ==( BigDecimal left, BigDecimal right ) {
        object leftOb = left as object;
        object rightOb = right as object;

        if ( leftOb == null && rightOb == null ) return true;
        else if ( leftOb == null ) return false;
        else if ( rightOb == null ) return false;
        string decimal1 = left.GetValue();
        string decimal2 = right.GetValue();
        return decimal1 == decimal2;
    }

    public static bool operator !=( BigDecimal left, BigDecimal right ) {
        object leftOb = left as object;
        object rightOb = right as object;

        if ( leftOb == null && rightOb == null ) return false;
        else if ( leftOb == null ) return true;
        else if ( rightOb == null ) return true;
        string decimal1 = left.GetValue();
        string decimal2 = right.GetValue();
        return decimal1 != decimal2;
    }

    public static BigDecimal One = new BigDecimal( "1" );
    public static BigDecimal Zero = new BigDecimal( "0" );
}

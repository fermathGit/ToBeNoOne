using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#pragma warning disable 0219
#pragma warning disable 0168
#pragma warning disable 0162
#pragma warning disable 0414
/// <summary>
/// used for some simple situation
/// </summary>
public class EasyMath
{
    // global Vars
    private static Quaternion rotateQuaternion = Quaternion.identity;

    #region Enum
    public enum EaseType
    {
        easeInQuad,
        easeOutQuad,
        easeInOutQuad,
        easeInCubic,
        easeOutCubic,
        easeInOutCubic,
        easeInQuart,
        easeOutQuart,
        easeInOutQuart,
        easeInQuint,
        easeOutQuint,
        easeInOutQuint,
        easeInSine,
        easeOutSine,
        easeInOutSine,
        easeInExpo,
        easeOutExpo,
        easeInOutExpo,
        easeInCirc,
        easeOutCirc,
        easeInOutCirc,
        linear,
        spring,
        /* GFX47 MOD START */
        //bounce,
        easeInBounce,
        easeOutBounce,
        easeInOutBounce,
        /* GFX47 MOD END */
        easeInBack,
        easeOutBack,
        easeInOutBack,
        /* GFX47 MOD START */
        //elastic,
        easeInElastic,
        easeOutElastic,
        easeInOutElastic,
        /* GFX47 MOD END */
        punch
    }
    #endregion

    #region Easing Curves
    public static Vector3 rollingY(Vector3 dir, float value, int rollDir = 1)
    {
        // value 0 pos == dir Start
        // value 1 pos == dir End 
        float curAngle = value * 0.75f * 2.0f * Mathf.PI;
        if (rollDir == 1)
        {
            rotateQuaternion.Set(0.0f, Mathf.Sin(curAngle * 0.5f), 0.0f, Mathf.Cos(curAngle * 0.5f));
        }
        else
        {
            rotateQuaternion.Set(0.0f, -Mathf.Sin(curAngle * 0.5f), 0.0f, Mathf.Cos(curAngle * 0.5f));
        }

        return rotateQuaternion * (-dir * (1 - value));
    }

    public static float linear(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, value);
    }

    public static float clerp(float start, float end, float value)
    {
        float min = 0.0f;
        float max = 360.0f;
        float half = Mathf.Abs((max - min) * 0.5f);
        float retval = 0.0f;
        float diff = 0.0f;
        if ((end - start) < -half)
        {
            diff = ((max - start) + end) * value;
            retval = start + diff;
        }
        else if ((end - start) > half)
        {
            diff = -((max - end) + start) * value;
            retval = start + diff;
        }
        else retval = start + (end - start) * value;
        return retval;
    }

    public static float spring(float start, float end, float value)
    {
        value = Mathf.Clamp01(value);
        value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
        return start + (end - start) * value;
    }

    public static float easeInQuad(float start, float end, float value)
    {
        end -= start;
        return end * value * value + start;
    }

    public static float easeOutQuad(float start, float end, float value)
    {
        end -= start;
        return -end * value * (value - 2) + start;
    }

    public static float easeInOutQuad(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value + start;
        value--;
        return -end * 0.5f * (value * (value - 2) - 1) + start;
    }

    public static float easeInCubic(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value + start;
    }

    public static float easeOutCubic(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * (value * value * value + 1) + start;
    }

    public static float easeInOutCubic(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value * value + start;
        value -= 2;
        return end * 0.5f * (value * value * value + 2) + start;
    }

    public static float easeInQuart(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value * value + start;
    }

    public static float easeOutQuart(float start, float end, float value)
    {
        value--;
        end -= start;
        return -end * (value * value * value * value - 1) + start;
    }

    public static float easeInOutQuart(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value * value * value + start;
        value -= 2;
        return -end * 0.5f * (value * value * value * value - 2) + start;
    }

    public static float easeInQuint(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value * value * value + start;
    }

    public static float easeOutQuint(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * (value * value * value * value * value + 1) + start;
    }

    public static float easeInOutQuint(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value * value * value * value + start;
        value -= 2;
        return end * 0.5f * (value * value * value * value * value + 2) + start;
    }

    public static float easeInSine(float start, float end, float value)
    {
        end -= start;
        return -end * Mathf.Cos(value * (Mathf.PI * 0.5f)) + end + start;
    }

    public static float easeOutSine(float start, float end, float value)
    {
        end -= start;
        return end * Mathf.Sin(value * (Mathf.PI * 0.5f)) + start;
    }

    public static float easeInOutSine(float start, float end, float value)
    {
        end -= start;
        return -end * 0.5f * (Mathf.Cos(Mathf.PI * value) - 1) + start;
    }

    public static float easeInExpo(float start, float end, float value)
    {
        end -= start;
        return end * Mathf.Pow(2, 10 * (value - 1)) + start;
    }

    public static float easeOutExpo(float start, float end, float value)
    {
        end -= start;
        return end * (-Mathf.Pow(2, -10 * value) + 1) + start;
    }

    public static float easeInOutExpo(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * Mathf.Pow(2, 10 * (value - 1)) + start;
        value--;
        return end * 0.5f * (-Mathf.Pow(2, -10 * value) + 2) + start;
    }

    public static float easeInCirc(float start, float end, float value)
    {
        end -= start;
        return -end * (Mathf.Sqrt(1 - value * value) - 1) + start;
    }

    public static float easeOutCirc(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * Mathf.Sqrt(1 - value * value) + start;
    }

    public static float easeInOutCirc(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return -end * 0.5f * (Mathf.Sqrt(1 - value * value) - 1) + start;
        value -= 2;
        return end * 0.5f * (Mathf.Sqrt(1 - value * value) + 1) + start;
    }

    /* GFX47 MOD START */
    public static float easeInBounce(float start, float end, float value)
    {
        end -= start;
        float d = 1f;
        return end - easeOutBounce(0, end, d - value) + start;
    }
    /* GFX47 MOD END */

    /* GFX47 MOD START */
    //private float bounce(float start, float end, float value){
    public static float easeOutBounce(float start, float end, float value)
    {
        value /= 1f;
        end -= start;
        if (value < (1 / 2.75f))
        {
            return end * (7.5625f * value * value) + start;
        }
        else if (value < (2 / 2.75f))
        {
            value -= (1.5f / 2.75f);
            return end * (7.5625f * (value) * value + .75f) + start;
        }
        else if (value < (2.5 / 2.75))
        {
            value -= (2.25f / 2.75f);
            return end * (7.5625f * (value) * value + .9375f) + start;
        }
        else
        {
            value -= (2.625f / 2.75f);
            return end * (7.5625f * (value) * value + .984375f) + start;
        }
    }
    /* GFX47 MOD END */

    /* GFX47 MOD START */
    public static float easeInOutBounce(float start, float end, float value)
    {
        end -= start;
        float d = 1f;
        if (value < d * 0.5f) return easeInBounce(0, end, value * 2) * 0.5f + start;
        else return easeOutBounce(0, end, value * 2 - d) * 0.5f + end * 0.5f + start;
    }
    /* GFX47 MOD END */

    public static float easeInBack(float start, float end, float value)
    {
        end -= start;
        value /= 1;
        float s = 1.70158f;
        return end * (value) * value * ((s + 1) * value - s) + start;
    }

    public static float easeOutBack(float start, float end, float value)
    {
        float s = 1.70158f;
        end -= start;
        value = (value) - 1;
        return end * ((value) * value * ((s + 1) * value + s) + 1) + start;
    }

    public static float easeInOutBack(float start, float end, float value)
    {
        float s = 1.70158f;
        end -= start;
        value /= .5f;
        if ((value) < 1)
        {
            s *= (1.525f);
            return end * 0.5f * (value * value * (((s) + 1) * value - s)) + start;
        }
        value -= 2;
        s *= (1.525f);
        return end * 0.5f * ((value) * value * (((s) + 1) * value + s) + 2) + start;
    }

    public static float punch(float amplitude, float value)
    {
        float s = 9;
        if (value == 0)
        {
            return 0;
        }
        else if (value == 1)
        {
            return 0;
        }
        float period = 1 * 0.3f;
        s = period / (2 * Mathf.PI) * Mathf.Asin(0);
        return (amplitude * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * 1 - s) * (2 * Mathf.PI) / period));
    }

    /* GFX47 MOD START */
    public static float easeInElastic(float start, float end, float value)
    {
        end -= start;

        float d = 1f;
        float p = d * .3f;
        float s = 0;
        float a = 0;

        if (value == 0) return start;

        if ((value /= d) == 1) return start + end;

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        return -(a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + start;
    }
    /* GFX47 MOD END */

    /* GFX47 MOD START */
    //private float elastic(float start, float end, float value){
    public static float easeOutElastic(float start, float end, float value)
    {
        /* GFX47 MOD END */
        //Thank you to rafael.marteleto for fixing this as a port over from Pedro's UnityTween
        end -= start;

        float d = 1f;
        float p = d * .3f;
        float s = 0;
        float a = 0;

        if (value == 0) return start;

        if ((value /= d) == 1) return start + end;

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p * 0.25f;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        return (a * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) + end + start);
    }

    /* GFX47 MOD START */
    public static float easeInOutElastic(float start, float end, float value)
    {
        end -= start;

        float d = 1f;
        float p = d * .3f;
        float s = 0;
        float a = 0;

        if (value == 0) return start;

        if ((value /= d * 0.5f) == 2) return start + end;

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        if (value < 1) return -0.5f * (a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + start;
        return a * Mathf.Pow(2, -10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) * 0.5f + end + start;
    }
    /* GFX47 MOD END */

    #endregion


    #region 简单贝塞尔函数
    /// <summary>
    /// special Bezier line for HUD present
    /// </summary>
    public class ThreePointBezierVec2
    {
        // 三阶贝塞尔函数 起始0点
        private static readonly Vector2 mc_startPoint = Vector2.zero;         // first point from zero

        private static readonly Vector2 mc_directUp = new Vector2(0, 50);
        private static readonly Vector2 mc_directRight = new Vector2(100, 0);
        private static readonly Vector2 mc_directDown = new Vector2(0, -50);
        private static readonly Vector2 mc_directLeft = new Vector2(-100, 0);

        // interpolation from 0 - 1
        public static Vector2 GetPointWithNormalizedInterpolation(Vector2 point1, Vector2 point2, Vector2 point3, float interpolation)
        {
            Vector2 retVec2 = Vector2.zero;
            float t = interpolation;
            // check if out of range , reset position
            if (t > 1.0f)
            {
                t = 1.0f;
            }
            if (t < 0.0f)
            {
                t = 0.0f;
            }
            // caculate interpolation point
            {
                // 对起始0点优化
                //float B0 = Mathf.Pow( ( 1 - t ), 3 );
                float B1 = 3 * t * Mathf.Pow((1 - t), 2);
                float B2 = 3 * Mathf.Pow(t, 2) * (1 - t);
                float B3 = Mathf.Pow(t, 3);

                //retVec2 = ( B0 * mc_startPoint ) + ( B1 * point1 ) + ( B2 * point2 ) + ( B3 * point3 );
                retVec2 = (B1 * point1) + (B2 * point2) + (B3 * point3);
            }
            return retVec2;
        }

        #region PreSetting HUD moving Curve
        // 预设曲线
        // up
        public class CurveUp
        {
            private static Vector2 point1 = mc_directUp;
            private static Vector2 point2 = mc_directUp * 2;
            private static Vector2 point3 = mc_directUp * 4;

            public static Vector2 InterpolationPos(float interpolation)
            {
                return GetPointWithNormalizedInterpolation(point1, point2, point3, interpolation);
            }
        }

        // down
        public class CurveDown
        {
            private static Vector2 point1 = mc_directDown;
            private static Vector2 point2 = mc_directDown * 2;
            private static Vector2 point3 = mc_directDown * 3;

            public static Vector2 InterpolationPos(float interpolation)
            {
                return GetPointWithNormalizedInterpolation(point1, point2, point3, interpolation);
            }
        }

        // left
        public class CurveLeft
        {
            private static Vector2 point1 = mc_directLeft;
            private static Vector2 point2 = mc_directLeft * 2;
            private static Vector2 point3 = mc_directLeft * 3;

            public static Vector2 InterpolationPos(float interpolation)
            {
                return GetPointWithNormalizedInterpolation(point1, point2, point3, interpolation);
            }
        }

        // right
        public class CurveRight
        {
            private static Vector2 point1 = mc_directRight;
            private static Vector2 point2 = mc_directRight * 2;
            private static Vector2 point3 = mc_directRight * 3;

            public static Vector2 InterpolationPos(float interpolation)
            {
                return GetPointWithNormalizedInterpolation(point1, point2, point3, interpolation);
            }
        }

        // right down
        public class CurveRightDown
        {
            private static Vector2 point1 = mc_directRight;
            private static Vector2 point2 = mc_directRight + mc_directDown;
            private static Vector2 point3 = mc_directRight + mc_directDown * 3;

            public static Vector2 InterpolationPos(float interpolation)
            {
                return GetPointWithNormalizedInterpolation(point1, point2, point3, interpolation);
            }
        }

        // right up
        public class CurveRightUp
        {
            private static Vector2 point1 = mc_directRight;
            private static Vector2 point2 = mc_directRight + mc_directUp;
            private static Vector2 point3 = mc_directRight + mc_directUp * 3;

            public static Vector2 InterpolationPos(float interpolation)
            {
                return GetPointWithNormalizedInterpolation(point1, point2, point3, interpolation);
            }
        }

        // left down
        public class CurveLeftDown
        {
            private static Vector2 point1 = mc_directLeft;
            private static Vector2 point2 = mc_directLeft + mc_directDown;
            private static Vector2 point3 = mc_directLeft + mc_directDown * 3;

            public static Vector2 InterpolationPos(float interpolation)
            {
                return GetPointWithNormalizedInterpolation(point1, point2, point3, interpolation);
            }
        }

        // left up
        public class CurveLeftUp
        {
            private static Vector2 point1 = mc_directLeft;
            private static Vector2 point2 = mc_directLeft + mc_directUp;
            private static Vector2 point3 = mc_directLeft + mc_directUp * 3;

            public static Vector2 InterpolationPos(float interpolation)
            {
                return GetPointWithNormalizedInterpolation(point1, point2, point3, interpolation);
            }
        }

        #endregion
        // Free 任意三点曲线
        public class CurveFree
        {
            private Vector2 point1;
            private Vector2 point2;
            private Vector2 point3;

            public CurveFree(Vector2 p1, Vector2 p2, Vector2 p3)
            {
                point1 = p1;
                point2 = p2;
                point3 = p3;
            }

            public Vector2 InterpolationPos(float interpolation)
            {
                return GetPointWithNormalizedInterpolation(point1, point2, point3, interpolation);
            }
        }


    }

    // 提供无限阶贝塞尔函数 (Vector2, Vector3) -- 极限约为50个点
    public class UnlimitedPointBezier
    {
        public enum PointType
        {
            PointVec2 = 0,
            PointVec3
        }
        // control vars
        private PointType pointType;

        // data Lists
        private List<Vector2> pointListVec2 = new List<Vector2>();
        private List<Vector3> pointListVec3 = new List<Vector3>();

        // n阶对应n基底的抽样函数 --> 起始点 points[0] ....
        public UnlimitedPointBezier(List<Vector2> points)
        {
            pointType = PointType.PointVec2;
            if (points != null)
            {
                pointListVec2 = points;
            }
        }
        public UnlimitedPointBezier(List<Vector3> points)
        {
            pointType = PointType.PointVec3;
            if (points != null)
            {
                pointListVec3 = points;
            }
        }

        // 计算出归一化差值的返回坐标 需要对返回拆箱转换
        public object InterpolationPos(float interpolation)
        {
            Vector2 retVec2 = Vector2.zero;
            Vector3 retVec3 = Vector3.zero;
            UInt32 n = 0;
            float t = interpolation;
            if (pointType == PointType.PointVec2)
            {
                n = (UInt32)pointListVec2.Count - 1;
            }
            else
            {
                n = (UInt32)pointListVec3.Count - 1;
            }

            for (UInt32 k = 0; k <= n; k++)
            {
                // calculate
                if (pointType == PointType.PointVec2)
                {
                    retVec2 += (pointListVec2[(int)k] * Binomial.C((UInt32)n, (UInt32)k) * Binomial.BinomialNormalizeExpansion(t, n, k));
                }
                else
                {
                    retVec3 += (pointListVec3[(int)k] * Binomial.C((UInt32)n, (UInt32)k) * Binomial.BinomialNormalizeExpansion(t, n, k));
                }
            }

            if (pointType == PointType.PointVec2)
            {
                return retVec2;
            }
            else
            {
                return retVec3;
            }
        }
    }

    // 阶乘
    public class Factorial
    {
        // calculate from [num] to 1
        public static UInt32 CalculateFactorial(UInt32 num)
        {
            UInt32 retNum = 1;
            if (num == 0)
            {
                return retNum;
            }
            else
            {
                for (UInt32 i = 1; i <= num; i++)
                {
                    retNum = retNum * i;
                }
                return retNum;
            }
        }

        // calculate from n to k (without k)
        public static UInt32 CalculateFactorialRange(UInt32 n, UInt32 k)
        {
            // divisorNum is n! / k!
            UInt32 divisorNum = 1;
            for (UInt32 num = n; num > k; num--)
            {
                divisorNum = divisorNum * num;
            }
            return divisorNum;
        }
    }

    // 二项式 
    public class Binomial
    {
        // 优化 抽样定理 C (n, k) n为基底 k为展开项
        public static UInt32 C(UInt32 n, UInt32 k)
        {
            UInt32 retNum = 1;
            if (n == 0)
            {
                return retNum;
            }
            else if (k > n)
            {
                return 0;
            }
            else
            {
                UInt32 adjustN = n;
                UInt32 adjustK = k;
                if (adjustK < (adjustN / 2))
                {
                    adjustK = adjustN - adjustK;
                }
                UInt32 divisorNum = Factorial.CalculateFactorialRange(adjustN, adjustK);
                UInt32 dividendNum = Factorial.CalculateFactorial(adjustN - adjustK);
                retNum = (UInt32)(divisorNum / dividendNum);
                return retNum;
            }
        }

        // 二项式展开 t(k)次方 + (1-t)(n-k)次方
        public static float BinomialNormalizeExpansion(float t, UInt32 n, UInt32 k)
        {
            float retNum = -1.0f;

            if (t <= 1 && t >= 0 && n >= k)
            {
                if (n == k)
                {
                    retNum = Mathf.Pow(t, k);
                }
                else
                {
                    retNum = Mathf.Pow(t, k) * Mathf.Pow((1 - t), (n - k));
                }

            }

            return retNum;
        }
    }
    #endregion


    #region 插值曲线 -- 预设
    /// <summary>
    /// 预设的插值曲线 使用自带的AnimationCurve -- 向上
    /// </summary>
    public class PreSettingAnimeCurve
    {
        // 预设各种曲线的存活时间
        protected float m_totalCurveTime = 1.0f;
        protected float m_elapsedTime = 0.0f;
        protected float m_normalizedTime = 0.0f;
        protected float m_upCurveTime = 0.3f;
        protected float m_downCurveTime = 0.7f;
        protected static float ms_alphaCurveTime = 0.6f;
        protected static float ms_preScaleCurveTime = 0.1f;
        protected static float ms_scaleCurveTime = 0.6f;

        // 微调数值
        protected float m_epsilonScale_Y = 1.0f;
        protected float m_epsilonScale_X = 1.0f;

        // 曲线数值设定
        protected float ms_lenX = 100.0f;
        protected float ms_lenY = 40.0f;

        // 预设曲线 Up
        protected AnimationCurve m_curveXUp;
        protected AnimationCurve m_curveYUp;

        // 预设曲线 Down
        protected AnimationCurve m_curveXDown;
        protected AnimationCurve m_curveYDown;

        // 预设曲线 Alpha变化曲线
        protected AnimationCurve ms_alphaCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0.3f, 1.0f), new Keyframe(0.3f + ms_alphaCurveTime, 0.0f) });

        // 预设曲线 Scale变化曲线 -- 暴击表现
        private static AnimationCurve ms_preScaleCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, 1.0f), new Keyframe(ms_preScaleCurveTime, 3.0f) });
        private static AnimationCurve ms_scaleCurve = new AnimationCurve(new Keyframe[] { new Keyframe(ms_preScaleCurveTime, 3.0f), new Keyframe(ms_scaleCurveTime, 1.2f) });

        public PreSettingAnimeCurve(float totalCurveTime, float rotation, float epsilonScaleY = 1.0f, float epsilonScaleX = 1.0f)
        {
            // 由曲线来控制缩放
            m_totalCurveTime = totalCurveTime;
            m_epsilonScale_Y = epsilonScaleY;
            m_epsilonScale_X = epsilonScaleX;
            InitCurves(rotation);
        }

        public PreSettingAnimeCurve(float totalCurveTime)
        {
            m_totalCurveTime = totalCurveTime;
        }

        protected virtual void InitCurves(float rotation)
        {
            float x_pos = Mathf.Cos(rotation) * ms_lenX * m_epsilonScale_X;
            float y_pos = ms_lenY + Mathf.Abs(Mathf.Sin(rotation) * ms_lenY) * m_epsilonScale_Y;

            m_curveXUp = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, 0.0f), new Keyframe(m_upCurveTime, x_pos) });
            m_curveXDown = new AnimationCurve(new Keyframe[] { new Keyframe(m_upCurveTime, x_pos), new Keyframe(m_upCurveTime + m_downCurveTime, x_pos) });

            m_curveYUp = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, 0.0f), new Keyframe(m_upCurveTime, y_pos) });
            m_curveYDown = new AnimationCurve(new Keyframe[] { new Keyframe(m_upCurveTime, y_pos), new Keyframe(m_upCurveTime + m_downCurveTime, 0.0f) });

            ms_alphaCurve = new AnimationCurve(new Keyframe[] { new Keyframe(m_upCurveTime, 1.0f), new Keyframe(m_upCurveTime + ms_alphaCurveTime, 0.0f) });
        }

        // 更新位置曲线
        public virtual bool UpdatePos(float normalizedTime, ref Vector2 pos)
        {
            bool updateSuccessed = false;
            // 测试是否属于曲线生命周期内 获得位置
            if (normalizedTime >= 0 && normalizedTime <= m_upCurveTime)
            {
                pos.x = m_curveXUp.Evaluate(normalizedTime);
                pos.y = m_curveYUp.Evaluate(normalizedTime);
                updateSuccessed = true;
            }
            else if (normalizedTime > m_upCurveTime && normalizedTime <= m_upCurveTime + m_downCurveTime)
            {
                pos.x = m_curveXDown.Evaluate(normalizedTime);
                pos.y = m_curveYDown.Evaluate(normalizedTime);
                updateSuccessed = true;
            }
            return updateSuccessed;
        }

        // 更新alpha曲线
        public virtual bool UpdateAlpha(float normalizedTime, ref float alpha)
        {
            bool updateSuccessed = false;
            // 测试是否属于alpha变化生命周期
            if (normalizedTime >= m_upCurveTime && normalizedTime <= m_upCurveTime + ms_alphaCurveTime)
            {
                alpha = ms_alphaCurve.Evaluate(normalizedTime);
                updateSuccessed = true;
            }
            return updateSuccessed;
        }

        // 更新缩放曲线
        public virtual bool UpdateScale(float normalizedTime, ref float scale)
        {
            bool updateSuccessed = false;
            // 测试是否属于缩放周期
            if (normalizedTime >= 0.0f && normalizedTime <= ms_scaleCurveTime)
            {
                if (normalizedTime < ms_preScaleCurveTime)
                {
                    scale = ms_preScaleCurve.Evaluate(normalizedTime);
                }
                else
                {
                    scale = ms_scaleCurve.Evaluate(normalizedTime);
                }
                updateSuccessed = true;
            }
            return updateSuccessed;
        }

        // 通过时间获取变化曲线
        public virtual bool UpdateAllCurve(float deltaTime, ref Vector2 pos, ref float alpha, ref float scale)
        {
            bool updateSuccessed = false;
            if (deltaTime + m_elapsedTime < m_totalCurveTime)
            {
                updateSuccessed = true;

                m_elapsedTime += deltaTime;
                m_normalizedTime = m_elapsedTime / m_totalCurveTime;

                if (!UpdatePos(m_normalizedTime, ref pos))
                {
                    updateSuccessed = false;
                }

                UpdateAlpha(m_normalizedTime, ref alpha);
                UpdateScale(m_normalizedTime, ref scale);
            }
            return updateSuccessed;
        }

    }
    /// <summary>
    /// 预设的插值曲线 使用自带的AnimationCurve -- 向下
    /// </summary>
    public class PreSettingAnimeCurveUpgrade : PreSettingAnimeCurve
    {
        private static float ms_startY = -150.0f;

        public PreSettingAnimeCurveUpgrade(float totalCurveTime, float rotation)
            : base(totalCurveTime)
        {
            ms_lenX = 150.0f;
            ms_lenY = -100.0f;
            m_upCurveTime = 1.0f;
            m_downCurveTime = 0.0f;
            this.InitCurves(rotation);
        }

        protected override void InitCurves(float rotation)
        {
            float x_pos = Mathf.Cos(rotation) * ms_lenX;
            float y_pos = ms_lenY + Mathf.Abs(Mathf.Sin(rotation)) * ms_lenY;

            m_curveXUp = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, 0.0f), new Keyframe(m_upCurveTime, x_pos * m_upCurveTime) });
            m_curveXDown = new AnimationCurve(new Keyframe[] { new Keyframe(m_upCurveTime, x_pos * m_upCurveTime), new Keyframe(m_upCurveTime + m_downCurveTime, x_pos * (m_upCurveTime + m_downCurveTime)) });

            m_curveYUp = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, ms_startY), new Keyframe(m_upCurveTime, y_pos * m_upCurveTime + ms_startY) });
            m_curveYDown = new AnimationCurve(new Keyframe[] { new Keyframe(m_upCurveTime, y_pos * m_upCurveTime + ms_startY), new Keyframe(m_upCurveTime + m_downCurveTime, y_pos * (m_upCurveTime + m_downCurveTime) + ms_startY) });

            ms_alphaCurve = new AnimationCurve(new Keyframe[] { new Keyframe(m_upCurveTime, 1.0f), new Keyframe(m_upCurveTime + ms_alphaCurveTime, 0.0f) });
        }
    }

    public class PreSettingAnimeCurve_UP : PreSettingAnimeCurve
    {
        public PreSettingAnimeCurve_UP(float totalCurveTime, float rotation, float upCurveTime)
            : base(totalCurveTime)
        {
            m_upCurveTime = upCurveTime;
            m_downCurveTime = 1.0f - upCurveTime;
            this.InitCurves(rotation);
        }

        protected override void InitCurves(float rotation)
        {
            float x_pos = Mathf.Cos(rotation) * ms_lenX;
            float y_pos = ms_lenY * 1.5f + Mathf.Abs(Mathf.Sin(rotation) * ms_lenY);

            m_curveXUp = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, 0.0f), new Keyframe(m_upCurveTime, x_pos) });
            m_curveXDown = new AnimationCurve(new Keyframe[] { new Keyframe(m_upCurveTime, x_pos), new Keyframe(m_upCurveTime + m_downCurveTime, x_pos) });

            m_curveYUp = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, 0.0f), new Keyframe(m_upCurveTime, y_pos) });
            m_curveYDown = new AnimationCurve(new Keyframe[] { new Keyframe(m_upCurveTime, y_pos), new Keyframe(m_upCurveTime + m_downCurveTime, y_pos) });

            ms_alphaCurve = new AnimationCurve(new Keyframe[] { new Keyframe(m_upCurveTime, 1.0f), new Keyframe(m_upCurveTime + ms_alphaCurveTime, 0.0f) });
        }
    }
    #endregion


    #region 插值曲线 -- Scale
    /// <summary>
    /// 预设曲线, 对一般曲线进行缩放得到新曲线
    /// </summary>
    public class ScaleableAnimeCurve
    {
        // 曲线时间控制
        private float ms_totalCurveTime = 1.0f;
        private float m_elapsedTime = 0.0f;
        private float m_normalizedTime = 0.0f;
        private static float ms_preCurveNormalizedTime = 0.7f;
        private static float ms_rearCurveNormalizedTime = 0.3f;

        // 设定分段曲线
        private AnimationCurve m_preCurveX;                 // = new AnimationCurve( new Keyframe[] { new Keyframe( 0.0f, 0.0f ), new Keyframe( 0.7f, 100.0f ) } );
        private AnimationCurve m_rearCurveX;

        private AnimationCurve m_preCurveY;
        private AnimationCurve m_rearCurveY;

        private AnimationCurve m_preCurveZ;
        private AnimationCurve m_rearCurveZ;

        // 目标向量
        private Vector3 m_targetVector;
        private Vector3 m_innerPoint;

        // 曲线位置控制
        private float m_scaleX;
        private float m_scaleY;
        private float m_scaleZ;


        /*
         * 使用预设关联点(associate = 0 -> 1.0.0    = 1 -> 1.1.0)
         * 使用一次贝塞尔计算得到中间点
         */

        public ScaleableAnimeCurve(float totalCurveTime, Vector3 targetVector, float curveAssociate = 0.0f, float curveRotate = 0.0f)
        {
            ms_totalCurveTime = totalCurveTime;
            m_innerPoint = new Vector3(1.0f, curveAssociate, 0.0f);
            if (curveRotate != 0.0f)
            {
                Matrix4x4 rotateMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(curveRotate, Vector3.forward), Vector3.one);
                m_innerPoint = rotateMatrix.MultiplyPoint(m_innerPoint);
            }

            ResetTargetPos(targetVector);
            // 初始化分段曲线       
            InitCurve();
        }

        private void InitCurve()
        {
            List<Vector3> tempPoints = new List<Vector3>();
            tempPoints.Add(Vector3.zero);
            tempPoints.Add(m_innerPoint);
            tempPoints.Add(Vector3.one);
            // 使用一次函数计算出差值曲线中间点的位置
            UnlimitedPointBezier tempCurve = new UnlimitedPointBezier(tempPoints);
            Vector3 InterpolatePoint = (Vector3)tempCurve.InterpolationPos(ms_preCurveNormalizedTime);

            m_preCurveX = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, 0.0f), new Keyframe(ms_preCurveNormalizedTime, InterpolatePoint.x) });
            m_rearCurveX = new AnimationCurve(new Keyframe[] { new Keyframe(ms_preCurveNormalizedTime, InterpolatePoint.x), new Keyframe(ms_preCurveNormalizedTime + ms_rearCurveNormalizedTime, 1.0f) });

            m_preCurveY = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, 0.0f), new Keyframe(ms_preCurveNormalizedTime, InterpolatePoint.y) });
            m_rearCurveY = new AnimationCurve(new Keyframe[] { new Keyframe(ms_preCurveNormalizedTime, InterpolatePoint.y), new Keyframe(ms_preCurveNormalizedTime + ms_rearCurveNormalizedTime, 1.0f) });

            m_preCurveZ = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, 0.0f), new Keyframe(ms_preCurveNormalizedTime, InterpolatePoint.z) });
            m_rearCurveZ = new AnimationCurve(new Keyframe[] { new Keyframe(ms_preCurveNormalizedTime, InterpolatePoint.z), new Keyframe(ms_preCurveNormalizedTime + ms_rearCurveNormalizedTime, 1.0f) });

        }

        private Vector3 EvaluateCurve(float normalizedTime)
        {
            Vector3 retVec3 = Vector3.zero;
            if (normalizedTime < ms_preCurveNormalizedTime)
            {
                retVec3.x = m_preCurveX.Evaluate(normalizedTime);
                retVec3.y = m_preCurveY.Evaluate(normalizedTime);
                retVec3.z = m_preCurveZ.Evaluate(normalizedTime);

            }
            else if (normalizedTime < (ms_preCurveNormalizedTime + ms_rearCurveNormalizedTime))
            {
                retVec3.x = m_rearCurveX.Evaluate(normalizedTime);
                retVec3.y = m_rearCurveY.Evaluate(normalizedTime);
                retVec3.z = m_rearCurveZ.Evaluate(normalizedTime);
            }
            return retVec3;
        }

        // 重置一次目标位置
        public void ResetTargetPos(Vector3 targetVector)
        {
            m_targetVector = targetVector;
            // 初始化拉伸计算
            if (m_targetVector.x >= 0 && m_targetVector.x < 0.1f)
            {
                m_targetVector.x = 0.1f;
            }
            if (m_targetVector.x <= 0 && m_targetVector.x > -0.1f)
            {
                m_targetVector.x = -0.1f;
            }

            if (m_targetVector.y >= 0 && m_targetVector.y < 0.1f)
            {
                m_targetVector.y = 0.1f;
            }
            if (m_targetVector.y <= 0 && m_targetVector.y > -0.1f)
            {
                m_targetVector.y = -0.1f;
            }

            if (m_targetVector.z >= 0 && m_targetVector.z < 0.1f)
            {
                m_targetVector.z = 0.1f;
            }
            if (m_targetVector.z <= 0 && m_targetVector.z > -0.1f)
            {
                m_targetVector.z = -0.1f;
            }

            m_scaleX = m_targetVector.x;
            m_scaleY = m_targetVector.y;
            m_scaleZ = m_targetVector.z;

        }

        // 外部调用更新曲线当前点
        public Vector3? ScaleCurvePosByDeltaTime(float deltaTime)
        {
            Vector3? retVec3 = null;
            if ((m_elapsedTime + deltaTime) < ms_totalCurveTime)
            {
                m_elapsedTime += deltaTime;
                m_normalizedTime = m_elapsedTime / ms_totalCurveTime;

                Vector3 tempVec3 = EvaluateCurve(m_normalizedTime);
                // 乘上缩放
                tempVec3.x = tempVec3.x * m_scaleX;
                tempVec3.y = tempVec3.y * m_scaleY;
                tempVec3.z = tempVec3.z * m_scaleZ;

                retVec3 = tempVec3;

            }
            return retVec3;
        }

        public Vector3? ScaleCurvePosByCurrentTime(float currentTime)
        {
            Vector3? retVec3 = null;
            if (currentTime < ms_totalCurveTime)
            {
                m_normalizedTime = currentTime / ms_totalCurveTime;

                Vector3 tempVec3 = EvaluateCurve(m_normalizedTime);
                // 乘上缩放
                tempVec3.x = tempVec3.x * m_scaleX;
                tempVec3.y = tempVec3.y * m_scaleY;
                tempVec3.z = tempVec3.z * m_scaleZ;

                retVec3 = tempVec3;

            }
            return retVec3;
        }

        public Vector3? OffsetCurvePosByDeltaTime(float deltaTime)
        {
            Vector3? retVec3 = null;
            if ((m_elapsedTime + deltaTime) < ms_totalCurveTime)
            {
                m_elapsedTime += deltaTime;
                m_normalizedTime = m_elapsedTime / ms_totalCurveTime;

                Vector3 tempInnerPoint = Vector3.Lerp(Vector3.zero, m_targetVector, m_normalizedTime);
                tempInnerPoint.x = tempInnerPoint.x * m_scaleX;
                tempInnerPoint.y = tempInnerPoint.y * m_scaleY;
                tempInnerPoint.z = tempInnerPoint.z * m_scaleZ;


                Vector3 tempOffset = EvaluateCurve(m_normalizedTime);
                // normalizedTime可以代表目标向量的线性变化
                float offsetX = tempOffset.x - m_normalizedTime;
                float offsetY = tempOffset.y - m_normalizedTime;
                float offsetZ = tempOffset.z - m_normalizedTime;

                tempOffset = new Vector3(offsetX * m_scaleX, offsetY * m_scaleY, offsetZ * m_scaleZ);
                tempOffset = tempOffset + tempInnerPoint;
                retVec3 = tempOffset;
            }

            return retVec3;
        }

    }
    #endregion


    #region Vec3插值曲线基类 -- 派生类
    /// <summary>
    /// 插值曲线基类 非实时计算
    /// </summary>
    public class ConstCurve
    {
        // 时间控制
        protected float m_totalCurveTime = 1.0f;
        protected float m_elapsedTime = 0.0f;
        protected float m_normalizedTime = 0.0f;
        protected static float ms_preCurveNormalizedTime = 0.5f;
        protected static float ms_rearCurveNormalizedTime = 0.5f;
        protected float m_missileArcSize_x = 4.0f;
        protected float m_missileArcSize_y = 3.0f;
        protected float m_missileArcSize_z = 0.0f;
        protected static float m_comparisonDis = 64.0f;

        // 设定分段曲线
        protected AnimationCurve m_preCurveX;
        protected AnimationCurve m_rearCurveX;

        protected AnimationCurve m_preCurveY;
        protected AnimationCurve m_rearCurveY;

        protected AnimationCurve m_preCurveZ;
        protected AnimationCurve m_rearCurveZ;

        // 位置控制
        protected Vector3 m_targetVector;
        protected Vector3 m_startPos;

        // 曲线控制
        protected float m_arcScale;

        #region Member Access
        public Vector3 targetVector
        {
            get
            {
                return m_targetVector;
            }
        }
        public float getCurveLifeTime
        {
            get
            {
                return m_totalCurveTime;
            }
        }
        #endregion

        public ConstCurve(float totalCurveTime, Vector3 startPos, Vector3 targetPos, float rotation = 0.0f,
            float? missileArcSize_x = null, float? missileArcSize_y = null, float? missileArcSize_z = null)
        {
            m_totalCurveTime = totalCurveTime;
            m_startPos = startPos;
            ResetTargetVector(targetPos);
            if (missileArcSize_x.HasValue)
            {
                // 修改预设曲率半径
                m_missileArcSize_x = (float)missileArcSize_x;
            }
            if (missileArcSize_y.HasValue)
            {
                m_missileArcSize_y = (float)missileArcSize_y;
            }
            if (missileArcSize_z.HasValue)
            {
                m_missileArcSize_z = (float)missileArcSize_z;
            }
            InitCurves(rotation, m_missileArcSize_x, m_missileArcSize_y, m_missileArcSize_z);
        }

        public virtual void InitCurves(float rotation, float missileArcSize_x, float missileArcSize_y, float missileArcSize_z)
        {
            // 以弹道距离作为弹道与直线的相关性
            m_arcScale = Mathf.Min(Vector3.SqrMagnitude(m_targetVector) / m_comparisonDis, 1.0f);
            float x_pos = Mathf.Cos(rotation) * m_arcScale * missileArcSize_x;
            float y_pos = Mathf.Abs(Mathf.Sin(rotation) * m_arcScale * missileArcSize_y);
            float z_pos = m_arcScale * missileArcSize_z;

            m_preCurveX = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, 0.0f), new Keyframe(ms_preCurveNormalizedTime, x_pos) });
            m_rearCurveX = new AnimationCurve(new Keyframe[] { new Keyframe(ms_preCurveNormalizedTime, x_pos), new Keyframe(ms_preCurveNormalizedTime + ms_rearCurveNormalizedTime, 0.0f) });

            m_preCurveY = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, 0.0f), new Keyframe(ms_preCurveNormalizedTime, y_pos) });
            m_rearCurveY = new AnimationCurve(new Keyframe[] { new Keyframe(ms_preCurveNormalizedTime, y_pos), new Keyframe(ms_preCurveNormalizedTime + ms_rearCurveNormalizedTime, 0.0f) });

            m_preCurveZ = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, 0.0f), new Keyframe(ms_preCurveNormalizedTime, z_pos) });
            m_rearCurveZ = new AnimationCurve(new Keyframe[] { new Keyframe(ms_preCurveNormalizedTime, z_pos), new Keyframe(ms_preCurveNormalizedTime + ms_rearCurveNormalizedTime, 0.0f) });

        }

        protected Vector3 EvaluateCurve(float normalizedTime)
        {
            Vector3 retVec3 = Vector3.zero;
            if (normalizedTime >= 0 && normalizedTime <= ms_preCurveNormalizedTime)
            {
                retVec3.x = m_preCurveX.Evaluate(normalizedTime);
                retVec3.y = m_preCurveY.Evaluate(normalizedTime);
                retVec3.z = m_preCurveZ.Evaluate(normalizedTime);

            }
            else if (normalizedTime > ms_preCurveNormalizedTime && normalizedTime <= (ms_preCurveNormalizedTime + ms_rearCurveNormalizedTime))
            {
                retVec3.x = m_rearCurveX.Evaluate(normalizedTime);
                retVec3.y = m_rearCurveY.Evaluate(normalizedTime);
                retVec3.z = m_rearCurveZ.Evaluate(normalizedTime);
            }
            return retVec3;
        }

        // 重置一次目标位置
        public virtual void ResetTargetVector(Vector3 targetPos, Vector3? startPos = null)
        {
            m_targetVector = targetPos - m_startPos;
        }


        // 内部控制曲线
        public virtual Vector3? UpdateCurvePosByDeltaTime(float deltaTime)
        {
            Vector3? retVec3 = null;
            if (deltaTime + m_elapsedTime < m_totalCurveTime)
            {
                Vector3 offsetPos = EvaluateCurve(m_normalizedTime);
                offsetPos = offsetPos + Vector3.Lerp(Vector3.zero, m_targetVector, m_normalizedTime);

                retVec3 = offsetPos + m_startPos;
            }
            return retVec3;
        }
        // 外部控制曲线
        public virtual Vector3? UpdateCurvePosByNormalizedTime(float normalizedTime)
        {
            Vector3? retVec3 = null;
            if (normalizedTime >= 0 && normalizedTime <= 1)
            {
                Vector3 offsetPos = EvaluateCurve(normalizedTime);
                offsetPos = offsetPos + Vector3.Lerp(Vector3.zero, m_targetVector, normalizedTime);

                retVec3 = offsetPos + m_startPos;
            }
            return retVec3;
        }

        public virtual void Clear()
        {
            // 清除曲线
            if (m_preCurveX != null)
            {
                m_preCurveX = null;
            }
            if (m_rearCurveX != null)
            {
                m_rearCurveX = null;
            }
            if (m_preCurveY != null)
            {
                m_preCurveY = null;
            }
            if (m_rearCurveY != null)
            {
                m_rearCurveY = null;
            }
            if (m_preCurveZ != null)
            {
                m_preCurveZ = null;
            }
            if (m_rearCurveZ != null)
            {
                m_rearCurveZ = null;
            }
        }
    }

    /// <summary>
    /// 制作直线
    /// </summary>
    public class Straightline : ConstCurve
    {
        // vars -- setting a shaking control value
        private static float ms_shakingVal = 0.01f;
        private float m_speed = 10.0f;

        public Straightline(float totalCurveTime, Vector3 startPos, Vector3 targetPos, float speed = 10.0f)
            : base(totalCurveTime, startPos, targetPos, 0.0f, 0.0f, 0.0f)
        {
            m_speed = speed;
        }

        // 防止抖动 -- 修改为直接以当前效果点为起始点
        public override void ResetTargetVector(Vector3 targetPos, Vector3? startPos = null)
        {
            if (startPos.HasValue)
            {
                m_startPos = (Vector3)startPos;
            }
            base.ResetTargetVector(targetPos);
        }

        public override Vector3? UpdateCurvePosByDeltaTime(float deltaTime)
        {
            Vector3? retVec3 = null;
            retVec3 = m_startPos + Vector3.Normalize(m_targetVector) * deltaTime * m_speed;
            return retVec3;
        }

        public override Vector3? UpdateCurvePosByNormalizedTime(float normalizedTime)
        {
            Vector3? retVec3 = null;
            if (normalizedTime >= 0 && normalizedTime <= 1)
            {
                // 直线计算不需要用到插值曲线
                Vector3 offsetPos = m_targetVector * normalizedTime;

                retVec3 = offsetPos + m_startPos;
            }
            return retVec3;
        }
    }



    /// <summary>
    /// 蛇形曲线 由于是世界坐标插值, 只使用y插值
    /// </summary>
    public class SerpentineCurve : ConstCurve
    {
        public SerpentineCurve(float totalCurveTime, Vector3 startPos, Vector3 targetPos)
            : base(totalCurveTime, startPos, targetPos, 0.0f, 3.0f, 0.0f)
        {
            // 因为y的估值为正数, 使用x的估值作为y偏移的值
        }

        public override Vector3? UpdateCurvePosByDeltaTime(float deltaTime)
        {
            Vector3? retVec3 = null;
            if (deltaTime + m_elapsedTime < m_totalCurveTime)
            {
                m_elapsedTime += deltaTime;
                m_normalizedTime = m_elapsedTime / m_totalCurveTime;

                retVec3 = UpdateCurvePosByNormalizedTime(m_normalizedTime);
            }
            return retVec3;
        }

        public override Vector3? UpdateCurvePosByNormalizedTime(float normalizedTime)
        {
            Vector3? retVec3 = null;
            if (normalizedTime >= 0 && normalizedTime <= 1)
            {
                float curveRepeat = Vector3.Distance(Vector3.zero, m_targetVector) / 4.0f;
                float adjustedTime = (normalizedTime * curveRepeat) % 1.0f;

                Vector3 offsetPos = EvaluateCurve(adjustedTime);
                offsetPos.y = offsetPos.x;
                offsetPos.x = 0.0f;
                offsetPos.z = 0.0f;
                offsetPos = offsetPos + Vector3.Lerp(Vector3.zero, m_targetVector, normalizedTime);

                retVec3 = offsetPos + m_startPos;
            }


            return retVec3;
        }
    }

    /// <summary>
    /// 使用物体本地坐标制作插值曲线
    /// </summary>
    public class UVNAxisCurve : ConstCurve
    {
        // UNV坐标系曲线扩展 使用一个静态GameObject维护全部相对坐标系
        protected static GameObject ms_UVN_Axis;
        protected float m_zDistance;
        protected Matrix4x4 m_localToWorld;

        public UVNAxisCurve(float totalCurveTime, Vector3 startPos, Vector3 targetPos, float rotation = 0.0f)
            : base(totalCurveTime, startPos, targetPos, rotation)
        {
            InitAxis(startPos, targetPos);
        }

        // 复杂设置
        public UVNAxisCurve(float totalCurveTime, Vector3 startPos, Vector3 targetPos, float rotation,
            float missileArcSize_x, float missileArcSize_y, float missileArcSize_z)
            : base(totalCurveTime, startPos, targetPos, rotation, missileArcSize_x, missileArcSize_y, missileArcSize_z)
        {
            InitAxis(startPos, targetPos);
        }

        private void InitAxis(Vector3 startPos, Vector3 targetPos)
        {
            if (ms_UVN_Axis == null)
            {
                ms_UVN_Axis = new GameObject();
                ms_UVN_Axis.name = "LocalAxisTrans";
                GameObject.DontDestroyOnLoad(ms_UVN_Axis);
                ms_UVN_Axis.hideFlags = HideFlags.HideInHierarchy;
            }
            if (ms_UVN_Axis != null)
            {
                ms_UVN_Axis.transform.position = startPos;
                this.ResetTargetVector(targetPos);
            }
        }

        public override void ResetTargetVector(Vector3 targetPos, Vector3? startPos = null)
        {
            base.ResetTargetVector(targetPos);
            if (ms_UVN_Axis != null)
            {
                ms_UVN_Axis.transform.localRotation = Quaternion.LookRotation(m_targetVector);
                m_localToWorld = ms_UVN_Axis.transform.localToWorldMatrix;
            }
            m_zDistance = Vector3.Distance(Vector3.zero, m_targetVector);
        }

        public override Vector3? UpdateCurvePosByDeltaTime(float deltaTime)
        {
            Vector3? retVec3 = null;
            if (deltaTime + m_elapsedTime < m_totalCurveTime)
            {
                m_elapsedTime += deltaTime;
                m_normalizedTime = m_elapsedTime / m_totalCurveTime;

                retVec3 = UpdateCurvePosByNormalizedTime(m_normalizedTime);
            }
            return retVec3;
        }

        public override Vector3? UpdateCurvePosByNormalizedTime(float normalizedTime)
        {
            Vector3? retVec3 = null;
            if (normalizedTime >= 0 && normalizedTime <= 1)
            {
                Vector3 offsetPos = base.EvaluateCurve(normalizedTime);
                Vector3 localZPos = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, normalizedTime * m_zDistance), normalizedTime);
                offsetPos = offsetPos + localZPos;

                Vector3 worldPos = m_localToWorld.MultiplyPoint(offsetPos);
                retVec3 = worldPos;
            }
            return retVec3;
        }

        protected Vector3? BaseUpdateCurvePosByNormalizedTime(float normalizedTime)
        {
            return base.UpdateCurvePosByNormalizedTime(normalizedTime);
        }
    }

    /// <summary>
    /// 相对坐标系插值曲线 眼镜蛇弹道
    /// </summary>
    public class UVNAxisCurve_Serpentine : UVNAxisCurve
    {
        // 曲线在x轴的偏移量
        private static float ms_xValue = 3.0f;

        public UVNAxisCurve_Serpentine(float totalCurveTime, Vector3 startPos, Vector3 targetPos)
            : base(totalCurveTime, startPos, targetPos, 0.0f, ms_xValue, 0.0f, 0.0f)
        {
        }

        public override Vector3? UpdateCurvePosByDeltaTime(float deltaTime)
        {
            Vector3? retVec3 = null;
            if (deltaTime + m_elapsedTime < m_totalCurveTime)
            {
                m_elapsedTime += deltaTime;
                m_normalizedTime = m_elapsedTime / m_totalCurveTime;

                retVec3 = UpdateCurvePosByNormalizedTime(m_normalizedTime);
            }
            return retVec3;
        }

        public override Vector3? UpdateCurvePosByNormalizedTime(float normalizedTime)
        {
            Vector3? retVec3 = null;
            if (normalizedTime >= 0 && normalizedTime <= 1)
            {
                // 重复次数为每xx距离一次
                float curveRepeat = m_zDistance / 3.0f;
                float adjustedTime = (normalizedTime * curveRepeat) % 1.0f;

                Vector3 offsetPos = base.EvaluateCurve(adjustedTime);
                offsetPos = AdjustXPos(offsetPos);
                // 按照归一化数压缩 offsetPos 确保最后特效会打到目标身上
                offsetPos = offsetPos * (1.0f - normalizedTime);

                Vector3 localZPos = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, normalizedTime * m_zDistance), normalizedTime);
                offsetPos = offsetPos + localZPos;

                Vector3 worldPos = m_localToWorld.MultiplyPoint(offsetPos);
                retVec3 = worldPos;
            }
            return retVec3;
        }

        // 由于x轴插值为 0~x 需要添加偏移值

        private Vector3 AdjustXPos(Vector3 offsetPos)
        {
            Vector3 retVec3 = offsetPos;
            float x_halfVal = m_arcScale * ms_xValue / 2.0f;
            retVec3.x = retVec3.x - x_halfVal;
            return retVec3;
        }
    }

    /// <summary>
    /// 相对坐标系插值曲线 弧线弹道(北半球弧线)
    /// </summary>
    public class UVNAxisCurve_Rainbow : UVNAxisCurve
    {

        public UVNAxisCurve_Rainbow(float totalCurveTime, Vector3 startPos, Vector3 targetPos, float rotation = 0.0f)
            : base(totalCurveTime, startPos, targetPos, rotation)
        {
            // 以弹道距离作为弹道与直线的相关性
            m_arcScale = Mathf.Min(Vector3.SqrMagnitude(m_targetVector) / m_comparisonDis, 1.0f);
            float x_pos = Mathf.Cos(rotation) * m_arcScale * 0.0f;
            float y_pos = Mathf.Abs(Mathf.Sin(rotation) * m_arcScale * 5.0f);
            float z_pos = m_arcScale * 0.0f;
            ms_preCurveNormalizedTime = 0.7f;
            ms_rearCurveNormalizedTime = 0.3f;
            m_preCurveX = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, 0.0f), new Keyframe(ms_preCurveNormalizedTime, x_pos) });
            m_rearCurveX = new AnimationCurve(new Keyframe[] { new Keyframe(ms_preCurveNormalizedTime, x_pos), new Keyframe(ms_preCurveNormalizedTime + ms_rearCurveNormalizedTime, 0.0f) });

            m_preCurveY = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, 0.0f), new Keyframe(ms_preCurveNormalizedTime, y_pos) });
            m_rearCurveY = new AnimationCurve(new Keyframe[] { new Keyframe(ms_preCurveNormalizedTime, y_pos), new Keyframe(ms_preCurveNormalizedTime + ms_rearCurveNormalizedTime, 0.0f) });

            m_preCurveZ = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, 0.0f), new Keyframe(ms_preCurveNormalizedTime, 0.0f) });
            m_rearCurveZ = new AnimationCurve(new Keyframe[] { new Keyframe(ms_preCurveNormalizedTime, 0.0f), new Keyframe(ms_preCurveNormalizedTime + ms_rearCurveNormalizedTime, 0.1f) });

        }


        public override Vector3? UpdateCurvePosByDeltaTime(float deltaTime)
        {
            Vector3? retVec3 = null;
            if (deltaTime + m_elapsedTime < m_totalCurveTime)
            {
                m_elapsedTime += deltaTime;
                m_normalizedTime = m_elapsedTime / m_totalCurveTime;

                retVec3 = UpdateCurvePosByNormalizedTime(m_normalizedTime);
            }
            return retVec3;
        }

        public override Vector3? UpdateCurvePosByNormalizedTime(float normalizedTime)
        {
            return base.BaseUpdateCurvePosByNormalizedTime(normalizedTime);
        }
    }

    /// <summary>
    /// 相对坐标系插值曲线 弧线弹道(北半球弧线)
    /// </summary>
    public class UVNAxisCurve_Accelerate : UVNAxisCurve
    {
        private static float ms_preCurveTime = 0.3f;
        private float m_evaluateTime;

        public UVNAxisCurve_Accelerate(float totalCurveTime, Vector3 startPos, Vector3 targetPos, float rotation = 0.0f)
            : base(totalCurveTime, startPos, targetPos, rotation)
        {
            m_evaluateTime = ms_preCurveTime + 0.05f;
            ResetCurve(rotation);
        }

        private void ResetCurve(float rotation)
        {
            float x_pos = Mathf.Cos(rotation) * m_arcScale * m_missileArcSize_x;
            float y_pos = Mathf.Abs(Mathf.Sin(rotation) * m_arcScale * m_missileArcSize_y);

            m_preCurveX = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, 0.0f), new Keyframe(ms_preCurveTime, x_pos) });
            m_rearCurveX = AnimationCurve.Linear(ms_preCurveTime, x_pos, 1.0f, 0.0f);

            m_preCurveY = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, 0.0f), new Keyframe(ms_preCurveTime, y_pos) });
            m_rearCurveY = AnimationCurve.Linear(ms_preCurveTime, y_pos, 1.0f, 0.0f);
        }

        new private Vector3 EvaluateCurve(float normalizedTime)
        {
            Vector3 retVec3 = Vector3.zero;
            if (normalizedTime >= 0 && normalizedTime <= ms_preCurveTime)
            {
                retVec3.x = m_preCurveX.Evaluate(normalizedTime);
                retVec3.y = m_preCurveY.Evaluate(normalizedTime);
                retVec3.z = m_preCurveZ.Evaluate(normalizedTime);

            }
            else if (normalizedTime > ms_preCurveTime && normalizedTime < 1.0f)
            {
                retVec3.x = m_rearCurveX.Evaluate(normalizedTime);
                retVec3.y = m_rearCurveY.Evaluate(normalizedTime);
                retVec3.z = m_rearCurveZ.Evaluate(normalizedTime);
            }
            return retVec3;
        }

        public override Vector3? UpdateCurvePosByDeltaTime(float deltaTime)
        {
            Vector3? retVec3 = null;
            if (deltaTime + m_elapsedTime < m_totalCurveTime)
            {
                m_elapsedTime += deltaTime;
                m_normalizedTime = m_elapsedTime / m_totalCurveTime;

                retVec3 = UpdateCurvePosByNormalizedTime(m_normalizedTime);
            }
            return retVec3;
        }

        public override Vector3? UpdateCurvePosByNormalizedTime(float normalizedTime)
        {
            Vector3? retVec3 = null;
            if (normalizedTime >= 0 && normalizedTime <= 1)
            {
                if (normalizedTime > m_evaluateTime)
                {
                    normalizedTime = Mathf.Min(normalizedTime + (normalizedTime - m_evaluateTime) * 2, 1.0f);
                }
                Vector3 offsetPos = this.EvaluateCurve(normalizedTime);
                offsetPos = offsetPos + Vector3.Lerp(Vector3.zero, m_targetVector, normalizedTime);

                retVec3 = offsetPos + m_startPos;
            }
            return retVec3;
        }


    }

    /// <summary>
    /// 相对坐标系插值曲线 龙卷风弹道
    /// </summary>
    public class UVNAxisCurve_Cyclone : UVNAxisCurve
    {
        // 曲线在x轴的偏移量
        private static float ms_xValue = 2.0f;
        private static float ms_yValue = 2.0f;


        public UVNAxisCurve_Cyclone(float totalCurveTime, Vector3 startPos, Vector3 targetPos)
            : base(totalCurveTime, startPos, targetPos, 0.0f, ms_xValue, ms_yValue, 0.0f)
        {
            ResetCurves();
        }


        // 使用平均插值
        private void ResetCurves()
        {
            // 以弹道距离作为弹道与直线的相关性           
            float x_pos = m_arcScale * ms_xValue;
            float y_pos = m_arcScale * ms_yValue;

            m_preCurveX = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, -x_pos), new Keyframe(0.5f, 0.0f) });
            m_rearCurveX = new AnimationCurve(new Keyframe[] { new Keyframe(0.5f, 0.0f), new Keyframe(1.0f, x_pos) });

            m_preCurveY = new AnimationCurve(new Keyframe[] { new Keyframe(0.0f, -y_pos), new Keyframe(0.5f, 0.0f) });
            m_rearCurveY = new AnimationCurve(new Keyframe[] { new Keyframe(0.5f, 0.0f), new Keyframe(1.0f, y_pos) });

        }

        public override Vector3? UpdateCurvePosByDeltaTime(float deltaTime)
        {
            Vector3? retVec3 = null;
            if (deltaTime + m_elapsedTime < m_totalCurveTime)
            {
                m_elapsedTime += deltaTime;
                m_normalizedTime = m_elapsedTime / m_totalCurveTime;

                retVec3 = UpdateCurvePosByNormalizedTime(m_normalizedTime);
            }
            return retVec3;
        }

        public override Vector3? UpdateCurvePosByNormalizedTime(float normalizedTime)
        {
            Vector3? retVec3 = null;
            if (normalizedTime >= 0 && normalizedTime <= 1)
            {
                // 重复次数为每xx距离一次
                float curveRepeat = m_zDistance / 3.0f;
                float adjustedTime = (normalizedTime * curveRepeat) % 1.0f;

                Vector3 offsetPos = base.EvaluateCurve(adjustedTime);
                // 按照归一化数压缩 offsetPos 确保最后特效会打到目标身上
                offsetPos = offsetPos * (1.0f - normalizedTime);

                Vector3 localZPos = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, normalizedTime * m_zDistance), normalizedTime);
                offsetPos = offsetPos + localZPos;

                Vector3 worldPos = m_localToWorld.MultiplyPoint(offsetPos);
                retVec3 = worldPos;
            }
            return retVec3;
        }
    }
    #endregion


    #region 其他曲线

    /// <summary>
    /// mixed Straightline and iTween -- x-z移动控制为一般曲线, y移动曲线为iTweenType曲线
    /// </summary>
    public class ComplexCurve
    {
        public enum iTweenType
        {
            DEFAULT = -1,
            linear = 0,
            easeOutBounce,
            easeOutQuad,
            easeInQuad,
            easeOutQuart,
            rolling
        }

        private Vector3 m_startPos_XZ;
        private Vector3 m_startPos_Y;

        private Vector3 m_endPos_XZ;
        private Vector3 m_endPos_Y;

        private Vector3 curXZPos = Vector3.zero;
        private Vector3 curYPos = Vector3.zero;

        private float m_startScale = 1.0f;
        private float m_tagScale = 1.0f;

        private iTweenType m_iTweenTypeY = iTweenType.DEFAULT;
        private iTweenType m_iTweenTypeXZ = iTweenType.DEFAULT;

        private float m_totalCurveTime = 1.0f;
        private float m_elapsedTime = 0.0f;
        private float m_normalizedTime = 0.0f;

        private int m_specialVar;
        private System.Func<Vector3?> m_setTagPos = null;

        public int specialVar {
            set {
                m_specialVar = value;
            }
        }

        public ComplexCurve(float totalCurveTime, Vector3 startPos, Vector3 targetPos,
            iTweenType curveTypeXZ = iTweenType.DEFAULT,
            iTweenType curveTypeY = iTweenType.DEFAULT,
            System.Func<Vector3?> setTagPos = null )
        {
            m_iTweenTypeXZ = curveTypeXZ;
            m_iTweenTypeY = curveTypeY;

            m_elapsedTime = 0.0f;
            m_totalCurveTime = totalCurveTime;

            m_startPos_XZ = new Vector3(startPos.x, 0.0f, startPos.z);
            m_endPos_XZ = new Vector3(targetPos.x, 0.0f, targetPos.z);

            m_startPos_Y = Vector3.up * startPos.y;
            m_endPos_Y = Vector3.up * targetPos.y;

            m_setTagPos = setTagPos;
        }

        public Vector3? UpdateCurvePosByDeltaTime(float deltaTime)
        {
            Vector3? retVec3 = null;
            if( m_setTagPos != null)
            {
                var tempPos = m_setTagPos();
                if (tempPos.HasValue)
                {
                    ResetTargetVector( tempPos.Value );
                }
            }

            if (m_elapsedTime + deltaTime < m_totalCurveTime)
            {
                m_elapsedTime += deltaTime;
                m_normalizedTime = m_elapsedTime / m_totalCurveTime;

                // xz                
                if (m_iTweenTypeXZ != iTweenType.DEFAULT)
                {
                    curXZPos = CaculateByCurve(m_startPos_XZ, m_endPos_XZ, m_iTweenTypeXZ, m_normalizedTime);
                }
                else
                {
                    curXZPos = m_startPos_XZ;
                }
                // y               
                if (m_iTweenTypeY != iTweenType.DEFAULT)
                {
                    curYPos = CaculateByCurve(m_startPos_Y, m_endPos_Y, m_iTweenTypeY, m_normalizedTime);
                }
                else
                {
                    curYPos = m_startPos_Y;
                }
                retVec3 = curXZPos + curYPos;
            }
            return retVec3;
        }

        public float UpdateScale()
        {
            return Mathf.Lerp(m_startScale, m_tagScale, m_normalizedTime);
        }

        private Vector3 CaculateByCurve(Vector3 startPos, Vector3 endPos, iTweenType curveType, float value)
        {
            if (curveType != iTweenType.rolling)
            {
                return Vector3.Lerp(startPos, endPos, SwitchiTweenCurve(curveType, value));
            }
            else
            {
                // 对value进行非线性处理, 整理成加速效果
                Vector3 dir = endPos - startPos;
                return endPos + rollingY(dir, easeInCubic(0.0f, 1.0f, value), m_specialVar);
            }
        }

        private float SwitchiTweenCurve(iTweenType type, float normalized, Vector2? Dir = null)
        {
            switch (type)
            {
                case iTweenType.linear:
                    {
                        return linear(0.0f, 1.0f, normalized);
                    } break;
                case iTweenType.easeOutQuad:
                    {
                        return easeOutQuad(0.0f, 1.0f, normalized);
                    } break;
                case iTweenType.easeOutBounce:
                    {
                        return easeOutBounce(0.0f, 1.0f, normalized * 1.3f);
                    } break;
                case iTweenType.easeInQuad:
                    {
                        return easeInQuad(0.0f, 1.0f, normalized);
                    } break;
                case iTweenType.easeOutQuart:
                    {
                        return easeOutQuart(0.0f, 1.0f, normalized);
                    } break;
                default:
                    {
                        return normalized;
                    } break;
            }
        }

        public void ResetTargetVector(Vector3 targetPos)
        {
            m_endPos_XZ = new Vector3(targetPos.x, 0.0f, targetPos.z);
            m_endPos_Y = Vector3.up * targetPos.y;
        }

        public void ResetScale(float tagetSclae, float startScale)
        {
            m_tagScale = tagetSclae;
            m_startScale = startScale;
        }
    }
    #endregion


    #region 数值变换
    // read UInt16 from Byte[]
    public static UInt16 BytesToUInt16(byte[] data, int startPos = 0)
    {
        UInt16 retValue = 0;
        if (data != null)
        {
            int dataLen = data.Length;
            if (dataLen > startPos)
            {
                retValue = (UInt16)(data[startPos]);
            }
            if (dataLen > (startPos + 1))
            {
                retValue |= (UInt16)(data[startPos + 1] << 8);
            }
        }
        return retValue;
    }
    // read UInt32 from Byte[]
    public static UInt32 BytesToUInt32(byte[] data, int startPos = 0)
    {
        UInt32 retValue = 0;
        if (data != null)
        {
            int dataLen = data.Length;
            if (dataLen > startPos)
            {
                retValue = (UInt32)(data[startPos]);
            }
            if (dataLen > (startPos + 1))
            {
                retValue |= (UInt32)(data[startPos + 1] << 8);
            }
            if (dataLen > (startPos + 2))
            {
                retValue |= (UInt32)(data[startPos + 2] << (8 * 2));
            }
            if (dataLen > (startPos + 3))
            {
                retValue |= (UInt32)(data[startPos + 3] << (8 * 3));
            }
        }
        return retValue;
    }
    // NextPowerOf2
    public static UInt32 NextPowerOf2(UInt32 value)
    {
        UInt32 rval = 1;
        Byte counter = 1;
        while (rval < value && counter != 32)
        {
            rval <<= 1;
            counter++;
        }
        if (counter == 32)
        {
            rval = UInt32.MaxValue;
        }
        return rval;
    }

    public static string DecodeUInt32ToString(string str, Int32 colorValue)
    {
        return string.Format("[{0:x}]{1}[-]", colorValue, str);
    }

    //模拟摇杆方向 -- 屏幕方向 -> 世界方向
    //public static Vector2? JoystickDegreeToDirection( int degree ) {
    //    if ( Camera.main != null ) {
    //        Vector3 cdir = new Vector3( Mathf.Cos( Mathf.Deg2Rad * ( -degree ) ), 0.0f, Mathf.Sin( Mathf.Deg2Rad * ( -degree ) ) );
    //        var wdir = Camera.main.cameraToWorldMatrix.MultiplyVector( cdir );
    //        float yaw = MathLib.CalculateYaw( wdir );
    //        return new Vector2( Mathf.Sin( yaw ), Mathf.Cos( yaw ) );               // x,z
    //    } else {
    //        return null;
    //    }
    //}
    // 反向方向 -- 世界方向 -> 屏幕方向
    public static Vector2 WorldPosToScreenDegree(Vector3 worldStartPos, Vector3 worldTargetPos)
    {
        // 开始向量不一定在中心点 -- 简易计算       
        //UniScene.BaseScene scene = WorldState.activeScene;
        if (Camera.main != null)
        {
            Vector2 screenStartPos = Camera.main.WorldToViewportPoint(worldStartPos);
            Vector2 screenTargetPos = Camera.main.WorldToViewportPoint(worldTargetPos);
            return (screenTargetPos - screenStartPos).normalized;
        }
        return Vector2.up;
    }
    // 反向方向 -- 世界方向 -> 屏幕方向 -> 限制在某个屏幕空间内
    public static Vector2 WorldPosToScreenDegree_Limited(Vector3 worldStartPos, Vector3 worldTargetPos)
    {
        // 开始向量不一定在中心点 -- 简易计算       
        //UniScene.BaseScene scene = WorldState.activeScene;
        if (Camera.main != null)
        {
            // target边缘时, 位置置换
            Vector2 screenStartPos = Camera.main.WorldToViewportPoint(worldStartPos);
            if (screenStartPos.x > 0.8f)
            {
                screenStartPos.x = 0.2f;
            }
            else if (screenStartPos.x < 0.2f)
            {
                screenStartPos.x = 0.8f;
            }
            if (screenStartPos.y > 0.8f)
            {
                screenStartPos.y = 0.2f;
            }
            else if (screenStartPos.y < 0.2f)
            {
                screenStartPos.y = 0.8f;
            }
            Vector2 screenTargetPos = Camera.main.WorldToViewportPoint(worldTargetPos);
            if (screenTargetPos.x > 0.8f)
            {
                screenTargetPos.x = 0.2f;
            }
            else if (screenTargetPos.x < 0.2f)
            {
                screenTargetPos.x = 0.8f;
            }
            if (screenTargetPos.y > 0.8f)
            {
                screenTargetPos.y = 0.2f;
            }
            else if (screenTargetPos.y < 0.2f)
            {
                screenTargetPos.y = 0.8f;
            }
            return (screenTargetPos - screenStartPos).normalized;
        }
        return Vector2.up;
    }

    public static float UnilateralCloseTo(float standard, float v)
    {
        if (standard <= 0.0f)
        {
            return 1.0f;
        }
        float adjustedVal = Mathf.Abs(v);
        bool doAfterAdjust = false;
        while (adjustedVal > standard)
        {
            doAfterAdjust = true;
            adjustedVal -= standard;
        }
        if (doAfterAdjust)
        {
            adjustedVal = standard - adjustedVal;
        }
        return adjustedVal;
    }

    public static Vector3 GetNearestNodeFromRay(List<Vector3> points, ref Ray ray, ref float minDistance)
    {
        if (points != null)
        {
            float minDis = float.MaxValue;
            Vector3 minVec3 = ray.origin;
            for (int i = 0; i < points.Count; i++)
            {
                var pos = points[i];
                float getDis = Distance(ref pos, ref ray);
                if (getDis < minDis)
                {
                    minDis = getDis;
                    minVec3 = pos;
                }
            }
            minDistance = minDis;
            return minVec3;
        }
        return ray.origin;
    }
    // 点到射线最短距离(无垂线距离时取到射线原点距离)
    private static float Distance(ref Vector3 pointPos, ref Ray ray)
    {
        Vector3 dir_orgin_point = pointPos - ray.origin;
        float dotDir = Vector3.Dot(dir_orgin_point, ray.direction);
        if (dotDir > 0)
        {
            return Mathf.Sqrt(dir_orgin_point.sqrMagnitude - dotDir * dotDir);          // ray.direction is normalized
        }
        return dir_orgin_point.magnitude;
    }
    // 与射线垂直交点位置
    public static Vector3? CrossPoint(ref Vector3 pointPos, ref Ray ray)
    {
        Vector3 dir_orgin_point = pointPos - ray.origin;
        float dotDir = Vector3.Dot(dir_orgin_point, ray.direction);
        if (dotDir >= 0)
        {
            return ray.GetPoint(dotDir);
        }
        return null;
    }

    public static bool CheckPointsInLine(ref Vector3 point0, ref Vector3 point1, ref Vector3 point2)
    {
        float x01 = point0.x - point1.x;
        float x21 = point2.x - point1.x;

        float y01 = point0.y - point1.y;
        float y21 = point2.y - point1.y;

        float z01 = point0.z - point1.z;
        float z21 = point2.z - point1.z;

        float var1 = x01 * y21 * z21;
        float var2 = y01 * x21 * z21;
        float var3 = z01 * x21 * y21;

        if (var1 == var2 && var1 == var3)
        {
            return true;
        }
        return false;
    }

    public static float GetVectorAngle( Vector3 from_, Vector3 to_ ) {
        Vector3 v3 = Vector3.Cross( from_, to_ );
        if ( v3.z > 0 )
            return Vector3.Angle( from_, to_ );
        else
            return 360 - Vector3.Angle( from_, to_ );
    }

    //两点决定的直线与x轴的角度
    public static float GetVector2Angle( Vector2 from_, Vector2 to_ ) {
        if ( to_.y - from_.y == 0 || to_.y - from_.y == 0 ) return 0;

        float angel = Mathf.Atan( Mathf.Abs( to_.y - from_.y ) / Mathf.Abs( to_.x - from_.x ) ) * 180 / Mathf.PI;
        return angel;
    }

    //层级用Z轴控制，取决于纵向Y
    public static Vector3 TurnVector3For2DGame( Vector3 v3 ) {
        return new Vector3( v3.x, v3.y, v3.y );
    }

    public static float GetVectorSqrMagnitude( Vector3 v1, Vector3 v2 ) {
        return ( v1 - v2 ).sqrMagnitude;
    }

    //向量是否近似
    public static bool IsApproximateVector3( Vector3 v1, Vector3 v2 ) {
        float value = 0.01f;
        return ( Mathf.Abs( v1.x - v2.x ) <= value
            && Mathf.Abs( v1.y - v2.y ) <= value
            && Mathf.Abs( v1.z - v2.z ) <= value );
    }
    #endregion

    #region 

    #endregion
}
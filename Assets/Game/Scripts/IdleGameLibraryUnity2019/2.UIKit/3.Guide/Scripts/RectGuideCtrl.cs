﻿/****************************************************************************
 * .3 

 ****************************************************************************/
using UnityEngine;

namespace QFramework
{
	public class RectGuideCtrl : GuideCtrl
	{
		/// <summary>
		/// 镂空区域中心
		/// </summary>
		private Vector4 mCenter;
		/// <summary>
		/// 最终的偏移值X
		/// </summary>
		private float mTargetOffsetX = 0f;
		/// <summary>
		/// 最终的偏移值Y
		/// </summary>
		private float mTargetOffsetY = 0f;
		/// <summary>
		/// 当前的偏移值X
		/// </summary>
		private float mCurrentOffsetX = 0f;
		/// <summary>
		/// 当前的偏移值Y
		/// </summary>
		private float mCurrentOffsetY = 0f;

		protected override void SetMatShader()
		{
			mShaderName = "UI/Guide/RectGuide";
		}

		protected override void InitData()
		{
			//获取高亮区域四个顶点的世界坐标
			Target.rectTransform.GetWorldCorners(_corners);
			//计算高亮显示区域咋画布中的范围
			mTargetOffsetX = Vector2.Distance(World2CanvasPos(mCanvas, _corners[0]), World2CanvasPos(mCanvas, _corners[3])) / 2f;
			mTargetOffsetY = Vector2.Distance(World2CanvasPos(mCanvas, _corners[0]), World2CanvasPos(mCanvas, _corners[1])) / 2f;
			//计算高亮显示区域的中心
			float x = _corners[0].x + ((_corners[3].x - _corners[0].x) / 2f);
			float y = _corners[0].y + ((_corners[1].y - _corners[0].y) / 2f);
			Vector3 centerWorld = new Vector3(x,y,0);
			Vector2 center = World2CanvasPos(mCanvas, centerWorld);
			//设置遮罩材料中中心变量
			Vector4 centerMat = new Vector4(center.x,center.y,0,0);
			mMaterial.SetVector("_Center",centerMat);
			//计算当前偏移的初始值
			RectTransform canvasRectTransform = (mCanvas.transform as RectTransform);
			if (canvasRectTransform != null)
			{
				//获取画布区域的四个顶点
				canvasRectTransform.GetWorldCorners(_corners);
				//求偏移初始值
				for (int i = 0; i < _corners.Length; i++)
				{
					if (i % 2 == 0)
						mCurrentOffsetX = Mathf.Max(Vector3.Distance(World2CanvasPos(mCanvas, _corners[i]), center), mCurrentOffsetX);
					else
						mCurrentOffsetY = Mathf.Max(Vector3.Distance(World2CanvasPos(mCanvas, _corners[i]), center), mCurrentOffsetY);
				}
			}
			//设置遮罩材质中当前偏移的变量
			float initX = ShowAnim ? mCurrentOffsetX : mTargetOffsetX;
			float initY = ShowAnim ? mCurrentOffsetY : mTargetOffsetY;
			mMaterial.SetFloat("_SliderX",initX);
			mMaterial.SetFloat("_SliderY",initY);
		}
		
		private float shrinkVelocityX = 0f;
		private float shrinkVelocityY = 0f;
		protected override void PlayShrinkAnim()
		{
			if (!ShowAnim)
				return;
			//从当前偏移值到目标偏移值差值显示收缩动画
			float valueX = Mathf.SmoothDamp(mCurrentOffsetX, mTargetOffsetX, ref shrinkVelocityX, ShrinkTime);
			float valueY = Mathf.SmoothDamp(mCurrentOffsetY, mTargetOffsetY, ref shrinkVelocityY, ShrinkTime);
			if (!Mathf.Approximately(valueX, mCurrentOffsetX))
			{
				mCurrentOffsetX = valueX;
				mMaterial.SetFloat("_SliderX",mCurrentOffsetX);
			}

			if (!Mathf.Approximately(valueY, mCurrentOffsetY))
			{
				mCurrentOffsetY = valueY;
				mMaterial.SetFloat("_SliderY",mCurrentOffsetY);
			}
		}
	}
}

/****************************************************************************
 * Copyright (c) 2017 ~ 2018.6 .

 ****************************************************************************/


using UnityEngine.UI;

namespace QFramework
{
	using UnityEngine;

	/// <inheritdoc />
	/// <summary>
	/// UI的标记
	/// </summary>
	public class UIMark : MonoBehaviour, IMark
	{
		public UIMarkType MarkType = UIMarkType.DefaultUnityElement;

		public string Comment
		{
			get { return CustomComment; }
		}

		public Transform Transform
		{
			get { return transform; }
		}

		public string CustomComponentName;

		/// <summary>
		/// 注释
		/// </summary>
		[Header("注释")]
		public string CustomComment;
		
		public UIMarkType GetUIMarkType()
		{
			return MarkType;
		}

		public virtual string ComponentName
		{
			get
			{
				if (MarkType == UIMarkType.DefaultUnityElement)
				{
					if (null != GetComponent("SkeletonAnimation"))
						return "SkeletonAnimation";
					if (null != GetComponent<ScrollRect>())
						return "ScrollRect";
					if (null != GetComponent<InputField>())
						return "InputField";
                    if (null != GetComponent("TMP.TextMeshProUGUI"))
                        return "TextMeshProUGUI";
					if (null != GetComponent<Button>())
						return "Button";
					if (null != GetComponent<Text>())
						return "Text";
                    if ( null != GetComponent<LocalizationText>() )
                        return "Text";
                    if (null != GetComponent<RawImage>())
						return "RawImage";
					if (null != GetComponent<Toggle>())
						return "Toggle";
					if (null != GetComponent<Slider>())
						return "Slider";
					if (null != GetComponent<Scrollbar>())
						return "Scrollbar";
					if (null != GetComponent<Image>())
						return "Image";
					if (null != GetComponent<ToggleGroup>())
						return "ToggleGroup";
					if (null != GetComponent<Animator>())
						return "Animator";
					if (null != GetComponent<Canvas>())
						return "Canvas";
					if (null != GetComponent("Empty4Raycast"))
						return "Empty4Raycast";
					if (null != GetComponent<RectTransform>())
						return "RectTransform";

					return "Transform";
				}

				return CustomComponentName;
			}
		}
	}
}
﻿/****************************************************************************
 * Copyright (c) 2017 xiaojun、imagicbell
 * Copyright (c) 2017 ~ 2019.1 . 

 ****************************************************************************/

using System.Linq;
using QFramework.GraphDesigner;

namespace QFramework
{
	using UnityEngine;
	using UnityEditor;
	using System.IO;

	public class UICodeGenerator
	{
		[MenuItem("Assets/@UI Kit - Create UICode")]
		public static void CreateUICode()
		{
			var objs = Selection.GetFiltered(typeof(GameObject), SelectionMode.Assets | SelectionMode.TopLevel);
			var displayProgress = objs.Length > 1;
			if (displayProgress) EditorUtility.DisplayProgressBar("", "Create UIPrefab Code...", 0);
			for (var i = 0; i < objs.Length; i++)
			{
				mInstance.CreateCode(objs[i] as GameObject, AssetDatabase.GetAssetPath(objs[i]));
				if (displayProgress)
					EditorUtility.DisplayProgressBar("", "Create UIPrefab Code...", (float) (i + 1) / objs.Length);
			}

			AssetDatabase.Refresh();
			if (displayProgress) EditorUtility.ClearProgressBar();
		}

		private void CreateCode(GameObject obj, string uiPrefabPath)
		{
			if (obj.IsNotNull())
			{
				var prefabType = PrefabUtility.GetPrefabType(obj);
				if (PrefabType.Prefab != prefabType)
				{
					return;
				}

				var clone = PrefabUtility.InstantiatePrefab(obj) as GameObject;
				if (null == clone)
				{
					return;
				}
				


				UIMarkCollector.mPanelCodeData = new PanelCodeData();
				Debug.Log(clone.name);
				UIMarkCollector.mPanelCodeData.PanelName = clone.name.Replace("(clone)", string.Empty);
				UIMarkCollector.FindAllMarkTrans(clone.transform, "");
				CreateUIPanelCode(obj, uiPrefabPath);
				
				UISerializer.AddSerializeUIPrefab(obj);

				Object.DestroyImmediate(clone);
			}
		}

		private void CreateUIPanelCode(GameObject uiPrefab, string uiPrefabPath)
		{
			if (null == uiPrefab)
				return;

			var behaviourName = uiPrefab.name;

			var strFilePath = CodeGenUtil.GenSourceFilePathFromPrefabPath(uiPrefabPath, behaviourName);

			if (File.Exists(strFilePath) == false)
			{
				RegisteredTemplateGeneratorsFactory.RegisterTemplate<PanelCodeData,UIPanelDataTemplate>();
				RegisteredTemplateGeneratorsFactory.RegisterTemplate<PanelCodeData,UIPanelTemplate>();
				
				var factory = new RegisteredTemplateGeneratorsFactory();
				
				var generators = factory.CreateGenerators(new UIGraph(), UIMarkCollector.mPanelCodeData);
								
				CompilingSystem.GenerateFile(new FileInfo(strFilePath),new CodeFileGenerator(UIKitSettingData.GetProjectNamespace())
				{
					Generators = generators.ToArray()
				});

				RegisteredTemplateGeneratorsFactory.UnRegisterTemplate<PanelCodeData>();
			}

			CreateUIPanelDesignerCode(behaviourName, strFilePath);
			Debug.Log(">>>>>>>Success Create UIPrefab Code: " + behaviourName);
		}
		
		private void CreateUIPanelDesignerCode(string behaviourName, string uiUIPanelfilePath)
		{
			var dir = uiUIPanelfilePath.Replace(behaviourName + ".cs", "");
			var generateFilePath = dir + behaviourName + ".Designer.cs";

			RegisteredTemplateGeneratorsFactory.RegisterTemplate<PanelCodeData,UIPanelDesignerTemplate>();

			var factory = new RegisteredTemplateGeneratorsFactory();
				
			var generators = factory.CreateGenerators(new UIGraph(), UIMarkCollector.mPanelCodeData);
								
			CompilingSystem.GenerateFile(new FileInfo(generateFilePath),new CodeFileGenerator(UIKitSettingData.GetProjectNamespace())
			{
				Generators = generators.ToArray()
			});
			
			RegisteredTemplateGeneratorsFactory.UnRegisterTemplate<PanelCodeData>();

			foreach (var elementCodeData in UIMarkCollector.mPanelCodeData.ElementCodeDatas)
			{
				var elementDir = string.Empty;
				elementDir = elementCodeData.MarkedObjInfo.MarkObj.GetUIMarkType() == UIMarkType.Element
					? (dir + behaviourName + "/").CreateDirIfNotExists()
					: (Application.dataPath + "/" + UIKitSettingData.GetScriptsPath() + "/Components/").CreateDirIfNotExists();
				CreateUIElementCode(elementDir, elementCodeData);
			}
		}

		private static void CreateUIElementCode(string generateDirPath, ElementCodeData elementCodeData)
		{
			var panelFilePathWhithoutExt = generateDirPath + elementCodeData.BehaviourName;

			if (File.Exists(panelFilePathWhithoutExt + ".cs") == false)
			{
				UIElementCodeTemplate.Generate(panelFilePathWhithoutExt + ".cs",
					elementCodeData.BehaviourName, UIKitSettingData.GetProjectNamespace(), elementCodeData);
			}

			UIElementCodeComponentTemplate.Generate(panelFilePathWhithoutExt + ".Designer.cs",
				elementCodeData.BehaviourName, UIKitSettingData.GetProjectNamespace(), elementCodeData);

			foreach (var childElementCodeData in elementCodeData.ElementCodeDatas)
			{
				var elementDir = (panelFilePathWhithoutExt + "/").CreateDirIfNotExists();
				CreateUIElementCode(elementDir, childElementCodeData);
			}
		}

		private static readonly UICodeGenerator mInstance = new UICodeGenerator();
	}
}
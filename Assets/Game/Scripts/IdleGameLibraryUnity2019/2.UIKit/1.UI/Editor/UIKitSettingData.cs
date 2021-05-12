/****************************************************************************
 * Copyright (c) 2017 magicbell
 * .3 ~ 2019.1 .

 ****************************************************************************/

using System;
using System.IO;
using UnityEngine;

namespace QFramework {
    using UnityEditor;

    [Serializable]
    public class UIKitSettingData {
        static string mConfigSavedDir {
            get { return (Application.dataPath + "/Game/Scripts/IdleGameLibraryUnity2019/ProjectConfig/" ).CreateDirIfNotExists(); }
        }
        
        private const string mConfigSavedFileName = "ProjectConfig.json";

        public string Namespace;

        public string UIScriptDir = "/Scripts/UI";

        public string UIPrefabDir = "/Resources/UI/Prefab";

        public static string GetScriptsPath() {
            return Load().UIScriptDir;
        }

        public static string GetProjectNamespace() {
            return Load().Namespace;
        }

        public static UIKitSettingData Load() {
            mConfigSavedDir.CreateDirIfNotExists();

            if (!File.Exists(mConfigSavedDir + mConfigSavedFileName)) {
                using (var fileStream = File.Create(mConfigSavedDir + mConfigSavedFileName)) {
                    fileStream.Close();
                }
            }

            UIKitSettingData frameworkConfigData = null; 

            if (frameworkConfigData == null || string.IsNullOrEmpty(frameworkConfigData.Namespace)) {
                frameworkConfigData = new UIKitSettingData { Namespace = "QFramework" };
            }

            return frameworkConfigData;
        }

        public void Save() {
            //LitJson.JsonMapper.ToJson( mConfigSavedDir + mConfigSavedFileName);
            AssetDatabase.Refresh();

        }
    }
}
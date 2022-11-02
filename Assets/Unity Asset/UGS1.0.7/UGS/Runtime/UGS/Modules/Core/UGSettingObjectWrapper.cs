#if UNITY_2017_1_OR_NEWER || UNITY_BUILD
using System.Configuration;
using UnityEngine;
namespace UGS
{
    public static class UGSettingObjectWrapper
    {
        public static string GoogleFolderID
        {
            get
            {
#if UGS_SECURITY_MODE
                return null;
#else
                UGSettingObject.Create();
                UGSettingObject setting = Resources.Load<UGSettingObject>("UGSettingObject");
                if (setting == null)
                {
                    throw new System.Exception("Cannot Find UG/Resources/UGSettingObject.asset file, Setting Object is null!, please check <color=#00ff00><b>HamsterLib -> UGS -> UGSetting</b></color>");
                }
                return setting.GoogleFolderID;
#endif

            }
            set
            {
#if !UGS_SECURITY_MODE
                UGSettingObject setting = Resources.Load<UGSettingObject>("UGSettingObject");
                if (setting == null)
                {
                    throw new System.Exception("Cannot Find UG/Resources/UGSettingObject.asset file, Setting Object is null!, please check  <color=#00ff00><b>HamsterLib -> UGS -> UGSetting</b></color>");
                }
                setting.GoogleFolderID = value;
#endif
                var _ = value;
#if UNITY_EDITOR && !UGS_SECURITY_MODE
                UnityEditor.EditorUtility.SetDirty(setting);
#endif 
            }
        }
        public static string ScriptURL
        {
            get
            {
#if !UGS_SECURITY_MODE       
                UGSettingObject.Create();
                UGSettingObject setting = Resources.Load<UGSettingObject>("UGSettingObject");
                if (setting == null)
                { 
                    throw new System.Exception("Cannot Find UG/Resources/UGSettingObject.asset file, Setting Object is null!, please check <color=#00ff00><b>HamsterLib -> UGS -> UGSetting</b></color>");
                }
                return setting.ScriptURL;
#endif
#pragma warning disable 162
                return null;
#pragma warning restore 162
            }
            set
            {
#if !UGS_SECURITY_MODE
                UGSettingObject setting = Resources.Load<UGSettingObject>("UGSettingObject");
                if (setting == null)
                {
                    throw new System.Exception("Cannot Find UG/Resources/UGSettingObject.asset file, Setting Object is null!, please check  <color=#00ff00><b>HamsterLib -> UGS -> UGSetting</b></color>");
                }
                setting.ScriptURL = value;
#endif
                var __ = value;
#if UNITY_EDITOR&& !UGS_SECURITY_MODE
                UnityEditor.EditorUtility.SetDirty(setting);
#endif
            }
        }

        public static string ScriptPassword
        {
            get
            {
#if !UGS_SECURITY_MODE
                UGSettingObject.Create();
                UGSettingObject setting = Resources.Load<UGSettingObject>("UGSettingObject");
                if (setting == null)
                {
                    throw new System.Exception("Cannot Find UG/Resources/UGSettingObject.asset file, Setting Object is null!, please check <color=#00ff00><b>HamsterLib -> UGS -> UGSetting</b></color>");
                }
                return setting.ScriptPassword;
#endif
#pragma warning disable 162
                return null;
#pragma warning restore 162
            }
            set
            {
#if !UGS_SECURITY_MODE
                UGSettingObject setting = Resources.Load<UGSettingObject>("UGSettingObject");
                if (setting == null)
                {
                    throw new System.Exception("Cannot Find UG/Resources/UGSettingObject.asset file, Setting Object is null!, please check <color=#00ff00><b>HamsterLib -> UGS -> UGSetting</b></color>");
                }
                setting.ScriptPassword = value;
#endif
                var _ = value;
#if UNITY_EDITOR&& !UGS_SECURITY_MODE
                UnityEditor.EditorUtility.SetDirty(setting);
#endif
            }
        }

        public static string GenerateCodePath
        {
            get
            {
                UGSettingObject.Create();
                UGSettingObject setting = Resources.Load<UGSettingObject>("UGSettingObject");
                if (setting == null)
                {
                    throw new System.Exception("Cannot Find UG/Resources/UGSettingObject.asset file, Setting Object is null!, please check <color=#00ff00><b>HamsterLib -> UGS -> UGSetting</b></color>");
                }
                return setting.GenerateCodePath;
            }
            set
            {
                UGSettingObject setting = Resources.Load<UGSettingObject>("UGSettingObject");
                if (setting == null)
                {
                    throw new System.Exception("Cannot Find UG/Resources/UGSettingObject.asset file, Setting Object is null!, please check  <color=#00ff00><b>HamsterLib -> UGS -> UGSetting</b></color>");
                }
                setting.GenerateCodePath = value;
#if UNITY_EDITOR&& !UGS_SECURITY_MODE
                UnityEditor.EditorUtility.SetDirty(setting);
#endif
            }
        }

        public static string JsonDataPath
        {
            get
            {
                UGSettingObject.Create();
                UGSettingObject setting = Resources.Load<UGSettingObject>("UGSettingObject");
                if (setting == null)
                {
                    throw new System.Exception("Cannot Find UG/Resources/UGSettingObject.asset file, Setting Object is null!, please check <color=#00ff00><b>HamsterLib -> UGS -> UGSetting</b></color>");
                }
                return setting.DataPath;
            }
            set
            {
                UGSettingObject setting = Resources.Load<UGSettingObject>("UGSettingObject");
                if (setting == null)
                {
                    throw new System.Exception("Cannot Find UG/Resources/UGSettingObject.asset file, Setting Object is null!, please check  <color=#00ff00><b>HamsterLib -> UGS -> UGSetting</b></color>");
                }
                setting.DataPath = value;
#if UNITY_EDITOR&& !UGS_SECURITY_MODE
                UnityEditor.EditorUtility.SetDirty(setting);
#endif
            }
        }

        public static (string url, string pass, string driveId) GetUGSSetting()
        {
            return (UGSettingObjectWrapper.GoogleFolderID, UGSettingObjectWrapper.ScriptPassword, UGSettingObjectWrapper.ScriptURL);
        }

        public static void ClearSetting()
        {
            UGSettingObjectWrapper.ScriptPassword = null;
            UGSettingObjectWrapper.GoogleFolderID = null;
            UGSettingObjectWrapper.ScriptURL = null;
        }

        public static void CacheSettingEditorPrefs()
        {
#if UNITY_EDITOR

#endif
        }

    }
}
#endif
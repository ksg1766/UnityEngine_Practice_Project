#if UNITY_EDITOR || UNITY_BUILD 
using UGS;
using UnityEditor;
using UnityEngine;

namespace UGS.Editor
{
    public class UGSSetting : EditorWindow
    {
        static UGSSetting instance;

        [MenuItem("Window/HamsterLib/Debug/DebugMode")]
        public static void ToggleDebugMode()
        {
            EditorPrefsManager.Toggle("UGS.DebugMode");
        }

        public static bool IsDebugMode()
        {
            return EditorPrefsManager.Get<bool>("UGS.DebugMode");
        }


        [MenuItem("HamsterLib/UGS/Setting", priority = 10000)]
        public static void CreateInstance()
        {
            // Get existing open window or if none, make a new one:
            instance = (UGSSetting)EditorWindow.GetWindow(typeof(UGSSetting));
            instance.maxSize = new Vector2(700, 400);
            instance.minSize = new Vector2(700, 400);
            instance.Show();
        }

        private string _googleScriptURL;
        private string _password;
        private string _googleFolderId; 
        private string _generateCodePath;
        private string _jsonDataPath;

        public void OnEnable()
        {
            _googleScriptURL = UGSettingObjectWrapper.ScriptURL;
            _password = UGSettingObjectWrapper.ScriptPassword;
            _googleFolderId = UGSettingObjectWrapper.GoogleFolderID;
            _generateCodePath = UGSettingObjectWrapper.GenerateCodePath;
            _jsonDataPath = UGSettingObjectWrapper.JsonDataPath;
        }

        public void EditorPrefsToggle(string id)
        {
            EditorPrefsManager.Toggle(id);
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void CreateAssetWhenReady()
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating)
            {
                EditorApplication.delayCall += CreateAssetWhenReady;
                return;
            }
            EditorApplication.delayCall += CreateAssetNow;
        }

        public static void CreateAssetNow()
        {

            var data = EditorPrefsManager.Get<string>("UGSetting.ScriptPassword");
            var data2 = EditorPrefsManager.Get<string>("UGSetting.GoogleFolderID");
            var data3 = EditorPrefsManager.Get<string>("UGSetting.ScriptURL");
            if (data == null || data2 == null || data3 == null) return;
           
            UGSettingObjectWrapper.ScriptURL = data3;
            UGSettingObjectWrapper.ScriptPassword = data;
            UGSettingObjectWrapper.GoogleFolderID = data2;

        }
        GUIStyle _fontsizeBig;
        GUIStyle _fontsizeBigGreen;
        GUIStyle _fontMiddleRed;
        GUIStyle _fontMiddelGreen;

        public void OnGUI()
        {
            var fontsizeBig = new GUIStyle(GUI.skin.label);
            fontsizeBig.fontSize = 18;

            var fontsizeBigGreen = new GUIStyle(GUI.skin.label);
            fontsizeBigGreen.normal.textColor = new Color(0, 1, 0, 1);
            fontsizeBigGreen.fontSize = 18;

            var fontMiddleRed = new GUIStyle(GUI.skin.label);
            fontMiddleRed.normal.textColor = new Color(1, 0, 0, 1);
            fontMiddleRed.fontSize = 15;
            var fontMiddelGreen = new GUIStyle(GUI.skin.label);
            fontMiddelGreen.normal.textColor = new Color(0, 1, 0, 1);
            fontMiddelGreen.fontSize = 15;


            var toggleStyle = new GUIStyle(GUI.skin.toggle);
            toggleStyle.fontSize = 13;
            GUILayout.Space(10);
            GUILayout.Label("Credentials Settings", fontsizeBig);

            _googleScriptURL = EditorGUILayout.TextField("GoogleScriptURL", _googleScriptURL);
            _password = EditorGUILayout.TextField("Script Password", _password);
            _googleFolderId = EditorGUILayout.TextField("Google Folder Id", _googleFolderId);

            GUILayout.Space(10);
            GUILayout.Label("Generated Data Save Path", fontsizeBig);

            GUILayout.BeginHorizontal();
            _generateCodePath = EditorGUILayout.TextField("Generated Code Save Path", _generateCodePath);
            if (GUILayout.Button("Set Directory"))
            {

                var path = EditorUtility.OpenFolderPanel("Select Your Generated Code Save Path", "Assets/", "Assets/");
                string relativepath = null;
                if (path.StartsWith(Application.dataPath))
                {
                    relativepath = "Assets" + path.Substring(Application.dataPath.Length);
                }
                if (string.IsNullOrEmpty(relativepath) == false)
                    _generateCodePath = relativepath;
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _jsonDataPath = EditorGUILayout.TextField("Generated Json Save Path", _jsonDataPath);
            if (GUILayout.Button("Set Directory"))
            {
                var path = EditorUtility.OpenFolderPanel("Select Your Generated Json Save Path", "Assets/", "Assets/");
                string relativepath = null;
                if (path.StartsWith(Application.dataPath))
                {
                    relativepath = "Assets" + path.Substring(Application.dataPath.Length);
                }
                if (string.IsNullOrEmpty(relativepath) == false)
                    _jsonDataPath = relativepath;
            }
            GUILayout.EndHorizontal();



            GUILayout.Space(25);
            GUILayout.Label("UGS Security (Important)", fontsizeBig);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Security Warning Check This");
            if (GUILayout.Button("Go To Document..."))
            {
                Application.OpenURL("https://shlifedev.gitbook.io/unitygooglesheet/additional/apps-script-backend-security");
            }


            GUILayout.EndHorizontal();
            var isUsedSecurityMode = DefineSymbolManager.IsUsed("UGS_SECURITY_MODE");
            if (isUsedSecurityMode)
            {
                GUILayout.Label("Security Mode Enabled!", fontMiddelGreen);
                GUILayout.Label("If Security Mode Enabled, Google Drive Networking All Not work! So You Can't Use Drive GUI, Live Load/Write.. etc");
                GUILayout.Label("Recommand Enable Security Mode On Product Build For Hacking Prevention.");
            }
            else
            {
                GUILayout.Label("Security Mode Disabled!", fontMiddleRed);
            }

            if (isUsedSecurityMode)
            {
                GUI.backgroundColor = new Color(0, 1, 0, 1);

                if (GUILayout.Button("Disable Security Mode"))
                {

                    DefineSymbolManager.RemoveDefineSymbol("UGS_SECURITY_MODE");
                }
            }
            else
            {
                GUI.backgroundColor = new Color(1, 0, 0, 1);
                if (GUILayout.Button("Enable Security Mode"))
                {
                    EditorPrefsManager.Set<string>("UGSetting.ScriptPassword", UGSettingObjectWrapper.ScriptPassword);
                    EditorPrefsManager.Set<string>("UGSetting.GoogleFolderID", UGSettingObjectWrapper.GoogleFolderID);
                    EditorPrefsManager.Set<string>("UGSetting.ScriptURL", UGSettingObjectWrapper.ScriptURL);

                    DefineSymbolManager.AddDefineSymbols("UGS_SECURITY_MODE");

                }
            }


            GUI.backgroundColor = new Color(1, 1, 1, 1);
            if (GUILayout.Button("Save"))
            {
                UGSettingObject setting = Resources.Load<UGSettingObject>("UGSettingObject");
                UGSettingObjectWrapper.ScriptURL = _googleScriptURL;
                UGSettingObjectWrapper.ScriptPassword = _password;
                UGSettingObjectWrapper.GoogleFolderID = _googleFolderId;
                UGSettingObjectWrapper.GenerateCodePath = _generateCodePath;
                UGSettingObjectWrapper.JsonDataPath = _jsonDataPath;
                EditorPrefsManager.Set<string>("UGSetting.ScriptPassword", UGSettingObjectWrapper.ScriptPassword);
                EditorPrefsManager.Set<string>("UGSetting.GoogleFolderID", UGSettingObjectWrapper.GoogleFolderID);
                EditorPrefsManager.Set<string>("UGSetting.ScriptURL", UGSettingObjectWrapper.ScriptURL);

                EditorUtility.SetDirty(setting);
                AssetDatabase.SaveAssets();
            }
            if (EditorPrefsManager.Get<bool>("UGS.DebugMode"))
            {
                if (GUILayout.Button("Load Debug Data"))
                {
                    this._googleScriptURL = "https://script.google.com/macros/s/AKfycbxpqlYM5SfX0pL2RHzgiT_cFykKFLkcr_PgzU1KKnVx2Aa6YNN3/exec";
                    this._googleFolderId = "1fanZLAqJEvy366vw3dOHQ4o9rhqcz-vI";
                }
            }

        }
    }
}
#endif
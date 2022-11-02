#if UNITY_2017_1_OR_NEWER || UNITY_BUILD
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace UGS
{
    [CreateAssetMenu(fileName = "UGSettingObject", menuName = "HamsterLib/UG/SettingObject", order = 0)]

    public class UGSettingObject : ScriptableObject
    {
#if UGS_SECURITY_MODE
      
#else
        public string ScriptURL;
        public string ScriptPassword = "default";
        public string GoogleFolderID;
#endif

        public string GenerateCodePath = "Assets/UGS.Generated/Scripts/";
        public string DataPath = "Assets/UGS.Generated/Resources/";
        [HideInInspector, System.Obsolete]
        public string RuntimeDataPath = "UGS/UGS.Data/";


        public static void Create()
        {
#if UNITY_EDITOR
            var res = Resources.Load("UGSettingObject") as UGSettingObject;
            if (res != null)
            {

                return;
            }
            Debug.Log("UGSettingObject Missing, New Created");
            var di = System.IO.Directory.CreateDirectory("Assets/Resources/");
            var obj = new UGSettingObject();
            UnityEditor.AssetDatabase.CreateAsset(obj, "Assets/Resources/UGSettingObject.asset");
            UnityEditor.EditorUtility.SetDirty(obj);
#endif

        }
    }
}
#endif
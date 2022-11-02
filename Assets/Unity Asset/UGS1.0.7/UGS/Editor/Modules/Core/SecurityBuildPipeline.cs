 
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace UGS.Editor
{
    public class SecurityBuildPipeline : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPostprocessBuild(BuildReport report)
        {

        }


        public void OnPreprocessBuild(BuildReport report)
        {
            var confirm = UnityEditor.EditorPrefs.GetBool("UGS.BuildMsg", false);
            if (!confirm)
            {
                string x = "UGS Setting Object File (Assets/UG/Resources/UGSettingObject.asset) Is Included Api url, password, google drive id. So, not recommended to include UGSettingObject.asset  before distributing it to users as a release. \n\nThis Message Only Onetime Showing.";
                var res = UnityEditor.EditorUtility.DisplayDialog("UGS Warning", x, "OK!");
                if (res)
                {
                    UnityEditor.EditorPrefs.SetBool("UGS.BuildMsg", true);
                }

            }
        }

    }

}
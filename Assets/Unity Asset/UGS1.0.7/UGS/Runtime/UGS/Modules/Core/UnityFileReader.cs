#if UNITY_2017_1_OR_NEWER || UNITY_BUILD 
using GoogleSheet.IO;
using System.Collections.Generic; 
using UnityEngine;

namespace UGS.IO
{
    public class UnityFileReader : IFileReader
    {
        public string ReadData(string fileName)
        { 
            if (Application.isPlaying == false)
            {
                return EditorAssetLoad(fileName);
            }
            else
            {
                return RuntimeAssetLoad(fileName);
            } 
        }

        string ToUnityResourcePath(string path)
        {
            var paths = path.Split('/');
            bool link = false;
            List<string> newPath = new List<string>();
            foreach(var value in paths)
            {
                if (value == "Resources")
                {
                    link = true;
                    continue;
                }

                if (link)
                {
                    newPath.Add(value);
                }
            }
             
            return string.Join("/", newPath);
        } 
        public string EditorAssetLoad(string fileName)
        {
            var combine = System.IO.Path.Combine(UGSettingObjectWrapper.JsonDataPath, fileName);
            combine = combine.Replace("\\", "/");
            var filePath = ToUnityResourcePath(combine);

            var textasset = Resources.Load<TextAsset>(filePath);
            if (textasset != null)
            {
                return textasset.text;
            }
            else
            {

                throw new System.Exception($"UGS File Read Failed (path = {"UGS.Data/" + fileName})");
            }
        }

        public string RuntimeAssetLoad(string fileName)
        {



            var combine = System.IO.Path.Combine(UGSettingObjectWrapper.JsonDataPath, fileName);
            combine = combine.Replace("\\", "/");
            var filePath = ToUnityResourcePath(combine);

            filePath = filePath.Replace("Resources/", null);
#if UGS_DEBUG
            Debug.Log(filePath);
#endif 

            var textasset = Resources.Load<TextAsset>(filePath);
            if (textasset == null)
                return null;

            return textasset.text;
        }
    }
}


#endif
#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.Linq;

namespace AS.EditorExtension
{
    class BuildNumberIncrement : IPreprocessBuildWithReport
    {
        int IOrderedCallback.callbackOrder => -100;

        void IPreprocessBuildWithReport.OnPreprocessBuild(BuildReport report)
        {

#if UNITY_IOS
            string[] bundle = PlayerSettings.iOS.buildNumber.Split('.');
#else
            string[] bundle = PlayerSettings.bundleVersion.Split('.');
#endif
            if (int.TryParse(bundle.Last(), out int version))
            {
                bundle[bundle.Length - 1] = (version + 1).ToString();
#if UNITY_IOS
                PlayerSettings.iOS.buildNumber = string.Join(".", bundle);
#else
                PlayerSettings.bundleVersion = string.Join(".", bundle);
#endif
            }
            else
            {
                Debug.LogWarning("Cannot parse bundle");
            }
        }
    }

}

#endif
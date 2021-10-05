namespace com.faith.sdk.analytics
{
#if UNITY_EDITOR

    using UnityEngine;
    using UnityEditor;
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    public static class FaithAnalyticsScriptDefineSymbol
    {
        #region CustomVariables

        private struct PackageInfo
        {
            public string packageName;
            public string packageURL;
        }

        #endregion

        #region Private Variables

        private const string m_NameOfManifestDirectory = "Packages";

        #endregion

        #region Configuretion   :   Reading manifest.json

        private static string RemoveUnwantedChar(string t_String)
        {

            string t_ConcatinatedString = "";
            List<char> t_Converted = t_String.ToList();
            int t_NumberOfCharacter = t_Converted.Count;
            for (int j = 0; j < t_NumberOfCharacter;)
            {

                int t_Ascii = System.Convert.ToInt32(t_Converted[j]);
                if (t_Ascii >= 0 && t_Ascii <= 31 ||
                    t_Converted[j] == ' ' ||
                    t_Converted[j] == '{' ||
                    t_Converted[j] == '}' ||
                    t_Converted[j] == '\t' ||
                    t_Converted[j] == '\n')
                {

                    t_Converted.RemoveAt(j);
                    t_Converted.TrimExcess();

                    t_NumberOfCharacter--;
                }
                else
                {
                    t_ConcatinatedString += t_Converted[j];
                    j++;
                }
            }

            return t_ConcatinatedString;
        }

        private static string GetManifestPath()
        {

            string t_StreamingAssetPath = Application.streamingAssetsPath;
            string[] t_Split = t_StreamingAssetPath.Split('/');
            string t_ManifestPath = "";

            int t_NumberOfSplit = t_Split.Length - 2;
            for (int i = 0; i < t_NumberOfSplit; i++)
            {

                t_ManifestPath += t_Split[i];
                t_ManifestPath += "/";
            }
            t_ManifestPath += m_NameOfManifestDirectory;
            t_ManifestPath += "/";
            t_ManifestPath += "manifest.json";

            return t_ManifestPath;
        }

        private static List<PackageInfo> GetPackageInfosFromManifest()
        {

            string t_StreamingAssetPath = Application.streamingAssetsPath;
            string[] t_Split = t_StreamingAssetPath.Split('/');
            string t_ManifestPath = "";

            int t_NumberOfSplit = t_Split.Length - 2;
            for (int i = 0; i < t_NumberOfSplit; i++)
            {

                t_ManifestPath += t_Split[i];
                t_ManifestPath += "/";
            }
            t_ManifestPath += m_NameOfManifestDirectory;
            t_ManifestPath += "/";
            t_ManifestPath += "manifest.json";


            //Extracting    :   Package
            string t_RawManifestText = File.ReadAllText(t_ManifestPath);
            string[] t_SplitByComa = t_RawManifestText.Split(',');
            t_NumberOfSplit = t_SplitByComa.Length;
            List<PackageInfo> t_Result = new List<PackageInfo>();

            for (int i = 0; i < t_NumberOfSplit; i++)
            {

                string t_CleanString = RemoveUnwantedChar(t_SplitByComa[i]);
                string[] t_SplitByColon = t_CleanString.Split(':');
                if (i == 0)
                {
                    string t_PackageVersion = "";
                    for (int k = 2; k < t_SplitByColon.Length; k++)
                    {
                        t_PackageVersion += ((k > 2 ? ":" : "") + t_SplitByColon[k]);
                    }
                    t_Result.Add(new PackageInfo()
                    {
                        packageName = RemoveUnwantedChar(t_SplitByColon[1]),
                        packageURL = RemoveUnwantedChar(t_PackageVersion)
                    });
                }
                else
                {
                    string t_PackageVersion = "";
                    for (int k = 1; k < t_SplitByColon.Length; k++)
                    {
                        t_PackageVersion += ((k > 1 ? ":" : "") + t_SplitByColon[k]);
                    }
                    t_Result.Add(new PackageInfo()
                    {
                        packageName = RemoveUnwantedChar(t_SplitByColon[0]),
                        packageURL = RemoveUnwantedChar(t_PackageVersion)
                    });
                }
            }

            return t_Result;
        }

        private static bool IsPackageBeenAddedToManifest(string packageName)
        {

            packageName = string.Format("{0}{1}{2}", "\"", packageName, "\"");
            List<PackageInfo> packageInfos = GetPackageInfosFromManifest();
            foreach (PackageInfo packageInfo in packageInfos)
            {

                if (packageInfo.packageName == packageName)
                {


                    return true;
                }
            }

            return false;
        }

        private static void UpdateDefines(string entry, bool enabled, BuildTargetGroup[] groups)
        {
            foreach (var group in groups)
            {
                var defines = new List<string>(PlayerSettings.GetScriptingDefineSymbolsForGroup(group).Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
                var edited = false;
                if (enabled && !defines.Contains(entry))
                {
                    defines.Add(entry);
                    edited = true;
                }
                else if (!enabled && defines.Contains(entry))
                {
                    defines.Remove(entry);
                    edited = true;
                }
                if (edited)
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(group, string.Join(";", defines.ToArray()));
                    AssetDatabase.Refresh();
                }
            }
        }

        #endregion

        #region Public Callback

        public static bool CheckFacebookIntegration(string sdkName)
        {
            bool isAdded = AssetDatabase.IsValidFolder("Assets/FacebookSDK");
            UpdateDefines(
                    sdkName,
                    isAdded,
                    new BuildTargetGroup[] { BuildTargetGroup.Android, BuildTargetGroup.iOS }
                );
            return isAdded;
        }

        public static bool CheckAdjustIntegration(string sdkName)
        {
            bool isAdded = AssetDatabase.IsValidFolder("Assets/Adjust/Unity");
            UpdateDefines(
                    sdkName,
                    isAdded,
                    new BuildTargetGroup[] { BuildTargetGroup.Android, BuildTargetGroup.iOS }
                );
            return isAdded;
        }

        public static bool CheckGameAnalyticsIntegration(string sdkName)
        {

            bool isAdded = IsPackageBeenAddedToManifest("com.gameanalytics.sdk") || AssetDatabase.IsValidFolder("Assets/GameAnalytics/Editor/Playmaker");
            UpdateDefines(
                    sdkName,
                    isAdded,
                    new BuildTargetGroup[] { BuildTargetGroup.Android, BuildTargetGroup.iOS }
                );
            return isAdded;
        }

        public static bool CheckFirebaseIntegration(string sdkName)
        {

            bool isAdded = AssetDatabase.IsValidFolder("Assets/Firebase");
            UpdateDefines(
                    sdkName,
                    isAdded,
                    new BuildTargetGroup[] { BuildTargetGroup.Android, BuildTargetGroup.iOS }
                );
            return isAdded;
        }


        #endregion
    }

#endif
}


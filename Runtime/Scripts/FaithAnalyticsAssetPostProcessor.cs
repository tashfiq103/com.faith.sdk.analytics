namespace com.faith.sdk.analytics
{
#if UNITY_EDITOR

    using UnityEngine;
    using UnityEditor;

    public class FaithAnalyticsAssetPostProcessor : AssetPostprocessor
    {
        public static void LookForSDK()
        {

        }

        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            LookForSDK();
        }
    }

#endif
}



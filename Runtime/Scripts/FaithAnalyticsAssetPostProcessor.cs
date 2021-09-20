namespace com.faith.sdk.analytics
{
#if UNITY_EDITOR

    using UnityEngine;
    using UnityEditor;

    public class FaithAnalyticsAssetPostProcessor : AssetPostprocessor
    {
        public static void LookForSDK()
        {
            Object[] analyticsConfiguretionObjects = Resources.LoadAll("", typeof(FaithBaseClassForAnalyticsConfiguretionInfo));
            foreach (Object analyticsConfiguretionObject in analyticsConfiguretionObjects)
            {

                FaithBaseClassForAnalyticsConfiguretionInfo analyticsConfiguretion = (FaithBaseClassForAnalyticsConfiguretionInfo)analyticsConfiguretionObject;
                if (analyticsConfiguretion != null)
                    analyticsConfiguretion.SetNameAndIntegrationStatus();
            }
        }

        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            LookForSDK();
        }
    }

#endif
}



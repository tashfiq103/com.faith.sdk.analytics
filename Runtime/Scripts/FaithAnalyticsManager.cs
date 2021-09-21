namespace com.faith.sdk.analytics
{
    using UnityEngine;

    public static class FaithAnalyticsManager
    {
        public static bool IsATTEnabled
        {
            get;
            private set;
        } = false;

        public static bool IsInitialized
        {
            get;
            private set;
        } = false;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnGameStart()
        {
            FaithAnalyticsGeneralConfiguretionInfo faithAnalyticsGeneralConfiguretionInfo = Resources.Load<FaithAnalyticsGeneralConfiguretionInfo>("FaithAnalyticsGeneralConfiguretionInfo");

            if (faithAnalyticsGeneralConfiguretionInfo.IsAutoInitialize) {

                Initialize();
            }
        }

        public static void Initialize() {

            FaithAnalyticsGeneralConfiguretionInfo faithAnalyticsGeneralConfiguretionInfo = Resources.Load<FaithAnalyticsGeneralConfiguretionInfo>("FaithAnalyticsGeneralConfiguretionInfo");

            Object[] analyticsConfiguretionObjects = Resources.LoadAll("", typeof(FaithBaseClassForAnalyticsConfiguretionInfo));
            foreach (Object analyticsConfiguretionObject in analyticsConfiguretionObjects)
            {

                FaithBaseClassForAnalyticsConfiguretionInfo faithAnalyticsConfiguretion = (FaithBaseClassForAnalyticsConfiguretionInfo)analyticsConfiguretionObject;
                if (faithAnalyticsConfiguretion != null)
                    faithAnalyticsConfiguretion.Initialize(faithAnalyticsGeneralConfiguretionInfo, IsATTEnabled);
            }

            FaithAnalytics.Initialize(faithAnalyticsGeneralConfiguretionInfo);
        }
    }
}


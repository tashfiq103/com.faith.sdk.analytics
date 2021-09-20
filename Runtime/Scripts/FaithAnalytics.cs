namespace com.faith.sdk.analytics
{
    using UnityEngine;

    public static class FaithAnalytics
    {
        #region Private Variables

        private static FaithAnalyticsGeneralConfiguretionInfo _faithAnalyticsGeneralConfiguretionInfo;

        #endregion

        #region Public Callback

        public static void Initialize(FaithAnalyticsGeneralConfiguretionInfo faithAnalyticsGeneralConfiguretionInfo) {

            _faithAnalyticsGeneralConfiguretionInfo = faithAnalyticsGeneralConfiguretionInfo;
        }

        #endregion
    }
}


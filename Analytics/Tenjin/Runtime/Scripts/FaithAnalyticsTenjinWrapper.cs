#if FaithAnalytics_Tenjin



namespace com.faith.sdk.analytics
{
    using System;
    using UnityEngine;
#if UNITY_IOS
    using UnityEngine.iOS;
#endif
    public class FaithAnalyticsTenjinWrapper : MonoBehaviour
    {
        #region Public Variables

        public static FaithAnalyticsTenjinWrapper Instance;

        #endregion

        #region Private Variables

        public FaithAnalyticsGeneralConfiguretionInfo FaithAnalyticsGeneralConfiguretionInfoReference { get; private set; }
        public FaithAnalyticsTenjinConfiguretionInfo FaithAnalyticsTenjinConfiguretionInfoReference { get; private set; }

        #endregion

        #region Mono Behaviour

        private void Start()
        {
            TenjinConnect();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (!pauseStatus)
            {
                TenjinConnect();
            }
        }

        #endregion

        #region Configuretion

        private void TenjinConnect()
        {
            BaseTenjin instance = Tenjin.getInstance(FaithAnalyticsTenjinConfiguretionInfoReference.APIKey);

#if UNITY_ANDROID
            instance.SetAppStoreType(AppStoreType.googleplay);
#endif

#if UNITY_IOS
            if (new Version(Device.systemVersion).CompareTo(new Version("14.0")) >= 0)
            {
                // Tenjin wrapper for requestTrackingAuthorization
                instance.RequestTrackingAuthorizationWithCompletionHandler((status) => {
                    Debug.Log("===>(Tenjin) App Tracking Transparency Authorization Status: " + status);

                    // Sends install/open event to Tenjin
                    instance.Connect();

                });
            }
            else
            {
                instance.Connect();
            }
#elif UNITY_ANDROID

      // Sends install/open event to Tenjin
      instance.Connect();

#endif
        }

        private bool CanLogEvent()
        {
            if (FaithAnalyticsTenjinConfiguretionInfoReference.IsAnalyticsEventEnabled)
            {
                return true;
            }
            else
            {
                FaithAnalyticsLogger.LogWarning("'logTenjinEvent' is currently turned off from APSDkIntegrationManager, please set it to 'true'");
                return false;
            }
        }

#endregion

#region Public Callback

        public void Initialize(FaithAnalyticsGeneralConfiguretionInfo apSdkConfiguretionInfo, FaithAnalyticsTenjinConfiguretionInfo tenjinConfiguretion, bool isATEEnabled)
        {

            FaithAnalyticsGeneralConfiguretionInfoReference = apSdkConfiguretionInfo;
            FaithAnalyticsTenjinConfiguretionInfoReference = tenjinConfiguretion;

        }

#endregion
    }
}



#endif


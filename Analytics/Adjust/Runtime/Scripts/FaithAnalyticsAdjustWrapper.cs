#if FaithAnalytics_Adjust

namespace com.faith.sdk.analytics
{
    using System.Collections.Generic;
    using UnityEngine;
    using com.adjust.sdk;

    public class FaithAnalyticsAdjustWrapper : MonoBehaviour
    {
        #region Public Variables

        public static FaithAnalyticsAdjustWrapper Instance;

        #endregion

        #region Private Variables

        private FaithAnalyticsGeneralConfiguretionInfo _apSdkConfiguretionInfo;
        private FaithAnalyticsAdjustConfiguretionInfo _adjustConfiguretion;


        #endregion

        #region Mono Behaviour

        private void OnApplicationPause(bool pause)
        {

#if UNITY_EDITOR
            return;
#elif UNITY_IOS
            // No action, iOS SDK is subscribed to iOS lifecycle notifications.
#elif UNITY_ANDROID
            if (pause)
                {
                    AdjustAndroid.OnPause();
                }
                else
                {
                    AdjustAndroid.OnResume();
                }
#endif
        }

        #endregion

        #region Public Callback

        public void Initialize(FaithAnalyticsGeneralConfiguretionInfo apSdkConfiguretionInfo, FaithAnalyticsAdjustConfiguretionInfo adjustConfiguretion)
        {

            _apSdkConfiguretionInfo = apSdkConfiguretionInfo;
            _adjustConfiguretion = adjustConfiguretion;

            AdjustConfig adjustConfig = new AdjustConfig(
                adjustConfiguretion.appToken,
                adjustConfiguretion.Environment,
                adjustConfiguretion.LogLevel == AdjustLogLevel.Suppress);

            adjustConfig.setLogLevel(adjustConfiguretion.LogLevel);
            adjustConfig.setSendInBackground(adjustConfiguretion.SendInBackground);
            adjustConfig.setEventBufferingEnabled(adjustConfiguretion.EventBuffering);
            adjustConfig.setLaunchDeferredDeeplink(adjustConfiguretion.LaunchDeferredDeeplink);

            adjustConfig.setDelayStart(adjustConfiguretion.StartDelay);

            Adjust.start(adjustConfig);

            FaithAnalyticsLogger.Log("Adjust Initialized");
        }


        public void ProgressionEvent(string eventName, Dictionary<string, object> eventParams)
        {

            if (_adjustConfiguretion.IsTrackingProgressionEvent)
            {
                LogEvent(eventName, eventParams);
            }
            else
            {
                FaithAnalyticsLogger.LogWarning("'ProgressionEvent' is disabled for 'AdjustSDK'");
            }
        }

        public void AdEvent(string eventName, Dictionary<string, object> eventParams)
        {

            if (_adjustConfiguretion.IsTrackingAdEvent)
            {
                LogEvent(eventName, eventParams);
            }
            else
            {
                FaithAnalyticsLogger.LogWarning("'AdEvent' is disabled for 'AdjustSDK'");
            }
        }

        public void LogEvent(string eventName, Dictionary<string, object> eventParams)
        {
            if (_adjustConfiguretion.IsAnalyticsEventEnabled)
            {
                AdjustEvent newEvent = new AdjustEvent(eventName);
                Adjust.trackEvent(newEvent);
            }
            else
            {

                FaithAnalyticsLogger.LogWarning("'logAdjustEvent' is currently turned off from APSDkIntegrationManager, please set it to 'true'");
            }
        }

        #endregion
    }
}

#endif

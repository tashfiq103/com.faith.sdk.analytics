#if FaithAnalytics_Facebook

namespace com.faith.sdk.analytics
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using Facebook.Unity;

    public class FaithAnalyticsFacebookWrapper : MonoBehaviour
    {
#region Public Variables

        public static FaithAnalyticsFacebookWrapper Instance;

        public static bool IsFacebookInitialized { get { return FB.IsInitialized; } }

#endregion

#region Private Variables

        private FaithAnalyticsGeneralConfiguretionInfo _faithAnalyticsGeneralConfiguretionInfo;
        private FaithFacebookConfiguretionInfo _facebookConfiguretion;
        private bool _isATTEnabled = false;
        private UnityAction _OnInitialized;

#endregion

#region Configuretion

        private void OnInitializeCallback()
        {

            if (FB.IsInitialized)
            {
                FB.ActivateApp();
                FaithAnalyticsLogger.Log("FacebookSDK initialized");


#if UNITY_IOS

                FaithAnalyticsLogger.Log(string.Format("Facebook ATT Status (iOS) = {0}", _isATTEnabled));
                FB.Mobile.SetAdvertiserTrackingEnabled(_isATTEnabled);

#if UNITY_EDITOR
                FaithAnalyticsLogger.LogWarning(string.Format("AudienceNetwork.AdSettings.SetAdvertiserTrackingEnabled() -> is not set as non iOS platform"));
#else
                FaithAnalyticsLogger.Log(string.Format("AudienceNetwork = {0}", _isATTEnabled));
                AudienceNetwork.AdSettings.SetAdvertiserTrackingEnabled(_isATTEnabled);
#endif


#endif
                _OnInitialized?.Invoke();
            }
            else
                FaithAnalyticsLogger.LogError("Failed to Initialize the Facebook SDK");
        }

        private void OnHideUnityCallback(bool isGameShown)
        {


        }

#endregion


#region Public Callback

        public void Initialize(FaithAnalyticsGeneralConfiguretionInfo faithAnalyticsGeneralConfiguretionInfo, FaithFacebookConfiguretionInfo facebookConfiguretion, bool isATTEnabled, UnityAction OnInitialized = null)
        {

            _faithAnalyticsGeneralConfiguretionInfo = faithAnalyticsGeneralConfiguretionInfo;
            _facebookConfiguretion = facebookConfiguretion;
            _isATTEnabled = isATTEnabled;
            _OnInitialized = OnInitialized;

            if (!FB.IsInitialized)
            {
                FB.Init(OnInitializeCallback, OnHideUnityCallback);
            }
            else
            {

                FaithAnalyticsLogger.Log("FacebookSDK already initialized");
            }
        }

        public void ProgressionEvent(string eventName, Dictionary<string, object> eventParams)
        {

            if (_facebookConfiguretion.IsTrackingProgressionEvent)
            {
                LogEvent(eventName, eventParams);
            }
            else
            {
                FaithAnalyticsLogger.LogWarning("'ProgressionEvent' is disabled for 'FacebookSDK'");
            }
        }

        public void AdEvent(string eventName, Dictionary<string, object> eventParams)
        {

            if (_facebookConfiguretion.IsTrackingAdEvent)
            {
                LogEvent(eventName, eventParams);
            }
            else
            {
                FaithAnalyticsLogger.LogWarning("'AdEvent' is disabled for 'FacebookSDK'");
            }
        }

        public void LogEvent(string eventName, Dictionary<string, object> eventParams)
        {

            if (_facebookConfiguretion.IsAnalyticsEventEnabled)
            {

                if (FB.IsInitialized)
                {
                    FB.LogAppEvent(
                            eventName,
                            parameters: eventParams
                        );
                }
                else
                {
                    FaithAnalyticsLogger.LogError(string.Format("{0}\n{1}", "Failed to log event for facebook analytics! as it's not initialized", eventName, eventParams));
                }
            }
            else
            {
                FaithAnalyticsLogger.LogWarning("'logFacebookEvent' is currently turned off from APSDkIntegrationManager, please set it to 'true'");
            }
        }

#endregion

    }
}

#endif


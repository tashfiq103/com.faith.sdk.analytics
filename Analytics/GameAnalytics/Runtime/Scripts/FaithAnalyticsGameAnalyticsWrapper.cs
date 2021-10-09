#if FaithAnalytics_GameAnalytics

namespace com.faith.sdk.analytics
{
    using System.Collections;
    using UnityEngine;
    using GameAnalyticsSDK;

    public class FaithAnalyticsGameAnalyticsWrapper : MonoBehaviour, IGameAnalyticsATTListener
    {
        #region Public Variables

        public static FaithAnalyticsGameAnalyticsWrapper Instance;

        #endregion

        #region Private Variables

        private FaithAnalyticsGeneralConfiguretionInfo _faithAnalyticsGeneralConfiguretionInfo;
        private FaithAnalyticsGameAnalyticsConfiguretionInfo _faithAnalyticsGameAnalyticsConfiguretionInfo;

        #endregion

        #region Configuretion

        private bool CanLogEvent()
        {
            if (_faithAnalyticsGameAnalyticsConfiguretionInfo.IsAnalyticsEventEnabled)
            {
                return true;
            }
            else
            {
                FaithAnalyticsLogger.LogWarning("'logGAEvent' is currently turned off from APSDkIntegrationManager, please set it to 'true'");
                return false;
            }
        }

        private IEnumerator InitializationWithDelay()
        {

            yield return new WaitForSeconds(1f);

#if UNITY_IOS

            //----------
#if UNITY_EDITOR
            FaithAnalyticsLogger.Log("GA Initialized (Editor Only : Will fire 'GameAnalytics.RequestTrackingAuthorization(this)' on native iOS");
            GameAnalytics.Initialize();
#else
            GameAnalytics.RequestTrackingAuthorization(this);
#endif
            //----------

#else
            FaithAnalyticsLogger.Log("GA Initialized");
            GameAnalytics.Initialize();
#endif

        }

        #endregion

        #region GameAnalytics Callback

        public void GameAnalyticsATTListenerAuthorized()
        {
            GameAnalytics.Initialize();
            FaithAnalyticsLogger.Log("GA : ATTListenerAuthorized");
        }

        public void GameAnalyticsATTListenerDenied()
        {
            GameAnalytics.Initialize();
            FaithAnalyticsLogger.LogError("GA : ATTListenerDenied");
        }

        public void GameAnalyticsATTListenerNotDetermined()
        {
            GameAnalytics.Initialize();
            FaithAnalyticsLogger.LogError("GA : ATTListenerNotDetermined");
        }

        public void GameAnalyticsATTListenerRestricted()
        {
            GameAnalytics.Initialize();
            FaithAnalyticsLogger.LogWarning("GA : ATTListenerRestricted");
        }

#endregion

#region Public Callback

        public void Initialize(FaithAnalyticsGeneralConfiguretionInfo apSdkConfiguretionInfo, FaithAnalyticsGameAnalyticsConfiguretionInfo apGameAnalyticsConfiguretion) {

            _faithAnalyticsGeneralConfiguretionInfo = apSdkConfiguretionInfo;
            _faithAnalyticsGameAnalyticsConfiguretionInfo = apGameAnalyticsConfiguretion;

            StartCoroutine(InitializationWithDelay());
        }

#endregion

#region Progression Event

        public void ProgressionEvents(GAProgressionStatus progressionStatus, int level, int score = -1)
        {
            ProgressionEvents(progressionStatus, level, -1, score);
        }

        public void ProgressionEvents(GAProgressionStatus progressionStatus, int level, int world = -1, int score = -1)
        {
            if (score < 0)
                ProgressionEvent(progressionStatus, string.Format("world{0}", world == -1 ? _faithAnalyticsGameAnalyticsConfiguretionInfo.DefaultWorldIndex : world), string.Format("level{0}", level));
            else
                ProgressionEvent(progressionStatus, string.Format("world{0}", world == -1 ? _faithAnalyticsGameAnalyticsConfiguretionInfo.DefaultWorldIndex : world), string.Format("level{0}", level), score);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01)
        {
            if (CanLogEvent() && _faithAnalyticsGameAnalyticsConfiguretionInfo.IsTrackingProgressionEvent)
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, int score)
        {
            if (CanLogEvent() && _faithAnalyticsGameAnalyticsConfiguretionInfo.IsTrackingProgressionEvent)
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, score);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02)
        {
            if (CanLogEvent() && _faithAnalyticsGameAnalyticsConfiguretionInfo.IsTrackingProgressionEvent)
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, int score)
        {
            if (CanLogEvent() && _faithAnalyticsGameAnalyticsConfiguretionInfo.IsTrackingProgressionEvent)
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02, score);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03)
        {
            if (CanLogEvent() && _faithAnalyticsGameAnalyticsConfiguretionInfo.IsTrackingProgressionEvent)
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02, progression03);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03, int score)
        {
            if (CanLogEvent() && _faithAnalyticsGameAnalyticsConfiguretionInfo.IsTrackingProgressionEvent)
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02, progression03, score);
        }

        #endregion

        #region Ad Event

        public void AdEvent(GAAdAction adAction, GAAdType adType, string adPlacement)
        {
            if (CanLogEvent() && _faithAnalyticsGameAnalyticsConfiguretionInfo.IsTrackingAdEvent)
                GameAnalytics.NewAdEvent(adAction, adType, "Undefined", adPlacement);
        }

        public void AdEvent(GAAdAction adAction, GAAdType adType, string sdkName, string adPlacement)
        {
            if (CanLogEvent() && _faithAnalyticsGameAnalyticsConfiguretionInfo.IsTrackingAdEvent)
                GameAnalytics.NewAdEvent(adAction, adType, sdkName, adPlacement);
        }

        public void AdEvent(GAAdAction adAction, GAAdType adType, string sdkName, string adPlacement, long duration)
        {

            if (CanLogEvent() && _faithAnalyticsGameAnalyticsConfiguretionInfo.IsTrackingAdEvent)
                GameAnalytics.NewAdEvent(adAction, adType, sdkName, adPlacement, duration);
        }

        public void AdEvent(GAAdAction adAction, GAAdType adType, string sdkName, string adPlacement, GAAdError noAdError)
        {
            if (CanLogEvent() && _faithAnalyticsGameAnalyticsConfiguretionInfo.IsTrackingAdEvent)
                GameAnalytics.NewAdEvent(adAction, adType, sdkName, adPlacement, noAdError);
        }

#endregion

    }
}

#endif




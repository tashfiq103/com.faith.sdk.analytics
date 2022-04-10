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
        public bool IsATEEnabled { get; private set; }
        #endregion

        #region Private Variables

        public FaithAnalyticsGeneralConfiguretionInfo FaithAnalyticsGeneralConfiguretionInfoReference { get; private set; }
        public FaithAnalyticsGameAnalyticsConfiguretionInfo FaithAnalyticsGameAnalyticsConfiguretionInfoReference { get; private set; }

        #endregion

        #region Configuretion

        private bool CanLogEvent()
        {
            if (FaithAnalyticsGameAnalyticsConfiguretionInfoReference.IsAnalyticsEventEnabled)
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

            yield return new WaitForSeconds(2.5f);

            

#if UNITY_IOS

            //----------
#if UNITY_EDITOR
            FaithAnalyticsLogger.Log("GA Initialized (Editor Only : Will fire 'GameAnalytics.RequestTrackingAuthorization(this)' on native iOS");
            GameAnalytics.Initialize();
#else
            if (IsATEEnabled)
            {
                GameAnalytics.RequestTrackingAuthorization(this);
            }
            else
            {
                FaithAnalyticsLogger.Log("GA Initialized");
                GameAnalytics.Initialize();
            }
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

        public void Initialize(FaithAnalyticsGeneralConfiguretionInfo apSdkConfiguretionInfo, FaithAnalyticsGameAnalyticsConfiguretionInfo apGameAnalyticsConfiguretion, bool isATEEnabled) {

            FaithAnalyticsGeneralConfiguretionInfoReference = apSdkConfiguretionInfo;
            FaithAnalyticsGameAnalyticsConfiguretionInfoReference = apGameAnalyticsConfiguretion;
            IsATEEnabled = isATEEnabled;
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
                ProgressionEvent(progressionStatus, string.Format("world{0}", world == -1 ? FaithAnalyticsGameAnalyticsConfiguretionInfoReference.DefaultWorldIndex : world), string.Format("level{0}", level));
            else
                ProgressionEvent(progressionStatus, string.Format("world{0}", world == -1 ? FaithAnalyticsGameAnalyticsConfiguretionInfoReference.DefaultWorldIndex : world), string.Format("level{0}", level), score);
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01)
        {
            if (CanLogEvent() && FaithAnalyticsGameAnalyticsConfiguretionInfoReference.IsTrackingProgressionEvent)
            {
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01);
                FaithAnalyticsLogger.Log(string.Format("ProgressionStatus : {0}, progression01 : {1}", progressionStatus, progression01));
            }
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, int score)
        {
            if (CanLogEvent() && FaithAnalyticsGameAnalyticsConfiguretionInfoReference.IsTrackingProgressionEvent)
            {
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, score);
                FaithAnalyticsLogger.Log(string.Format("ProgressionStatus : {0}, progression01 : {1}, score = {2}", progressionStatus, progression01, score));
            }
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02)
        {
            if (CanLogEvent() && FaithAnalyticsGameAnalyticsConfiguretionInfoReference.IsTrackingProgressionEvent)
            {
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02);
                FaithAnalyticsLogger.Log(string.Format("ProgressionStatus : {0}, progression01 : {1}, progression02 = {2}", progressionStatus, progression01, progression02));
            }
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, int score)
        {
            if (CanLogEvent() && FaithAnalyticsGameAnalyticsConfiguretionInfoReference.IsTrackingProgressionEvent)
            {
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02, score);
                FaithAnalyticsLogger.Log(string.Format("ProgressionStatus : {0}, progression01 : {1}, progression02 = {2}, score = {3}", progressionStatus, progression01, progression02, score));
            }
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03)
        {
            if (CanLogEvent() && FaithAnalyticsGameAnalyticsConfiguretionInfoReference.IsTrackingProgressionEvent)
            {
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02, progression03);
                FaithAnalyticsLogger.Log(string.Format("ProgressionStatus : {0}, progression01 : {1}, progression02 = {2}, progression03 = {3}", progressionStatus, progression01, progression02, progression03));
            }
        }

        public void ProgressionEvent(GAProgressionStatus progressionStatus, string progression01, string progression02, string progression03, int score)
        {
            if (CanLogEvent() && FaithAnalyticsGameAnalyticsConfiguretionInfoReference.IsTrackingProgressionEvent)
            {
                GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02, progression03, score);
                FaithAnalyticsLogger.Log(string.Format("ProgressionStatus : {0}, progression01 : {1}, progression02 = {2}, progression03 = {3}, score = {4}", progressionStatus, progression01, progression02, progression03, score));

            }
        }
        #endregion

        #region Ad Event

        public void AdEvent(GAAdAction adAction, GAAdType adType, string adPlacement)
        {
            if (CanLogEvent() && FaithAnalyticsGameAnalyticsConfiguretionInfoReference.IsTrackingAdEvent)
                GameAnalytics.NewAdEvent(adAction, adType, "Undefined", adPlacement);
        }

        public void AdEvent(GAAdAction adAction, GAAdType adType, string sdkName, string adPlacement)
        {
            if (CanLogEvent() && FaithAnalyticsGameAnalyticsConfiguretionInfoReference.IsTrackingAdEvent)
                GameAnalytics.NewAdEvent(adAction, adType, sdkName, adPlacement);
        }

        public void AdEvent(GAAdAction adAction, GAAdType adType, string sdkName, string adPlacement, long duration)
        {

            if (CanLogEvent() && FaithAnalyticsGameAnalyticsConfiguretionInfoReference.IsTrackingAdEvent)
                GameAnalytics.NewAdEvent(adAction, adType, sdkName, adPlacement, duration);
        }

        public void AdEvent(GAAdAction adAction, GAAdType adType, string sdkName, string adPlacement, GAAdError noAdError)
        {
            if (CanLogEvent() && FaithAnalyticsGameAnalyticsConfiguretionInfoReference.IsTrackingAdEvent)
                GameAnalytics.NewAdEvent(adAction, adType, sdkName, adPlacement, noAdError);
        }

#endregion

    }
}

#endif




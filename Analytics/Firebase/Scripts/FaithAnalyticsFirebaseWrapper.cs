namespace com.faith.sdk.analytics
{
#if FaithAnalytics_Firebase

    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using Firebase;
    using Firebase.Analytics;

    public class FaithAnalyticsFirebaseWrapper : MonoBehaviour
    {
        #region Public Variables

        public static FaithAnalyticsFirebaseWrapper Instance;

        public bool IsFirebaseInitialized { get; private set; } = false;

        #endregion

        #region Private Variables

        private FaithAnalyticsGeneralConfiguretionInfo _apSdkConfiguretionInfo;
        private FaithAnalyticsFirebaseConfiguretionInfo _apFirebaseConfiguretion;

        private Queue<UnityAction> _waitingForFirebaseToInitialized = new Queue<UnityAction>();

        #endregion

        #region Configuretion

        private bool CanLogEvent()
        {
            if (_apFirebaseConfiguretion.IsAnalyticsEventEnabled)
            {
                return true;
            }
            else
            {
                FaithAnalyticsLogger.LogWarning("'logFirebaseEvent' is currently turned off from APSDkIntegrationManager, please set it to 'true'");
                return false;
            }
        }

        #endregion

        #region Public Callback

        public void Initialize(FaithAnalyticsGeneralConfiguretionInfo apSdkConfiguretionInfo, FaithAnalyticsFirebaseConfiguretionInfo apFirebaseConfiguretion, UnityAction OnInitialized = null)
        {
            _apSdkConfiguretionInfo = apSdkConfiguretionInfo;
            _apFirebaseConfiguretion = apFirebaseConfiguretion;

            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    // subscribe to firebase events
                    // subscribe here so avoid error if dependency check fails

                    IsFirebaseInitialized = true;
                    while (_waitingForFirebaseToInitialized.Count > 0)
                        _waitingForFirebaseToInitialized.Dequeue()?.Invoke();

                    FaithAnalyticsLogger.Log("Firebase Initialized");
                    OnInitialized?.Invoke();
                }
                else
                {
                    FaithAnalyticsLogger.LogError($"Firebase: Could not resolve all Firebase dependencies: {dependencyStatus}");
                }
            });
        }

        public void OnFirebaseInitializedEvent(UnityAction OnFirebaseInitialized)
        {
            if (IsFirebaseInitialized)
                OnFirebaseInitialized?.Invoke();
            else
                _waitingForFirebaseToInitialized.Enqueue(OnFirebaseInitialized);
        }


        public void LogFirebaseEvent(string eventName)
        {
            if (CanLogEvent())
            {
                FirebaseAnalytics.LogEvent(
                   eventName);
            }

        }

        public void LogFirebaseEvent(string eventName, string parameName, string paramValue)
        {

            if (CanLogEvent())
            {
                FirebaseAnalytics.LogEvent(
                        eventName,
                        parameName,
                        paramValue
                    );
            }
        }

        public void LogFirebaseEvent(string eventName, List<Parameter> parameter)
        {

            if (CanLogEvent())
            {

                FirebaseAnalytics.LogEvent(
                    eventName,
                    parameter.ToArray()
                );
            }
        }

        public void ProgressionEvent(string eventName)
        {

            if (_apFirebaseConfiguretion.IsTrackingAdEvent)
            {
                LogFirebaseEvent(eventName);
            }
            else
            {
                FaithAnalyticsLogger.LogWarning("'AdEvent' is disabled for 'FirebaseSDK'");
            }
        }

        public void ProgressionEvent(string eventName, string parameName, string paramValue)
        {

            if (_apFirebaseConfiguretion.IsTrackingProgressionEvent)
            {
                LogFirebaseEvent(eventName, parameName, paramValue);
            }
            else
            {
                FaithAnalyticsLogger.LogWarning("'ProgressionEvent' is disabled for 'FirebaseSDK'");
            }
        }

        public void ProgressionEvent(string eventName, List<Parameter> parameter)
        {

            if (_apFirebaseConfiguretion.IsTrackingProgressionEvent)
            {
                LogFirebaseEvent(eventName, parameter);
            }
            else
            {
                FaithAnalyticsLogger.LogWarning("'ProgressionEvent' is disabled for 'FirebaseSDK'");
            }
        }

        public void AdEvent(string eventName, string parameName, string paramValue)
        {

            if (_apFirebaseConfiguretion.IsTrackingAdEvent)
            {
                LogFirebaseEvent(eventName, parameName, paramValue);
            }
            else
            {
                FaithAnalyticsLogger.LogWarning("'AdEvent' is disabled for 'FirebaseSDK'");
            }
        }


        #endregion
    }

#endif
}


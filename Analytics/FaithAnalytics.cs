namespace com.faith.sdk.analytics
{
    using UnityEngine;
    using System.Collections.Generic;

    public static class FaithAnalytics
    {

        #region Custom Variables

        public static class Key
        {
            public static string level
            {
                get
                {
                    return "level";
                }
            }
            public static string score
            {
                get
                {
                    return "score";
                }
            }
            public static string rank
            {
                get
                {
                    return "rank";
                }
            }
            public static string level_started
            {
                get
                {
                    return "level_started";
                }
            }
            public static string level_complete
            {
                get
                {
                    return "level_complete";
                }
            }
            public static string level_failed
            {
                get
                {
                    return "level_failed";
                }
            }
        }

        #endregion

        

        #region Preset

        public static void LevelStarted(object level, object score = null)
        {
            Dictionary<string, object> eventParam = new Dictionary<string, object>();
            eventParam.Add(Key.level, level);
            if (score != null)
                eventParam.Add(Key.score, score);

#if FaithAnalytics_Facebook
            FaithAnalyticsFacebookWrapper.Instance.ProgressionEvent(Key.level_started, eventParam);
#endif




#if FaithAnalytics_Adjust
            FaithAnalyticsAdjustWrapper.Instance.ProgressionEvent(Key.level_started, eventParam);
#endif




#if FaithAnalytics_GameAnalytics
            FaithAnalyticsGameAnalyticsWrapper.Instance.ProgressionEvents(
                    GameAnalyticsSDK.GAProgressionStatus.Start,
                    (int)level,
                    world: -1);
#endif




#if FaithAnalytics_Firebase
            if (score == null)
                FaithAnalyticsFirebaseWrapper.Instance.ProgressionEvent(Key.level_started);
            else
            {
                FaithAnalyticsFirebaseWrapper.Instance.ProgressionEvent(
                        Key.level_started,
                        Key.score,
                    (string)score
                    );
            }
#endif
        }

        public static void LevelComplete(object level, object score = null)
        {
            Dictionary<string, object> eventParam = new Dictionary<string, object>();
            eventParam.Add(Key.level, level);
            if (score != null)
                eventParam.Add(Key.score, score);

#if FaithAnalytics_Facebook
            FaithAnalyticsFacebookWrapper.Instance.ProgressionEvent(Key.level_complete, eventParam);
#endif




#if FaithAnalytics_Adjust
            FaithAnalyticsAdjustWrapper.Instance.ProgressionEvent(Key.level_complete, eventParam);
#endif




#if FaithAnalytics_GameAnalytics
            FaithAnalyticsGameAnalyticsWrapper.Instance.ProgressionEvents(
                    GameAnalyticsSDK.GAProgressionStatus.Complete,
                    (int)level,
                    world: -1);
#endif




#if FaithAnalytics_Firebase
            if (score == null)
                FaithAnalyticsFirebaseWrapper.Instance.ProgressionEvent(Key.level_complete);
            else
            {
                FaithAnalyticsFirebaseWrapper.Instance.ProgressionEvent(
                        Key.level_complete,
                        Key.score,
                    (string)score
                    );
            }
#endif
        }

        public static void LevelFailed(object level, object score = null)
        {
            Dictionary<string, object> eventParam = new Dictionary<string, object>();
            eventParam.Add(Key.level, level);
            if (score != null)
                eventParam.Add(Key.score, score);

#if FaithAnalytics_Facebook
            FaithAnalyticsFacebookWrapper.Instance.ProgressionEvent(Key.level_failed, eventParam);
#endif




#if FaithAnalytics_Adjust
            FaithAnalyticsAdjustWrapper.Instance.ProgressionEvent(Key.level_failed, eventParam);
#endif




#if FaithAnalytics_GameAnalytics
            FaithAnalyticsGameAnalyticsWrapper.Instance.ProgressionEvents(
                    GameAnalyticsSDK.GAProgressionStatus.Fail,
                    (int)level,
                    world: -1);
#endif




#if FaithAnalytics_Firebase
            if (score == null)
                FaithAnalyticsFirebaseWrapper.Instance.ProgressionEvent(Key.level_failed);
            else
            {
                FaithAnalyticsFirebaseWrapper.Instance.ProgressionEvent(
                        Key.level_failed,
                        Key.score,
                    (string)score
                    );
            }
#endif
        }

        #endregion
    }
}


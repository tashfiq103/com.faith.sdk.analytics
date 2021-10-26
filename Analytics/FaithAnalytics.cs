namespace com.faith.sdk.analytics
{
    using System.Collections.Generic;
    using UnityEngine;
    public static class FaithAnalytics
    {

        #region Custom Variables

        public enum AdType
        {
            RewardedAd,
            InterstitialAd,
            BannerAd,
            MREC
        }

        public enum Status
        {
            Start,
            Complete,
            Failed
        }


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

        #region Configuretion

        private static void LogEvent(string paramName, string paramValue, string eventName, Dictionary<string, object> eventParams)
        {
#if FaithAnalytics_Facebook
                            FaithFacebookWrapper.Instance.AdEvent(
                                    eventName,
                                    eventParams
                                );
#endif

#if FaithAnalytics_Adjust
                            FaithAdjustWrapper.Instance.AdEvent(
                                    eventName,
                                    eventParams
                                );
#endif

#if FaithAnalytics_Firebase
                            FaithFirebaseWrapper.Instance.AdEvent(
                                    eventName,
                                    paramName,
                                    paramValue
                                );
#endif
        }

        #endregion

        #region Preset

        public static void LevelStarted(object level, object score = null)
        {
            LevelStarted(
                Resources.Load<FaithAnalyticsGameAnalyticsConfiguretionInfo>("FaithAnalyticsGameAnalyticsConfiguretionInfo").DefaultWorldIndex,
                level,
                score);

        }

        public static void LevelStarted(object world, object level, object score = null)
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

            int castedWorld = 0;
            if (world != null)
            {
                if (int.TryParse(world.ToString(), out castedWorld))
                {

                }
                else
                {
                    castedWorld = Resources.Load<FaithAnalyticsGameAnalyticsConfiguretionInfo>("FaithAnalyticsGameAnalyticsConfiguretionInfo").DefaultWorldIndex;
                }
            }

            int castedLevel = 0;

            if (level != null)
            {
                if (int.TryParse(level.ToString(), out castedLevel))
                {

                }
            }

            int castedScore = 0;
            if (score != null) {
                if (int.TryParse(score.ToString(), out castedScore))
                {

                }
            }


            FaithAnalyticsGameAnalyticsWrapper.Instance.ProgressionEvent(
                    GameAnalyticsSDK.GAProgressionStatus.Start,
                    castedWorld.ToString(),
                    castedLevel.ToString(),
                    castedScore
                );
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
            LevelComplete(
                Resources.Load<FaithAnalyticsGameAnalyticsConfiguretionInfo>("FaithAnalyticsGameAnalyticsConfiguretionInfo").DefaultWorldIndex,
                level,
                score);
        }

        public static void LevelComplete(object world, object level, object score = null)
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

            int castedWorld = 0;
            if (world != null)
            {
                if (int.TryParse(world.ToString(), out castedWorld))
                {

                }
                else
                {
                    castedWorld = Resources.Load<FaithAnalyticsGameAnalyticsConfiguretionInfo>("FaithAnalyticsGameAnalyticsConfiguretionInfo").DefaultWorldIndex;
                }
            }

            int castedLevel = 0;

            if (level != null)
            {
                if (int.TryParse(level.ToString(), out castedLevel))
                {

                }
            }

            int castedScore = 0;
            if (score != null)
            {
                if (int.TryParse(score.ToString(), out castedScore))
                {

                }
            }


            FaithAnalyticsGameAnalyticsWrapper.Instance.ProgressionEvent(
                    GameAnalyticsSDK.GAProgressionStatus.Complete,
                    castedWorld.ToString(),
                    castedLevel.ToString(),
                    castedScore
                );

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
            LevelFailed(
                Resources.Load<FaithAnalyticsGameAnalyticsConfiguretionInfo>("FaithAnalyticsGameAnalyticsConfiguretionInfo").DefaultWorldIndex,
                level,
                score);
        }

        public static void LevelFailed(object world, object level, object score = null)
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

            int castedWorld = 0;
            if (world != null)
            {
                if (int.TryParse(world.ToString(), out castedWorld))
                {

                }
                else
                {
                    castedWorld = Resources.Load<FaithAnalyticsGameAnalyticsConfiguretionInfo>("FaithAnalyticsGameAnalyticsConfiguretionInfo").DefaultWorldIndex;
                }
            }

            int castedLevel = 0;

            if (level != null)
            {
                if (int.TryParse(level.ToString(), out castedLevel))
                {

                }
            }

            int castedScore = 0;
            if (score != null)
            {
                if (int.TryParse(score.ToString(), out castedScore))
                {

                }
            }


            FaithAnalyticsGameAnalyticsWrapper.Instance.ProgressionEvent(
                    GameAnalyticsSDK.GAProgressionStatus.Fail,
                    castedWorld.ToString(),
                    castedLevel.ToString(),
                    castedScore
                );

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

        public static void AdStatusEvent(AdType adType, Status adStatus) {

            string adTypeString = "Undefined";

            switch (adType) {
                case AdType.RewardedAd:
                    adTypeString = "RewardedAd";
                    break;
                case AdType.InterstitialAd:
                    adTypeString = "InterstitialAd";
                    break;
                case AdType.BannerAd:
                    adTypeString = "BannerAd";
                    break;
                case AdType.MREC:
                    adTypeString = "MREC";
                    break;
            }

            string paramName = "adPlacement";
            string paramValue = adStatus.ToString();
            string eventName = adTypeString;

            Dictionary<string, object> eventParams = new Dictionary<string, object>();
            eventParams.Add(paramName, paramValue);

            LogEvent(paramName, paramValue, eventName, eventParams);

#if FaithAnalytics_GameAnalytics

            GameAnalyticsSDK.GAAdType gAAdType = GameAnalyticsSDK.GAAdType.Undefined;

            switch (adType)
            {
                case AdType.RewardedAd:
                    gAAdType = GameAnalyticsSDK.GAAdType.RewardedVideo;
                    break;
                case AdType.InterstitialAd:
                    gAAdType = GameAnalyticsSDK.GAAdType.Interstitial;
                    break;
                case AdType.BannerAd:
                    gAAdType = GameAnalyticsSDK.GAAdType.Banner;
                    break;
                case AdType.MREC:
                    gAAdType = GameAnalyticsSDK.GAAdType.OfferWall;
                    break;
            }

            GameAnalyticsSDK.GAAdAction gAAdAction = GameAnalyticsSDK.GAAdAction.Undefined;

            switch (adStatus) {

                case Status.Start:
                    gAAdAction = GameAnalyticsSDK.GAAdAction.Request;
                    break;
                case Status.Complete:
                    if (gAAdType == GameAnalyticsSDK.GAAdType.RewardedVideo)
                        gAAdAction = GameAnalyticsSDK.GAAdAction.RewardReceived;
                    else
                        gAAdAction = GameAnalyticsSDK.GAAdAction.Show;
                    break;
                case Status.Failed:
                    gAAdAction = GameAnalyticsSDK.GAAdAction.FailedShow;
                    break;
            }

            FaithAnalyticsGameAnalyticsWrapper.Instance.AdEvent(
                        gAAdAction,
                        gAAdType,
                        adTypeString
                    );
#endif
        }

        #endregion
    }
}


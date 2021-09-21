# FaithAnalytics Integration Manager
[Reference Videos] : <https://www.youtube.com/watch?v=CoxcUw8iCsM>




## Downloading "FaithAnalytics" package.

Please go to the release section on this repository and download the latest version of "FaithAnalytics". Once you download the ".unityPackage", import it to your unity project. Once the project recompilation is complete, you will be able to see the following windows as the given screenshot below (Faith -> FaithAnalytics Integration Manager)

![](https://github.com/tashfiq103/com.faith.sdk.analytics/blob/main/_GitHubResources/ss0_menu.png)




## Understanding the basic interface.

FaithAnalytics Integration manager comes with the following section

- General
- Analytics
- Debugging




## General
- Download : Will redirect you the following repository
- Documentation : Will redirect you to the README.md file.
- Auto Initialize : For automatic initialization at start for all the analytics, you need to set the value = 'true'. If you want to manually initialize the sdk, make sure to call 'FaithAnalyticsManager.Initialize()' when the value = 'false'
![](https://github.com/tashfiq103/com.faith.sdk.analytics/blob/main/_GitHubResources/ss1_general_auto.png)
![](https://github.com/tashfiq103/com.faith.sdk.analytics/blob/main/_GitHubResources/ss2_general_manual.png)



## Analytics

- Each Analytics will have their own section to confiure it settings.
- The tabs would be grayed out if you haven't imported the following SDK with the status message of 'SDK - Not Found'.
- Once the SDK has been imported, you will be able to interact with the section.
- In order to iniatize the SDK and work properly, make sure to "Enable" the imported SDK.
- For "TrackProgressionEvent" on each analytics : LevelStarted, LevelComplete & LevelFailed will pass their data on the following analytics.
- For "TrackAdEvent" on each analytics : RewardedAd, InterstitialAd & BannerAd will pass their data on the following analytics.

- APIs
```sh
using com.faith.sdk.analytics;
public static class AnalyticsCall
{
    public static void LogLevelStarted(int levelIndex = 0) {

        FaithAnalytics.LevelStarted(levelIndex);
    }

    public static void LogLevelComplete(int levelIndex = 0)
    {
        FaithAnalytics.LevelComplete(levelIndex);
    }

    public static void LogLevelFailed(int levelIndex = 0)
    {
        FaithAnalytics.LevelFailed(levelIndex);
    }
}
```

![](https://github.com/tashfiq103/com.faith.sdk.analytics/blob/main/_GitHubResources/ss4_analytics_enable_disable.png)




## Debugging

- You will be able to "Toggle" the APSdk log by taping "Show AP Sdk Log In Console".
- You will be able to change the colors of the log on the following section as well.

![](https://github.com/tashfiq103/com.faith.sdk.analytics/blob/main/_GitHubResources/ss9_debugging.png)


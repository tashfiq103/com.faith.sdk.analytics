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

![](https://github.com/tashfiq103/com.faith.sdk.analytics/blob/main/_GitHubResources/ss3_analytics_overview.png)

![](https://github.com/tashfiq103/com.faith.sdk.analytics/blob/main/_GitHubResources/ss4_analytics_enable_disable.png)

- Each Analytics will have their own section to confiure it settings.
- The tabs would be grayed out if you haven't imported the following SDK with the status message of 'SDK - Not Found'.
- Once the SDK has been imported, you will be able to interact with the section.
- In order to iniatize the SDK and work properly, make sure to "Enable" the imported SDK.
- For "TrackProgressionEvent" on each analytics : LevelStarted, LevelComplete & LevelFailed will pass their data on the following analytics.
- For "TrackAdEvent" on each analytics : RewardedAd, InterstitialAd & BannerAd will pass their data on the following analytics.

### Adjust

![](https://github.com/tashfiq103/com.faith.sdk.analytics/blob/main/_GitHubResources/ss5_analytics_adjust.png)

- AppToken Android : Fill it with your android app token from adjust
- AppToken iOS : Fill it with your iOS app token from adjust
- Environment : For QA/Test purpose, set it to 'Sandbox', for publishing, make sure to switch to 'Production'.
- For the advance settings, please check the Adjust documentation for more detail.

### Facebook

![](https://github.com/tashfiq103/com.faith.sdk.analytics/blob/main/_GitHubResources/ss8_analytics_facebook.png)

- Warning : You need to create 'FacebookSettings' by going to 'Facebook/Edit Settings' from menu in order to facebook sdk for working properly.
- AppName : Place your name like "MyNewSdkApp"
- AppID : Which you can get from the 'Facebook' app dashboard.

### GameAnalytics

![](https://github.com/tashfiq103/com.faith.sdk.analytics/blob/main/_GitHubResources/ss7_analytics_game_analytics.png)

- Warning : You need to create GA_'Settings' by going to 'Window/Game Analytics/Select Settings' from menu in order to ga_sdk for working properly.
- Note : If you haven't setup your game on GA, please do by loging, adding platform and selecting your games from down below. Make sure to put the right 'sdk key' and 'secret key' for your specefic platform.
- GameKey & SecretKey : Make sure to get these keys from 'GameAnalytics' dashboard or by loging through interface on the given screenshot.
- Default World Index : GA seperate the level to different world. lets say you have 5 different world consist of 10 level. But in general hyper casual game, we go for different level and not considering the sub level system.

### Firebase

![](https://github.com/tashfiq103/com.faith.sdk.analytics/blob/main/_GitHubResources/ss6_analytics_firebase.png)

- No additional requirement for initialization.

### APIs
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

- You will be able to "Toggle" the FaithAnalytics log by taping "Show Analytics Log In Console".
- You will be able to change the colors of the log on the following section as well.

![](https://github.com/tashfiq103/com.faith.sdk.analytics/blob/main/_GitHubResources/ss9_debugging.png)


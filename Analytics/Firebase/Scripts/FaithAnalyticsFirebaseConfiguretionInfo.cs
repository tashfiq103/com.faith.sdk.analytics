namespace com.faith.sdk.analytics
{
    using UnityEngine;
    using System.Collections.Generic;

    //[CreateAssetMenu(fileName = "FaithAnalyticsFirebaseConfiguretionInfo", menuName = FaithAnalyticsGeneralConfiguretionInfo.NAME_OF_SDK + "/FaithAnalyticsFirebaseConfiguretionInfo")]
    public class FaithAnalyticsFirebaseConfiguretionInfo : FaithBaseClassForAnalyticsConfiguretionInfo
    {
        public override void Initialize(FaithAnalyticsGeneralConfiguretionInfo faithAnalyticsGeneralConfiguretionInfo, bool isATTEnable = false)
        {
#if FaithAnalytics_Firebase
            if (FaithAnalyticsFirebaseWrapper.Instance == null && IsAnalyticsEventEnabled) {

                GameObject newfaithAnalyticsFirebaseWrapper = new GameObject("FaithAnalyticsFirebaseWrapper");
                FaithAnalyticsFirebaseWrapper.Instance = newfaithAnalyticsFirebaseWrapper.AddComponent<FaithAnalyticsFirebaseWrapper>();
                FaithAnalyticsFirebaseWrapper.Instance.Initialize(faithAnalyticsGeneralConfiguretionInfo, this);

                DontDestroyOnLoad(newfaithAnalyticsFirebaseWrapper);

            }
#endif
        }

        public override void PostCustomEditorGUI()
        {
            
        }

        public override void PreCustomEditorGUI()
        {
            
        }

        public override void SetNameAndIntegrationStatus()
        {
            string sdkName = FaithAnalyticsGeneralConfiguretionInfo.NAME_OF_SDK + "_Firebase";
            SetNameOfConfiguretion(sdkName);
#if UNITY_EDITOR
            _isSDKIntegrated = FaithAnalyticsScriptDefineSymbol.CheckFirebaseIntegration(sdkName);
#endif
        }
    }
}


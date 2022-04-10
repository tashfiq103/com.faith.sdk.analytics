namespace com.faith.sdk.analytics
{
    using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

    //[CreateAssetMenu(fileName = "FaithAnalyticsTenjinConfiguretionInfo", menuName = FaithAnalyticsGeneralConfiguretionInfo.NAME_OF_SDK + "/FaithAnalyticsTenjinConfiguretionInfo")]
    public class FaithAnalyticsTenjinConfiguretionInfo : FaithBaseClassForAnalyticsConfiguretionInfo
    {
        #region Public Variables

        public string APIKey { get { return _apiKey; } }

        #endregion

        #region Private Variables

        [SerializeField] private string _apiKey;

        #endregion

        #region Override Method

        public override void Initialize(FaithAnalyticsGeneralConfiguretionInfo faithAnalyticsGeneralConfiguretionInfo, bool isATTEnable = false)
        {

#if FaithAnalytics_Tenjin
            if (FaithAnalyticsTenjinWrapper.Instance == null && IsAnalyticsEventEnabled)
            {
                GameObject newfaithAnalyticsTenjinWrapper = new GameObject("FaithAnalyticsTenjinWrapper");
                FaithAnalyticsTenjinWrapper.Instance = newfaithAnalyticsTenjinWrapper.AddComponent<FaithAnalyticsTenjinWrapper>();

                DontDestroyOnLoad(newfaithAnalyticsTenjinWrapper);

                FaithAnalyticsTenjinWrapper.Instance.Initialize(faithAnalyticsGeneralConfiguretionInfo, this, isATTEnable);
            }
#endif
        }

        public override void PostCustomEditorGUI()
        {
           
        }

        public override void PreCustomEditorGUI()
        {
            _apiKey = EditorGUILayout.TextField("APIKey", _apiKey);

            FaithAnalyticsEditorModule.DrawHorizontalLine();
        }

        public override void SetNameAndIntegrationStatus()
        {
            string sdkName = FaithAnalyticsGeneralConfiguretionInfo.NAME_OF_SDK + "_Tenjin";
            SetNameOfConfiguretion(sdkName);
#if UNITY_EDITOR
            _isSDKIntegrated = FaithAnalyticsScriptDefineSymbol.CheckTenjinIntegration(sdkName);
#endif
        }
    }

#endregion
}


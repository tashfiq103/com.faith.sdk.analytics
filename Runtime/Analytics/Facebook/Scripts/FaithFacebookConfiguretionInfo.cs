namespace com.faith.sdk.analytics
{
    using UnityEngine;
    using UnityEngine.Events;

    [CreateAssetMenu(fileName = "FaithFacebookConfiguretionInfo", menuName = FaithAnalyticsConstant.NameOfSDK + "/FaithFacebookConfiguretionInfo")]
    public class FaithFacebookConfiguretionInfo : FaithBaseClassForAnalyticsConfiguretionInfo
    {
        #region Private Variables

#if FaithAnalytics_Facebook

        [HideInInspector, SerializeField] private string _facebookAppName;
        [HideInInspector, SerializeField] private string _facebookAppId;
        private Facebook.Unity.Settings.FacebookSettings _facebookSettings;
#endif

        #endregion

        #region Override Method

        public override void SetNameAndIntegrationStatus()
        {
            throw new System.NotImplementedException();
        }

        public override void Initialize(FaithAnalyticsGeneralConfiguretionInfo faithAnalyticsGeneralConfiguretionInfo, bool isATTEnable = false)
        {
            throw new System.NotImplementedException();
        }

        public override void PostCustomEditorGUI()
        {
            throw new System.NotImplementedException();
        }

        public override void PreCustomEditorGUI()
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}


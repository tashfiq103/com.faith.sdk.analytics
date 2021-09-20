namespace com.faith.sdk.analytics
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "FaithAnalyticsGeneralConfiguretionInfo", menuName = FaithAnalyticsConstant.NameOfSDK + "/FaithAnalyticsGeneralConfiguretionInfo")]
    public class FaithAnalyticsGeneralConfiguretionInfo : ScriptableObject
    {
        #region Private Variables

#if UNITY_EDITOR
        [HideInInspector, SerializeField] private bool _showGeneralSetting = false;
        [HideInInspector, SerializeField] private bool _showAnalytics = false;
        [HideInInspector, SerializeField] private bool _showDebuggingSetting = false;
#endif

        [HideInInspector, SerializeField] private bool _autoInitialize = true;

        [HideInInspector, SerializeField] private bool _showAnalyticsLogInConsole = true;

        [HideInInspector, SerializeField] private Color _infoLogColor = Color.cyan;
        [HideInInspector, SerializeField] private Color _warningLogColor = Color.yellow;
        [HideInInspector, SerializeField] private Color _errorLogColor = Color.red;


        #endregion

        #region Public Variables

        public bool IsAutoInitialize { get { return _autoInitialize; } }

        public bool ShowAPSdkLogInConsole { get { return _showAnalyticsLogInConsole; } }

        public Color InfoLogColor { get { return _infoLogColor; } }
        public Color WarningLogColor { get { return _warningLogColor; } }
        public Color ErrorLogColor { get { return _errorLogColor; } }

        #endregion

    }
}


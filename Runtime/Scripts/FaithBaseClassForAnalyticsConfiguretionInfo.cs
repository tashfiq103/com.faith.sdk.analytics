namespace com.faith.sdk.analytics
{
    using UnityEngine;

    public abstract class FaithBaseClassForAnalyticsConfiguretionInfo : ScriptableObject
    {
        #region Public Variables

        public string NameOfConfiguretion { get { return _nameOfConfiguretion; } }

        public bool IsAnalyticsEventEnabled { get { return _enableAnalyticsEvent; } }

        public bool IsTrackingProgressionEvent { get { return _trackProgressionEvent; } }
        public bool IsTrackingAdEvent { get { return _trackAdEvent; } }

        public bool IsSubscribedToLionEvent { get { return _subscribeToLionEvent; } }
        public bool IsSubscribedToLionEventUA { get { return _subscribeToLionEventUA; } }

        #endregion

        #region Protected Variables

#if UNITY_EDITOR
        [HideInInspector, SerializeField] protected bool    _showSettings;
#endif
        [HideInInspector, SerializeField] protected string  _nameOfConfiguretion;
        [HideInInspector, SerializeField] protected bool    _isSDKIntegrated;

        [HideInInspector, SerializeField] protected bool    _enableAnalyticsEvent = false;

        [HideInInspector, SerializeField] protected bool    _trackProgressionEvent = false;
        [HideInInspector, SerializeField] protected bool    _trackAdEvent = false;

        [HideInInspector, SerializeField] protected bool    _subscribeToLionEvent = false;
        [HideInInspector, SerializeField] protected bool    _subscribeToLionEventUA = false;


        #endregion

        #region Protected Method

        /// <summary>
        /// Editor Only
        /// </summary>
        /// <param name="scriptDefineSymbol"></param>
        protected void SetNameOfConfiguretion(string scriptDefineSymbol, string concatinate = "")
        {

            string[] splited = scriptDefineSymbol.Split('_');
            _nameOfConfiguretion = splited[1] + concatinate;
        }

        #endregion

        #region Abstract Method

        public abstract void SetNameAndIntegrationStatus();

        public abstract void Initialize(FaithAnalyticsGeneralConfiguretionInfo faithAnalyticsGeneralConfiguretionInfo, bool isATTEnable = false );

        /// <summary>
        /// You can write your editor script for the variables on your derived class before the template editor script
        /// </summary>
        public abstract void PreCustomEditorGUI();

        /// <summary>
        /// You can write your editor script for the variables on your derived class after the template editor script
        /// </summary>
        public abstract void PostCustomEditorGUI();

        #endregion
    }
}



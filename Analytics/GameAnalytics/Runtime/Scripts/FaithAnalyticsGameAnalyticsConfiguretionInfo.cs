namespace com.faith.sdk.analytics
{
    using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

    //[CreateAssetMenu(fileName = "FaithAnalyticsGameAnalyticsConfiguretionInfo", menuName = FaithAnalyticsGeneralConfiguretionInfo.NAME_OF_SDK + "/FaithAnalyticsGameAnalyticsConfiguretionInfo")]
    public class FaithAnalyticsGameAnalyticsConfiguretionInfo : FaithBaseClassForAnalyticsConfiguretionInfo
    {

        #region Public Variables

        public int DefaultWorldIndex { get { return _defaultWorldIndex; } }

        #endregion

        #region Private Variables

        [HideInInspector, SerializeField] private int _defaultWorldIndex = 1;

#if UNITY_EDITOR && FaithAnalytics_GameAnalytics
        private GameAnalyticsSDK.Setup.Settings _gaSettings;
        private Editor _gaSettingsEditor;
        private bool _isShowingGASettings;

#endif

        #endregion

        #region Override Method

        public override void SetNameAndIntegrationStatus()
        {
            string sdkName = FaithAnalyticsGeneralConfiguretionInfo.NAME_OF_SDK + "_GameAnalytics";
            SetNameOfConfiguretion(sdkName);
#if UNITY_EDITOR
            _isSDKIntegrated = FaithAnalyticsScriptDefineSymbol.CheckGameAnalyticsIntegration(sdkName);
#endif
        }

        public override void Initialize(FaithAnalyticsGeneralConfiguretionInfo faithAnalyticsGeneralConfiguretionInfo, bool isATTEnable = false)
        {
#if FaithAnalytics_GameAnalytics
            if (FaithAnalyticsGameAnalyticsWrapper.Instance == null && IsAnalyticsEventEnabled)
            {
                Instantiate(Resources.Load("GameAnalytics/FA_GameAnalytics"));

                GameObject newfaithAnalyticsGameAnalyticsWrapper = new GameObject("FaithAnalyticsGameAnalyticsWrapper");
                FaithAnalyticsGameAnalyticsWrapper.Instance = newfaithAnalyticsGameAnalyticsWrapper.AddComponent<FaithAnalyticsGameAnalyticsWrapper>();

                DontDestroyOnLoad(newfaithAnalyticsGameAnalyticsWrapper);

                FaithAnalyticsGameAnalyticsWrapper.Instance.Initialize(faithAnalyticsGeneralConfiguretionInfo, this, isATTEnable);
            }
#endif
        }

        public override void PreCustomEditorGUI()
        {

#if UNITY_EDITOR && FaithAnalytics_GameAnalytics
            

            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("DefaultWorldIndexOnGameAnalytics", GUILayout.Width(FaithAnalyticsGeneralConfiguretionInfo.EDITOR_LABEL_WIDTH));
                    _defaultWorldIndex = EditorGUILayout.IntField(_defaultWorldIndex);
                }
                EditorGUILayout.EndHorizontal();


            }
            EditorGUILayout.EndVertical();

            FaithAnalyticsEditorModule.DrawHorizontalLine();

            if (IsAnalyticsEventEnabled)
            {
                if (_gaSettings == null)
                    _gaSettings = Resources.Load<GameAnalyticsSDK.Setup.Settings>("GameAnalytics/Settings");

                if (_gaSettings == null)
                    EditorGUILayout.HelpBox("You need to create GA_'Settings' by going to 'Window/Game Analytics/Select Settings' from menu in order to ga_sdk for working properly", MessageType.Error);
                else
                {
                    EditorGUILayout.HelpBox("If you haven't setup your game on GA, please do by loging, adding platform and selecting your games from down below. Make sure to put the right 'sdk key' and 'secret key' for your specefic platform", MessageType.Warning);
                    FaithAnalyticsEditorModule.DrawSettingsEditor(_gaSettings, null, ref _isShowingGASettings, ref _gaSettingsEditor);
                }

                FaithAnalyticsEditorModule.DrawHorizontalLine();
            }
#endif

        }

        public override void PostCustomEditorGUI()
        {

        }


        #endregion

    }
}


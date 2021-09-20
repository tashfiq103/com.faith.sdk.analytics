namespace com.faith.sdk.analytics
{
    using UnityEngine;
    using System.Collections.Generic;

#if FaithAnalytics_Adjust
    using com.adjust.sdk;
#endif

#if UNITY_EDITOR
    using UnityEditor;
#endif

    [CreateAssetMenu(fileName = "FaithAnalyticsAdjustConfiguretionInfo", menuName = FaithAnalyticsConstant.NameOfSDK + "/FaithAnalyticsAdjustConfiguretionInfo")]
    public class FaithAnalyticsAdjustConfiguretionInfo : FaithBaseClassForAnalyticsConfiguretionInfo
    {
        #region Public Variables

#if FaithAnalytics_Adjust

        public string appToken
        {
            get
            {
#if UNITY_ANDROID
                return _appTokenForAndroid;
#elif UNITY_IOS
                return _appTokenForIOS;
#else
                return "invalid_platform";
#endif
            }
        }

        public AdjustEnvironment Environment { get { return _environment; } }

        public AdjustLogLevel LogLevel { get { return _logLevel; } }
        public float StartDelay { get { return _startDelay; } }
        public bool StartManually { get { return _startManually; } }
        public bool EventBuffering { get { return _eventBuffering; } }
        public bool SendInBackground { get { return _sendInBackground; } }
        public bool LaunchDeferredDeeplink { get { return _launchDeferredDeeplink; } }

#endif

        #endregion

        #region Private Variables

#if FaithAnalytics_Adjust

#if UNITY_EDITOR

        [HideInInspector, SerializeField] private bool _showBasicInfo;
        [HideInInspector, SerializeField] private bool _showAdvancedInfo;

#endif

        [HideInInspector, SerializeField] private string _appTokenForAndroid;
        [HideInInspector, SerializeField] private string _appTokenForIOS;
        [HideInInspector, SerializeField] private AdjustEnvironment _environment = AdjustEnvironment.Sandbox;

        [HideInInspector, SerializeField] private AdjustLogLevel _logLevel = AdjustLogLevel.Suppress;
        [HideInInspector, SerializeField] private float _startDelay = 0;
        [HideInInspector, SerializeField] private bool _startManually = true;
        [HideInInspector, SerializeField] private bool _eventBuffering;
        [HideInInspector, SerializeField] private bool _sendInBackground;
        [HideInInspector, SerializeField] private bool _launchDeferredDeeplink = true;
#endif


        #endregion

        #region Override Method

        public override void Initialize(FaithAnalyticsGeneralConfiguretionInfo faithAnalyticsGeneralConfiguretionInfo, bool isATTEnable = false)
        {
#if FaithAnalytics_Adjust
            if (FaithAnalyticsAdjustWrapper.Instance == null)
            {

                GameObject newfaithAnalyticsAdjustWrapper = new GameObject("FaithAnalyticsAdjustWrapper");
                FaithAnalyticsAdjustWrapper.Instance = newfaithAnalyticsAdjustWrapper.AddComponent<FaithAnalyticsAdjustWrapper>();

                DontDestroyOnLoad(newfaithAnalyticsAdjustWrapper);

                FaithAnalyticsAdjustWrapper.Instance.Initialize(faithAnalyticsGeneralConfiguretionInfo, this);
            }
#endif
        }

        public override void PostCustomEditorGUI()
        {

        }

        public override void PreCustomEditorGUI()
        {
#if UNITY_EDITOR && FaithAnalytics_Adjust

            #region Settings    :   Basic

            EditorGUI.indentLevel += 2;
            {
                EditorGUILayout.BeginHorizontal(GUI.skin.box);
                {
                    string basicLabel = "[" + (!_showBasicInfo ? "+" : "-") + "] [Settings : Basic]";
                    GUIContent basicLabelContent = new GUIContent(
                            basicLabel

                        );
                    GUIStyle basicLabelStyle = new GUIStyle(EditorStyles.boldLabel);
                    basicLabelStyle.alignment = TextAnchor.MiddleLeft;
                    basicLabelStyle.padding.left = 28;

                    if (GUILayout.Button(basicLabelContent, basicLabelStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth)))
                    {
                        _showBasicInfo = !_showBasicInfo;
                    }
                }
                EditorGUILayout.EndHorizontal();

                if (_showBasicInfo)
                {

                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("AppToken : Android", GUILayout.Width(FaithAnalyticsConstant.EDITOR_LABEL_WIDTH));
                            _appTokenForAndroid = EditorGUILayout.TextField(_appTokenForAndroid);
                        }
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("AppToken : iOS", GUILayout.Width(FaithAnalyticsConstant.EDITOR_LABEL_WIDTH));
                            _appTokenForIOS = EditorGUILayout.TextField(_appTokenForIOS);
                        }
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("Environment", GUILayout.Width(FaithAnalyticsConstant.EDITOR_LABEL_WIDTH));
                            _environment = (AdjustEnvironment)EditorGUILayout.EnumPopup(_environment);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.EndVertical();

                }


            }
            EditorGUI.indentLevel -= 2;

            #endregion

            //-------------
            #region Settings    :   Advance

            EditorGUI.indentLevel += 2;
            {
                EditorGUILayout.BeginHorizontal(GUI.skin.box);
                {
                    string advanceLabel = "[" + (!_showAdvancedInfo ? "+" : "-") + "] [Settings : Advance]";
                    GUIContent advanceLabelContent = new GUIContent(
                            advanceLabel

                        );
                    GUIStyle advanceLabelStyle = new GUIStyle(EditorStyles.boldLabel);
                    advanceLabelStyle.alignment = TextAnchor.MiddleLeft;
                    advanceLabelStyle.padding.left = 28;

                    if (GUILayout.Button(advanceLabelContent, advanceLabelStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth)))
                    {
                        _showAdvancedInfo = !_showAdvancedInfo;
                    }
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginVertical(GUI.skin.box);
                {
                    if (_showAdvancedInfo)
                    {

                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("LogLevel", GUILayout.Width(FaithAnalyticsConstant.EDITOR_LABEL_WIDTH));
                            _logLevel = (AdjustLogLevel)EditorGUILayout.EnumPopup(_logLevel);
                        }
                        EditorGUILayout.EndHorizontal();



                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("StartDelay", GUILayout.Width(FaithAnalyticsConstant.EDITOR_LABEL_WIDTH));
                            _startDelay = EditorGUILayout.FloatField(_startDelay);
                        }
                        EditorGUILayout.EndHorizontal();



                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("StartManually", GUILayout.Width(FaithAnalyticsConstant.EDITOR_LABEL_WIDTH));
                            _startManually = EditorGUILayout.Toggle(_startManually);
                        }
                        EditorGUILayout.EndHorizontal();


                        EditorGUILayout.Space();
                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("EventBuffering", GUILayout.Width(FaithAnalyticsConstant.EDITOR_LABEL_WIDTH));
                            _eventBuffering = EditorGUILayout.Toggle(_eventBuffering);
                        }
                        EditorGUILayout.EndHorizontal();



                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("SendInBackground", GUILayout.Width(FaithAnalyticsConstant.EDITOR_LABEL_WIDTH));
                            _sendInBackground = EditorGUILayout.Toggle(_sendInBackground);
                        }
                        EditorGUILayout.EndHorizontal();



                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("LaunchDeferredDeeplink", GUILayout.Width(FaithAnalyticsConstant.EDITOR_LABEL_WIDTH));
                            _launchDeferredDeeplink = EditorGUILayout.Toggle(_launchDeferredDeeplink);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUI.indentLevel -= 2;


            #endregion

            FaithAnalyticsEditorModule.DrawHorizontalLine();
#endif
        }

        public override void SetNameAndIntegrationStatus()
        {
            string sdkName = FaithAnalyticsConstant.NameOfSDK + "_Adjust";
            SetNameOfConfiguretion(sdkName);
#if UNITY_EDITOR
            _isSDKIntegrated = FaithAnalyticsScriptDefineSymbol.CheckAdjustIntegration(sdkName);
#endif
        }

#endregion

    }
}


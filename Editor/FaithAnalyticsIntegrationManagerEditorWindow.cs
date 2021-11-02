namespace com.faith.sdk.analytics
{
#if UNITY_EDITOR
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    public class FaithAnalyticsIntegrationManagerEditorWindow : EditorWindow
    {
        #region Private Variables

        private static EditorWindow _reference;


        private bool _IsInformationFetched = false;
        private Vector2 _scrollPosition;

        private GUIStyle _settingsTitleStyle;
        private GUIStyle _hyperlinkStyle;

        private List<FaithBaseClassForAnalyticsConfiguretionInfo> _listOfAnalyticsConfiguretion;

        private const string _linkForDownload = "https://github.com/tashfiq103/com.faith.sdk.analytics";
        private const string _linkForDocumetation = "https://github.com/tashfiq103/com.faith.sdk.analytics/blob/main/README.md";

        #endregion

        #region Private Variables   :   FaithAnalyticsConfiguretionInfo

        private FaithAnalyticsGeneralConfiguretionInfo  _faithAnalyticsGeneralConfiguretionInfo;
        private SerializedObject                        _serializedFaithAnalyticsGeneralConfiguretionInfo;

        private GUIContent _generalSettingContent;
        private GUIContent _analyticsSettingContent;
        private GUIContent _debuggingSettingContent;

        private SerializedProperty _autoInitialize;

        private SerializedProperty _showGeneralSettings;
        private SerializedProperty _showAnalytics;
        private SerializedProperty _showDebuggingSettings;

        private SerializedProperty _showAnalyticsLogInConsole;

        private SerializedProperty _infoLogColor;
        private SerializedProperty _warningLogColor;
        private SerializedProperty _errorLogColor;

        #endregion

        #region Editor

        [MenuItem("FAITH/FaithAnalytics Integration Manager")]
        public static void Create()
        {
            if (_reference == null)
            {
                _reference = GetWindow<FaithAnalyticsIntegrationManagerEditorWindow>("FaithAnalytics Integration Manager", typeof(FaithAnalyticsIntegrationManagerEditorWindow));
                _reference.minSize = new Vector2(340, 240);
            }
            else
            {
                _reference.Show();
            }
            _reference.Focus();
        }

        private void OnEnable()
        {
            FetchAllTheReference();

        }

        private void OnDisable()
        {
            _IsInformationFetched = false;
        }

        private void OnFocus()
        {
            FetchAllTheReference();
        }

        private void OnLostFocus()
        {
            _IsInformationFetched = false;
        }

        private void OnGUI()
        {

            if (!_IsInformationFetched)
            {
                FetchAllTheReference();
                _IsInformationFetched = true;
            }

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, false, false);
            {
                EditorGUILayout.Space();

                EditorGUI.indentLevel += 1;
                {
                    GeneralSettingGUI();

                    EditorGUILayout.Space();
                    AnalyticsSettingsGUI();

                    EditorGUILayout.Space();
                    DebuggingSettingsGUI();
                }
                EditorGUI.indentLevel -= 1;
            }
            EditorGUILayout.EndScrollView();
        }

        #endregion

        #region CustomGUI

        private void DrawHeaderGUI(string title, ref GUIContent gUIContent, ref GUIStyle gUIStyle, ref SerializedProperty serializedProperty)
        {

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                if (GUILayout.Button(gUIContent, gUIStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth)))
                {
                    serializedProperty.boolValue = !serializedProperty.boolValue;
                    serializedProperty.serializedObject.ApplyModifiedProperties();

                    gUIContent = new GUIContent(
                        "[" + (!serializedProperty.boolValue ? "+" : "-") + "] " + title
                    );
                }
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawAnalyticsGUI(FaithBaseClassForAnalyticsConfiguretionInfo analyticsConfiguretion)
        {

            //Referencing Variables
            SerializedObject serailizedAnalyticsConfiguretion = new SerializedObject(analyticsConfiguretion);

            SerializedProperty _nameOfConfiguretion = serailizedAnalyticsConfiguretion.FindProperty("_nameOfConfiguretion");
            SerializedProperty _isSDKIntegrated = serailizedAnalyticsConfiguretion.FindProperty("_isSDKIntegrated");

            SerializedProperty _showSettings = serailizedAnalyticsConfiguretion.FindProperty("_showSettings");

            SerializedProperty _enableAnalyticsEvent = serailizedAnalyticsConfiguretion.FindProperty("_enableAnalyticsEvent");

            SerializedProperty _trackProgressionEvent = serailizedAnalyticsConfiguretion.FindProperty("_trackProgressionEvent");
            SerializedProperty _trackAdEvent = serailizedAnalyticsConfiguretion.FindProperty("_trackAdEvent");


            //Setting Titles
            GUIContent titleContent = new GUIContent(
                    "[" + (!_showSettings.boolValue ? "+" : "-") + "] " + (_nameOfConfiguretion.stringValue + (_isSDKIntegrated.boolValue ? "" : " - (SDK Not Found)"))
                );
            GUIStyle titleStyle = new GUIStyle(EditorStyles.boldLabel);
            titleStyle.alignment = TextAnchor.MiddleLeft;
            titleStyle.padding.left = 18;

            EditorGUI.BeginDisabledGroup(!_isSDKIntegrated.boolValue);
            {
                EditorGUILayout.BeginHorizontal(GUI.skin.box);
                {
                    if (GUILayout.Button(titleContent, titleStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth - 100f)))
                    {
                        _showSettings.boolValue = !_showSettings.boolValue;
                        _showSettings.serializedObject.ApplyModifiedProperties();

                        titleContent = new GUIContent(
                            "[" + (!_showSettings.boolValue ? "+" : "-") + "] " + (_nameOfConfiguretion.stringValue + (_isSDKIntegrated.boolValue ? "" : " - (SDK Not Found)"))
                        );
                    }

                    if (GUILayout.Button(_enableAnalyticsEvent.boolValue ? "Disable" : "Enable", GUILayout.Width(80)))
                    {
                        _enableAnalyticsEvent.boolValue = !_enableAnalyticsEvent.boolValue;
                        _enableAnalyticsEvent.serializedObject.ApplyModifiedProperties();
                    }

                    GUILayout.FlexibleSpace();
                }
                EditorGUILayout.EndHorizontal();


                //Showing Settings
                if (_showSettings.boolValue)
                {

                    EditorGUI.BeginDisabledGroup(!_enableAnalyticsEvent.boolValue);
                    {
                        EditorGUI.indentLevel += 1;
                        {
                            analyticsConfiguretion.PreCustomEditorGUI();

                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_trackProgressionEvent.displayName, GUILayout.Width(FaithAnalyticsGeneralConfiguretionInfo.EDITOR_LABEL_WIDTH));
                                EditorGUI.BeginChangeCheck();
                                _trackProgressionEvent.boolValue = EditorGUILayout.Toggle(_trackProgressionEvent.boolValue);
                                if (EditorGUI.EndChangeCheck())
                                    _trackProgressionEvent.serializedObject.ApplyModifiedProperties();
                            }
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField(_trackAdEvent.displayName, GUILayout.Width(FaithAnalyticsGeneralConfiguretionInfo.EDITOR_LABEL_WIDTH));
                                EditorGUI.BeginChangeCheck();
                                _trackAdEvent.boolValue = EditorGUILayout.Toggle(_trackAdEvent.boolValue);
                                if (EditorGUI.EndChangeCheck())
                                    _trackAdEvent.serializedObject.ApplyModifiedProperties();
                            }
                            EditorGUILayout.EndHorizontal();

                            analyticsConfiguretion.PostCustomEditorGUI();
                        }
                        EditorGUI.indentLevel -= 1;

                    }
                    EditorGUI.EndDisabledGroup();

                }

            }
            EditorGUI.EndDisabledGroup();

        }

        private void GeneralSettingGUI()
        {
            DrawHeaderGUI("General", ref _generalSettingContent, ref _settingsTitleStyle, ref _showGeneralSettings);

            if (_showGeneralSettings.boolValue)
            {

                EditorGUI.indentLevel += 1;
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField("Reference/Link", GUILayout.Width(FaithAnalyticsGeneralConfiguretionInfo.EDITOR_LABEL_WIDTH + 30));
                        if (GUILayout.Button("Download", _hyperlinkStyle, GUILayout.Width(100)))
                        {
                            Application.OpenURL(_linkForDownload);
                        }
                        if (GUILayout.Button("Documentation", _hyperlinkStyle, GUILayout.Width(100)))
                        {
                            Application.OpenURL(_linkForDocumetation);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    //-----------
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(_autoInitialize.displayName, GUILayout.Width(FaithAnalyticsGeneralConfiguretionInfo.EDITOR_LABEL_WIDTH));
                        EditorGUI.BeginChangeCheck();
                        _autoInitialize.boolValue = EditorGUILayout.Toggle(_autoInitialize.boolValue);
                        if (EditorGUI.EndChangeCheck())
                        {
                            _autoInitialize.serializedObject.ApplyModifiedProperties();
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    if (!_autoInitialize.boolValue) {

                        FaithAnalyticsEditorModule.DrawHorizontalLine();
                        EditorGUILayout.HelpBox(
                            "For automatic initialization at start for all the analytics, you need to set the value = 'true'. If you want to manually initialize the sdk, make sure to call 'FaithAnalyticsManager.Initialize()' when the value = 'false'",
                            MessageType.Warning);
                    }
                }
                EditorGUI.indentLevel -= 1;
            }
        }


        private void AnalyticsSettingsGUI()
        {

            DrawHeaderGUI("Analytics", ref _analyticsSettingContent, ref _settingsTitleStyle, ref _showAnalytics);

            if (_showAnalytics.boolValue)
            {
                
                foreach (FaithBaseClassForAnalyticsConfiguretionInfo analyticsConfiguretion in _listOfAnalyticsConfiguretion)
                {
                    if (analyticsConfiguretion != null)
                        DrawAnalyticsGUI(analyticsConfiguretion);
                }
            }
        }

        private void DebuggingSettingsGUI()
        {

            DrawHeaderGUI("Debugging", ref _debuggingSettingContent, ref _settingsTitleStyle, ref _showDebuggingSettings);

            if (_showDebuggingSettings.boolValue)
            {
                EditorGUI.indentLevel += 1;

                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(_showAnalyticsLogInConsole.displayName, GUILayout.Width(FaithAnalyticsGeneralConfiguretionInfo.EDITOR_LABEL_WIDTH));
                        EditorGUI.BeginChangeCheck();
                        _showAnalyticsLogInConsole.boolValue = EditorGUILayout.Toggle(_showAnalyticsLogInConsole.boolValue);
                        if (EditorGUI.EndChangeCheck())
                        {
                            _showAnalyticsLogInConsole.serializedObject.ApplyModifiedProperties();
                        }
                    }
                    EditorGUILayout.EndHorizontal();


                    EditorGUILayout.BeginHorizontal(GUI.skin.box);
                    {
                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.PropertyField(_infoLogColor);
                        if (EditorGUI.EndChangeCheck())
                        {

                            _infoLogColor.serializedObject.ApplyModifiedProperties();
                        }

                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.PropertyField(_warningLogColor);
                        if (EditorGUI.EndChangeCheck())
                        {
                            _warningLogColor.serializedObject.ApplyModifiedProperties();
                        }

                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.PropertyField(_errorLogColor);
                        if (EditorGUI.EndChangeCheck())
                        {
                            _errorLogColor.serializedObject.ApplyModifiedProperties();
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();

                EditorGUI.indentLevel -= 1;
            }
        }

        #endregion

        #region Configuretion

        private void FetchAllTheReference() {

            _faithAnalyticsGeneralConfiguretionInfo             = Resources.Load<FaithAnalyticsGeneralConfiguretionInfo>("FaithAnalyticsGeneralConfiguretionInfo");
            _serializedFaithAnalyticsGeneralConfiguretionInfo     = new SerializedObject(_faithAnalyticsGeneralConfiguretionInfo);

            _autoInitialize                     = _serializedFaithAnalyticsGeneralConfiguretionInfo.FindProperty("_autoInitialize");


            _showGeneralSettings                = _serializedFaithAnalyticsGeneralConfiguretionInfo.FindProperty("_showGeneralSetting");
            _showAnalytics                      = _serializedFaithAnalyticsGeneralConfiguretionInfo.FindProperty("_showAnalytics");
            _showDebuggingSettings              = _serializedFaithAnalyticsGeneralConfiguretionInfo.FindProperty("_showDebuggingSetting");


            _generalSettingContent = new GUIContent(
                        "[" + (!_showGeneralSettings.boolValue ? "+" : "-") + "] General"
                    );
            _analyticsSettingContent = new GUIContent(
                       "[" + (!_showAnalytics.boolValue ? "+" : "-") + "] " + "Analytics"
                   );

            _debuggingSettingContent = new GUIContent(
                        "[" + (!_showDebuggingSettings.boolValue ? "+" : "-") + "] Debugging"
                    );

            _settingsTitleStyle = new GUIStyle();
            _settingsTitleStyle.normal.textColor = Color.white;
            _settingsTitleStyle.fontStyle = FontStyle.Bold;
            _settingsTitleStyle.alignment = TextAnchor.MiddleLeft;

            _hyperlinkStyle = new GUIStyle();
            _hyperlinkStyle.normal.textColor = new Color(50 / 255.0f, 139 / 255.0f, 217 / 255.0f);
            _hyperlinkStyle.fontStyle = FontStyle.BoldAndItalic;
            _hyperlinkStyle.wordWrap = true;
            _hyperlinkStyle.richText = true;

            _showAnalyticsLogInConsole = _serializedFaithAnalyticsGeneralConfiguretionInfo.FindProperty("_showAnalyticsLogInConsole");

            _infoLogColor = _serializedFaithAnalyticsGeneralConfiguretionInfo.FindProperty("_infoLogColor");
            _warningLogColor = _serializedFaithAnalyticsGeneralConfiguretionInfo.FindProperty("_warningLogColor");
            _errorLogColor = _serializedFaithAnalyticsGeneralConfiguretionInfo.FindProperty("_errorLogColor");

            _listOfAnalyticsConfiguretion = new List<FaithBaseClassForAnalyticsConfiguretionInfo>();
            Object[] analyticsConfiguretionObjects = Resources.LoadAll("", typeof(FaithBaseClassForAnalyticsConfiguretionInfo));
            foreach (Object analyticsConfiguretionObject in analyticsConfiguretionObjects) {

                FaithBaseClassForAnalyticsConfiguretionInfo faithAnalyticsConfiguretion = (FaithBaseClassForAnalyticsConfiguretionInfo)analyticsConfiguretionObject;
                if (faithAnalyticsConfiguretion != null)
                    _listOfAnalyticsConfiguretion.Add(faithAnalyticsConfiguretion);
            }

            FaithAnalyticsAssetPostProcessor.LookForSDK();
        }

        #endregion
    }
#endif
}


namespace com.faith.sdk.analytics
{
    using UnityEngine;
    using System.Collections.Generic;

#if UNITY_EDITOR
    using UnityEditor;
#endif

    [CreateAssetMenu(fileName = "FaithFacebookConfiguretionInfo", menuName = FaithAnalyticsGeneralConfiguretionInfo.NAME_OF_SDK + "/FaithFacebookConfiguretionInfo")]
    public class FaithAnalyticsFacebookConfiguretionInfo : FaithBaseClassForAnalyticsConfiguretionInfo
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
            string sdkName = FaithAnalyticsGeneralConfiguretionInfo.NAME_OF_SDK + "_Facebook";
            SetNameOfConfiguretion(sdkName);
#if UNITY_EDITOR
            _isSDKIntegrated = FaithAnalyticsScriptDefineSymbol.CheckFacebookIntegration(sdkName);
#endif
        }

        public override void Initialize(FaithAnalyticsGeneralConfiguretionInfo faithAnalyticsGeneralConfiguretionInfo, bool isATTEnable = false)
        {

#if FaithAnalytics_Facebook
            GameObject newFaithAnalyticsFacebookWrapper = new GameObject("FaithAnalyticsFacebookWrapper");
            FaithAnalyticsFacebookWrapper.Instance = newFaithAnalyticsFacebookWrapper.AddComponent<FaithAnalyticsFacebookWrapper>();
            FaithAnalyticsFacebookWrapper.Instance.Initialize(faithAnalyticsGeneralConfiguretionInfo, this, isATTEnable);

            DontDestroyOnLoad(newFaithAnalyticsFacebookWrapper);
#endif
        }

        public override void PostCustomEditorGUI()
        {
            
        }

        public override void PreCustomEditorGUI()
        {
#if UNITY_EDITOR && FaithAnalytics_Facebook

            if (_facebookSettings == null)
                _facebookSettings = Resources.Load<Facebook.Unity.Settings.FacebookSettings>("FacebookSettings");

            if (_facebookSettings == null)
            {
                EditorGUILayout.HelpBox(string.Format("You need to create 'FacebookSettings' by going to 'Facebook/Edit Settings' from menu in order to facebook sdk for working properly"), MessageType.Error);
            }
            else
            {

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("appName", GUILayout.Width(FaithSdkConstant.EDITOR_LABEL_WIDTH));
                    EditorGUI.BeginChangeCheck();
                    _facebookAppName = EditorGUILayout.TextField(_facebookAppName);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Facebook.Unity.Settings.FacebookSettings.AppLabels = new List<string>() { _facebookAppName };
                    }

                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("appId", GUILayout.Width(FaithSdkConstant.EDITOR_LABEL_WIDTH));
                    EditorGUI.BeginChangeCheck();
                    _facebookAppId = EditorGUILayout.TextField(_facebookAppId);
                    if (EditorGUI.EndChangeCheck())
                    {

                        Facebook.Unity.Settings.FacebookSettings.AppIds = new List<string>() { _facebookAppId };
                    }
                }
                EditorGUILayout.EndHorizontal();
            }



            FaithSdkEditorModule.DrawHorizontalLine();
#endif
        }

#endregion

    }
}


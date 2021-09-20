namespace com.faith.sdk.analytics
{
#if UNITY_EDITOR

    using UnityEngine;
    using UnityEditor;
    using System.Collections.Generic;
    using System.Linq;
    public class FaithAnalyticsEditorModule : Editor
    {
    #region EditorModule    :   CustomClass

        public class ReorderableList
        {

    #region Private Variables

            private UnityEditorInternal.ReorderableList _reorderableList;
            private SerializedObject _serializedObject;
            private SerializedProperty _sourceProperty;
            private bool _isFoldout = false;
            private int _popUpValue = 0;
            private string[] _popupOptions = new string[] { "Generic", "Reorderable" };
    #endregion

    #region Configuretion

            private void SaveModifiedProperties()
            {
                _serializedObject.ApplyModifiedProperties();
                _sourceProperty.serializedObject.ApplyModifiedProperties();
            }



    #endregion

    #region Public Callback

            public ReorderableList(SerializedObject serializedObject, SerializedProperty sourceProperty, bool drawLineSeperator = false)
            {
                _serializedObject = serializedObject;
                _sourceProperty = sourceProperty;
                float singleLineHeight = EditorGUIUtility.singleLineHeight;

                _reorderableList = new UnityEditorInternal.ReorderableList(_serializedObject, _sourceProperty)
                {

                    displayAdd = true,
                    displayRemove = true,
                    draggable = true,
                    drawHeaderCallback = rect =>
                    {
                        _isFoldout = EditorGUI.Foldout(
                                new Rect(rect.x, rect.y, rect.width - 125, singleLineHeight),
                                _isFoldout,
                                _sourceProperty.displayName,
                                true
                            );

                        _popUpValue = EditorGUI.Popup(
                            new Rect(rect.x + rect.width - 125, rect.y, 125, singleLineHeight),
                            _popUpValue,
                            _popupOptions);
                    },
                    drawElementCallback = (rect, index, isActive, isFocused) => {

                        SerializedProperty element = _sourceProperty.GetArrayElementAtIndex(index);
                        float heightOfElement = EditorGUI.GetPropertyHeight(element);

                        if (_isFoldout)
                        {

                            EditorGUI.PropertyField(
                                new Rect(rect.x, rect.y, rect.width, heightOfElement),
                                element,
                                true
                            );

                            if (drawLineSeperator)
                                EditorGUI.LabelField(new Rect(rect.x, rect.y + heightOfElement, rect.width, singleLineHeight), "", GUI.skin.horizontalSlider);
                        }



                    },
                    elementHeightCallback = index => {
                        return _isFoldout ? EditorGUI.GetPropertyHeight(_sourceProperty.GetArrayElementAtIndex(index)) + (drawLineSeperator ? singleLineHeight : 0) : 0;
                    }
                };
            }

            public void DoLayoutList()
            {
                EditorGUI.BeginChangeCheck();
                if (_popUpValue == 0)
                {

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PropertyField(_sourceProperty);
                        if (!_sourceProperty.isExpanded)
                        {
                            _popUpValue = EditorGUILayout.Popup(
                                _popUpValue,
                                _popupOptions,
                                GUILayout.Width(125)
                            );
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                else
                {

                    _reorderableList.DoLayoutList();
                }
                if (EditorGUI.EndChangeCheck())
                {

                    SaveModifiedProperties();
                }

            }

    #endregion

        }

    #endregion

    #region Editor Module   :   GUI

        public static void ShowScriptReference(SerializedObject serializedObject)
        {

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));
            EditorGUI.EndDisabledGroup();
        }

        public static void DrawHorizontalLine()
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }

        public static void DrawHorizontalLineOnGUI(Rect rect)
        {
            EditorGUI.LabelField(rect, "", GUI.skin.horizontalSlider);
        }

        public static void DrawSettingsEditor(Object settings, System.Action OnSettingsUpdated, ref bool foldout, ref Editor editor)
        {

            if (settings != null)
            {

                using (var check = new EditorGUI.ChangeCheckScope())
                {

                    foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);

                    if (foldout)
                    {

                        CreateCachedEditor(settings, null, ref editor);
                        editor.OnInspectorGUI();

                        if (check.changed)
                        {

                            if (OnSettingsUpdated != null)
                            {

                                OnSettingsUpdated.Invoke();
                            }
                        }
                    }
                }
            }

        }

    #endregion

    #region Editor Module   :   Asset

        public static List<T> GetAsset<T>(bool returnIfGetAny = false, params string[] directoryFilters)
        {

            return GetAsset<T>("t:" + typeof(T).ToString().Replace("UnityEngine.", ""), returnIfGetAny, directoryFilters);
        }

        public static List<T> GetAsset<T>(string nameFilter, bool returnIfGetAny = false, params string[] directoryFilters)
        {

            List<T> listOfAsset = new List<T>();
            string[] GUIDs;
            if (directoryFilters == null) GUIDs = AssetDatabase.FindAssets(nameFilter);
            else GUIDs = AssetDatabase.FindAssets(nameFilter, directoryFilters);

            foreach (string GUID in GUIDs)
            {

                string assetPath = AssetDatabase.GUIDToAssetPath(GUID);
                listOfAsset.Add((T)System.Convert.ChangeType(AssetDatabase.LoadAssetAtPath(assetPath, typeof(T)), typeof(T)));
                if (returnIfGetAny)
                    break;
            }

            return listOfAsset;
        }

    #endregion

    #region Editor Module   :   Scene

        public static string GetSceneNameFromPath(string scenePath)
        {

            string[] splitedByDash = scenePath.Split('/');
            string[] splitedByDot = splitedByDash[splitedByDash.Length - 1].Split('.');
            return splitedByDot[0];
        }

        public static bool IsSceneAlreadyInBuild(string scenePath)
        {
            int numberOfSceneInBuild = EditorBuildSettings.scenes.Length;
            for (int i = 0; i < numberOfSceneInBuild; i++)
            {
                if (EditorBuildSettings.scenes[i].path == scenePath)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsSceneEnabled(string scenePath)
        {


            if (IsSceneAlreadyInBuild(scenePath))
            {

                EditorBuildSettingsScene[] edtiorBuildSettingsScene = EditorBuildSettings.scenes;
                foreach (EditorBuildSettingsScene buildScene in edtiorBuildSettingsScene)
                {
                    if (buildScene.path == scenePath && buildScene.enabled)
                        return true;
                }
            }

            return false;
        }

        public static void EnableAndDisableScene(string scenePath, bool value)
        {

            if (IsSceneAlreadyInBuild(scenePath))
            {

                EditorBuildSettingsScene[] editorBuildSettingsScene = EditorBuildSettings.scenes;
                foreach (EditorBuildSettingsScene buildScene in editorBuildSettingsScene)
                {
                    if (buildScene.path == scenePath)
                    {

                        buildScene.enabled = value;
                        break;
                    }
                }
                EditorBuildSettings.scenes = editorBuildSettingsScene;

            }
        }

        public static void AddSceneToBuild(string scenePath, bool isEnabled = true)
        {

            EditorBuildSettingsScene newBuildScene = new EditorBuildSettingsScene(scenePath, isEnabled);
            List<EditorBuildSettingsScene> tempBuildSettingsScene = EditorBuildSettings.scenes.ToList();
            tempBuildSettingsScene.Add(newBuildScene);
            EditorBuildSettings.scenes = tempBuildSettingsScene.ToArray();
        }

        public static void RemoveSceneFromBuild(string t_ScenePath)
        {
            List<EditorBuildSettingsScene> tempBuildSettingsScene = EditorBuildSettings.scenes.ToList();
            int numberOfCurrentSceneInTheBuild = tempBuildSettingsScene.Count;
            for (int i = 0; i < numberOfCurrentSceneInTheBuild; i++)
            {
                if (tempBuildSettingsScene[i].path == t_ScenePath)
                {
                    tempBuildSettingsScene.RemoveAt(i);
                    tempBuildSettingsScene.TrimExcess();
                    break;
                }
            }
            EditorBuildSettings.scenes = tempBuildSettingsScene.ToArray();
        }

    #endregion

    #region Editor Module   :   UnityTechnology

        public static bool DropDownToggle(ref bool toggled, GUIContent content, GUIStyle toggleButtonStyle)
        {
            Rect toggleRect = GUILayoutUtility.GetRect(content, toggleButtonStyle);
            Rect arrowRightRect = new Rect(toggleRect.xMax - toggleButtonStyle.padding.right, toggleRect.y, toggleButtonStyle.padding.right, toggleRect.height);
            bool clicked = EditorGUI.DropdownButton(arrowRightRect, GUIContent.none, FocusType.Passive, GUIStyle.none);

            if (!clicked)
            {
                toggled = GUI.Toggle(toggleRect, toggled, content, toggleButtonStyle);
            }

            return clicked;
        }

        //Extended  :   Tashfiq


    #endregion
    }

#endif

}



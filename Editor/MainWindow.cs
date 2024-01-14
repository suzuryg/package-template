using System;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Suzuryg.PackageName
{
    internal class MainWindow : EditorWindow
    {
        private static readonly float Margin = 5;
        private static readonly float LabelWidth = 180;

        private LocalizationSetting _loc;
        private GUIStyle _radioButtonStyle;

        private void OnEnable()
        {
            _loc = new LocalizationSetting();

            titleContent = new GUIContent(Constants.SystemName);
            minSize = new Vector2(500, 300);
        }

        private void OnGUI()
        {
            GetStyles();

            // locale
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();
                GUILayout.Label("言語設定 (Language Setting)");
                var locale = (LocalizationSetting.Locale)EditorGUILayout.EnumPopup(string.Empty, _loc.CurrentLocale, GUILayout.Width(65));
                if (locale != _loc.CurrentLocale)
                {
                    _loc.CurrentLocale = locale;
                }
                GUILayout.Space(10);
            }

            GUILayout.Space(Margin);

            GUILayout.FlexibleSpace();

            // execute button
            if (GUILayout.Button(_loc.Table.Execute, GUILayout.Height(30)))
            {
                var check = CheckArguments();
                if (check.canExecute)
                {
                    if (EditorUtility.DisplayDialog(Constants.SystemName, _loc.Table.Confirm, _loc.Table.Execute, _loc.Table.Cancel))
                    {
                        Execute();
                    }
                }
                else
                {
                    EditorUtility.DisplayDialog(Constants.SystemName, check.message, "OK");
                }
            }

            GUILayout.Space(15);
        }

        private void GetStyles()
        {
            if (_radioButtonStyle == null)
            {
                try
                {
                    _radioButtonStyle = new GUIStyle(EditorStyles.radioButton);
                }
                catch (NullReferenceException)
                {
                    _radioButtonStyle = new GUIStyle();
                }
                var padding = _radioButtonStyle.padding;
                _radioButtonStyle.padding = new RectOffset(padding.left + 3, padding.right, padding.top, padding.bottom);
            }
        }

        private GameObject ObjectField(GameObject exising, string label)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Label(label, GUILayout.Width(LabelWidth));
                var newValue = EditorGUILayout.ObjectField(string.Empty, exising, typeof(GameObject), allowSceneObjects: true) as GameObject;
                if (newValue != null)
                {
                    if (newValue.scene.IsValid())
                    {
                        return newValue;
                    }
                    else
                    {
                        EditorUtility.DisplayDialog(Constants.SystemName, _loc.Table.PleaseSelectSceneObject, "OK");
                        return exising;
                    }
                }
                else
                {
                    return newValue;
                }
            }
        }

        private (bool canExecute, string message) CheckArguments()
        {
            return (true, string.Empty);
        }

        private void Execute()
        {
            try
            {
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        [MenuItem("Tools/PackageName")]
        private static void Open() => GetWindow<MainWindow>();
    }
}

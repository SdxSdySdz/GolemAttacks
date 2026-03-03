#if UNITY_EDITOR
using System;
using System.Reflection;
using CodeBase.Core.Attributes;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
    [InitializeOnLoad]
    public static class SerializedFieldGuard
    {
        static SerializedFieldGuard()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state != PlayModeStateChange.ExitingEditMode)
                return;

            if (TryFindMissing(out var missing))
            {
                EditorApplication.isPlaying = false;
                string msg = "Найдены незаполненные [SerializeField]-поля без [CanBeNull] в CodeBase пространстве имён:\n" + missing;
                EditorUtility.DisplayDialog("Ошибка инициализации", msg, "OK");
            }
        }

        public static bool TryFindMissing(out string report)
        {
            report = string.Empty;
            var scene = EditorSceneManager.GetActiveScene();
            foreach (var root in scene.GetRootGameObjects())
            {
                foreach (var mb in root.GetComponentsInChildren<MonoBehaviour>(true))
                {
                    var type = mb.GetType();
                    if (type.Namespace == null || !type.Namespace.StartsWith("CodeBase"))
                        continue;

                    foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        if (!Attribute.IsDefined(field, typeof(SerializeField))) continue;
                        if (Attribute.IsDefined(field, typeof(CanBeNullAttribute))) continue;
                        if (!typeof(UnityEngine.Object).IsAssignableFrom(field.FieldType)) continue;
                        if (field.GetValue(mb) == null)
                        {
                            report += $"{mb.gameObject.name}.{type.Name}.{field.Name}\n";
                        }
                    }
                }
            }
            return !string.IsNullOrEmpty(report);
        }
    }
}
#endif
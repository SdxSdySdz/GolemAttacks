#if UNITY_EDITOR
using System.Reflection;
using CodeBase.Core.Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [InitializeOnLoad]
    public static class HierarchyHighlighter
    {
        private const float BaseAlpha = 0.5f;

        static HierarchyHighlighter()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
            EditorApplication.update += RepaintHierarchyWindow;
        }

        private static void RepaintHierarchyWindow()
        {
            EditorApplication.RepaintHierarchyWindow();
        }

        private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (obj == null) return;

            int? distance = GetMissingDistance(obj);
            if (distance.HasValue)
            {
                float alpha = BaseAlpha / (distance.Value + 1);
                EditorGUI.DrawRect(selectionRect, new Color(1f, 0.92f, 0.5f, alpha));
            }
        }

        private static int? GetMissingDistance(GameObject go)
        {
            if (HasMissingInSelf(go))
                return 0;

            int min = int.MaxValue;
            foreach (Transform child in go.transform)
            {
                int? childDist = GetMissingDistance(child.gameObject);
                if (childDist.HasValue)
                    min = Mathf.Min(min, childDist.Value + 1);
            }
            return min == int.MaxValue ? (int?)null : min;
        }

        private static bool HasMissingInSelf(GameObject go)
        {
            foreach (var mb in go.GetComponents<MonoBehaviour>())
            {
                var type = mb.GetType();
                if (type.Namespace == null || !type.Namespace.StartsWith("CodeBase")) continue;
            
                foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    if (!field.IsDefined(typeof(SerializeField), false)) continue;
                    if (field.IsDefined(typeof(CanBeNullAttribute), false)) continue;
                    if (!typeof(Object).IsAssignableFrom(field.FieldType)) continue;
                    if (field.GetValue(mb) == null)
                        return true;
                }
            }
            return false;
        }
    }
}
#endif
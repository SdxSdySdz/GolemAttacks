using UnityEngine;
using UnityEditor;

public class DeleteObjectByNameEditor : EditorWindow
{
    private string objectNameToDelete = "LB Player Data";

    [MenuItem("Tools/Удалить объект по имени")]
    public static void ShowWindow()
    {
        GetWindow<DeleteObjectByNameEditor>("Удаление объекта");
    }

    void OnGUI()
    {
        GUILayout.Label("Удалить объект со сцены по имени", EditorStyles.boldLabel);
        objectNameToDelete = EditorGUILayout.TextField("Имя объекта", objectNameToDelete);

        if (GUILayout.Button("Удалить"))
        {
            DeleteObjectByName(objectNameToDelete);
        }
    }

    void DeleteObjectByName(string name)
    {
        GameObject obj = GameObject.Find(name);
        if (obj != null)
        {
            Undo.DestroyObjectImmediate(obj);  // позволяет откатить действие через Ctrl+Z
            Debug.Log($"Объект \"{name}\" удалён.");
        }
        else
        {
            Debug.LogWarning($"Объект \"{name}\" не найден.");
        }
    }
}
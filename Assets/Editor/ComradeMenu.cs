using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CodeBase.Core.Constants;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    public class ComradeMenu
    {
        [MenuItem("Comrade/Provide Main")]
        public static void ProvideMain()
        {
            GameObject mainProviderPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Paths.MainProviderPrefabPath);
            if (mainProviderPrefab == null)
            {
                Debug.LogError("Префаб 'MainProvider' не найден по указанному пути.");
                return;
            }

            GameObject mainProviderInstance = PrefabUtility.InstantiatePrefab(mainProviderPrefab) as GameObject;
            mainProviderInstance.name = "MainProvider";

            GameObject emptyObject = new GameObject("=================");
            
            emptyObject.transform.SetAsFirstSibling();
            mainProviderInstance.transform.SetAsFirstSibling();

            Debug.Log("Объект 'MainProvider' и '==============' созданы на сцене.");
        }

        [MenuItem("Comrade/Update Scenes")]
        public static void UpdateScenes()
        {
            string[] scenePaths = Directory.GetFiles(
                Application.dataPath + "/Scenes/", "*.unity", 
                SearchOption.AllDirectories
                );
            List<string> existingSceneNames = scenePaths
                .Select(path => Path.GetFileNameWithoutExtension(path))
                .ToList();
        
            Assembly assembly = Assembly.Load("Assembly-CSharp");
            Type baseType = typeof(CodeBase.Core.Infrastructure.Scenes.Scene);
            IEnumerable<Type> derivedTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(baseType));

            foreach (var type in derivedTypes)
            {
                string sceneName = type.Name.Replace("Scene", "");

                if (existingSceneNames.Contains(sceneName) == false)
                    CreateNewScene(sceneName);
            }

            Debug.Log("Обновление сцен завершено.");
        }

        [MenuItem("Comrade/Set Execution Order")]
        public static void SetExecutionOrder()
        {
            MonoScript mainScript = AssetDatabase.LoadAssetAtPath<MonoScript>(Paths.MainPath);
            MonoScript mainProviderScript = AssetDatabase.LoadAssetAtPath<MonoScript>(Paths.MainProviderPath); 

            if (mainScript != null)
                MonoImporter.SetExecutionOrder(mainScript, -999); 

            if (mainProviderScript != null)
                MonoImporter.SetExecutionOrder(mainProviderScript, -998); 

            Debug.Log("Порядок выполнения скриптов установлен.");
        }

        private static void CreateNewScene(string sceneName)
        {
            string newScenePath = $"Assets/Scenes/{sceneName}.unity";
            Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Additive);
            EditorSceneManager.SaveScene(newScene, newScenePath);
            EditorSceneManager.CloseScene(newScene, true);
            AssetDatabase.Refresh();
            Debug.Log($"Создана новая сцена: {newScenePath}");
        }
    }
}
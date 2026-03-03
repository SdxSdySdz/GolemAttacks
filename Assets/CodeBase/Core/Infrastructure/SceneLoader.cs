using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = CodeBase.Core.Infrastructure.Scenes.Scene;

namespace CodeBase.Core.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public void Load<TScene>(Action onLoaded = null, bool doReloadOnSameScene = false)
            where TScene : Scene
        {
            Load(typeof(TScene).Name.Replace("Scene", ""), doReloadOnSameScene, onLoaded: onLoaded);
        }

        private void Load(string name, bool doReloadOnSameScene, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(name, doReloadOnSameScene, onLoaded));
    
        private IEnumerator LoadScene(string nextScene, bool doReloadOnSameScene, Action onLoaded = null)
        {
            if (doReloadOnSameScene == false)
            {
                if (SceneManager.GetActiveScene().name == nextScene)
                {
                    onLoaded?.Invoke();
                    yield break;
                }
            }
      
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                yield return null;
      
            onLoaded?.Invoke();
        }
    }
}
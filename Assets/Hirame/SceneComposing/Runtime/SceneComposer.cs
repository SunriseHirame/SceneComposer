using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hirame.SceneComposing
{
    public static class SceneComposer
    {
        private static List<AsyncOperation> waitingForActivation = new List<AsyncOperation> ();

        public static void LoadSceneStack (SceneStack stack, bool additive = false)
        {
            var masterOps = SceneManager.LoadSceneAsync (stack.MasterScene.SceneName);
            masterOps.completed += (ao) => { Debug.Log (ao.isDone); };

//            foreach (var subScene in stack.SubScenes)
//            {
//                var ops = SceneManager.LoadSceneAsync (subScene.SceneName);
//                waitingForActivation.Add (ops);
//            }
        }
    }
}
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hirame.SceneComposing
{
    public static class SceneComposer
    {
        private static List<AsyncOperation> waitingForActivation = new List<AsyncOperation> ();

        public static void LoadSceneStack (SceneStack stack, bool additive = false)
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
            {
                Internal_LoadInEditorMode (stack, additive);
                return;
            }
#endif
            Internal_LoadScenesAsync (stack, additive);
        }

        private static void Internal_LoadScenesAsync (SceneStack stack, bool additive = false)
        {
            var masterOps = SceneManager.LoadSceneAsync (stack.MasterScene.SceneName);
            masterOps.completed += (ao) => { Debug.Log (ao.isDone); };

//            foreach (var subScene in stack.SubScenes)
//            {
//                var ops = SceneManager.LoadSceneAsync (subScene.SceneName);
//                waitingForActivation.Add (ops);
//            }
        }

#if UNITY_EDITOR
        private static void Internal_LoadInEditorMode (SceneStack stack, bool additive = false)
        {
            EditorSceneManager.OpenScene (stack.MasterScene.SceneName);
        }
#endif
    }
}
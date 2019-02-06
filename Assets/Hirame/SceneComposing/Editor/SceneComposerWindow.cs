using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Hirame.SceneComposing.Editor
{
    public class SceneComposerWindow : EditorWindow
    {
        private SceneStack[] sceneStacks;

        [MenuItem ("Hirame/Scene Composer")]
        public static void CreateWindow ()
        {
            var window = GetWindow<SceneComposerWindow> ();
            window.titleContent = new GUIContent ("Scene Composer");
        }

        private void OnEnable ()
        {
            LoadSceneStackAssets ();
        }

        private void LoadSceneStackAssets ()
        {
            var assets = AssetDatabase.FindAssets ("t:SceneStack");
            sceneStacks = new SceneStack[assets.Length];

            for (var i = 0; i < assets.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath (assets[i]);
                sceneStacks[i] = AssetDatabase.LoadAssetAtPath<SceneStack> (path);
            }
        }

        private void OnGUI ()
        {
            if (sceneStacks == null)
                return;

            foreach (var stack in sceneStacks)
            {
                using (new GUILayout.HorizontalScope ())
                {
                    EditorGUILayout.PrefixLabel (stack.name);

                    if (GUILayout.Button ("Load"))
                    {
                        stack.LoadScenes ();
                        return;
                    }

                    if (GUILayout.Button ("Select"))
                    {
                        Selection.activeObject = stack;
                        return;
                    }
                }
            }
        }
    }
}
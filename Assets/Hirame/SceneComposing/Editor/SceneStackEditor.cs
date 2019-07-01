using UnityEngine;
using UnityEditor;

namespace Hirame.SceneComposing.Editor
{
    [CustomEditor (typeof (SceneStack))]
    public class SceneStackEditor : UnityEditor.Editor
    {
        private static GUIStyle whiteBox;
        private SceneStack sceneStack;

        private SerializedProperty masterSceneProp;
        private SerializedProperty subScenesProp;
        
        private void OnEnable ()
        {
            sceneStack = target as SceneStack;

            masterSceneProp = serializedObject.FindProperty ("masterScene");
            subScenesProp = serializedObject.FindProperty ("subScenes");
            whiteBox = new GUIStyle(GUI.skin.box);
            whiteBox.font.material.color = Color.white;
        }

        public override void OnInspectorGUI ()
        {
            Debug.Log (whiteBox);

            if (GUILayout.Button ("Load Scenes"))
            {
                SceneStackUtility.OpenSceneStack (sceneStack);
            }
            EditorGUILayout.Space ();

            EditorGUILayout.PropertyField (masterSceneProp);
            
            // Draw sub-scenes
            using (new EditorGUILayout.VerticalScope (EditorStyles.helpBox))
            {
                //var rect = EditorGUILayout.GetControlRect (true, 18);
                //Debug.Log (whiteBox);
                //GUI.Box (rect, "Sub-Scenes", whiteBox);

                var subSceneCount = subScenesProp.arraySize;
                for (var i = 0; i < subSceneCount; i++)
                {
                    EditorGUILayout.PropertyField (subScenesProp.GetArrayElementAtIndex (i));
                }
            }
           
        }
    }

}
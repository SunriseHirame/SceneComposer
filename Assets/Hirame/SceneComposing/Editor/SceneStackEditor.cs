using UnityEngine;
using UnityEditor;

namespace Hirame.SceneComposing.Editor
{
    [CustomEditor (typeof (SceneStack))]
    public class SceneStackEditor : UnityEditor.Editor
    {
        private SceneStack sceneStack;
        
        private void OnEnable ()
        {
            sceneStack = target as SceneStack;
        }

        public override void OnInspectorGUI ()
        {
            if (GUILayout.Button ("Load Scenes"))
            {
                SceneStackUtility.OpenSceneStack (sceneStack);
            }
            
            DrawPropertiesExcluding (serializedObject, "m_Script");
        }
    }

}
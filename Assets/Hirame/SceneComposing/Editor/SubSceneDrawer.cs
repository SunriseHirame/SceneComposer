using UnityEditor;
using UnityEngine;

namespace Hirame.SceneComposing.Editor
{
    [CustomPropertyDrawer (typeof (SubScene))]
    public class SubSceneDrawer : PropertyDrawer
    {
        
        
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField (position, property);
            
            if (!property.isExpanded)
                return;
            
            var sceneNameProp = property.FindPropertyRelative ("SceneName");

            position.width /= 2f;
            position.x += position.width;

            EditorGUI.TextField (position, sceneNameProp.displayName, EditorStyles.label);
        }

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            return property.isExpanded ? 16 * 3 : 16;
        }
    }

}
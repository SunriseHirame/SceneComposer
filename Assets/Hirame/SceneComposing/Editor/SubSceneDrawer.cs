using UnityEditor;
using UnityEngine;

namespace Hirame.SceneComposing.Editor
{
    [CustomPropertyDrawer (typeof (SubScene))]
    public class SubSceneDrawer : PropertyDrawer
    {
        private SerializedProperty sceneNameProp;
        private SerializedProperty sceneGuidProp;
            
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            var initialRect = position;
            position.height = 16;
            EditorGUI.PropertyField (position, property);
  
            if (!property.isExpanded)
                return;
            
            if (sceneNameProp == null)
                sceneNameProp = property.FindPropertyRelative ("SceneName");

            var depthOffset  = property.depth * 16;
            var labelWidth = EditorGUIUtility.labelWidth - depthOffset;
            
            var nameRect = position;
            nameRect.width -= labelWidth;
            nameRect.x += labelWidth;

            EditorGUI.TextField (nameRect, sceneNameProp.displayName, EditorStyles.label);

            position.y += 16;
            position.height = 16;
            DrawSceneAssetSelector (position, property, label);
        }

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            return property.isExpanded ? 16 * 3 : 16;
        }

        private void DrawSceneAssetSelector (Rect position, SerializedProperty property, GUIContent label)
        {
            if (sceneGuidProp == null)
                sceneGuidProp = property.FindPropertyRelative ("sceneAssetGuid");
            
            var sceneGuid = sceneGuidProp?.stringValue ?? string.Empty;
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset> (AssetDatabase.GUIDToAssetPath (sceneGuid));
            var newSceneAsset = EditorGUI.ObjectField (position, sceneAsset, typeof(SceneAsset), false)
                as SceneAsset;

            if (sceneAsset == null && newSceneAsset == null)
                return;
            
            if (sceneAsset != null && sceneAsset.Equals (newSceneAsset))
                return;
            
            var assetPath = AssetDatabase.GetAssetPath (newSceneAsset);
            // ReSharper disable once PossibleNullReferenceException
            sceneGuidProp.stringValue = AssetDatabase.AssetPathToGUID (assetPath);
            sceneNameProp.stringValue = newSceneAsset?.name;

            property.serializedObject.ApplyModifiedProperties ();
        }
    }

}
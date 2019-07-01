using System;
using UnityEditor;
using UnityEngine;

namespace Hirame.SceneComposing.Editor
{
    [CustomPropertyDrawer (typeof (SubScene))]
    public class SubSceneDrawer : PropertyDrawer
    {
        private SerializedProperty cachedProp;
        private SerializedProperty sceneNameProp;
        private SerializedProperty sceneGuidProp;

        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            var indent = EditorGUI.indentLevel;
            position.x += 18 * indent;
            position.width -= 16 * indent;

            var fullRect = position;

            position.height = 16;

            GUI.Box (fullRect, string.Empty);
            GUI.Box (position, string.Empty);

            EditorGUI.PropertyField (position, property);

            if (!property.isExpanded)
                return;

            sceneNameProp = property.FindPropertyRelative ("SceneName");

            var depthOffset = property.depth * 16;
            var labelWidth = EditorGUIUtility.labelWidth - depthOffset;

            var nameRect = position;
            nameRect.width -= labelWidth;
            nameRect.x += labelWidth;

            // Scene Name
            EditorGUI.TextField (nameRect, sceneNameProp.displayName, EditorStyles.label);

            position.y += 18;
            position.height = 16;
            DrawSceneAssetSelector (position, property, depthOffset);
        }

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            return property.isExpanded ? 18 * 3 : 16;
        }

        private void DrawSceneAssetSelector (Rect position, SerializedProperty property, int depthOffset)
        {
            var labelWidth = EditorGUIUtility.labelWidth;
            var textRect = position;
            //textRect.x -= depthOffset;
            textRect.width = labelWidth;

            var assetRect = position;
            assetRect.x += labelWidth;
            assetRect.width = position.width - labelWidth;

            EditorGUI.LabelField (textRect, "Scene Asset");
            
            sceneGuidProp = property.FindPropertyRelative ("sceneAssetGuid"); 

            var sceneGuid = sceneGuidProp?.stringValue ?? string.Empty;
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset> (AssetDatabase.GUIDToAssetPath (sceneGuid));
            var newSceneAsset = EditorGUI.ObjectField (assetRect, sceneAsset, typeof (SceneAsset), false)
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
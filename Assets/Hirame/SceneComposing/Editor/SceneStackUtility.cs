using UnityEditor;

namespace Hirame.SceneComposing.Editor
{
    public static class SceneStackUtility
    {
        public static SceneAsset FindSceneAsset (SubScene subScene)
        {
            var path = AssetDatabase.GUIDToAssetPath (subScene.AssetGuid);
            return AssetDatabase.LoadAssetAtPath<SceneAsset> (path);
        }
    }

}

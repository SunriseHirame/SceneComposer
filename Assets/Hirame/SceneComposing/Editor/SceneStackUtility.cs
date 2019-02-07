using UnityEditor;
using UnityEditor.SceneManagement;

namespace Hirame.SceneComposing.Editor
{
    public static class SceneStackUtility
    {

        public static void OpenSceneStack (SceneStack sceneStack)
        {          
            EditorSceneManager.OpenScene (GetSceneAssetPath (sceneStack.MasterScene));
            foreach (var subScene in sceneStack.SubScenes)
            {
                EditorSceneManager.OpenScene (GetSceneAssetPath (subScene), OpenSceneMode.Additive);
            }
        }
        
        public static SceneAsset FindSceneAsset (SubScene subScene)
        {
            return AssetDatabase.LoadAssetAtPath<SceneAsset> (GetSceneAssetPath (subScene));
        }

        public static string GetSceneAssetPath (SubScene subScene)
        {
            return AssetDatabase.GUIDToAssetPath (subScene.SceneAssetGuid);
        }
    }

}

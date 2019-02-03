using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hirame.SceneComposing
{
    [CreateAssetMenu (menuName = "Scene Composer/Scene Stack")]
    public class SceneStack : ScriptableObject
    {
        [SerializeField]
        private SubScene masterScene;
        
        [SerializeField]
        private SubScene[] subScenes;

        public SubScene MasterScene => masterScene;

        public SubScene[] SubScenes => subScenes;

        public int SceneCount => 1 + subScenes.Length;

        public SubScene this [int index] => --index == -1 ? masterScene : subScenes[index];

        public void LoadScenes ()
        {
            SceneComposer.LoadSceneStack (this);
        }
    }
}

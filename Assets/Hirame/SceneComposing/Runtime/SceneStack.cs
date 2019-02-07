using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hirame.SceneComposing
{
    [CreateAssetMenu (menuName = "Scene Composer/Scene Stack")]
    public class SceneStack : ScriptableObject, IEnumerable<SubScene>
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

        public IEnumerator<SubScene> GetEnumerator ()
        {
            return new SceneStackEnumerator (this);
        }
        
        IEnumerator IEnumerable.GetEnumerator ()
        {
            return GetEnumerator ();
        }

        public class SceneStackEnumerator : IEnumerator<SubScene>
        {
            private readonly SceneStack sceneStack;
            private int position = 0;

            public SceneStackEnumerator (SceneStack sceneStack)
            {
                this.sceneStack = sceneStack;
            }
            
            public bool MoveNext () => ++position < sceneStack.SceneCount;

            public void Reset () => position = 0;

            SubScene IEnumerator<SubScene>.Current => sceneStack[position];

            public object Current => sceneStack[position];
            
            public void Dispose ()
            {
            }
        }
    }
}

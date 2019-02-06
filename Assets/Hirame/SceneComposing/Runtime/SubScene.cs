using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hirame.SceneComposing
{
    [System.Serializable]
    public sealed class SubScene
    {
        public string SceneName;
        public int SceneBuildIndex;

        [SerializeField]
        private string sceneAssetGuid;

        public string SceneAssetGuid
        {
            get => sceneAssetGuid;
            internal set => sceneAssetGuid = value;
        }
    }
}

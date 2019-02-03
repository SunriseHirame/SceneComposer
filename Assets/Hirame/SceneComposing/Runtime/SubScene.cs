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

        private string assetGuid;

        public string AssetGuid
        {
            get => assetGuid;
            internal set => assetGuid = value;
        }
    }
}

using ScriptableObjects;
using UnityEngine;

namespace Zong.Test.Core
{
    public class AssetManager : MonoBehaviour
    {
        public AssetCollection AssetCollection;
        
        public GameObject GetPrefab(string name)
        {
            Debug.Log(name);
            foreach (var asset in AssetCollection.Assets)
            {
                if (asset.Name.Equals(name))
                {
                    return asset.Prefab;
                }
            }

            return null;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Assets", menuName = "Assets/AssetCollection", order = 0)]
    public class AssetCollection : ScriptableObject
    {
        public List<Asset> Assets;
    }

    [System.Serializable]
    public class Asset
    {
        public string Name;
        public GameObject Prefab;
    }
}
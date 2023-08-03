using System;
using Newtonsoft.Json;
using UnityEngine;
using Zong.Test.Data;

namespace Core
{
    public class DataManager : MonoBehaviour
    {
        private const string CATEGORIES_FILE = "categories";
        private ItemCategory _itemCategories;
        public ItemCategory ItemCategories => _itemCategories;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _itemCategories = GetData<ItemCategory>(CATEGORIES_FILE);
        }

        public T GetData<T>(string name)
        {
            string jsonText = Resources.Load<TextAsset>(name).text;
            T data = JsonConvert.DeserializeObject<T>(jsonText);
            return data;
        }
    }
}
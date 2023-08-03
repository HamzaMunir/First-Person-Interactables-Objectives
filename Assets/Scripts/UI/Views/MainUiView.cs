using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using Zong.Test.Data;
using Zong.Test.Gameplay;

namespace Zong.Test.Views
{
    public class MainUiView : UiBaseView
    {
        private MainUiData _uiData => Data as MainUiData;
        public CategoryButtonView CategoryButtonPrefab;
        public ItemView ItemViewPrefab;
        
        public GameObject SubCategoriesContainer;
        public GameObject ItemsContainer;
        public GameObject CategoryLayoutContent;
        public GameObject SubCategoriesContent;
        public GameObject ItemsContent;

        private List<CategoryButtonView> _spawnedCategories = new();
        protected override void RefreshView()
        {
            ResetView();
            foreach (var item in _uiData.Categories.categories)
            {
                var categoryButtonView = SpawnCategory(item.name, CategoryLayoutContent, OnCategorySelected);
                _spawnedCategories.Add(categoryButtonView);
            }
        }

        private void OnCategorySelected(string category)
        {
            ResetContent(SubCategoriesContent);
            ResetContent(ItemsContent);
            switch (category)
            {
                case nameof(ECategory.Weapons):
                {
                    ItemsContainer.SetActive(false);
                    SubCategoriesContainer.SetActive(true);
                    Category categoryData = GetCategory(ECategory.Weapons);
                    foreach (var item in categoryData.subcategories)
                    {
                        SpawnCategory(item, SubCategoriesContent);
                    }
                    
                    break;
                }
                case nameof(ECategory.Instruments):
                {
                    ItemsContainer.SetActive(true);
                    SubCategoriesContainer.SetActive(false);
                    if (_uiData.Player.PickedItems.TryGetValue(ECategory.Instruments, out List<Item> equippedItem))
                    {
                        foreach (var item in equippedItem)
                        {
                            var spawnedItemView = Instantiate(ItemViewPrefab, ItemsContent.transform);
                            spawnedItemView.SetData(item, OnItemSelected);
                        }
                    }

                    break;
                }
            }
        }

        private void OnItemSelected(Item item)
        {
            MessageBroker.Instance.Publish(new ItemEquippedMessage(){Item = item, Player = _uiData.Player});
            Hide();
        }

        private CategoryButtonView SpawnCategory(string categoryName, GameObject content, Action<string> OnCategorySelected = null)
        {
            var categoryButtonView = Instantiate(CategoryButtonPrefab, content.transform);
            categoryButtonView.SetData(categoryName, OnCategorySelected);
            return categoryButtonView;
        }
        
        private Category GetCategory(ECategory category)
        {
            foreach (var item in _uiData.Categories.categories)
            {
                if (item.name == Enum.GetName(typeof(ECategory), category))
                {
                    return item;
                }
            }

            return null;
        }

        private void ResetContent(GameObject content)
        {
            foreach (Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }
        }
        protected override void ResetView()
        {
            ResetContent(CategoryLayoutContent);
            ResetContent(SubCategoriesContent);
            ResetContent(ItemsContent);
            SubCategoriesContainer.SetActive(false);
            ItemsContainer.SetActive(false);
        }
    }
}
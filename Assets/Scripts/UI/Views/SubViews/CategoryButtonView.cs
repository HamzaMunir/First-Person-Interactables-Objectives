using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Zong.Test.Views
{
    public class CategoryButtonView : MonoBehaviour
    {
        public TextMeshProUGUI CategoryNameText;
        private string _category;
        private Action<string> _onCategorySelected;
        [SerializeField] private Button _btn;


        private void OnEnable()
        {
            _btn.onClick.AddListener(OnButtonClick);
        }
        
        private void OnDisable()
        {
            _btn.onClick.RemoveListener(OnButtonClick);
        }

        public void SetData(string category, Action<string> onCategorySelected)
        {
            _category = category;
            _onCategorySelected = onCategorySelected;
            RefreshView();
        }

        private void RefreshView()
        {
            CategoryNameText.text = _category;
        }

        private void OnButtonClick()
        {
            _onCategorySelected?.Invoke(_category);
        }
    }
}
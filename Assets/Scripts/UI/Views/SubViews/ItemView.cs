using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zong.Test.Gameplay;

namespace Zong.Test.Views
{
    public class ItemView : MonoBehaviour
    {
        public TextMeshProUGUI ItemNameText;
        private Item _item;
        private Action<Item> _onItemSelected;
        [SerializeField] private Button _btn;
        
        private void OnEnable()
        {
            _btn.onClick.AddListener(OnButtonClick);
        }
        
        private void OnDisable()
        {
            _btn.onClick.RemoveListener(OnButtonClick);
        }

        public void SetData(Item item, Action<Item> onItemSelected)
        {
            _item = item;
            _onItemSelected = onItemSelected;
            RefreshView();
        }

        private void RefreshView()
        {
            ItemNameText.text = _item.Name;
        }

        private void OnButtonClick()
        {
            _onItemSelected?.Invoke(_item);
        }
    }
}
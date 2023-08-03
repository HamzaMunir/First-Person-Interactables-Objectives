using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zong.Test.Data;

namespace Zong.Test.Views
{
    public class ObjectiveUIView : UiBaseView
    {
        [SerializeField] private TextMeshProUGUI _resultText;
        [SerializeField] private Button OnGameRestart;
        
        private ObjectiveUiData UiData => Data as ObjectiveUiData;

        private void OnEnable()
        {
            OnGameRestart.onClick.AddListener(OnRestartRequested);
        }
        
        private void OnDisable()
        {
            OnGameRestart.onClick.RemoveListener(OnRestartRequested);
        }

        protected override void RefreshView()
        {
            _resultText.text = $"You have dropped on box {UiData.ObjectiveName}";
        }

        protected override void ResetView()
        {
            
        }
        
        private void OnRestartRequested()
        {
            UiData.OnRestart?.Invoke();
            Hide();
        }
    }
}
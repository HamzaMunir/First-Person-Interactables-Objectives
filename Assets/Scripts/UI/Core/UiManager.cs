using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zong.Test.Data;

namespace Zong.Test.Views
{
    public class UiManager : MonoBehaviour
    {
        public MainUiView MainUI;
        public ObjectiveUIView ObjectiveUi;

        private UiBaseView _activeView;

        public void ShowMainUI(MainUiData data)
        {
            HideActiveView();
            MainUI.SetData(data);
            _activeView = MainUI;
        }

        public void HideMainUI()
        {
            MainUI.Hide();
        }

        public void ShowObjectiveUi(ObjectiveUiData data)
        {
            HideActiveView();
            ObjectiveUi.SetData(data);
            _activeView = ObjectiveUi;
        }
        public void HideObjectiveUi(ObjectiveUiData data)
        {
            ObjectiveUi.Hide();
        }

        private void HideActiveView()
        {
            if (_activeView != null)
            {
                _activeView.Hide();
            }
        }
    }
}
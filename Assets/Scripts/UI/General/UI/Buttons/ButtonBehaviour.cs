using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kit.UI.Buttons
{
	/// <summary>Parent class for behaviours that want to react to a <see cref="UnityEngine.UI.Button" /> or UI element's click.</summary>
	public abstract class ButtonBehaviour: MonoBehaviour, IPointerClickHandler
	{
		public bool IsEnabled = true;

		/// <summary>Action to execute after the button is clicked.</summary>
		public abstract void OnClicked();

		protected Button button;

		protected virtual void Awake()
		{
			button = GetComponent<Button>();
			if (button != null)
			 	button.onClick.AddListener(OnClickedInternal);
		}

		protected void OnClickedInternal()
		{
			if (IsEnabled)
				OnClicked();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (button == null)
				OnClickedInternal();
		}
	}
}
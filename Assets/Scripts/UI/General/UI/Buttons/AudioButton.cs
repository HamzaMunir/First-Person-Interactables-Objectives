using UnityEngine;
using UnityEngine.EventSystems;

namespace Kit.UI.Buttons
{
	/// <summary>Button that plays an audio on the UI audio group.</summary>
	public class AudioButton: ButtonBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
	{
		/// <summary>Audio to play when the button is clicked.</summary>
		[Tooltip("Audio to play when the button is clicked.")]
		public AudioClip Clicked;

		/// <summary>Audio to play when the button is selected.</summary>
		[Tooltip("Audio to play when the button is selected.")]
		public AudioClip Selected;

		private bool isPointerInside = false;

		public override void OnClicked()
		{
			AudioManager.Instance.PlayUI(Clicked);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			isPointerInside = true;
			AudioManager.Instance.PlayUI(Selected);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			isPointerInside = false;
		}

		public void OnSelect(BaseEventData eventData)
		{
			if (!isPointerInside)
				AudioManager.Instance.PlayUI(Selected);
		}
	}
}
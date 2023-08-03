using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game.UI
{
	[RequireComponent(typeof(Button))]
	public abstract class InputButton: InputBehaviour
	{
		protected Button button;

		protected override void Awake()
		{
			base.Awake();
			button = GetComponent<Button>();
		}

		protected override void OnAction(InputAction.CallbackContext context)
		{
			if (button != null)
			{
				if (button.interactable && button.isActiveAndEnabled)
					button.onClick.Invoke();
			}
			else
			{
				if (gameObject.activeInHierarchy)
					ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
			}
		}
	}
}
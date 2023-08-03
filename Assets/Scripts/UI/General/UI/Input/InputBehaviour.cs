using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace Game.UI
{
	public abstract class InputBehaviour: MonoBehaviour
	{
		protected InputSystemUIInputModule input;

		protected virtual void Awake()
		{
			if (EventSystem.current == null)
				return;

			input = EventSystem.current.GetComponent<InputSystemUIInputModule>();
		}

		protected virtual void OnEnable()
		{
			Action.action.performed += OnAction;
		}

		protected virtual void OnDisable()
		{
			Action.action.performed -= OnAction;
		}

		protected abstract void OnAction(InputAction.CallbackContext context);
		protected abstract InputActionReference Action { get; }
	}
}
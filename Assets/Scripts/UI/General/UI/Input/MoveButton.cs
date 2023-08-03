using Kit;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.UI
{
	public class MoveButton: InputButton
	{
		//public Direction4Way Direction;

		protected override InputActionReference Action => input.move;

		protected override void OnAction(InputAction.CallbackContext context)
		{
			// Direction4Way direction = context.ReadValue<Vector2>().ToDirection4Way();
			// if (direction == Direction)
			// 	base.OnAction(context);
		}
	}
}
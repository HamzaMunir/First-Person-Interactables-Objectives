using UnityEngine.InputSystem;

namespace Game.UI
{
	public class CancelButton : InputButton
	{
		protected override InputActionReference Action => input.cancel;
	}
}
using UnityEngine.InputSystem;

namespace Game.UI
{
	public class SubmitButton: InputButton
	{
		protected override InputActionReference Action => input.submit;
	}
}
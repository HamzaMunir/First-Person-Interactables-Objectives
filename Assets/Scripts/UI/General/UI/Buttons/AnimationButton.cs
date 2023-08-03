using UnityEngine;

namespace Kit.UI.Buttons
{
	public class AnimationButton: ButtonBehaviour
	{
		public Animator Animator;
		public string Animation = "";

		public override void OnClicked()
		{
			Animator.Play(Animation);
		}
	}
}
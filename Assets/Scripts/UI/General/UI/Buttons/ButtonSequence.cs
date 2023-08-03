using System.Collections.Generic;

namespace Kit.UI.Buttons
{
	/// <summary>Allows to execute different <see cref="Behaviours"/> in a particular sequence.</summary>
	public class ButtonSequence: ButtonBehaviour
	{
		/// <summary>List of behaviours to execute.</summary>
		public List<ButtonBehaviour> Behaviours;

		protected override void Awake()
		{
			base.Awake();
			foreach (ButtonBehaviour behaviour in Behaviours)
				behaviour.IsEnabled = false;
		}

		public override void OnClicked()
		{
			foreach (ButtonBehaviour behaviour in Behaviours)
				behaviour.OnClicked();
		}
	}
}
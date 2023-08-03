using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kit.UI.Behaviours
{
	/// <summary>Allows to turn <see cref="Panel" />s on/off with a <see cref="UnityEngine.UI.Toggle" />.</summary>
	[RequireComponent(typeof(Toggle))]
	public class TogglePanels: ToggleBehaviour
	{
		/// <summary>The <see cref="Panel" />s to show or hide.</summary>
		[Tooltip("The Panel to show or hide.")]
		public List<GameObject> Panels;

		protected override void OnValueChanged(bool value)
		{
			foreach (GameObject panel in Panels)
				panel.SetActive(value);
		}
	}
}
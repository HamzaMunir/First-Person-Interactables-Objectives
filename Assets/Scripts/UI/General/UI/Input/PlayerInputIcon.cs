using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game.UI
{
	[RequireComponent(typeof(Image))]
	public class PlayerInputIcon: InputIcon
	{
		public PlayerInput Input { get; set; }

		protected Image image;

		protected virtual void Awake()
		{
			image = GetComponent<Image>();
		}

		protected virtual void Start()
		{
			Refresh(Input.devices.First());
		}

		public override void Refresh(Sprite sprite)
		{
			image.sprite = sprite;
		}

		protected override InputAction inputAction => Input.actions.FindAction(Action.action.id);
	}
}
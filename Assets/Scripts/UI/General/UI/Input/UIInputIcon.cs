using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game.UI
{
	[RequireComponent(typeof(Image))]
	public class UIInputIcon: InputIcon
	{
		protected Image image;

		protected virtual void Awake()
		{
			image = GetComponent<Image>();
			DeviceChanged += Refresh;
		}

		protected virtual void Start()
		{
			Refresh();
		}

		protected virtual void OnEnable()
		{
			Refresh();
		}

		public void Refresh()
		{
			Refresh(LastDevice ?? InputSystem.devices.First());
		}

		public override void Refresh(Sprite sprite)
		{
			image.sprite = sprite;
		}

		protected void OnDestroy()
		{
			DeviceChanged -= Refresh;
		}
	}
}
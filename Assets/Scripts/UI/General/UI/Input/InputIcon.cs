using System;
using System.Collections.Generic;
using System.Linq;
using Kit;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.UI
{
	public abstract class InputIcon: MonoBehaviour
	{
		#region System-wide functions
		public static event Action<InputDevice> DeviceChanged;

		public class Data
		{
			public List<string> Devices;
			public string Atlas;
			public Sprite[] Icons;
		}

		public static InputDevice LastDevice;
		public static List<Data> IconsByDevices;
		public static string FallbackDevice;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void InitializeInternal()
		{
			DeviceChanged = null;
			LastDevice = null;
			IconsByDevices = null;
			FallbackDevice = null;
		}

		public static void Initialize(List<Data> icons, string fallbackDevice = "XInputController")
		{
			InputSystem.onActionChange += OnActionChanged;
			//ControlHelper.ApplicationQuit += OnQuit;

			FallbackDevice = fallbackDevice;
			foreach (Data data in icons)
				data.Icons = Resources.LoadAll<Sprite>(data.Atlas);
			IconsByDevices = icons;
		}

		private static void OnActionChanged(object obj, InputActionChange type)
		{
			if (type != InputActionChange.ActionPerformed)
				return;

			InputAction action = (InputAction) obj;
			InputDevice device = action.activeControl.device;
			if (device == LastDevice)
				return;

			LastDevice = device;
			DeviceChanged?.Invoke(device);
		}

		public static Data GetDataWithFallback(string device)
		{
			return GetData(device) ?? GetData(FallbackDevice);
		}

		public static Data GetData(string device)
		{
			return IconsByDevices.FirstOrDefault(d => d.Devices.Contains(device));
		}

		public static InputControl FindControl(Data data, InputAction action, int index)
		{
			InputControl control = null;
			foreach (string deviceName in data.Devices)
			{
				if (index == 0)
					control = action.controls.FirstOrDefault(c => c.device.layout == deviceName);
				else
				{
					var controls = action.controls
											  .Where(c => c.device.layout == deviceName).ToList();
					if (index >= 0 && index < controls.Count)
						control = controls[index];
				}
				if (control != null)
					break;
			}

			return control;
		}

		private static void OnQuit()
		{
			InputSystem.onActionChange -= OnActionChanged;
		}
		#endregion




		#region Component functions
		public InputActionReference Action;
		public int Index = 0;

		protected virtual void Refresh(InputDevice device)
		{
			Data data = GetDataWithFallback(device.layout);

			// Icon set not found for changed device
			if (data == null)
				return;

			InputControl control = FindControl(data, inputAction, Index);

			// Action doesn't have a binding for the connected device.
			if (control == null)
				return;

			var matching = data.Icons.Where(s => control.name == s.name);

			// The icon set doesn't have the icon for the binding's key
			if (!matching.Any())
				return;

			// Get the longest matching key
			Sprite sprite = matching.Aggregate((s1, s2) => s1.name.Length > s2.name.Length ? s1 : s2);
			Refresh(sprite);
		}

		public abstract void Refresh(Sprite sprite);
		protected virtual InputAction inputAction => Action.action;
		#endregion
	}
}
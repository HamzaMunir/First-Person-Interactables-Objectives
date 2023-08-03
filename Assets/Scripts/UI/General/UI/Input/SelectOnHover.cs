using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
	public class SelectOnHover: MonoBehaviour, IPointerEnterHandler
	{
		public void OnPointerEnter(PointerEventData eventData)
		{
			Select();
		}

		private void Select()
		{
			Selectable selectable = GetComponent<Selectable>();
			if (selectable)
				selectable.Select();
			else
				EventSystem.current.SetSelectedGameObject(gameObject);
		}
	}
}
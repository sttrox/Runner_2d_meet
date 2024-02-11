using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
	public class UIChecker : MonoBehaviour
	{
		[SerializeField]
		private LayerMask _uiLayer;

		[SerializeField]
		private EventSystem _eventSystem;

		private readonly List<RaycastResult> _raycastResults = new List<RaycastResult>();

		public bool CheckPointerOverUI()
		{
			PointerEventData eventData = new PointerEventData(EventSystem.current);
			eventData.position = Input.mousePosition;
			_eventSystem.RaycastAll(eventData, _raycastResults);

			foreach (RaycastResult raycastResult in _raycastResults)
			{
				if (1 << raycastResult.gameObject.layer == _uiLayer.value)
				{
					return true;
				}
			}

			return false;
		}
	}
}
using System;
using UnityEngine;

namespace UI.Popups
{
	public abstract class BasePopup : MonoBehaviour
	{
		private Action<BasePopup> _closeCallback;

		public virtual void Show(Action<BasePopup> closeCallback)
		{
			_closeCallback = closeCallback;
		}

		public virtual void Hide()
		{
			this.gameObject.SetActive(false);
			_closeCallback?.Invoke(this);
		}
	}
}
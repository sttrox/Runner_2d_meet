using System;
using System.Collections.Generic;
using UI.Popups;
using UnityEngine;

namespace RunnerMeet.UI
{
	public class ScreenSwitcher : MonoBehaviour
	{
		[SerializeField]
		private List<BaseScreen> screensPrefabs = new List<BaseScreen>();

		[SerializeField]
		private List<BasePopup> popupsPrefabs = new List<BasePopup>();

		private readonly Dictionary<Type, BaseScreen> screenCache = new Dictionary<Type, BaseScreen>();
		private readonly List<BasePopup> currentOpeningPopups = new List<BasePopup>();

		private BaseScreen currentScreen;

		public TScreen ShowScreen<TScreen>() where TScreen : BaseScreen
		{
			Type screenType = typeof(TScreen);

			if (screenCache.TryGetValue(screenType, out var foundScreen))
			{
				currentScreen?.Hide();
				currentScreen = foundScreen;
				currentScreen.Show();
			}
			else
			{
				//sttrox: оптимизировать, лучше в какой словарь закинуть все префабы
				BaseScreen prefab = screensPrefabs.Find(x => x.GetType().FullName == screenType.FullName);
				if (prefab != null)
				{
					BaseScreen newScreen = Instantiate(prefab, transform);
					screenCache[screenType] = newScreen;
					currentScreen?.Hide();
					currentScreen = newScreen;
					currentScreen.Show();
				}
				else
				{
					Debug.LogError($"Screen prefab not found for type: {screenType}");
				}
			}

			return (TScreen)currentScreen;
		}

		public TPopup ShowPopup<TPopup>(Action closeCallback) where TPopup : BasePopup
		{
			Type popupType = typeof(TPopup);
			BasePopup instancePopup = null;

			//sttrox: оптимизировать, лучше в какой словарь закинуть все префабы
			BasePopup prefab = popupsPrefabs.Find(x => x.GetType().FullName == popupType.FullName);
			if (prefab != null)
			{
				instancePopup = Instantiate(prefab, transform);

				currentOpeningPopups.Add(instancePopup);
				instancePopup.Show(popup =>
				{
					currentOpeningPopups.Remove(popup);
					closeCallback.Invoke();
				});
			}
			else
			{
				Debug.LogError($"Popup prefab not found for type: {popupType}");
			}


			return (TPopup)instancePopup;
		}

		private void HideCurrentScreen()
		{
			if (currentScreen != null)
			{
				currentScreen.Hide();
				currentScreen = null;
			}
		}
	}
}
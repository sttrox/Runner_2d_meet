using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
	public class PausePopup : BasePopup
	{
		[SerializeField]
		private Button _closePopupButton;

		[SerializeField]
		private Button _restartButton;

		[SerializeField]
		private Button _menuButton;

		private IStarterGame _gameStarter;

		public void Construct(IStarterGame gameStarter)
		{
			_gameStarter = gameStarter;
		}

		private void OnEnable()
		{
			_restartButton.onClick.AddListener(OnRestartButtonClick);
			_menuButton.onClick.AddListener(OnMenuButtonClick);
			_closePopupButton.onClick.AddListener(OnClosePopupButtonClick);
		}

		private void OnDisable()
		{
			_restartButton.onClick.RemoveListener(OnRestartButtonClick);
			_menuButton.onClick.RemoveListener(OnMenuButtonClick);
		}

		private void OnRestartButtonClick()
		{
			Hide();
			_gameStarter.RestartGame();
		}

		private void OnMenuButtonClick()
		{
			Hide();
			_gameStarter.GoToMainMenu();
		}

		private void OnClosePopupButtonClick()
		{
			Hide();
		}
	}
}
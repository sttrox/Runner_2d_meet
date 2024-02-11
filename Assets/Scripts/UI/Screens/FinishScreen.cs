using System;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace RunnerMeet.UI
{
	public class FinishScreen : BaseScreen
	{
		[SerializeField]
		private Button _restartButton;

		[SerializeField]
		private Button _menuButton;

		private IStartGame _gameStarter;

		public void Construct(IStartGame gameStarter)
		{
			_gameStarter = gameStarter;
		}

		private void OnEnable()
		{
			_restartButton.onClick.AddListener(OnRestartButtonClick);
			_menuButton.onClick.AddListener(OnMenuButtonClick);
		}

		private void OnDisable()
		{
			_restartButton.onClick.RemoveListener(OnRestartButtonClick);
			_menuButton.onClick.RemoveListener(OnMenuButtonClick);
		}

		private void OnRestartButtonClick()
		{
			throw new NotImplementedException();
		}

		private void OnMenuButtonClick()
		{
			throw new NotImplementedException();
		}
	}
}
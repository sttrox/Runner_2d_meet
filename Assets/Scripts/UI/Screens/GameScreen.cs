using Core;
using TMPro;
using UI.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace RunnerMeet.UI
{
	public class GameScreen : BaseScreen
	{
		[SerializeField]
		private TMP_Text _scoreLabel;

		[SerializeField]
		private Button _pauseButton;

		[SerializeField]
		private Button _soundButton;

		private GameTimeScaler _gameTimeScaler;
		private ScreenSwitcher _screenSwitcher;
		private IStarterGame _starterGame;

		public void Construct(IStarterGame starterGame, GameTimeScaler gameTimeScaler, ScreenSwitcher screenSwitcher)
		{
			_starterGame = starterGame;
			_screenSwitcher = screenSwitcher;
			_gameTimeScaler = gameTimeScaler;
		}

		private void OnEnable()
		{
			_soundButton.onClick.AddListener(OnSoundButtonClick);
			_pauseButton.onClick.AddListener(OnPauseButtonClick);
		}

		private void OnDisable()
		{
			_soundButton.onClick.RemoveListener(OnSoundButtonClick);
			_pauseButton.onClick.RemoveListener(OnPauseButtonClick);
		}

		public void SetScore(int score)
		{
			_scoreLabel.text = score.ToString();
		}

		private void OnPauseButtonClick()
		{
			_gameTimeScaler.PauseGame();
			var popup = _screenSwitcher.ShowPopup<PausePopup>(() => _gameTimeScaler.ResumeGame());
			popup.Construct(_starterGame);
		}

		private void OnSoundButtonClick()
		{
			throw new System.NotImplementedException();
		}
	}
}
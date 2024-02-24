using System.Collections;
using Gameplay.Cameras;
using RunnerMeet.Configs;
using RunnerMeet.Gameplay;
using RunnerMeet.Inputs;
using RunnerMeet.UI;
using UI;
using UnityEngine;

namespace Core
{
	public class Main : MonoBehaviour, IStarterGame, ICoroutineRunner
	{
		[SerializeField]
		private LevelsConfig _levelsConfig;

		[SerializeField]
		private PlayerCharacterConfig _playerCharacterConfig;

		[SerializeField]
		private ScreenSwitcher _screenSwitcher;

		[SerializeField]
		private CameraDistributor _cameraDistributor;

		[SerializeField]
		private UIChecker _uiChecker;

		private readonly GameTimeScaler _gameTimeScaler = new GameTimeScaler();

		private IInput _playerInput;

		private Game _game;

		private void Awake()
		{
			_playerInput = new TouchAndKeyboardInput(_uiChecker);
		}

		private void Start()
		{
			_game = new Game(_levelsConfig.LevelPrefab, _playerCharacterConfig.PlayerCharacterPrefab, _screenSwitcher,
				_playerInput, _cameraDistributor, _gameTimeScaler, (ICoroutineRunner)this, (IStarterGame)this);
			MenuScreen menuScreen = _screenSwitcher.ShowScreen<MenuScreen>();
			menuScreen.Construct((IStarterGame)this);
		}

		private void Update()
		{
			_game?.ThisUpdate();
		}

		public void StartGame()
		{
			_game.StartGame();
		}

		public void RestartGame()
		{
			_game.Destroy();
			_game.StartGame();
		}

		public void GoToMainMenu()
		{
			_game.Destroy();

			MenuScreen menuScreen = _screenSwitcher.ShowScreen<MenuScreen>();
			menuScreen.Construct((IStarterGame)this);
		}

		Coroutine ICoroutineRunner.RunCoroutine(IEnumerator enumerator)
		{
			Coroutine result = StartCoroutine(enumerator);
			return result;
		}
	}
}
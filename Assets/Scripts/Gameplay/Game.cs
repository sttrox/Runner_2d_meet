using System.Collections;
using Core;
using Gameplay.Cameras;
using Gameplay.Characters;
using RunnerMeet.Inputs;
using RunnerMeet.UI;
using UnityEngine;

namespace RunnerMeet.Gameplay
{
	public class Game
	{
		private readonly Level _levelPrefab;
		private readonly ScreenSwitcher _screenSwitcher;
		private readonly NewPlayerCharacter _playerCharacterPrefab;
		private readonly IInput _playerInput;
		private readonly ICoroutineRunner _coroutineRunner;
		private readonly CameraDistributor _cameraDistributor;
		private readonly IStarterGame _starterGame;

		private GameScreen _gameScreen;
		private NewPlayerCharacter _playerInstance;

		private int _score;

		private Level _levelInstance;
		private GameTimeScaler _gameTimeScaler;

		public Game(Level levelPrefab, NewPlayerCharacter playerCharacterPrefab, ScreenSwitcher screenSwitcher,
			IInput playerInput, CameraDistributor cameraDistributor, GameTimeScaler gameTimeScaler,
			ICoroutineRunner coroutineRunner,
			IStarterGame starterGame)
		{
			_gameTimeScaler = gameTimeScaler;
			_starterGame = starterGame;
			_cameraDistributor = cameraDistributor;
			_coroutineRunner = coroutineRunner;
			_playerInput = playerInput;
			_playerCharacterPrefab = playerCharacterPrefab;
			_screenSwitcher = screenSwitcher;
			_levelPrefab = levelPrefab;
		}

		public void ThisUpdate()
		{
			if (_playerInstance != null)
			{
				_score = (int)_playerInstance.transform.position.x / 2;
				_gameScreen.SetScore(_score);
				_playerInput.CustomUpdate();
			}
		}

		public void StartGame()
		{
			_levelInstance = Object.Instantiate(_levelPrefab);
			_gameScreen = _screenSwitcher.ShowScreen<GameScreen>();
			_gameScreen.Construct(_starterGame, _gameTimeScaler, _screenSwitcher);

			Vector3 spawnPoint = _levelInstance.SpawnPoint;

			_playerInstance = Object.Instantiate(_playerCharacterPrefab, spawnPoint, Quaternion.identity);
			_playerInstance.Construct(_playerInput);
			_playerInstance.Died += PlayerInstanceOnDied;

			_cameraDistributor.SetTargetFollow(_playerInstance.transform);
		}

		public void Destroy()
		{
			_levelInstance.Destroy();
			_playerInstance.Destroy();
		}

		private void PlayerInstanceOnDied()
		{
			_coroutineRunner.RunCoroutine(DelayToShowFinishScreen());

			IEnumerator DelayToShowFinishScreen()
			{
				//todo move to configs
				yield return new WaitForSeconds(2);
				var finishScreen = _screenSwitcher.ShowScreen<FinishScreen>();
				finishScreen.Construct(_starterGame);
			}
		}
	}
}
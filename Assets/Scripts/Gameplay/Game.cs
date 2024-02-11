using System.Collections;
using Core;
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

		private GameScreen _gameScreen;
		private NewPlayerCharacter _playerInstance;

		private int _score;
		private IInput _playerInput;
		private ICoroutineRunner _coroutineRunner;

		public Game(Level levelPrefab, NewPlayerCharacter playerCharacterPrefab, ScreenSwitcher screenSwitcher,
			IInput playerInput, ICoroutineRunner coroutineRunner)
		{
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
			Level levelInstance = Object.Instantiate(_levelPrefab);
			_gameScreen = _screenSwitcher.ShowScreen<GameScreen>();

			Vector3 spawnPoint = levelInstance.SpawnPoint;

			_playerInstance = Object.Instantiate(_playerCharacterPrefab, spawnPoint, Quaternion.identity);
			_playerInstance.Construct(_playerInput);
			_playerInstance.Died += PlayerInstanceOnDied;
		}

		private void PlayerInstanceOnDied()
		{
			_coroutineRunner.RunCoroutine(DelayToShowFinishScreen());

			IEnumerator DelayToShowFinishScreen()
			{
				//todo move to configs
				yield return new WaitForSeconds(2);
				var finishScreen = _screenSwitcher.ShowScreen<FinishScreen>();
				//todo throw need args
				finishScreen.Construct(null);
			}
		}
	}
}
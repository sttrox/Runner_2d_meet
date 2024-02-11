using System.Collections;
using RunnerMeet.Configs;
using RunnerMeet.Gameplay;
using RunnerMeet.Inputs;
using RunnerMeet.UI;
using UI;
using UnityEngine;

namespace Core
{
	public class Main : MonoBehaviour, IStartGame, ICoroutineRunner
	{
		[SerializeField]
		private LevelsConfig _levelsConfig;

		[SerializeField]
		private PlayerCharacterConfig _playerCharacterConfig;

		[SerializeField]
		private ScreenSwitcher _screenSwitcher;

		[SerializeField]
		private UIChecker _uiChecker;

		private IInput _playerInput;

		private Game _game;

		private void Awake()
		{
			_playerInput = new TouchAndKeyboardInput(_uiChecker);
		}

		private void Start()
		{
			_game = new Game(_levelsConfig.LevelPrefab, _playerCharacterConfig.PlayerCharacterPrefab, _screenSwitcher,
				_playerInput, (ICoroutineRunner)this);
			MenuScreen menuScreen = _screenSwitcher.ShowScreen<MenuScreen>();
			menuScreen.Construct((IStartGame)this);
		}

		private void Update()
		{
			_game?.ThisUpdate();
		}

		public void StartGame()
		{
			_game.StartGame();
		}

		Coroutine ICoroutineRunner.RunCoroutine(IEnumerator enumerator)
		{
			Coroutine result = StartCoroutine(enumerator);
			return result;
		}
	}
}
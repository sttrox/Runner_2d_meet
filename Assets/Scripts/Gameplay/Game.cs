using Characters;
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

        public Game(Level levelPrefab, NewPlayerCharacter playerCharacterPrefab, ScreenSwitcher screenSwitcher)
        {
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
            }
        }

        public void StartGame()
        {
            Level levelInstance = Object.Instantiate(_levelPrefab);
            _gameScreen = _screenSwitcher.ShowScreen<GameScreen>();

            Vector3 spawnPoint = levelInstance.SpawnPoint;

            _playerInstance = Object.Instantiate(_playerCharacterPrefab, spawnPoint, Quaternion.identity);
        }
    }
}
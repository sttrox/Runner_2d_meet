using System;
using RunnerMeet.Configs;
using RunnerMeet.Gameplay;
using RunnerMeet.UI;
using UnityEngine;

namespace Core
{
    public class Main : MonoBehaviour, IStartGame
    {
        [SerializeField] private LevelsConfig _levelsConfig;
        [SerializeField] private PlayerCharacterConfig _playerCharacterConfig;
        [SerializeField] private ScreenSwitcher _screenSwitcher;

        private Game _game;

        private void Start()
        {
            _game = new Game(_levelsConfig.LevelPrefab, _playerCharacterConfig.PlayerCharacterPrefab, _screenSwitcher);
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
    }
}
using System;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace RunnerMeet.UI
{
    public class MenuScreen : BaseScreen
    {
        [SerializeField] private Button _playButton;

        private IStarterGame _gameStarter;

        public void Construct(IStarterGame gameStarter)
        {
            _gameStarter = gameStarter;
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick()
        {
            _gameStarter.StartGame();
        }
    }
}
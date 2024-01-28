using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RunnerMeet.UI
{
    public class GameScreen : BaseScreen
    {
        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _soundButton;

        public void Construct()
        {
        }

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(OnSoundButtonClick);
            _soundButton.onClick.AddListener(OnPauseButtonClick);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(OnSoundButtonClick);
            _soundButton.onClick.RemoveListener(OnPauseButtonClick);
        }

        public void SetScore(int score)
        {
            _scoreLabel.text = score.ToString();
        }

        private void OnPauseButtonClick()
        {
            throw new System.NotImplementedException();
        }

        private void OnSoundButtonClick()
        {
            throw new System.NotImplementedException();
        }
    }
}
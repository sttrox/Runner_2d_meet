using TMPro;
using UnityEngine;

namespace RunnerMeet
{
    public class ScoreCounter : RestartEntite, ISaved
    {
        [SerializeField] private PlayerCharacter _player;
        [SerializeField] private TMP_Text _bestScoreText;
        [SerializeField] private TMP_Text[] _scoreTexts;

        private int _score = 0;

        private const string BestScoreKey = "BestScore";

        public int BestScore { get; private set; } = 0;

        private void Start()
        {
            BestScore = PlayerPrefs.HasKey(BestScoreKey) ? PlayerPrefs.GetInt(BestScoreKey, BestScore) : 0;
        }

        private void LateUpdate()
        {
            _score = (int)_player.transform.position.x / 2;
            UpdateUI();
        }

        public void OnRestart()
        {
            _score = 0;
        }

        public void UpdateBestScore()
        {
            if (BestScore < _score)
            {
                BestScore = _score;
                Save();
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            foreach (var scoreText in _scoreTexts)
            {
                scoreText.text = _score.ToString();
            }
            
            _bestScoreText.text = BestScore.ToString();
        }

        public void Save()
        {
            PlayerPrefs.SetInt(BestScoreKey, BestScore);
            PlayerPrefs.Save();
        }

        public override void Restart()
        {
            _score = 0;
        }
    }
}
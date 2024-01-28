using UnityEngine;
using UnityEngine.UI;

namespace EndlessRunnerJoker
{
    public class GameController : MonoBehaviour
    {
        [Header("Ссылки")] [SerializeField] private ScoreCounter _scoreCounter;

        [Header("Окна")] [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _endGamePanel;

        [Header("Кнопки")] [SerializeField] private Button[] _openMainMenu;
        [SerializeField] private Button[] _restartGame;
        [SerializeField] private Button[] _buttonPauseMenu;

        [SerializeField] private Button _closeMainMenu;

        private RestartEntite[] _restartEntities;

        private void Awake()
        {
            _restartEntities = FindObjectsOfType<RestartEntite>(true);
        }

        private void Start()
        {
            TogglePause(true);

            _closeMainMenu.onClick.AddListener(CloseMainMenu);

            foreach (var button in _openMainMenu)
            {
                button.onClick.AddListener(OpenMenu);
            }

            foreach (var button in _restartGame)
            {
                button.onClick.AddListener(RestartGame);
            }

            foreach (var button in _buttonPauseMenu)
            {
                button.onClick.AddListener(OpenPauseMenu);
            }
        }

        public void TogglePause(bool pause)
        {
            Time.timeScale = pause ? 0 : 1;
        }

        public void ShowEndGameMenu()
        {
            TogglePause(true);
            _scoreCounter.UpdateBestScore();

            _pauseMenu.SetActive(false);
            _endGamePanel.SetActive(true);
        }


        private void OpenMenu()
        {
            _mainMenu.SetActive(true);
            //_pauseMenu.SetActive(false);

            TogglePause(true);
        }

        private void OpenPauseMenu()
        {
            _pauseMenu.SetActive(!_pauseMenu.activeSelf);
            TogglePause(_pauseMenu.activeSelf);
        }

        private void CloseMainMenu()
        {
            _mainMenu.SetActive(false);
            TogglePause(false);
            RestartGame();
        }

        public void RestartGame()
        {
            // var restart = FindObjectsOfType<MonoBehaviour>(true).OfType<IRestartable>();
            //
            // foreach (var restartable in restart)
            // {
            //     restartable.OnRestart();
            // }
            foreach (var restartEntity in _restartEntities)
            {
                restartEntity.Restart();
            }

            _pauseMenu.SetActive(false);
            _endGamePanel.SetActive(false);

            TogglePause(false);
        }
    }
}
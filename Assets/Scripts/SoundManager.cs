using UnityEngine;

namespace EndlessRunnerJoker
{
    public class SoundManager : MonoBehaviour, ISaved
    {
        [SerializeField] private AudioSource _musicSound;

        public static SoundManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            if (PlayerPrefs.HasKey("Sound"))
            {
                _musicSound.mute = PlayerPrefs.GetInt("Sound") == 1;
            }
            else
            {
                _musicSound.mute = false;
            }
        }

        public void ChangeMasterVolume(float value)
        {
            AudioListener.volume = value;
        }

        public void ToggleMusic()
        {
            _musicSound.mute = !_musicSound.mute;
            Save();
        }

        public void Save()
        {
            PlayerPrefs.SetInt("Sound", _musicSound.mute ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}
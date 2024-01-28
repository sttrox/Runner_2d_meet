using System;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessRunnerJoker
{
    public class ToggleAudio : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(Toggle);
        }

        private void Toggle()
        {
            SoundManager.Instance.ToggleMusic();
        }
    }
}
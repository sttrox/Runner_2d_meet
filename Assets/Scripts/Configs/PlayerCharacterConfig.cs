using Characters;
using UnityEngine;

namespace RunnerMeet.Configs
{
    [CreateAssetMenu(fileName = nameof(PlayerCharacterConfig), menuName = "Configs/" + nameof(PlayerCharacterConfig))]
    public class PlayerCharacterConfig : ScriptableObject
    {
        [SerializeField] private NewPlayerCharacter _playerCharacterPrefab;

        public NewPlayerCharacter PlayerCharacterPrefab => _playerCharacterPrefab;
    }
}
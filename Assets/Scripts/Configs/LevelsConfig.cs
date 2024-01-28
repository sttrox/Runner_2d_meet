using RunnerMeet.Gameplay;
using UnityEngine;

namespace RunnerMeet.Configs
{
    [CreateAssetMenu(fileName = nameof(LevelsConfig), menuName = "Configs/" + nameof(LevelsConfig))]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] private Level _levelPrefab;

        public Level LevelPrefab => _levelPrefab;
    }
}
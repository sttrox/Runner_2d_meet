using System.Collections.Generic;
using UnityEngine;

namespace RunnerMeet
{
    public class BackgroundPool : ObjectPool<Background>
    {
        [SerializeField] private Background _firstPrefab;
        [SerializeField] private List<Background> _prefabs;
        private bool _isSpawnFirst;

        protected override Background CreateObject()
        {
            // if (_isSpawnFirst == false)
            // {
            //     _isSpawnFirst = true;
            //     return Instantiate(_firstPrefab, transform);
            // }

            Background randomPrefab = _prefabs[Random.Range(0, _prefabs.Count)];
            var instantiate = Instantiate(randomPrefab, transform);
            return instantiate;
        }
    }
}
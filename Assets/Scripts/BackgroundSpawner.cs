using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunnerJoker
{
    public class BackgroundSpawner : RestartEntite, IRestartable
    {
        [SerializeField] private GameObject _startBackgroundPrefab;
        [SerializeField] private BackgroundPool _pool;
        [SerializeField] private Transform _player;
        [SerializeField] private float _spawnOffset = 10f;

        private Vector3 _lastSpawnedBackgroundTransform;
        private Background _lastSpawnedBackground;
        private Background _initialBackground;


        private List<Background> _container = new List<Background>();

        void Start()
        {
            // _initialBackground = Instantiate(_startBackgroundPrefab, transform.position, Quaternion.identity);
           // SpawnInitialBackground();
            _lastSpawnedBackgroundTransform = transform.position;
        }

        private void LateUpdate()
        {
            if (_player.position.x > _lastSpawnedBackgroundTransform.x)
            {
                SpawnBackground();
            }
        }

        private void SpawnInitialBackground()
        {
            _initialBackground.gameObject.SetActive(true);
            // _container.Add(_initialBackground);
            _initialBackground.transform.position = Vector3.zero;
            _lastSpawnedBackgroundTransform = _initialBackground.transform.position;
        }

        private void SpawnBackground()
        {
            Background background = _pool.Get();
            Transform backgroundTransform = background.transform;


            Vector3 spawnPosition = new Vector3(_lastSpawnedBackgroundTransform.x + _spawnOffset,
                transform.position.y,
                transform.position.z);

            backgroundTransform.position = spawnPosition;
            backgroundTransform.rotation = Quaternion.identity;

            _lastSpawnedBackgroundTransform = backgroundTransform.position;
            _container.Add(background);
        }

        public void OnRestart()
        {
            // foreach (var background in _container)
            // {
            //     _pool.Put(background);
            //     print("удалил");
            // }
            //
            // _container.Clear();
            // _startBackgroundPrefab = _pool.Get();
            // _startBackgroundPrefab.transform.position = Vector2.zero;
            //
            // // _lastSpawnedBackground = _startBackgroundPrefab;
            // _lastSpawnedBackgroundTransform = _startBackgroundPrefab.transform;
            // //_container.Add(_startBackgroundPrefab);
        }

        public override void Restart()
        {
            foreach (var background in _container)
            {
                _pool.Put(background);
            }

            _container.Clear();
            // _pool.Get();
            // _startBackgroundPrefab.transform.position = Vector2.zero;
            //
            //  //_lastSpawnedBackground = _startBackgroundPrefab;
            // _lastSpawnedBackgroundTransform = _startBackgroundPrefab.transform;
            // //_container.Add(_startBackgroundPrefab);

            // foreach (var background in _container)
            // {
            //     _pool.Put(background);
            // }

            //_container.Clear();


            _lastSpawnedBackgroundTransform = transform.position;
            _startBackgroundPrefab.SetActive(true);

          //  SpawnInitialBackground();
            // Background newStartBackground = Instantiate(_startBackgroundPrefab, Vector2.zero, Quaternion.identity);
            // _container.Add(newStartBackground);
            //
            // _lastSpawnedBackgroundTransform = newStartBackground.transform;
        }
    }
}
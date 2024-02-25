using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Chunks
{
	public class ChunksSpawner : MonoBehaviour
	{
		[SerializeField]
		private Transform _startSpawnPoint;

		[SerializeField]
		private Chunk _startChunkPrefab;

		[SerializeField]
		private List<Chunk> _chunkPrefabs;

		[FormerlySerializedAs("_startSpawnChunks")]
		[SerializeField]
		private int _countStartSpawnChunks = 3;

		[SerializeField]
		private float _distanceToSpawnChunk;

		[SerializeField]
		private float _distanceToDespawnChunk;

		private Transform _playerCharacterPoint;

		private Vector3 _startSpawnPosition;
		private int _countSpawnCunks = 0;
		private Vector3 _nextSpawnChunkPosition;

		private Vector3 _nextSpawnTriggerPosition;
		private Vector3 _nextDespawnTriggerPosition;

		private readonly Queue<Chunk> _spawnedChunks = new Queue<Chunk>();

		public void Construct(Transform playerCharacterPoint)
		{
			_playerCharacterPoint = playerCharacterPoint;
			_startSpawnPosition = _startSpawnPoint.position;
			_nextSpawnChunkPosition = _startSpawnPosition;
		}

		private void Start()
		{
			for (int i = 0; i < _countStartSpawnChunks; i++)
			{
				SpawnChunk(_startChunkPrefab, out _);
			}

			_nextSpawnTriggerPosition = _startSpawnPosition + Vector3.right * _distanceToSpawnChunk;
			_nextDespawnTriggerPosition = _startSpawnPosition + Vector3.right * _distanceToDespawnChunk;
		}

		private void Update()
		{
			if (_playerCharacterPoint.position.x >= _nextSpawnTriggerPosition.x)
			{
				SpawnNextChunk(out var lengthSpawnChunk);
				_nextSpawnTriggerPosition += Vector3.right * lengthSpawnChunk;
			}

			if (_playerCharacterPoint.position.x >= _nextDespawnTriggerPosition.x)
			{
				DespawnLastChunk(out var lengthDespawnChunk);

				_nextDespawnTriggerPosition += Vector3.right * lengthDespawnChunk;
			}
		}

		private void DespawnLastChunk(out float lenghtChunk)
		{
			var chunk = _spawnedChunks.Dequeue();
			lenghtChunk = chunk.Lenght;
			chunk.Destroy();
		}

		private void OnDrawGizmos()
		{
			if (!Application.isPlaying)
			{
				return;
			}

			var triggerAreaSize = new Vector3(0.1f, 5, 0);
			Gizmos.color = Color.yellow;

			Gizmos.DrawCube(_nextSpawnTriggerPosition, triggerAreaSize);

			Gizmos.color = Color.red;
			Gizmos.DrawCube(_nextDespawnTriggerPosition, triggerAreaSize);
		}

		private void SpawnNextChunk(out float chunkLenght)
		{
			int indexSpawnChunk = _countSpawnCunks % _chunkPrefabs.Count;
			var chunkPrefab = _chunkPrefabs[indexSpawnChunk];
			SpawnChunk(chunkPrefab, out chunkLenght);
		}

		private void SpawnChunk(Chunk chunkPrefab, out float chunkLenght)
		{
			var currentSpawnChunkPosition = _nextSpawnChunkPosition;
			var chunkInstance = Instantiate(chunkPrefab, currentSpawnChunkPosition, Quaternion.identity);
			_spawnedChunks.Enqueue(chunkInstance);

			chunkLenght = chunkInstance.Lenght;
			_nextSpawnChunkPosition += Vector3.right * chunkLenght;
		}
	}
}
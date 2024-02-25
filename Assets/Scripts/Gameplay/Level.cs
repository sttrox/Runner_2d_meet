using Gameplay.Chunks;
using UnityEngine;

namespace RunnerMeet.Gameplay
{
	public class Level : MonoBehaviour
	{
		[SerializeField]
		private Transform _spawnPoint;

		[SerializeField]
		private ChunksSpawner _chunksSpawner;

		public ChunksSpawner ChunksSpawner => _chunksSpawner;
		public Vector3 SpawnPoint => _spawnPoint.position;

		public void Destroy()
		{
			Destroy(this.gameObject);
		}
	}
}
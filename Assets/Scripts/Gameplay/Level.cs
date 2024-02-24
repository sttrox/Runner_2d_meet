using UnityEngine;

namespace RunnerMeet.Gameplay
{
	public class Level : MonoBehaviour
	{
		[SerializeField]
		private Transform _spawnPoint;

		public Vector3 SpawnPoint => _spawnPoint.position;

		public void Destroy()
		{
			Destroy(this.gameObject);
		}
	}
}
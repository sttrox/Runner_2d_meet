using System;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Chunks
{
	public class Chunk : MonoBehaviour
	{
		[SerializeField]
		private float _lenght;

		public float Lenght => _lenght;

		public void Destroy()
		{
			Destroy(gameObject);
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawCube(transform.position, Vector3.one + (Vector3.right * _lenght));
		}
	}
}
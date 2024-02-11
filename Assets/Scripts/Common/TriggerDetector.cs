using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	public class TriggerDetector : MonoBehaviour
	{
		[SerializeField]
		private LayerMask layerMask;

		public event Action<GameObject> Detected;

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if ((layerMask & (1 << collider.gameObject.layer)) != 0)
			{
				Detected?.Invoke(collider.gameObject);
			}
		}
	}
}
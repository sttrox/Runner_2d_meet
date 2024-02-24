using Cinemachine;
using UnityEngine;

namespace Gameplay.Cameras
{
	public class CameraDistributor : MonoBehaviour
	{
		[SerializeField]
		private CinemachineVirtualCamera _followVirtualCamera;

		public void SetTargetFollow(Transform target)
		{
			_followVirtualCamera.Follow = target;
		}
	}
}
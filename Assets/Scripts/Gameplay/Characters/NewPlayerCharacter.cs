using System;
using Core;
using RunnerMeet.Inputs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Characters
{
	public class NewPlayerCharacter : MonoBehaviour
	{
		[SerializeField]
		private PlayerAnimator _playerAnimator;

		[SerializeField]
		private MoverOnGround _moverOnGround;

		[FormerlySerializedAs("_diedTriggerDetector")]
		[SerializeField]
		private TriggerDetector _obstacleTriggerDetector;

		private bool isDied;

		public event Action Died;

		public void Construct(IInput playerInput)
		{
			_moverOnGround.Construct(playerInput);
			_obstacleTriggerDetector.Detected += ObstacleTriggerDetectorOnDetected;
		}

		private void Update()
		{
			if (!isDied)
			{
				_playerAnimator.SetMovementState(_moverOnGround.CurrentState);
			}
		}

		public void Destroy()
		{
			Destroy(this.gameObject);
		}

		private void ObstacleTriggerDetectorOnDetected(GameObject obstacle)
		{
			isDied = true;
			_playerAnimator.PlayDead();
			Died?.Invoke();
		}
	}
}
using UnityEngine;

namespace Gameplay.Characters
{
	public class PlayerAnimator : MonoBehaviour
	{
		[SerializeField]
		private Animator _animator;

		private readonly int _jumpKey = Animator.StringToHash("Jump");
		private readonly int _deadKey = Animator.StringToHash("Dead");
		private readonly int _runKey = Animator.StringToHash("Run");

		private MoverState _currentState;
		private MoverState _previousState;

		public void SetMovementState(MoverState moverState)
		{
			_currentState = moverState;

			if (_previousState == _currentState)
			{
				return;
			}

			_previousState = _currentState;

			PlayStateAnimation(moverState);
		}

		public void PlayDead()
		{
			_animator.SetTrigger(_deadKey);
		}

		private void PlayJump()
		{
			_animator.SetTrigger(_jumpKey);
		}

		private void PlayRun()
		{
			_animator.Play(_runKey);
		}

		private void PlayStateAnimation(MoverState moverState)
		{
			switch (moverState)
			{
				case MoverState.Run:
					PlayRun();
					break;
				case MoverState.Jump:
					PlayJump();
					break;
				default:
					Debug.LogError($"[{nameof(PlayerAnimator)}]: Not support state:{moverState}", this);
					return;
			}
		}
	}
}
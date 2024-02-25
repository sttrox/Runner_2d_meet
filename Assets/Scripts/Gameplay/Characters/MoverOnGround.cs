using System;
using RunnerMeet.Inputs;
using UnityEngine;

namespace Gameplay.Characters
{
	public class MoverOnGround : MonoBehaviour
	{
		[SerializeField]
		private float _speed = 5f;

		[SerializeField]
		private float _jumpForce = 10f;

		[SerializeField]
		private Rigidbody2D _rigidbody;

		[SerializeField]
		private Transform _groundCheck;

		[SerializeField]
		private LayerMask _groundLayer;

		private readonly Collider2D[] _raycastBufferGroundCheckColliders = new Collider2D[1];

		private IInput _input;

		private MoverState _currentState;
		private bool _isGrounded;
		private bool _isJumping;

		private bool _isNeedJumpByInput;

		public MoverState CurrentState => _currentState;
		private bool IsIdle => _rigidbody.velocity == Vector2.zero;

		private bool _isStop = false;

		public void Construct(IInput input)
		{
			_input = input;
		}

		private void Update()
		{
			_isNeedJumpByInput = _isNeedJumpByInput || _input.IsJump;
		}

		private void FixedUpdate()
		{
			if (_isStop)
			{
				return;
			}

			_isGrounded = CheckGrounded();

			Move();
			TryApplyJumpForce();

			CalculateCurrentState();
		}

		public void Stop()
		{
			_rigidbody.velocity = Vector2.zero;
			_isStop = true;
		}

		private void TryApplyJumpForce()
		{
			if (_isGrounded && _isNeedJumpByInput)
			{
				_rigidbody.velocity = Vector2.zero;
				_rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
				_isJumping = true;
				_isNeedJumpByInput = false;
			}

			if (IsFalling())
			{
				_isJumping = false;
			}

			bool IsFalling()
			{
				return _rigidbody.velocity.y < 0;
			}
		}

		private void Move()
		{
			Vector2 forwardVelocity = new Vector2(_speed, _rigidbody.velocity.y);
			_rigidbody.velocity = forwardVelocity;
		}

		private bool CheckGrounded()
		{
			int countGroundCollisions = Physics2D.OverlapCircleNonAlloc(_groundCheck.position, 0.2f,
				_raycastBufferGroundCheckColliders, _groundLayer);

			bool result = countGroundCollisions > 0;
			return result;
		}

		private void CalculateCurrentState()
		{
			if (_isGrounded)
			{
				_currentState = IsIdle ? MoverState.Idle : MoverState.Run;
			}

			if (!_isGrounded && _isJumping)
			{
				_currentState = MoverState.Jump;
			}
		}
	}
}
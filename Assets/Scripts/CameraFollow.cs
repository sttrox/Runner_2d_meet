using System;
using UnityEngine;

namespace EndlessRunnerJoker
{
    public class CameraFollow : RestartEntite
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _smoothSpeed = 0.125f;
        [SerializeField] private float _offset = 1;

        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void FixedUpdate()
        {
            if (_target is not null)
            {
                Vector3 desiredPosition = new Vector3(_target.position.x - _offset, transform.position.y,
                    transform.position.z);
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
                transform.position = smoothedPosition;
            }
        }

        public override void Restart()
        {
            transform.position = _startPosition;
        }
    }
}
using System.Collections;
using UnityEngine;

namespace RunnerMeet
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerCharacter : RestartEntite, IRestartable
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private float _seconds = 0.2f;

        private bool _isGrounded;
        private Vector2 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
        }
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Obstacle"))
            {
                StartCoroutine(Die());
            }
        }

        private IEnumerator Die()
        {
            //_animator.SetTrigger(Dead);
            yield return new WaitForSeconds(_seconds);
            _gameController.ShowEndGameMenu();
        }

        public void OnRestart()
        {
            //_animator.Play(Run);
            transform.position = _startPosition;
        }

        public override void Restart()
        {
           // _animator.Play(Run);
            transform.position = _startPosition;
        }
    }
}
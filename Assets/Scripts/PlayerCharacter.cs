using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RunnerMeet
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerCharacter : RestartEntite, IRestartable
    {
        [SerializeField] private LayerMask _uiLayer;
        [SerializeField] private GameController _gameController;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _jumpForce = 10f;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _seconds = 0.2f;

        private readonly int Jump = Animator.StringToHash("Jump");
        private readonly int Dead = Animator.StringToHash("Dead");
        private readonly int Run = Animator.StringToHash("Run");

        private bool _isGrounded;
        private Vector2 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);

            if (_isGrounded)
            {
                if (ShouldJumpFromKeyboard())
                {
                    ApplyJumpForce();
                }

                if (ShouldJumpFromTouch())
                {
                    ApplyJumpForce();
                }
            }
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
            _animator.SetTrigger(Dead);
            yield return new WaitForSeconds(_seconds);
            _gameController.ShowEndGameMenu();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            Vector2 forwardVelocity = new Vector2(_speed, _rigidbody.velocity.y);
            _rigidbody.velocity = forwardVelocity;
        }

        private void ApplyJumpForce()
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _animator.SetTrigger(Jump);
        }

        private bool ShouldJumpFromKeyboard()
        {
            return Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space);
        }

        private bool ShouldJumpFromTouch()
        {
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && CheckPointerOverUI() == false;
        }
        
        private bool CheckPointerOverUI() 
        { 
            PointerEventData eventData = new PointerEventData(EventSystem.current); 
            eventData.position = Input.mousePosition; 
            List<RaycastResult> raycastResults = new List<RaycastResult>(); 
            EventSystem.current.RaycastAll(eventData, raycastResults); 
            
            foreach (RaycastResult raycastResult in raycastResults) 
            { 
                if (1 << raycastResult.gameObject.layer == _uiLayer.value) 
                { 
                    return true; 
                } 
            } 
            return false; 
        }

        public void OnRestart()
        {
            _animator.Play(Run);
            transform.position = _startPosition;
        }

        public override void Restart()
        {
            _animator.Play(Run);
            transform.position = _startPosition;
        }
    }
}
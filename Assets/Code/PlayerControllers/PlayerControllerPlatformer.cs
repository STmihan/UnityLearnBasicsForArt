using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.PlayerControllers
{
    public class PlayerControllerPlatformer : MonoBehaviour
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _jumpForce = 10f;
        
        private Vector2 _direction;
        private bool _jumpPressed;
        private bool _isGrounded;
        
        
        private const float DeathTimer = 4f;
        private float _deathTimer;
        private Vector2 _spawnPosition;

        private void Start()
        {
            _spawnPosition = transform.position + Vector3.up;
        }

        private void Update()
        {
            _direction = _playerInput.actions["Move"].ReadValue<Vector2>();
            _jumpPressed = _playerInput.actions["Jump"].WasPressedThisFrame();

            ProcessAnimations(_direction);

            var distance = 1f;
            _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, distance, LayerMask.GetMask("Ground")).collider != null;
            if (!_isGrounded)
            {
                _deathTimer += Time.deltaTime;
                if (_deathTimer >= DeathTimer)
                {
                    _rigidbody.linearVelocity = Vector2.zero;
                    transform.position = _spawnPosition;
                    _deathTimer = 0f;
                }
            }
            else
            {
                _deathTimer = 0f;
            }
            
            if (_jumpPressed)
            {
                Jump();
            }
        }

        private void FixedUpdate()
        {
            Move(_direction);
        }

        private void ProcessAnimations(Vector2 direction)
        {
            var isMoving = _isGrounded && direction.x != 0;
            var isJumping = !_isGrounded && _rigidbody != null && _rigidbody.linearVelocity.y > 0;
            
            if (_animator != null)
            {
                _animator.SetBool(IsMoving, isMoving);
                _animator.SetBool(IsJumping, isJumping);
            }
            
            if (_spriteRenderer != null)
            {
                _spriteRenderer.flipX = direction.x < 0;
            }
        }

        private void Move(Vector2 direction)
        {
            if (_rigidbody == null) return;

            Vector2 velocity = _rigidbody.linearVelocity;
            velocity.x = direction.x * _moveSpeed;
            _rigidbody.linearVelocity = velocity;
        }
        
        private void Jump()
        {
            if (_isGrounded && _rigidbody != null)
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}
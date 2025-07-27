using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.PlayerControllers
{
    public class PlayerControllerIso : MonoBehaviour
    {
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private float _sprintSpeedMultiplier = 2f;
    
        private Camera _camera;
        private Vector2 _direction;
        private bool _isSprinting;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            _direction = _playerInput.actions["Move"].ReadValue<Vector2>();
            _isSprinting = _playerInput.actions["Sprint"].IsPressed();
        
            SmoothFollowCamera();
            ProcessAnimations(_direction);
        }

        private void FixedUpdate()
        {
            Move(_direction);
        }

        private void SmoothFollowCamera()
        {
            if (_camera == null) return;

            Vector3 targetPosition = transform.position;
            targetPosition.z = _camera.transform.position.z;

            _camera.transform.position = Vector3.Lerp(_camera.transform.position, targetPosition, Time.deltaTime * 5f);
        }

        private void ProcessAnimations(Vector2 direction)
        {
            if (_animator == null) return;

            _animator.SetFloat(MoveX, direction.x);
            _animator.SetFloat(MoveY, direction.y);
        }
        
        private void Move(Vector2 direction)
        {
            if (_rigidbody2D == null) return;
            
            var speed = _isSprinting ? _moveSpeed * _sprintSpeedMultiplier : _moveSpeed;
            speed *= Time.fixedDeltaTime;

            Vector2 movement = direction.normalized * speed;
            _rigidbody2D.MovePosition(_rigidbody2D.position + movement);
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

namespace CDB.Input
{
    public class PlayerMove : MonoBehaviour
    {
        public float moveSpeed = 5f;

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _moveCameraAnimationAmount = 0.7f; // Смещение от базовой позиции
        [SerializeField] private float _durationAnimation = 0.5f;

        private InputAction _move;
        private Vector3 _moveDirection3D;
        private Vector2 _inputVector; //Сохраняем значение ввода

        private Sequence _animationSequence;
        private Vector3 _cameraBasePosition;

        private void Awake()
        {
            _move = _playerInput.actions["Move"];

            _move.performed += OnMovePerformed;
            _move.canceled += OnMoveCanceled;

            _cameraBasePosition = _cameraTransform.localPosition;
        }

        private void OnEnable()
        {
            _move.Enable();
        }

        private void OnDisable()
        {
            _move.performed -= OnMovePerformed;
            _move.canceled -= OnMoveCanceled;

            _move.Disable();
        }


        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            _inputVector = context.ReadValue<Vector2>();
            StartAnimationPlayer();
            UpdateMoveDirection();


        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            _inputVector = Vector2.zero;
            StopAnimationPlayer();
            UpdateMoveDirection();
        }


        private void UpdateMoveDirection()
        {

            Vector3 playerForward = transform.forward;
            playerForward.y = 0;
            playerForward.Normalize();


            Vector3 playerRight = transform.right;
            playerRight.y = 0;
            playerRight.Normalize();

            _moveDirection3D = (playerForward * _inputVector.y + playerRight * _inputVector.x).normalized;

        }


        private void FixedUpdate()
        {
            _characterController.Move(_moveDirection3D * moveSpeed * Time.fixedDeltaTime);
        }

        private void StartAnimationPlayer()
        {
           
            _animationSequence?.Kill();
            _animationSequence = DOTween.Sequence();

            _animationSequence.Append(
                _cameraTransform.DOLocalMoveY(_cameraBasePosition.y - _moveCameraAnimationAmount, _durationAnimation)
                .SetEase(Ease.Linear)
            );

            _animationSequence.Append(
                _cameraTransform.DOLocalMoveY(_cameraBasePosition.y + _moveCameraAnimationAmount, _durationAnimation)
                .SetEase(Ease.Linear)
            );

            _animationSequence.SetLoops(-1, LoopType.Yoyo);  
        }

         private void StopAnimationPlayer()
        {
            _animationSequence?.Kill();
            _cameraTransform.DOLocalMove(_cameraBasePosition, _durationAnimation);

        }

        private void OnDestroy()
        {
            _animationSequence.Kill();
        }
    }
}

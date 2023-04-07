using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerCharacterMovement : MonoBehaviour
    {
        [SerializeField] private InputActionAsset actions;
        [SerializeField] private float gravity = 20.0f;
        [SerializeField] private float jumpHeight;
        private CharacterController _characterController;
        private Vector3 _moveDirection = Vector3.zero;
        private InputAction _moveAction;
        private InputAction _jumpAction;
        private PlayerStats _playerStats;

        [Inject]
        private void Construct(PlayerStats playerStats)
        {
            _playerStats = playerStats;
        }

        private void OnEnable() => actions.Enable();

        private void OnDisable() => actions.Disable();

        private void Awake()
        {
            _moveAction = actions.FindActionMap("Player").FindAction("Move");
            _jumpAction = actions.FindActionMap("Player").FindAction("Jump");
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector3 forward = Vector3.forward;
            Vector3 right = Vector3.right;
            float curSpeedX = _playerStats.GetSpeed() * _moveAction.ReadValue<Vector2>().y;
            float curSpeedY = _playerStats.GetSpeed() * _moveAction.ReadValue<Vector2>().x;
            float movementDirectionY = _moveDirection.y;
            _moveDirection = forward * curSpeedX + right * curSpeedY;

            if (_jumpAction.WasPerformedThisFrame() && IsGrounded())
            {
                _moveDirection.y = jumpHeight * Time.deltaTime;
            }
            else
            {
                _moveDirection.y = movementDirectionY;
            }

            if (!IsGrounded())
            {
                _moveDirection.y -= gravity * Time.deltaTime;
            }

            _characterController.Move(_moveDirection * Time.deltaTime);
        }

        private bool IsGrounded() => _characterController.isGrounded;
    }
}
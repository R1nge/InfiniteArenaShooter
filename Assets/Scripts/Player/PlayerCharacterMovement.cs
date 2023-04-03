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
        private CharacterController _characterController;
        private Vector3 _moveDirection = Vector3.zero;
        private InputAction _moveAction;
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
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (_characterController.isGrounded)
            {
                Vector3 forward = Vector3.back;
                Vector3 right = Vector3.left;
                float curSpeedX = _playerStats.GetSpeed() * _moveAction.ReadValue<Vector2>().y;
                float curSpeedY = _playerStats.GetSpeed() * _moveAction.ReadValue<Vector2>().x;
                _moveDirection = forward * curSpeedX + right * curSpeedY;
            }

            _moveDirection.y -= gravity * Time.deltaTime;

            _characterController.Move(_moveDirection * Time.deltaTime);
        }
    }
}
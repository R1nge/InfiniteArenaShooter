using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerRotationController : MonoBehaviour
    {
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask ignoreLayers;
        private Camera _mainCamera;

        private void Awake() => _mainCamera = Camera.main;

        private void Update()
        {
            LookAtMouse();
        }

        private void LookAtMouse()
        {
            var mouse = _mainCamera.ScreenPointToRay(Mouse.current.position.value);
            if (Physics.Raycast(mouse, out var hit,rayDistance, ~ignoreLayers))
            {
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            }
        }
    }
}
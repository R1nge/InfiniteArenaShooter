using System;
using PlayerWeapons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private InputActionAsset actions;
        [SerializeField] private GameObject[] characters;
        private InputAction _selectCube, _selectSphere, _selectPyramid;
        private int _lastCharacterIndex;

        public event Action<EnemyType> OnCharacterChangedEvent;

        private void Awake()
        {
            var map = actions.FindActionMap("Player");
            _selectCube = map.FindAction("SelectCubeCharacter");
            _selectSphere = map.FindAction("SelectSphereCharacter");
            _selectPyramid = map.FindAction("SelectPyramidCharacter");
        }

        private void Update()
        {
            if (_selectCube.WasPressedThisFrame())
            {
                SelectCharacter(0);
                OnCharacterChangedEvent?.Invoke(EnemyType.Cube);
            }
            else if (_selectSphere.WasPressedThisFrame())
            {
                SelectCharacter(1);
                OnCharacterChangedEvent?.Invoke(EnemyType.Sphere);
            }
            else if (_selectPyramid.WasPressedThisFrame())
            {
                SelectCharacter(2);
                OnCharacterChangedEvent?.Invoke(EnemyType.Pyramid);
            }
        }

        private void SelectCharacter(int index)
        {
            if (_lastCharacterIndex == index) return;
            characters[_lastCharacterIndex].SetActive(false);
            _lastCharacterIndex = index;
            characters[_lastCharacterIndex].SetActive(true);
        }
    }
}
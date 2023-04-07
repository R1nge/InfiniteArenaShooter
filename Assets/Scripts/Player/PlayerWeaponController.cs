using PlayerWeapons;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset actions;
        [SerializeField] private Transform weaponParent;
        private AutoWeapon _currentWeapon;
        private InputAction _attackAction;
        private WeaponDataManager _weaponDataManager;

        [Inject]
        private void Construct(WeaponDataManager weaponDataManager)
        {
            _weaponDataManager = weaponDataManager;
        }

        private void Awake()
        {
            _weaponDataManager.OnLoadCompleted += WeaponDataManagerOnLoadCompleted;
            _attackAction = actions.FindActionMap("Player").FindAction("Attack");
        }

        private void WeaponDataManagerOnLoadCompleted()
        {
            var weapon = _weaponDataManager.SpawnSavedWeapon();
            weapon.SetData(_weaponDataManager.GetWeaponData(weapon.GetData().GetName()));
            SetWeapon(weapon);
        }

        private void Update()
        {
            if (_attackAction.IsPressed())
            {
                _currentWeapon.Attack();
            }
        }

        private void SetWeapon(AutoWeapon autoWeapon)
        {
            if (_currentWeapon != null)
            {
                Destroy(_currentWeapon.gameObject);
            }

            _currentWeapon = autoWeapon;
            _currentWeapon.transform.parent = weaponParent;
            _currentWeapon.transform.SetLocalPositionAndRotation(Vector3.zero, quaternion.identity);
        }
    }
}
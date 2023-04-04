using System.Collections;
using UnityEngine;
using Zenject;

namespace PlayerWeapons
{
    public class AutoWeapon : MonoBehaviour
    {
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private Transform shootPoint;
        private int _currentAmmoAmount;
        private float _nextFire;
        private bool _isReloading;
        private BulletPool _bulletPool;

        public WeaponData GetData() => weaponData;
        public void SetData(WeaponData newData) => weaponData = newData;

        [Inject]
        private void Construct(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }

        private void Awake()
        {
            _currentAmmoAmount = weaponData.clipSize;
        }

        public void Attack()
        {
            if (_currentAmmoAmount > 0)
            {
                Shoot();
            }
            else
            {
                if (!_isReloading)
                {
                    StartCoroutine(Reload_c());
                }
            }
        }

        private void Shoot()
        {
            if (Time.time > _nextFire)
            {
                _nextFire = Time.time + 1 / (weaponData.fireRate / 60f);
                _currentAmmoAmount--;
                var bulletInstance = _bulletPool.GetFromPool(shootPoint.position, Quaternion.identity);
                bulletInstance.SetDamage(weaponData.damage);
                bulletInstance.AddForce(shootPoint.forward, weaponData.bulletSpeed);
            }
        }

        private IEnumerator Reload_c()
        {
            _isReloading = true;
            yield return new WaitForSeconds(weaponData.reloadTime);
            _isReloading = false;
            _currentAmmoAmount = weaponData.clipSize;
        }
    }
}
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
        private EnemyType _enemyType;

        [Inject]
        private void Construct(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }

        public WeaponData GetData() => weaponData;

        public void SetData(WeaponData data)
        {
            weaponData = data;
            _currentAmmoAmount = weaponData.GetClipSize();
        }

        public void SetBulletType(EnemyType newType) => _enemyType = newType;

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
                _nextFire = Time.time + 1 / (weaponData.GetFireRate() / 60);
                _currentAmmoAmount--;
                var bulletInstance = _bulletPool.GetFromPool(shootPoint.position, Quaternion.identity);
                bulletInstance.SetType(_enemyType);
                bulletInstance.SetDamage(weaponData.GetDamage());
                bulletInstance.AddForce(shootPoint.forward, weaponData.GetBulletSpeed());
            }
        }

        private IEnumerator Reload_c()
        {
            _isReloading = true;
            yield return new WaitForSeconds(weaponData.GetReloadTime());
            _isReloading = false;
            _currentAmmoAmount = weaponData.GetClipSize();
        }
    }
}
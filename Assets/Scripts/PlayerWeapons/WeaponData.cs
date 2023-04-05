using System;
using UnityEngine;

namespace PlayerWeapons
{
    [Serializable]
    public class WeaponData
    {
        //Any change will break previous save
        [SerializeField] private string name;
        [SerializeField] private float damage;
        [SerializeField] private float fireRate;
        [SerializeField] private float reloadTime;
        [SerializeField] private int clipSize;
        [SerializeField] private int bulletSpeed;
        [SerializeField] private int basePrice = 1;

        [SerializeField, HideInInspector] private bool _isUnlocked;
        [SerializeField, HideInInspector] private int _damageLevel;
        [SerializeField, HideInInspector] private int _fireRateLevel;
        [SerializeField, HideInInspector] private int _reloadTimeLevel;
        [SerializeField, HideInInspector] private int _clipSizeLevel;

        private static readonly float DamageModifier = 1.05f;
        private static readonly float FireRateModifier = 1.1f;
        private static readonly float ReloadTimeModifier = 0.1f;
        private static readonly int ClipSizeModifier = 1;

        private static readonly float PriceLevelModifier = 1.25f;

        public string GetName() => name;
        public void SetName(string value) => name = value;
        public float GetDamage() => damage;
        public float GetFireRate() => fireRate * _fireRateLevel;
        public float GetReloadTime() => reloadTime;
        public int GetClipSize() => clipSize;
        public int GetBulletSpeed() => bulletSpeed;
        public bool IsUnlocked() => _isUnlocked;
        public int GetDamageLevel() => _damageLevel;
        public int GetFireRateLevel() => _fireRateLevel;
        public int GetReloadTimeLevel() => _reloadTimeLevel;
        public int GetClipSizeLevel() => _reloadTimeLevel;
        public float GetDamagePrice() => basePrice * (_damageLevel * PriceLevelModifier);
        public float GetFireRatePrice() => basePrice * (_fireRateLevel * PriceLevelModifier);
        public float GetReloadTimePrice() => basePrice * (_reloadTimeLevel * PriceLevelModifier);
        public float GetClipSizePrice() => basePrice * (_clipSizeLevel * PriceLevelModifier);

        public void IncreaseDamage()
        {
            _damageLevel++;
            damage += DamageModifier * _damageLevel;
        }

        public void IncreaseFireRate()
        {
            _fireRateLevel++;
            fireRate += FireRateModifier * _fireRateLevel;
        }

        public void DecreaseReloadTime()
        {
            _reloadTimeLevel++;
            reloadTime -= ReloadTimeModifier * _reloadTimeLevel;
        }

        public void IncreaseClipSize()
        {
            _clipSizeLevel++;
            clipSize += ClipSizeModifier * _clipSizeLevel;
        }

        public string Stringify()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
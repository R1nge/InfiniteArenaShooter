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

        [SerializeField] private bool isUnlocked;
        [SerializeField] private int damageLevel;
        [SerializeField] private int fireRateLevel;
        [SerializeField] private int reloadTimeLevel;
        [SerializeField] private int clipSizeLevel;

        private static readonly float DamageModifier = 1.05f;
        private static readonly float FireRateModifier = 1.1f;
        private static readonly float ReloadTimeModifier = 0.1f;
        private static readonly int ClipSizeModifier = 1;

        private static readonly float PriceLevelModifier = 1.25f;

        public string GetName() => name;
        public void SetName(string value) => name = value;
        public float GetDamage() => damage;

        public float GetNextDamage()
        {
            var nextDamage = damage;
            nextDamage += DamageModifier * damageLevel;
            return nextDamage;
        }

        public float GetFireRate() => fireRate;

        public float GetNextFireRate()
        {
            var nextFireRate = fireRate;
            nextFireRate += FireRateModifier * fireRateLevel;
            return nextFireRate;
        }

        public float GetReloadTime() => reloadTime;

        public float GetNextReloadTime()
        {
            var nextReloadTime = reloadTime;
            nextReloadTime -= ReloadTimeModifier * reloadTime;
            return nextReloadTime;
        }

        public int GetClipSize() => clipSize;

        public int GetNextClipSize()
        {
            var nextClipSize = clipSize;
            nextClipSize += ClipSizeModifier;
            return nextClipSize;
        }

        public int GetBulletSpeed() => bulletSpeed;
        public bool IsUnlocked() => isUnlocked;
        public void SetLockState(bool value) => isUnlocked = value;
        public int GetDamageLevel() => damageLevel;
        public int GetFireRateLevel() => fireRateLevel;
        public int GetReloadTimeLevel() => reloadTimeLevel;
        public int GetClipSizeLevel() => reloadTimeLevel;
        public float GetDamagePrice() => basePrice * (damageLevel * PriceLevelModifier);
        public float GetFireRatePrice() => basePrice * (fireRateLevel * PriceLevelModifier);
        public float GetReloadTimePrice() => basePrice * (reloadTimeLevel * PriceLevelModifier);
        public float GetClipSizePrice() => basePrice * (clipSizeLevel * PriceLevelModifier);

        public void IncreaseDamage()
        {
            damageLevel++;
            damage += DamageModifier * damageLevel;
        }

        public void IncreaseFireRate()
        {
            fireRateLevel++;
            fireRate += FireRateModifier * fireRateLevel;
        }

        public void DecreaseReloadTime()
        {
            reloadTimeLevel++;
            reloadTime -= ReloadTimeModifier * reloadTimeLevel;
        }

        public void IncreaseClipSize()
        {
            clipSizeLevel++;
            clipSize += ClipSizeModifier * clipSizeLevel;
        }

        public string Stringify()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
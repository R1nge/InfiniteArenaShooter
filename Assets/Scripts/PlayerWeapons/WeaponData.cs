using System;

namespace PlayerWeapons
{
    [Serializable]
    public class WeaponData
    {
        public string name;
        public int damage;
        public int fireRate;
        public float reloadTime;
        public int clipSize;
        public int bulletSpeed;
    }
}
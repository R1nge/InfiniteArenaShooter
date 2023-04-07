namespace UI.Buttons
{
    public class DamageUpgradeButton : UpgradeButton
    {
        protected override void Init()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = weaponData.GetDamagePrice().ToString("#.##") + "$";
            value.text = $"{weaponData.GetDamage()}";
        }

        public override void OnHover()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = weaponData.GetDamagePrice().ToString("#.##") + "$";
            value.text = $"{weaponData.GetDamage()} -> {weaponData.GetNextDamage()}";
        }

        public override void OnExit()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = weaponData.GetDamagePrice().ToString("#.##") + "$";
            value.text = $"{weaponData.GetDamage()}";
        }

        public override void Upgrade()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            weaponData.IncreaseDamage();
            value.text = $"{weaponData.GetDamage()} -> {weaponData.GetNextDamage()}";
            upgradePrice.text = weaponData.GetDamagePrice().ToString("#.##") + "$";
        }
    }
}
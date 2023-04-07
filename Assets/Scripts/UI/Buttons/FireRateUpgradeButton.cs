namespace UI.Buttons
{
    public class FireRateUpgradeButton : UpgradeButton
    {
        protected override void Init()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = weaponData.GetFireRatePrice().ToString("#.##") + "$";
            value.text = $"{weaponData.GetFireRate()}";
        }

        public override void OnHover()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = weaponData.GetFireRatePrice().ToString("#.##") + "$";
            value.text = $"{weaponData.GetFireRate()} -> {weaponData.GetNextFireRate()}";
        }

        public override void OnExit()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = weaponData.GetFireRatePrice().ToString("#.##") + "$";
            value.text = $"{weaponData.GetFireRate()}";
        }

        public override void Upgrade()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            weaponData.IncreaseFireRate();
            value.text = $"{weaponData.GetFireRate()} -> {weaponData.GetNextFireRate()}";
            upgradePrice.text = weaponData.GetFireRatePrice().ToString("#.##") + "$";
        }
    }
}
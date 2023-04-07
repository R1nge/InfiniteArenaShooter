namespace UI.Buttons
{
    public class ReloadUpgradeButton : UpgradeButton
    {
        protected override void Init()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = weaponData.GetReloadTimePrice().ToString("#.##") + "$";
            value.text = $"{weaponData.GetReloadTime()}";
        }

        public override void OnHover()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = weaponData.GetReloadTimePrice().ToString("#.##") + "$";
            value.text = $"{weaponData.GetReloadTime()} -> {weaponData.GetNextReloadTime()}";
        }

        public override void OnExit()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = weaponData.GetReloadTimePrice().ToString("#.##") + "$";
            value.text = $"{weaponData.GetReloadTime()}";
        }

        public override void Upgrade()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            weaponData.DecreaseReloadTime();
            value.text = $"{weaponData.GetReloadTime()} -> {weaponData.GetNextReloadTime()}";
            upgradePrice.text = weaponData.GetReloadTimePrice().ToString("#.##") + "$";
        }
    }
}
namespace UI.Buttons
{
    public class ClipSizeUpgradeButton : UpgradeButton
    {
        protected override void Init()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = weaponData.GetClipSizePrice().ToString("#.##") + "$";
            value.text = $"{weaponData.GetClipSize()}";
        }

        public override void OnHover()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = weaponData.GetClipSizePrice().ToString("#.##") + "$";
            value.text = $"{weaponData.GetClipSize()} -> {weaponData.GetNextClipSize()}";
        }

        public override void OnExit()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = weaponData.GetClipSizePrice().ToString("#.##") + "$";
            value.text = $"{weaponData.GetClipSize()}";
        }

        public override void Upgrade()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            weaponData.IncreaseClipSize();
            value.text = $"{weaponData.GetClipSize()} -> {weaponData.GetNextClipSize()}";
            upgradePrice.text = weaponData.GetClipSizePrice().ToString("#.##") + "$";
        }
    }
}
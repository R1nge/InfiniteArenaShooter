namespace UI.Buttons
{
    public class ClipSizeUpgradeButton : UpgradeButton
    {
        protected override void Init()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = weaponData.GetClipSizePrice().ToString("#.##") + "$";
            button.interactable = Wallet.GetMoney() >= weaponData.GetClipSizePrice();
            value.text = $"{weaponData.GetClipSize()}";
        }

        public override void OnHover()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = weaponData.GetClipSizePrice().ToString("#.##") + "$";
            button.interactable = Wallet.GetMoney() >= weaponData.GetClipSizePrice();
            value.text = $"{weaponData.GetClipSize()} -> {weaponData.GetNextClipSize()}";
        }

        public override void OnExit()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = weaponData.GetClipSizePrice().ToString("#.##") + "$";
            button.interactable = Wallet.GetMoney() >= weaponData.GetClipSizePrice();
            value.text = $"{weaponData.GetClipSize()}";
        }

        public override void Upgrade()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            weaponData.IncreaseClipSize();
            value.text = $"{weaponData.GetClipSize()} -> {weaponData.GetNextClipSize()}";
            upgradePrice.text = weaponData.GetClipSizePrice().ToString("#.##") + "$";
            button.interactable = Wallet.GetMoney() >= weaponData.GetClipSizePrice();
            Wallet.Spend(weaponData.GetClipSizePrice());
        }

        protected override void UpdateButtonState(int money)
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            button.interactable = Wallet.GetMoney() >= weaponData.GetClipSizePrice();
        }
    }
}
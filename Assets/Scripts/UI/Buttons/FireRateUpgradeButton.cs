namespace UI.Buttons
{
    public class FireRateUpgradeButton : UpgradeButton
    {
        protected override void Init()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = weaponData.GetFireRatePrice().ToString("#.##") + "$";
            button.interactable = Wallet.GetMoney() >= weaponData.GetFireRatePrice();
            value.text = $"{weaponData.GetFireRate()}";
        }

        public override void OnHover()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = weaponData.GetFireRatePrice().ToString("#.##") + "$";
            button.interactable = Wallet.GetMoney() >= weaponData.GetFireRatePrice();
            value.text = $"{weaponData.GetFireRate()} -> {weaponData.GetNextFireRate()}";
        }

        public override void OnExit()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = weaponData.GetFireRatePrice().ToString("#.##") + "$";
            button.interactable = Wallet.GetMoney() >= weaponData.GetFireRatePrice();
            value.text = $"{weaponData.GetFireRate()}";
        }

        public override void Upgrade()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            weaponData.IncreaseFireRate();
            value.text = $"{weaponData.GetFireRate()} -> {weaponData.GetNextFireRate()}";
            upgradePrice.text = weaponData.GetFireRatePrice().ToString("#.##") + "$";
            button.interactable = Wallet.GetMoney() >= weaponData.GetFireRatePrice();
            Wallet.Spend(weaponData.GetFireRatePrice());
        }

        protected override void UpdateButtonState(int money)
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            button.interactable = Wallet.GetMoney() >= weaponData.GetFireRatePrice();
        }
    }
}
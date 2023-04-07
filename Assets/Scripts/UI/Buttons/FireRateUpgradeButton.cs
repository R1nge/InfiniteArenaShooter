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
            Wallet.Spend(weaponData.GetFireRatePrice());
            weaponData.IncreaseFireRate();
            button.interactable = Wallet.GetMoney() >= weaponData.GetFireRatePrice();
            value.text = $"{weaponData.GetFireRate()} -> {weaponData.GetNextFireRate()}";
            upgradePrice.text = weaponData.GetFireRatePrice().ToString("#.##") + "$";
        }

        protected override void UpdateButtonState(int money)
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            button.interactable = Wallet.GetMoney() >= weaponData.GetFireRatePrice();
        }
    }
}
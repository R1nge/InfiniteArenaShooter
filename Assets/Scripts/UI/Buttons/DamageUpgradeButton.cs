﻿namespace UI.Buttons
{
    public class DamageUpgradeButton : UpgradeButton
    {
        protected override void Init()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = $"{weaponData.GetDamagePrice()}$";
            button.interactable = Wallet.GetMoney() >= weaponData.GetDamagePrice();
            value.text = $"{weaponData.GetDamage()}";
        }

        public override void OnHover()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = $"{weaponData.GetDamagePrice()}$";
            button.interactable = Wallet.GetMoney() >= weaponData.GetDamagePrice();
            value.text = $"{weaponData.GetDamage()} -> {weaponData.GetNextDamage()}";
        }

        public override void OnExit()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            upgradePrice.text = $"{weaponData.GetDamagePrice()}$";
            button.interactable = Wallet.GetMoney() >= weaponData.GetDamagePrice();
            value.text = $"{weaponData.GetDamage()}";
        }

        public override void Upgrade()
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            weaponData.IncreaseDamage();
            value.text = $"{weaponData.GetDamage()} -> {weaponData.GetNextDamage()}";
            upgradePrice.text = $"{weaponData.GetDamagePrice()}$";
            button.interactable = Wallet.GetMoney() >= weaponData.GetDamagePrice();
            Wallet.Spend(weaponData.GetDamagePrice());
        }

        protected override void UpdateButtonState(int money)
        {
            var weaponData = WeaponDataManager.GetWeaponData(weapon.GetData().GetName());
            button.interactable = Wallet.GetMoney() >= weaponData.GetDamagePrice();
        }
    }
}
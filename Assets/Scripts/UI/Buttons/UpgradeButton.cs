using PlayerWeapons;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Buttons
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private AutoWeapon weapon;
        private WeaponDataManager _weaponDataManager;

        [Inject]
        private void Construct(WeaponDataManager weaponDataManager)
        {
            _weaponDataManager = weaponDataManager;
        }

        public void OnDamageHover()
        {
            text.text = _weaponDataManager.GetWeaponData(weapon.GetData().GetName()).GetDamage().ToString("#.##");
        }

        public void OnDamagedExit()
        {
            text.text = _weaponDataManager.GetWeaponData(weapon.GetData().GetName()).GetDamagePrice().ToString("#.##");
        }

        public void OnFireRateHover()
        {
            text.text = _weaponDataManager.GetWeaponData(weapon.GetData().GetName()).GetFireRate().ToString("#.##");
        }

        public void OnFireRateExit()
        {
            text.text = _weaponDataManager.GetWeaponData(weapon.GetData().GetName()).GetFireRatePrice().ToString("#.##");
        }

        public void OnReloadTimeHover()
        {
            text.text = _weaponDataManager.GetWeaponData(weapon.GetData().GetName()).GetReloadTime().ToString("#.##");
        }

        public void OnReloadTimeExit()
        {
            text.text = _weaponDataManager.GetWeaponData(weapon.GetData().GetName()).GetReloadTimePrice().ToString("#.##");
        }

        public void OnClipSizeHover()
        {
            text.text = _weaponDataManager.GetWeaponData(weapon.GetData().GetName()).GetClipSize().ToString("#.##");
        }

        public void OnClipSizeExit()
        {
            text.text = _weaponDataManager.GetWeaponData(weapon.GetData().GetName()).GetClipSizePrice().ToString("#.##");
        }

        public void UpgradeDamage()
        {
            _weaponDataManager.GetWeaponData(weapon.GetData().GetName()).IncreaseDamage();
            text.text = _weaponDataManager.GetWeaponData(weapon.GetData().GetName()).GetDamage().ToString("#.##");
        }

        public void UpgradeFireRate()
        {
            _weaponDataManager.GetWeaponData(weapon.GetData().GetName()).IncreaseFireRate();
            text.text = _weaponDataManager.GetWeaponData(weapon.GetData().GetName()).GetFireRate().ToString("#.##");
        }

        public void UpgradeReloadTime()
        {
            _weaponDataManager.GetWeaponData(weapon.GetData().GetName()).DecreaseReloadTime();
            text.text = _weaponDataManager.GetWeaponData(weapon.GetData().GetName()).GetReloadTime().ToString("#.##");
        }

        public void UpgradeClipSize()
        {
            _weaponDataManager.GetWeaponData(weapon.GetData().GetName()).IncreaseClipSize();
            text.text = _weaponDataManager.GetWeaponData(weapon.GetData().GetName()).GetClipSize().ToString("#.##");
        }
    }
}
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
            text.text = weapon.GetData().GetFireRate().ToString("#.##");
        }

        public void OnFireRateExit()
        {
            text.text = weapon.GetData().GetFireRatePrice().ToString("#.##");
        }

        public void OnReloadTimeHover()
        {
            text.text = weapon.GetData().GetReloadTime().ToString("#.##");
        }

        public void OnReloadTimeExit()
        {
            text.text = weapon.GetData().GetReloadTimePrice().ToString("#.##");
        }

        public void OnClipSizeHover()
        {
            text.text = weapon.GetData().GetClipSize().ToString("#.##");
        }

        public void OnClipSizeExit()
        {
            text.text = weapon.GetData().GetClipSizePrice().ToString("#.##");
        }

        public void UpgradeDamage()
        {
            _weaponDataManager.GetWeaponData(weapon.GetData().GetName()).IncreaseDamage();
            text.text = _weaponDataManager.GetWeaponData(weapon.GetData().GetName()).GetDamage().ToString("#.##");
        }

        public void UpgradeFireRate()
        {
            weapon.GetData().IncreaseFireRate();
            text.text = weapon.GetData().GetFireRate().ToString("#.##");
        }

        public void UpgradeReloadTime()
        {
            weapon.GetData().DecreaseReloadTime();
            text.text = weapon.GetData().GetReloadTime().ToString("#.##");
        }

        public void UpgradeClipSize()
        {
            weapon.GetData().IncreaseClipSize();
            text.text = weapon.GetData().GetClipSize().ToString("#.##");
        }
    }
}
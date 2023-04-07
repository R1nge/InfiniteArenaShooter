using System;
using PlayerWeapons;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Buttons
{
    public abstract class UpgradeButton : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI value;
        [SerializeField] protected TextMeshProUGUI upgradePrice;
        [SerializeField] protected AutoWeapon weapon;
        protected WeaponDataManager WeaponDataManager;

        [Inject]
        private void Construct(WeaponDataManager weaponDataManager)
        {
            WeaponDataManager = weaponDataManager;
        }

        private void Awake() => Init();

        protected abstract void Init();

        public abstract void OnHover();

        public abstract void OnExit();

        public abstract void Upgrade();
    }
}
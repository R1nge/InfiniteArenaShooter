using System;
using PlayerWeapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Buttons
{
    public abstract class UpgradeButton : MonoBehaviour
    {
        [SerializeField] protected Button button;
        [SerializeField] protected TextMeshProUGUI value;
        [SerializeField] protected TextMeshProUGUI upgradePrice;
        [SerializeField] protected AutoWeapon weapon;
        protected WeaponDataManager WeaponDataManager;
        protected Wallet Wallet;

        [Inject]
        private void Construct(WeaponDataManager weaponDataManager, Wallet wallet)
        {
            WeaponDataManager = weaponDataManager;
            Wallet = wallet;
        }

        private void Awake()
        {
            Wallet.OnMoneyAmountChanged += UpdateButtonState;
            Init();
        }

        protected abstract void Init();

        public abstract void OnHover();

        public abstract void OnExit();

        public abstract void Upgrade();

        protected abstract void UpdateButtonState(int money);

        private void OnDestroy() => Wallet.OnMoneyAmountChanged -= UpdateButtonState;
    }
}
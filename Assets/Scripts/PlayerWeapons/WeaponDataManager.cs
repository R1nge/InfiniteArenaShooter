using System;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using UnityEngine;
using Zenject;

namespace PlayerWeapons
{
    public class WeaponDataManager : MonoBehaviour
    {
        [SerializeField] private AutoWeapon[] weapons;
        private Dictionary<string, WeaponData> _data;
        private readonly string _identifier = "weaponData";
        private DiContainer _diContainer;
        private int _lastWeaponIndex;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public AutoWeapon GetWeapon(int index)
        {
            return _diContainer.InstantiatePrefabForComponent<AutoWeapon>(weapons[index]);
        }

        public AutoWeapon GetSavedWeapon()
        {
            return _diContainer.InstantiatePrefabForComponent<AutoWeapon>(weapons[_lastWeaponIndex]);
        }

        private void Awake()
        {
            if (SaveGame.Exists(_identifier))
                Load();
            else
                Save();
        }

        private void Save()
        {
            _data = new Dictionary<string, WeaponData>(weapons.Length);
            
            for (int i = 0; i < weapons.Length; i++)
            {
                _data[weapons[i].GetData().name] = weapons[i].GetData();
            }
            
            PlayerPrefs.SetInt("LastWeaponIndex", _lastWeaponIndex);
            PlayerPrefs.Save();
            
            SaveGame.Save(_identifier, _data);
        }

        private void Load()
        {
            _data = SaveGame.Load<Dictionary<string, WeaponData>>(_identifier);

            for (int i = 0; i < weapons.Length; i++)
            {
                if (_data.TryGetValue(weapons[i].GetData().name, out WeaponData weaponData))
                {
                    weapons[i].SetData(weaponData);
                }
            }

            _lastWeaponIndex = PlayerPrefs.GetInt("LastWeaponIndex");
        }
        
        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus == false)
            {
                Save();
            }
        }

        private void OnApplicationQuit() => Save();
    }
}
﻿using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Zenject;

namespace PlayerWeapons
{
    public class WeaponDataManager : MonoBehaviour
    {
        [SerializeField] private AutoWeapon[] weapons;
        private Dictionary<string, WeaponData> _data = new();
        private int _lastWeaponIndex;
        private DiContainer _diContainer;
        private PlayFabManager _playFabManager;
        private const string LastWeaponIndexString = "LastWeaponIndex";

        public event Action OnLoadCompleted;

        [Inject]
        private void Construct(DiContainer diContainer, PlayFabManager playFabManager)
        {
            _diContainer = diContainer;
            _playFabManager = playFabManager;
        }

        public AutoWeapon SpawnWeapon(int index)
        {
            return _diContainer.InstantiatePrefabForComponent<AutoWeapon>(weapons[index]);
        }

        public AutoWeapon SpawnSavedWeapon()
        {
            return _diContainer.InstantiatePrefabForComponent<AutoWeapon>(weapons[_lastWeaponIndex]);
        }

        public WeaponData GetWeaponData(string weaponName) => _data[weaponName];

        private void Awake()
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                _data.Add(weapons[i].GetData().GetName(), weapons[i].GetData());
            }

            Load();
        }

        private void Load()
        {
            var getLastWeaponIndex = new GetUserDataRequest
            {
                PlayFabId = _playFabManager.GetUserID(), Keys = new List<string> { LastWeaponIndexString }
            };
            
            PlayFabClientAPI.GetUserData(getLastWeaponIndex, result =>
            {
                if (result.Data.ContainsKey(LastWeaponIndexString))
                {
                    _lastWeaponIndex = Int32.Parse(result.Data[LastWeaponIndexString].Value);
                    print($"WeaponData: Loaded last weapon index: {_lastWeaponIndex}");
                }
            }, error => { Debug.LogError(error.GenerateErrorReport(), this); });
            
            var weaponKeyDict = new Dictionary<string, WeaponData>.KeyCollection(_data);
            var weaponsKeyList = new List<string>(weapons.Length);

            foreach (var key in weaponKeyDict)
            {
                weaponsKeyList.Add(key);
            }

            var getWeapons = new GetUserDataRequest
            {
                PlayFabId = _playFabManager.GetUserID(), Keys = weaponsKeyList
            };

            PlayFabClientAPI.GetUserData(getWeapons, result =>
            {
                for (int i = 0; i < result.Data.Count; i++)
                {
                    var weapon = result.Data[weapons[i].GetData().GetName()].Value;
                    _data[weapons[i].GetData().GetName()] = JsonUtility.FromJson<WeaponData>(weapon);
                }

                OnLoadCompleted?.Invoke();

                print("WeaponData: Loaded save");
            }, error => { Debug.LogError(error.GenerateErrorReport(), this); });
        }

        private void Save()
        {
            var updateWeapons = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>(weapons.Length)
            };

            for (int i = 0; i < weapons.Length; i++)
            {
                updateWeapons.Data[weapons[i].GetData().GetName()] = _data[weapons[i].GetData().GetName()].Stringify();
            }

            PlayFabClientAPI.UpdateUserData(updateWeapons, OnSaveSuccess, OnSaveError);

            var updateLastWeaponIndex = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>(1)
            };

            if (updateLastWeaponIndex.Data.ContainsKey(LastWeaponIndexString))
            {
                updateLastWeaponIndex.Data[LastWeaponIndexString] = _lastWeaponIndex.ToString();
            }
            else
            {
                updateLastWeaponIndex.Data.Add(LastWeaponIndexString, _lastWeaponIndex.ToString());
            }

            PlayFabClientAPI.UpdateUserData(updateLastWeaponIndex, OnSaveSuccess, OnSaveError);
        }

        private void OnSaveSuccess(UpdateUserDataResult result)
        {
            print("WeaponData: Save successful");
        }

        private void OnSaveError(PlayFabError error)
        {
            Debug.LogError("WeaponData: Save failed");
            Debug.LogError(error.GenerateErrorReport());
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus == false)
            {
                Save();
            }
        }
    }
}
﻿using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        private void Start()
        {
            Debug.Log("start", this);

            for (int i = 0; i < weapons.Length; i++)
            {
                _data.Add(weapons[i].GetData().GetName(), weapons[i].GetData());
            }

            _playFabManager.OnLogIn += Load;
        }

        private void Load()
        {
            var dic = new Dictionary<string, WeaponData>.KeyCollection(_data);
            var keysList = new List<string>(weapons.Length);

            foreach (var key in dic)
            {
                keysList.Add(key);
            }

            var request = new GetUserDataRequest
            {
                PlayFabId = _playFabManager.GetUserID(), Keys = keysList
            };

            PlayFabClientAPI.GetUserData(request, result =>
            {
                for (int i = 0; i < result.Data.Count; i++)
                {
                    var weapon = result.Data[weapons[i].GetData().GetName()].Value;
                    _data[weapons[i].GetData().GetName()] = JsonUtility.FromJson<WeaponData>(weapon);
                    print("Loaded save");
                    SceneManager.LoadScene("Home");
                }
            }, error =>
            {
                SceneManager.LoadScene("Home");
            });
        }

        private void Save()
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>(weapons.Length)
            };

            for (int i = 0; i < weapons.Length; i++)
            {
                request.Data[weapons[i].GetData().GetName()] = _data[weapons[i].GetData().GetName()].Stringify();
            }

            PlayFabClientAPI.UpdateUserData(request, OnSaveSuccess, OnSaveError);
        }

        private void OnSaveSuccess(UpdateUserDataResult result)
        {
            print("Save successful");
        }

        private void OnSaveError(PlayFabError error)
        {
            Debug.LogError("Save failed");
            Debug.LogError(error.GenerateErrorReport());
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus == false)
            {
                Save();
            }
        }

        private void OnDestroy()
        {
            _playFabManager.OnLogIn -= Load;
        }
    }
}
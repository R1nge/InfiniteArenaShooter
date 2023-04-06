using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Zenject;

public class Wallet : MonoBehaviour
{
    private int _money;
    private const string MoneyString = "Money";
    private PlayFabManager _playFabManager;

    private int Money
    {
        get => _money;

        set
        {
            _money = value;
            OnMoneyAmountChanged?.Invoke(_money);
            Save();
        }
    }

    public event Action<int> OnMoneyAmountChanged;


    [Inject]
    private void Construct(PlayFabManager playFabManager)
    {
        _playFabManager = playFabManager;
    }
    
    private void Start() => Load();

    public void Earn(int amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("Trying to earn negative value");
            return;
        }

        Money += amount;
    }

    public bool Spend(int amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("Trying to spend negative value");
            return false;
        }

        if (Money - amount >= 0)
        {
            Money -= amount;
            return true;
        }

        return false;
    }

    private void Load()
    {
        var keys = new List<string> { MoneyString };

        var request = new GetUserDataRequest
        {
            PlayFabId = _playFabManager.GetUserID(), Keys = keys
        };

        PlayFabClientAPI.GetUserData(request, result =>
        {
            if (result.Data.ContainsKey(MoneyString))
            {
                var moneyAmount = result.Data[MoneyString].Value;
                _money = Int32.Parse(moneyAmount);
                print($"Wallet: Loaded save. Money: {_money}");
            }
            else
            {
                print("Wallet: Save not found");
            }
        }, error => { Debug.LogError(error.GenerateErrorReport(), this); });
    }

    private void Save()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>(1)
        };

        if (request.Data.TryGetValue(MoneyString, out string value))
        {
            request.Data[MoneyString] = _money.ToString();
        }
        else
        {
            request.Data.Add(MoneyString, _money.ToString());
        }


        PlayFabClientAPI.UpdateUserData(request, OnSaveSuccess, OnSaveError);
    }

    private void OnSaveSuccess(UpdateUserDataResult result)
    {
        print($"Wallet: Saved money: {result}");
    }

    private void OnSaveError(PlayFabError error)
    {
        Debug.LogError($"Wallet: Error while saving money {error.GenerateErrorReport()}", this);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            Save();
        }
    }
}
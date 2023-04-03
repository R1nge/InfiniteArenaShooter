using TMPro;
using UnityEngine;
using Zenject;

public class WalletUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    private Wallet _wallet;

    [Inject] 
    private void Construct(Wallet wallet)
    {
        _wallet = wallet;
    }

    private void Awake() => _wallet.OnMoneyAmountChanged += UpdateUI;

    private void UpdateUI(int money) => moneyText.text = money.ToString();

    private void OnDestroy() => _wallet.OnMoneyAmountChanged -= UpdateUI;
}
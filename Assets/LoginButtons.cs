using System;
using PlayFab;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class LoginButtons : MonoBehaviour
{
    private PlayFabManager _playFabManager;
    private Button _logInButton;
    private TextField _usernameInput, _passwordInput;


    [Inject]
    private void Construct(PlayFabManager playFabManager)
    {
        _playFabManager = playFabManager;
    }

    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _usernameInput = root.Q<TextField>("username-input");
        _passwordInput = root.Q<TextField>("password-input");
        _logInButton = root.Q<Button>("login-button");
        _logInButton.clicked += Login;
    }

    public void Register()
    {
        _playFabManager.Register();
    }

    private void Login()
    {
        _playFabManager.SetUserName(_usernameInput.value);
        _playFabManager.SetPassword(_passwordInput.value);
        _playFabManager.Login();
    }

    private void OnDestroy() => _logInButton.clicked -= Login;
}

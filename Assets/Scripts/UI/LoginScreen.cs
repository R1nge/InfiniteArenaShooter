using PlayFab;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace UI
{
    public class LoginScreen : MonoBehaviour
    {
        private PlayFabManager _playFabManager;
        private VisualElement _loginContent, _registerContent;
        private Button _logInTabButton, _registerTabButton;
        private Button _logInButton, _registerButton;
        private TextField _loginUsernameInput, _loginPasswordInput;
        private TextField _registerUsernameInput, _registerEmailInput, _registerPasswordInput;
        private VisualElement _root;

        [Inject]
        private void Construct(PlayFabManager playFabManager)
        {
            _playFabManager = playFabManager;
        }

        private void OnEnable()
        {
            _loginUsernameInput = _root.Q<TextField>("login-username-input");
            _loginUsernameInput.SetPlaceholderText("username", false);
            _loginPasswordInput = _root.Q<TextField>("login-password-input");
            _loginPasswordInput.SetPlaceholderText("password", true);
        
            _registerUsernameInput = _root.Q<TextField>("register-username-input");
            _registerUsernameInput.SetPlaceholderText("username",false);
            _registerEmailInput = _root.Q<TextField>("register-email-input");
            _registerEmailInput.SetPlaceholderText("email", false);
            _registerPasswordInput = _root.Q<TextField>("register-password-input");
            _registerPasswordInput.SetPlaceholderText("password", true);
        }

        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;

            _logInTabButton = _root.Q<Button>("login-tab-button");
            _logInTabButton.clicked += LogInTabButtonClicked;

            _registerTabButton = _root.Q<Button>("register-tab-button");
            _registerTabButton.clicked += RegisterButtonClicked;

            _loginContent = _root.Q<VisualElement>("login-content");
            _registerContent = _root.Q<VisualElement>("register-content");

            _registerEmailInput = _root.Q<TextField>("email-input");

            _logInButton = _root.Q<Button>("login-button");
            _logInButton.clicked += Login;
        
            _registerButton = _root.Q<Button>("register-button");
            _registerButton.clicked += Register;
        }

        private void LogInTabButtonClicked()
        {
            _loginContent.visible = true;
            _registerContent.visible = false;
        }

        private void RegisterButtonClicked()
        {
            _loginContent.visible = false;
            _registerContent.visible = true;
        }

        private void Register()
        {
            _playFabManager.SetUserName(_registerUsernameInput.value);
            _playFabManager.SetEmail(_registerEmailInput.value);
            _playFabManager.SetPassword(_registerPasswordInput.value);
            _playFabManager.Register();
        }

        private void Login()
        {
            _playFabManager.SetUserName(_loginUsernameInput.value);
            _playFabManager.SetPassword(_loginPasswordInput.value);
            _playFabManager.Login();
        }

        private void OnDestroy()
        {
            _logInTabButton.clicked -= LogInTabButtonClicked;
            _registerTabButton.clicked -= RegisterButtonClicked;
            _logInButton.clicked -= Login;
            _registerButton.clicked -= Register;
        }
    }
}
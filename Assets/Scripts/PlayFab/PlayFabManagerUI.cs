using TMPro;
using UnityEngine;
using Zenject;

namespace PlayFab
{
    public class PlayFabManagerUI : MonoBehaviour
    {
        [SerializeField] private TMP_InputField userNameInput, passwordInput;
        private PlayFabManager _playFabManager;
        
        [Inject]
        private void Construct(PlayFabManager playFabManager)
        {
            _playFabManager = playFabManager;
        }

        public void SetUsername()
        {
            _playFabManager.SetUserName(userNameInput.text);
        }

        public void SetPassword()
        {
            _playFabManager.SetPassword(passwordInput.text);
        }

        public void Register()
        {
            _playFabManager.Register();
        }

        public void Login()
        {
            _playFabManager.Login();
        }
    }
}
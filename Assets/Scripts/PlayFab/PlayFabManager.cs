using System.Collections;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayFab
{
    public class PlayFabManager : MonoBehaviour
    {
        private string _email, _userName, _password;
        private string _userID;

        public string GetUserID() => _userID;

        public void SetUserName(string username)
        {
            _userName = username;
        }

        public void SetEmail(string email)
        {
            _email = email;
        }

        public void SetPassword(string password)
        {
            _password = password;
        }

        public void Register()
        {
            if (_userName == string.Empty || _email == string.Empty || _password == string.Empty)
            {
                return;
            }

            var request = new RegisterPlayFabUserRequest
            {
                Username = _userName,
                Email = _email,
                Password = _password,
                DisplayName = _userName
            };

            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterError);
        }

        private void OnRegisterSuccess(RegisterPlayFabUserResult result)
        {
            print("Successful register");
        }

        private void OnRegisterError(PlayFabError error)
        {
            Debug.LogError("Error while registering");
            Debug.LogError(error.GenerateErrorReport());
        }

        public void Login()
        {
            if (_userName == string.Empty || _password == string.Empty)
            {
                return;
            }

            var request = new LoginWithPlayFabRequest
            {
                Username = _userName,
                Password = _password
            };

            PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginError);
        }

        private void OnLoginSuccess(LoginResult result)
        {
            print("Successful login");
            _userID = result.PlayFabId;
            StartCoroutine(WaitForConnection());
        }

        private IEnumerator WaitForConnection()
        {
            yield return null;
            SceneManager.LoadScene("Home");
        }

        private void OnLoginError(PlayFabError error)
        {
            Debug.LogError("Error while logging in");
            Debug.LogError(error.GenerateErrorReport());
        }
    }
}
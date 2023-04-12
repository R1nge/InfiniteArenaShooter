using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Zenject;
using Cursor = UnityEngine.Cursor;

namespace UI
{
    public class DeathScreen : MonoBehaviour
    {
        private UIDocument _deathScreen;
        private Button _goHomeButton;
        private PlayerCharacter _player;
        private PlayerHealth _playerHealth;

        [Inject]
        private void Construct(PlayerCharacter player)
        {
            _player = player;
        }

        private void OnEnable()
        {
            _goHomeButton.clicked += GoHomeButtonClicked;
        }

        private void Awake()
        {
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _playerHealth.OnDiedEvent += ShowUI;
            _deathScreen = GetComponent<UIDocument>();
            var root = _deathScreen.rootVisualElement;
            _goHomeButton = root.Q<Button>("go-home-button");
            root.style.display = DisplayStyle.None;
        }

        private void GoHomeButtonClicked()
        {
            print("Home");
            SceneManager.LoadScene("Home");
        }

        private void ShowUI()
        {
            var root = _deathScreen.rootVisualElement;
            root.style.display = DisplayStyle.Flex;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void OnDestroy()
        {
            _playerHealth.OnDiedEvent -= ShowUI;
            _goHomeButton.clicked -= GoHomeButtonClicked;
        }
    }
}
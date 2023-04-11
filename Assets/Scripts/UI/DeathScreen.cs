using Player;
using UnityEngine;
using Zenject;

namespace UI
{
    public class DeathScreen : MonoBehaviour
    {
        [SerializeField] private GameObject deathScreen;
        private PlayerCharacter _player;
        private PlayerHealth _playerHealth;

        [Inject]
        private void Construct(PlayerCharacter player)
        {
            _player = player;
        }

        private void Awake()
        {
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _playerHealth.OnDiedEvent += ShowUI;
        }

        private void ShowUI() => deathScreen.SetActive(true);

        private void OnDestroy() => _playerHealth.OnDiedEvent -= ShowUI;
    }
}
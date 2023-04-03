using Player;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int reward;
        [SerializeField] private int damage;
        private PlayerCharacter _player;
        private EnemyHealth _enemyHealth;
        private Wallet _wallet;
        private NavMeshAgent _agent;
        private Rigidbody _rigidbody;
        private bool _landed;
        private const string Arena = "Arena";

        [Inject]
        private void Construct(PlayerCharacter player, Wallet wallet)
        {
            _player = player;
            _wallet = wallet;
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _rigidbody = GetComponent<Rigidbody>();
            _enemyHealth = GetComponent<EnemyHealth>();
            _enemyHealth.OnDiedEvent += EnemyOnDiedEvent;
        }

        private void EnemyOnDiedEvent() => _wallet.Earn(reward);

        private void Update()
        {
            if (!_landed) return;
            _agent.SetDestination(_player.transform.position);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(damage);
            }

            if (collision.gameObject.CompareTag(Arena))
            {
                OnLanded();
            }
        }

        private void OnLanded()
        {
            if (_landed) return;
            _rigidbody.isKinematic = true;
            _agent.enabled = _landed = true;
            _agent.SetDestination(_player.transform.position);
        }

        private void OnDestroy() => _enemyHealth.OnDiedEvent -= EnemyOnDiedEvent;
    }
}
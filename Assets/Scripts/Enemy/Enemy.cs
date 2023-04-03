using Player;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int damage;
        private PlayerCharacter _player;
        private NavMeshAgent _agent;
        private Rigidbody _rigidbody;
        private bool _landed;
        private const string Arena = "Arena";
        private Wallet _wallet;

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
        }

        private void Update()
        {
            if(!_landed) return;
            _agent.SetDestination(_player.transform.position);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(Arena))
            {
                OnLanded();
            }

            if (collision.gameObject.TryGetComponent(out PlayerCharacter player))
            {
                player.TakeDamage(damage);
            }
        }

        private void OnLanded()
        {
            if (_landed) return;
            _rigidbody.isKinematic = true;
            _agent.enabled = _landed = true;
            _agent.SetDestination(_player.transform.position);
        }
    }
}
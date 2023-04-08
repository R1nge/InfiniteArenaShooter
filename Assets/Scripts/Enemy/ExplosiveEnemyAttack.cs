using System;
using System.Collections;
using Player;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class ExplosiveEnemyAttack : MonoBehaviour
    {
        [SerializeField] private float distanceToExplode;
        [SerializeField] private int explodeDamage;
        [SerializeField] private float explodeRadius;
        [SerializeField] private float explodeDelay;
        [SerializeField] private LayerMask playerMask;
        private EnemyHealth _enemyHealth;
        private bool _exploded;
        private PlayerCharacter _player;
        private readonly Collider[] _hitColliders = new Collider[1];

        [Inject]
        private void Construct(PlayerCharacter player)
        {
            _player = player;
        }

        private void Awake()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
        }

        private void Update()
        {
            if (_exploded) return;
            if (Vector3.Distance(transform.position, _player.transform.position) <= distanceToExplode)
            {
                StartCoroutine(Explode());
            }
        }

        private IEnumerator Explode()
        {
            _exploded = true;
            yield return new WaitForSeconds(explodeDelay);
            Physics.OverlapSphereNonAlloc(transform.position, explodeRadius, _hitColliders, playerMask);

            if (_hitColliders[0].TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(explodeDamage);
            }
            
            _enemyHealth.TakeDamage(999999999f);
        }
    }
}
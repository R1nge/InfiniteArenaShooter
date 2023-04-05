using UnityEngine;

namespace PlayerWeapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        private float _damage;
        private Rigidbody _rigidbody;

        public void SetDamage(float damage) => _damage = damage;

        public void AddForce(Vector3 dir, int speed) => _rigidbody.AddForce(dir * speed);

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
            }

            gameObject.SetActive(false);
        }

        private void OnBecameInvisible() => gameObject.SetActive(false);
    }
}
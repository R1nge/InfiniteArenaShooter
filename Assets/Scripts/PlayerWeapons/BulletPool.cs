using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PlayerWeapons
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private int poolSize;
        private List<Bullet> _pool;
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        private void Awake() => CreatePool(bulletPrefab, poolSize);

        public void CreatePool(Bullet go, int amount)
        {
            if (_pool != null)
            {
                Debug.LogWarning("Trying to create a pool, when one already exists", this);
                return;
            }

            _pool = new List<Bullet>(amount);

            for (int i = 0; i < amount; i++)
            {
                var instance = _diContainer.InstantiatePrefab(go.gameObject, Vector3.zero, Quaternion.identity, null);
                instance.SetActive(false);
                _pool.Add(instance.GetComponent<Bullet>());
            }
        }

        public Bullet GetFromPool(Vector3 pos, Quaternion rot)
        {
            if (_pool == null)
            {
                Debug.LogError("Trying to get an object from uninitialized pool", this);
                return null;
            }

            for (int i = 0; i < _pool.Count; i++)
            {
                if (_pool[i].gameObject.activeInHierarchy)
                {
                    if (i == _pool.Capacity - 1)
                    {
                        Debug.LogError("Every pooled object is active, can't get one", this);
                        break;
                    }

                    continue;
                }

                _pool[i].transform.SetPositionAndRotation(pos, rot);
                _pool[i].gameObject.SetActive(true);
                return _pool[i];
            }

            return null;
        }
    }
}
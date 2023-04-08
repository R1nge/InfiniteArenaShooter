using System;
using Enemy;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private EnemyWavePreset[] waves;
    [SerializeField] private Vector2 arenaSize;
    private DiContainer _diContainer;
    private int _enemiesRemain;

    public event Action OnWaveClearedEvent;

    [Inject]
    private void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    private void Start() => SpawnWave();

    private void SpawnWave()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            for (int j = 0; j < waves[i].GetEnemies().Length; j++)
            {
                for (int k = 0; k < waves[i].GetEnemyCount(j); k++)
                {
                    var selectedEnemy = waves[i].GetEnemies()[j];
                    var position = new Vector3(
                        Random.Range(-arenaSize.x / 2f, arenaSize.x / 2f),
                        Random.Range(15, 30),
                        Random.Range(-arenaSize.y / 2f, arenaSize.y / 2f)
                    );

                    var enemy = _diContainer.InstantiatePrefabForComponent<Enemy.Enemy>(selectedEnemy, position,
                        Quaternion.identity, null);
                    enemy.OnDeathEvent += CheckWaveEnd;

                    _enemiesRemain++;
                }
            }
        }
    }

    private void CheckWaveEnd(Enemy.Enemy enemy)
    {
        enemy.OnDeathEvent -= CheckWaveEnd;
        _enemiesRemain--;
        if (_enemiesRemain <= 0)
        {
            OnWaveClearedEvent?.Invoke();
        }
    }
}
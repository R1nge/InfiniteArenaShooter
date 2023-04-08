using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Enemy.Enemy[] enemies;
    [SerializeField] private Vector2 possibleEnemyAmount;
    [SerializeField] private Vector2 arenaSize;
    private DiContainer _diContainer;
    private int _enemyRemain;

    public event Action OnWaveClearedEvent;

    [Inject]
    private void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    private void Start() => SpawnWave();

    private void SpawnWave()
    {
        var currentWaveEnemyAmount = Random.Range(possibleEnemyAmount.x, possibleEnemyAmount.y);
        for (int i = 0; i < currentWaveEnemyAmount; i++)
        {
            var selectedEnemy = enemies[Random.Range(0, enemies.Length)];
            var position = new Vector3(
                Random.Range(-arenaSize.x / 2f, arenaSize.x / 2f),
                Random.Range(15, 30),
                Random.Range(-arenaSize.y / 2f, arenaSize.y / 2f)
            );

            var enemy = _diContainer.InstantiatePrefabForComponent<Enemy.Enemy>(selectedEnemy, position, Quaternion.identity, null);
            enemy.OnDeathEvent += CheckWaveEnd;

            _enemyRemain++;
        }
    }

    private void CheckWaveEnd(Enemy.Enemy enemy)
    {
        enemy.OnDeathEvent -= CheckWaveEnd;
        _enemyRemain--;
        if (_enemyRemain <= 0)
        {
            OnWaveClearedEvent?.Invoke();
        }
    }
}
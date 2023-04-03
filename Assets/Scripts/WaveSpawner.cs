using UnityEngine;
using Zenject;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Enemy.Enemy[] enemies;
    [SerializeField] private Vector2 possibleEnemyAmount;
    [SerializeField] private Vector2 arenaSize;
    private DiContainer _diContainer;

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
            var enemy = enemies[Random.Range(0, enemies.Length)];
            var position = new Vector3(
                Random.Range(-arenaSize.x / 2f, arenaSize.x / 2f),
                Random.Range(15, 30),
                Random.Range(-arenaSize.y / 2f, arenaSize.y / 2f)
            );

            _diContainer.InstantiatePrefab(enemy, position, Quaternion.identity, null);
        }
    }
}
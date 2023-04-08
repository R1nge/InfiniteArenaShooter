using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyWave", menuName = "EnemyWave", order = 0)]
    public class EnemyWavePreset : ScriptableObject
    {
        [SerializeField] private Enemy[] enemies;
        [SerializeField] private int[] count;

        public Enemy[] GetEnemies() => enemies;
        public int GetEnemyCount(int index) => count[index];
    }
}
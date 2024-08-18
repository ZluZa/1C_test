using GD.MinMaxSlider;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/LevelData", fileName = "LevelData")]
public class LevelData : BaseData
{
    [SerializeField] private EnemyFactoryData _enemiesData;
    [MinMaxSlider(1, 100)][SerializeField] private Vector2Int _enemiesAmount;
    [MinMaxSlider(0.01f, 5)][SerializeField] private Vector2 _enemiesSpawnDelay;

    public EnemyFactoryData EnemiesData => _enemiesData;
    public Vector2Int EnemiesAmount => _enemiesAmount;
    public Vector2 EnemiesSpawnDelay => _enemiesSpawnDelay;
}
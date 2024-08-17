using GD.MinMaxSlider;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/LevelData", fileName = "LevelData")]
public class LevelData : BaseData
{
    [MinMaxSlider(1, 100)][SerializeField] private Vector2 enemiesAmount;
    [MinMaxSlider(0.01f, 5)][SerializeField] private Vector2 enemiesSpawnDelay;

    public Vector2 EnemiesAmount => enemiesAmount;
    public Vector2 EnemiesSpawnDelay => enemiesSpawnDelay;
}
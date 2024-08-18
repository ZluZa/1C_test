using System.Collections;
using System.Collections.Generic;
using GD.MinMaxSlider;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyFactoryData", fileName = "EnemyFactoryData")]
public class EnemyFactoryData : BaseData
{
    [MinMaxSlider(0.01f, 10)][SerializeField] private Vector2 enemySpeed;
    [MinMaxSlider(1, 100)][SerializeField] private Vector2 enemyHp;

    public Vector2 EnemySpeed => enemySpeed;
    public Vector2 EnemyHp => enemyHp;
}

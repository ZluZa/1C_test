using System.Collections;
using System.Collections.Generic;
using GD.MinMaxSlider;
using UnityEngine;

public class EnemyFactoryData : BaseData
{
    [MinMaxSlider(-20, 20)][SerializeField] private Vector2 enemySpeed;
    [MinMaxSlider(-20, 20)][SerializeField] private Vector2 enemyHp;
    
    
}

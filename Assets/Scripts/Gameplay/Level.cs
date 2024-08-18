using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;

public class Level : FactoryObject
{
    [SerializeField] private EnemiesFactory _enemiesFactory;
    [SerializeField] private PlayerFactory _playerFactory;
}

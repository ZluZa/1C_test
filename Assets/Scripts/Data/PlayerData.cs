using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerData", fileName = "PlayerData")]

public class PlayerData : BaseData
{
    [SerializeField] private float attackRange;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float movementSpeed;

    public float AttackRange => attackRange;
    public float AttackDamage => attackDamage;
    public float AttackSpeed => attackSpeed;
    public float BulletSpeed => bulletSpeed;
    public float MovementSpeed => movementSpeed;
}

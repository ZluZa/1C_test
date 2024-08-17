using System.Collections;
using System.Collections.Generic;
using GD.MinMaxSlider;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/BonusData", fileName = "BonusData")]

public class BonusData : BaseData
{
    [SerializeField] private float bulletSpeedMod;
    [SerializeField] private float rangeMod;
    [SerializeField] private float attackSpeedMod;
    [SerializeField] private float damagedMod;

    public float BulletSpeedMod => bulletSpeedMod;
    public float RangeMod => rangeMod;
    public float AttackSpeedMod => attackSpeedMod;
    public float DamagedMod => damagedMod;
}

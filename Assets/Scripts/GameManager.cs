using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager
{
    private List<LevelData> _availableLevels;
    private List<BonusData> _availableBonuses;
    private List<PlayerData> _availableSkins;
    public override IEnumerator Init()
    {
        return base.Init();
    }
}

using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.Events;

public class Level : FactoryObject
{
    [SerializeField] private EnemiesFactory _enemiesFactory;
    [SerializeField] private PlayerFactory _playerFactory;

    private LevelData _lData;
    
    [HideInInspector] public Player currentPlayer;

    public override FactoryObject Init(BaseData data)
    {
        _lData = (LevelData) data;
        GameManager gm = (GameManager) CoreGame.Instance.GetManager(typeof(GameManager));
        currentPlayer = _playerFactory.CreatePlayer(gm.AvailableSkins[gm.GetSelectedSkin()]);
        currentPlayer.transform.SetParent(_playerFactory.transform);
        currentPlayer.transform.localPosition = Vector3.zero;
        return base.Init(data);
    }

    public void StartGameplay()
    {
        currentPlayer.StartPlaying();
    }

    private IEnumerator StartEnemyFactory()
    {
        var enemiesDestroyed = 0;
        var enemiesNeededToDestory = Random.Range(_lData.EnemiesAmount.x, _lData.EnemiesAmount.y);
        yield break;
    }
}

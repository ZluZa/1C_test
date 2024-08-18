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
    private int _enemiesToSpawn;
    private int _enemiesSpawned;

    private Coroutine _enemyFactoryCoroutine;

    public UnityEvent onEnemyDestroyed;
    
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
        _enemyFactoryCoroutine = StartCoroutine(StartEnemyFactory());
    }

    public void StopGameplay()
    {
        if (_enemyFactoryCoroutine != null)
            StopCoroutine(_enemyFactoryCoroutine);
    }

    private IEnumerator StartEnemyFactory()
    {
        Debug.Log("START_FACTORY");
        Rect enemiesFactoryRect = _enemiesFactory.GetComponent<RectTransform>().rect;
        _enemiesSpawned = 0;
        _enemiesToSpawn = Random.Range(_lData.EnemiesAmount.x, _lData.EnemiesAmount.y);
        while (_enemiesSpawned < _enemiesToSpawn)
        {
            Debug.Log("FACTORY IN PROGRESS " + _enemiesSpawned + "/" + _enemiesToSpawn);
            var enemy = _enemiesFactory.CreateEnemy(_lData.EnemiesData);
            enemy.onBorderAchieve.AddListener(HitPlayer);
            enemy.onEnemyKilled.AddListener(delegate
            {
                OnEnemyKilled();
                enemy.onEnemyKilled.RemoveAllListeners();
            });
            enemy.transform.SetParent(_enemiesFactory.transform);
            enemy.transform.localPosition = new Vector3(
                Random.Range(enemiesFactoryRect.xMin, enemiesFactoryRect.xMax),
                0, 
                0);
            enemy.transform.localScale = Vector3.one;
            _enemiesSpawned++;
            yield return new WaitForSeconds(Random.Range(_lData.EnemiesSpawnDelay.x, _lData.EnemiesSpawnDelay.y));
            yield return new WaitForEndOfFrame();
        }
    }

    private void HitPlayer()
    {
        Debug.Log("HitPlayer");
    }

    private void OnEnemyKilled()
    {
        Debug.Log("EnemyKilled");
    }
}

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
    
    private int _enemiesKilled;
    private int _hpRemaining;

    private Coroutine _enemyFactoryCoroutine;

    private GameplayUiElement _gameplayUiElement;

    public UnityEvent onEnemyDestroyed;
    
    [HideInInspector] public Player currentPlayer;

    public override FactoryObject Init(BaseData data)
    {
        _lData = (LevelData) data;
        GameManager gm = (GameManager) CoreGame.Instance.GetManager(typeof(GameManager));
        CanvasManager ce = (CanvasManager) CoreGame.Instance.GetManager(typeof(CanvasManager));
        _gameplayUiElement = (GameplayUiElement) ce.GetManager(typeof(GameplayUiElement));
        _hpRemaining = _lData.BaseHp;
        _gameplayUiElement.UpdateHealth(_lData.BaseHp, _lData.BaseHp);
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
        if (currentPlayer != null)
            currentPlayer.StopPlaying();
    }

    private IEnumerator StartEnemyFactory()
    {
        Debug.Log("START_FACTORY");
        Rect enemiesFactoryRect = _enemiesFactory.GetComponent<RectTransform>().rect;
        _enemiesSpawned = 0;
        _enemiesKilled = 0;
        _enemiesToSpawn = Random.Range(_lData.EnemiesAmount.x, _lData.EnemiesAmount.y);
        _gameplayUiElement.UpdateProgress(_enemiesToSpawn, _enemiesToSpawn);
        while (_enemiesSpawned < _enemiesToSpawn)
        {
//            Debug.Log("FACTORY IN PROGRESS " + _enemiesSpawned + "/" + _enemiesToSpawn);
            var enemy = _enemiesFactory.CreateEnemy(_lData.EnemiesData);
            enemy.onBorderAchieve.AddListener(HitBase);
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

    private void HitBase()
    {
        _hpRemaining--;
        _gameplayUiElement.UpdateHealth(_hpRemaining, _lData.BaseHp);
        if (_hpRemaining <= 0)
        {
            StopGameplay();
            currentPlayer.KillPlayer();
            _gameplayUiElement.ShowWinLoseScreen(false);
        }
    }

    private void OnEnemyKilled()
    {
        _enemiesKilled++;
        GameManager gm = (GameManager) CoreGame.Instance.GetManager(typeof(GameManager));
        gm.SetEnemyToGlobalStats();
        _gameplayUiElement.UpdateProgress(_enemiesKilled, _enemiesToSpawn);
        if (_enemiesKilled+(_lData.BaseHp-_hpRemaining) >= _enemiesToSpawn)
        {
            StopGameplay();
            _gameplayUiElement.ShowWinLoseScreen(true);
        }
    }
}

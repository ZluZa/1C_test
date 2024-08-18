using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuElement : CanvasElement
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private TextMeshProUGUI levelNum;
    [SerializeField] private TextMeshProUGUI skinNum;
    [SerializeField] private TextMeshProUGUI globalKillsNum;

    public override IEnumerator Init(CanvasManager cm)
    {
        yield return base.Init(cm);
        startGameButton.onClick.AddListener(StartLevel);
        
        CanvasManager.onLoaderScreenHidden?.AddListener(() => ShowElement(true,
            delegate {
                CanvasManager.onLoaderScreenHidden.RemoveAllListeners();
            }));
        GameManager gameManager = (GameManager) CoreGame.Instance.GetManager(typeof(GameManager));
        UpdateLevelNumber(gameManager.GetSelectedLevel()+1);
        UpdateSkinNumber(gameManager.GetSelectedSkin()+1);
        UpdateGlobalKills(gameManager.GetGlobalEnemyKills());
    }

    public void UpdateLevelNumber(int id)
    {
        levelNum.text = "Level " + id;
    }
    public void UpdateSkinNumber(int id)
    {
        skinNum.text = "Skin " + id;
    }
    public void UpdateGlobalKills(int id)
    {
        globalKillsNum.text = "Global enemies kills: " + id;
    }
    
    public void StartLevel()
    {
        CanvasManager.onLoaderScreenHidden.RemoveAllListeners();
        HideElement(true, delegate {  });
        LoadingScreenElement ls = 
            (LoadingScreenElement) CanvasManager.GetManager(typeof(LoadingScreenElement));
        ls.ShowElement(true, delegate
        {
            GameManager gm = (GameManager) CoreGame.Instance.GetManager(typeof(GameManager));
            gm.StartLevel();
        });
    }

    public override void ShowElement(bool animated, Action onComplete)
    {
        GameManager gameManager = (GameManager) CoreGame.Instance.GetManager(typeof(GameManager));
        UpdateLevelNumber(gameManager.GetSelectedLevel()+1);
        UpdateSkinNumber(gameManager.GetSelectedSkin()+1);
        UpdateGlobalKills(gameManager.GetGlobalEnemyKills());
        base.ShowElement(animated, onComplete);
    }
}
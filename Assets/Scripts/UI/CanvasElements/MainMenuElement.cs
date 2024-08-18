using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuElement : CanvasElement
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private TextMeshProUGUI levelNum;
    public override IEnumerator Init(CanvasManager cm)
    {
        yield return base.Init(cm);
        startGameButton.onClick.AddListener(StartLevel);
        
        CanvasManager.onLoaderScreenHidden?.AddListener(() => ShowElement(true,
            delegate {
                CanvasManager.onLoaderScreenHidden.RemoveAllListeners();
            }));
        GameManager gameManager = (GameManager) CoreGame.Instance.GetManager(typeof(GameManager));
        UpdateLevelNumber(gameManager.GetSelectedLevel());
    }

    public void UpdateLevelNumber(int id)
    {
        levelNum.text = "Level " + id;
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
}
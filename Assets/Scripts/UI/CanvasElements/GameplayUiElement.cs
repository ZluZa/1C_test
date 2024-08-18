using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUiElement : CanvasElement
{
    [SerializeField] private Animation _uiAnimation;
    [SerializeField] private Animation _winLoseScreen;
    
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _winLoseScreenMenu;
    [SerializeField] private Button _continueButton;

    [SerializeField] private TextMeshProUGUI _winLoseText;
    [SerializeField] private TextMeshProUGUI _levelNameText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _progressText;

    private bool winLoseScreenOpened;


    public override IEnumerator Init(CanvasManager cm)
    {
        yield return base.Init(cm);
        _menuButton.onClick.AddListener(GoToMainMenu);
        _winLoseScreenMenu.onClick.AddListener(GoToMainMenu);
        _continueButton.onClick.AddListener(NextLevel);
    }

    public void PrepareStartAnimation()
    {
        ShowElement(false, delegate
        {
            GameManager gm = (GameManager) CoreGame.Instance.GetManager(typeof(GameManager));
            _levelNameText.text = "Level " + (gm.GetSelectedLevel()+1);
            _uiAnimation.Play("InitGameplayUiAnimation");
        });
    }
    public void StartGameAnimation()
    {
        if (winLoseScreenOpened)
            _winLoseScreen.Play("WinLoseScreenDisappear");

        _uiAnimation.Play("StartLevelGameplayUiAnimation");
    }

    public void ShowWinLoseScreen(bool win)
    {
        GameManager gm = (GameManager) CoreGame.Instance.GetManager(typeof(GameManager));
        winLoseScreenOpened = true;
        if (win)
        {
            _winLoseText.text = "Level " + gm.GetSelectedLevel() + 1 + " completed!";
            _continueButton.gameObject.SetActive(true);
            _winLoseScreen.Play("WinLoseScreenWin");
        }
        else
        {
            _winLoseText.text = "Level " + gm.GetSelectedLevel() + 1 + " failed!";
            _continueButton.gameObject.SetActive(false);
            _winLoseScreen.Play("WinLoseScreenLose");
        }
        
    }

    private void GoToMainMenu()
    {
        if (winLoseScreenOpened)
            _winLoseScreen.Play("WinLoseScreenDisappear");
        LoadingScreenElement ls = (LoadingScreenElement) CanvasManager.GetManager(typeof(LoadingScreenElement));
        
        ls.ShowElement(true, delegate
        {
            CanvasManager.HideEveryWindow();
            MainMenuElement mme = (MainMenuElement) CanvasManager.GetManager(typeof(MainMenuElement));
            mme.ShowElement(true, delegate { ls.HideElement(true, delegate {  }); });
        });
    }

    private void NextLevel()
    {
        if (winLoseScreenOpened)
            _winLoseScreen.Play("WinLoseScreenDisappear");
        LoadingScreenElement ls = (LoadingScreenElement) CanvasManager.GetManager(typeof(LoadingScreenElement));

        ls.ShowElement(true, delegate
        {
            CanvasManager.HideEveryWindow();
            GameManager gm = (GameManager) CoreGame.Instance.GetManager(typeof(GameManager));
            if (gm.GetSelectedLevel() + 1 >= gm.AvailableLevels.Count)
            { 
                GoToMainMenu();
            }
            else
            {
                gm.SetLevel(gm.GetSelectedLevel()+1);
                gm.StartLevel();    
            }
        });
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        _healthText.text = "Health remaining: " + currentHealth + "/" + maxHealth;
    }

    public void UpdateProgress(int currentEnemies, int maxEnemies)
    {
        _progressText.text = "Enemies remaining: " + currentEnemies + "/" + maxEnemies;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : MonoBehaviour
{
    [SerializeField] private string showAnimation = "SettingsWindowShow";
    [SerializeField] private string hideAnimation = "SettingsWindowHide";
    
    [SerializeField] private Button _closebutton;
    private Animation _animation;
    private Button _button;
    private bool _opened = false;

    [SerializeField] private List<Button> skinSelectors = new();
    [SerializeField] private List<Button> levelSelectors = new();
    private void Awake()
    {
        _animation = GetComponent<Animation>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ShowWindow);
        _closebutton.onClick.AddListener(HideWindow);
        for (int i = 0; i < skinSelectors.Count; i++)
        {
            var i1 = i;
            skinSelectors[i].onClick.AddListener(() => AssignSkin(i1));
        }
        for (int i = 0; i < levelSelectors.Count; i++)
        {
            var i1 = i;
            levelSelectors[i].onClick.AddListener(() => AssignLevel(i1));
        }
    }

    private void ShowWindow()
    {
        _animation.Play(showAnimation);
        _opened = true;
    }
    private void HideWindow()
    {
        _animation.Play(hideAnimation);
        _opened = false;
    }

    private void AssignLevel(int id)
    {
        GameManager gm = (GameManager) CoreGame.Instance.GetManager(typeof(GameManager));
        CanvasManager ce = (CanvasManager) CoreGame.Instance.GetManager(typeof(CanvasManager));
        MainMenuElement mme = (MainMenuElement) ce.GetManager(typeof(MainMenuElement));
        mme.UpdateLevelNumber(id+1);
        gm.SetLevel(id);
    }
    private void AssignSkin(int id)
    {
        GameManager gm = (GameManager) CoreGame.Instance.GetManager(typeof(GameManager));
        CanvasManager ce = (CanvasManager) CoreGame.Instance.GetManager(typeof(CanvasManager));
        MainMenuElement mme = (MainMenuElement) ce.GetManager(typeof(MainMenuElement));
        mme.UpdateSkinNumber(id+1);
        gm.SetSkin(id);
    }
}

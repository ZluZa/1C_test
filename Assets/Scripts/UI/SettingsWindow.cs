using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    
    [SerializeField] private Button selectorButtonPrefab;
    [SerializeField] private Transform levelSelectorParent;
    [SerializeField] private Transform skinSelectorParent;
    private void Start()
    {
        _animation = GetComponent<Animation>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ShowWindow);
        _closebutton.onClick.AddListener(HideWindow);
        GameManager gm = (GameManager) CoreGame.Instance.GetManager(typeof(GameManager));

        for (int i = 0; i < gm.AvailableLevels.Count; i++)
        {
            var button = Instantiate(selectorButtonPrefab, levelSelectorParent);
            var i1 = i;
            button.GetComponentInChildren<TextMeshProUGUI>().text = (i1+1).ToString();
            button.onClick.AddListener(() => AssignLevel(i1));
        }
        for (int i = 0; i < gm.AvailableSkins.Count; i++)
        {
            var button = Instantiate(selectorButtonPrefab, skinSelectorParent);
            var i1 = i;
            button.GetComponentInChildren<TextMeshProUGUI>().text = (i1+1).ToString();
            button.onClick.AddListener(() => AssignSkin(i1));
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

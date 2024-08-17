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

    private void Awake()
    {
        _animation = GetComponent<Animation>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ShowWindow);
        _closebutton.onClick.AddListener(HideWindow);
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
}

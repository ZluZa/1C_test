using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : MonoBehaviour
{
    [SerializeField] private string showAnimation = "SettingsWindowShow";
    [SerializeField] private string hideAnimation = "SettingsWindowHide";
    
    private Animation _animation;
    private Button _button;
    private bool _opened = false;

    private void Awake()
    {
        _animation = GetComponent<Animation>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ShowHideWindow);
    }

    private void ShowHideWindow()
    {
        _animation.Play(!_opened ? showAnimation : hideAnimation);
        _opened = !_opened;

    }
    
}

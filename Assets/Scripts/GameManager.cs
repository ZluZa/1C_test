using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager
{
    [SerializeField] private LevelFactory _levelFactory;
    private List<LevelData> _availableLevels;
    private List<BonusData> _availableBonuses;
    private List<PlayerData> _availableSkins;

    public List<LevelData> AvailableLevels => _availableLevels;
    public List<PlayerData> AvailableSkins => _availableSkins;
    public List<BonusData> AvailableBonuses => _availableBonuses;

    private int _selectedLevel = 0;
    private int _selectedSkin = 0;
    
    public override IEnumerator Init()
    {
        _availableLevels = new List<LevelData>(Resources.LoadAll<LevelData>("Data/Levels"));
        _availableSkins = new List<PlayerData>(Resources.LoadAll<PlayerData>("Data/Skins"));
        _availableBonuses = new List<BonusData>(Resources.LoadAll<BonusData>("Data/Bonuses"));
        
        LoadPrefs();
        return base.Init();
    }
    
    public int GetSelectedLevel()
    {
        return _selectedLevel + 1;
    }

    public void SetLevel(int id)
    {
        _selectedLevel = id;
    }

    public int GetSelectedSkin()
    {
        return _selectedSkin;
    }

    public void SetSkin(int id)
    {
        _selectedSkin = id;
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetInt("Level", GetSelectedLevel());
        PlayerPrefs.SetInt("Skin", GetSelectedSkin());
        PlayerPrefs.Save();
    }

    public void LoadPrefs()
    {
        SetLevel(PlayerPrefs.GetInt("Level", 0));
        SetSkin(PlayerPrefs.GetInt("Skin", 0));
    }
}

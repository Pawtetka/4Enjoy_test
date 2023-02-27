using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DefaultNamespace;
using UnityEngine;

public class DailyBonusController : MonoBehaviour
{
    [SerializeField] private DailyBonusView dailyBonusView;
    [SerializeField] private DailyBonusService dailyBonusService;

    private DateTime lastDailyBonusDate;
    private long coins;
    
    private const string LastDailyBonusPlayerPref = "lastDailyBonusDate";

    public void Awake()
    {
        LoadData();
        Background.OnBackgroundClick += () => ShowPopup(false);
    }

    public void Start()
    {
        dailyBonusView.ClaimButton.onClick.AddListener(() => ShowPopup(false));
        
        if (DateTime.Now.Date > lastDailyBonusDate)
        {
            coins = dailyBonusService.CountCoinsValue();
            lastDailyBonusDate = DateTime.Now.Date;
            SaveData();
            ShowPopup(true);
        }
    }

    private void ShowPopup(bool isActive)
    {
        if(dailyBonusView.CurrentActiveState == isActive) return;
        if(isActive) dailyBonusView.UpdateCoinsText(coins);
        
        Background.ShowBackground(isActive);
        dailyBonusView.ShowPopup(isActive);
    }

    private void SaveData()
    {
        PlayerPrefs.SetString(LastDailyBonusPlayerPref, lastDailyBonusDate.ToString(CultureInfo.CurrentCulture));
    }

    private void LoadData()
    {
        lastDailyBonusDate = StringToDateTime(PlayerPrefs.GetString(LastDailyBonusPlayerPref));
    }
    
    private DateTime StringToDateTime(string value)
    {
        if(String.IsNullOrEmpty(value)) return DateTime.MinValue.Date;
            
        return DateTime.Parse(value);
    }
}

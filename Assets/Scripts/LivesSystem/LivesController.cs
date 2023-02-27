using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

public class LivesController : MonoBehaviour
{
    [SerializeField] private LivesPanelView panelView;
    [SerializeField] private LivesPopupView popupView;
    [SerializeField] private LivesService livesService;

    // Start is called before the first frame update
    void Awake()
    {
        livesService.TimerValueUpdated += UpdateTimerValue;
        livesService.LivesCountUpdated += UpdateLivesValue;
        Background.OnBackgroundClick += () => ShowLivesPopup(false);
    }

    void Start()
    {
        panelView.OpenLivesPopupButton.onClick.AddListener(() => ShowLivesPopup(true));
        popupView.ClosePopupButton.onClick.AddListener(() => ShowLivesPopup(false));
        popupView.UseLifeButton.onClick.AddListener(UseLife);
        popupView.RefillLivesButton.onClick.AddListener(RefillLives);
        
        UpdateLivesValue(livesService.CurrentLivesCount);
    }

    public void ShowLivesPopup(bool isActive)
    {
        if(popupView.CurrentActiveState == isActive) return;
        UpdateLivesPopupState();
        Background.ShowBackground(isActive);
        popupView.ShowPopup(isActive);
    }

    private void UpdateTimerValue(string newValue)
    {
        panelView.UpdateTimerText(newValue);
        popupView.UpdateTimerText(newValue);
    }
    
    private void UpdateLivesValue(int newValue)
    {
        UpdateLivesPopupState();
        panelView.UpdateLivesCountText(newValue);
        popupView.UpdateLivesCount(newValue);
    }

    private void UpdateLivesPopupState()
    {
        LivesPopupState newState = LivesPopupState.Default;
        if (livesService.CurrentLivesCount <= 0) newState = LivesPopupState.EmptyLives;
        if (livesService.CurrentLivesCount >= livesService.LivesMax) newState = LivesPopupState.FullLives;
        
        popupView.ChangeState(newState);
    }

    private void UseLife()
    {
        livesService.UseLife();
    }

    private void RefillLives()
    {
        livesService.RefillLives();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LivesPopupView : MonoBehaviour
{
    [SerializeField] private ShowHideAnimator animator;
    [SerializeField] private TextMeshProUGUI livesCountText;
    [SerializeField] private TextMeshProUGUI livesTimerText;
    [SerializeField] private Button useLifeBtn;
    [SerializeField] private Button refillLivesBtn;
    [SerializeField] private Button closePopupBtn;

    public TextMeshProUGUI LivesTimerText => livesTimerText; 
    public Button UseLifeButton => useLifeBtn; 
    public Button RefillLivesButton => refillLivesBtn; 
    public Button ClosePopupButton => closePopupBtn; 

    private bool _currentActiveState = false;
    public bool CurrentActiveState => _currentActiveState;
    
    private LivesPopupState currentPopupState = LivesPopupState.EmptyLives;

    public void UpdateTimerText(string newValue)
    {
        livesTimerText.text = newValue;
    }
    
    public void UpdateLivesCount(int newValue)
    {
        if(newValue < 0) return;
        livesCountText.text = newValue.ToString();
    }

    public void ShowPopup(bool isActive)
    {
        if (isActive == _currentActiveState) return;
        
        if(isActive) animator.PlayAnim(ShowHideAnimState.Show);
        else animator.PlayAnim(ShowHideAnimState.Hide);

        _currentActiveState = isActive;
    }

    public void ChangeState(LivesPopupState newState)
    {
        if (currentPopupState == newState) return;

        switch (newState)
        {
            case LivesPopupState.EmptyLives:
                livesTimerText.gameObject.SetActive(true);
                useLifeBtn.gameObject.SetActive(false);
                refillLivesBtn.gameObject.SetActive(true);
                break;
            case LivesPopupState.Default:
                livesTimerText.gameObject.SetActive(true);
                useLifeBtn.gameObject.SetActive(true);
                refillLivesBtn.gameObject.SetActive(true);
                break;
            case LivesPopupState.FullLives:
                livesTimerText.gameObject.SetActive(false);
                useLifeBtn.gameObject.SetActive(true);
                refillLivesBtn.gameObject.SetActive(false);
                break;
            default:
                livesCountText.gameObject.SetActive(true);
                livesTimerText.gameObject.SetActive(true);
                useLifeBtn.gameObject.SetActive(true);
                refillLivesBtn.gameObject.SetActive(true);
                break;
        }

        currentPopupState = newState;
    }
}

public enum LivesPopupState
{
    EmptyLives,
    Default,
    FullLives
}

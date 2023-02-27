using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] private ShowHideAnimator animator;
    [SerializeField] private Image backgroundImg;
    [SerializeField] private Button backgroundBtn;
    
    public static Action<bool> ShowBackground;
    public static Action OnBackgroundClick;

    private bool _currentState = false;

    public void Awake()
    {
        ShowBackground += UpdateBackground;
    }

    private void Start()
    {
        backgroundBtn.onClick.AddListener(OnButtonClick);
        UpdateBackground(_currentState);
    }

    private void UpdateBackground(bool isActive)
    {
        if (isActive == _currentState) return;
        
        if(isActive) animator.PlayAnim(ShowHideAnimState.Show);
        else animator.PlayAnim(ShowHideAnimState.Hide);

        _currentState = isActive;
        backgroundImg.raycastTarget = isActive;
        backgroundBtn.interactable = isActive;
    }

    private void OnButtonClick()
    {
        OnBackgroundClick?.Invoke();
    }
}

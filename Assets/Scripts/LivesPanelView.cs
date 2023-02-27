using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LivesPanelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesCountText;
    [SerializeField] private TextMeshProUGUI livesTimerText;
    [SerializeField] private Button openLivesPopupBtn;

    public TextMeshProUGUI LivesTimerText => livesTimerText;
    public Button OpenLivesPopupButton => openLivesPopupBtn;

    public void UpdateTimerText(string newValue)
    {
        livesTimerText.text = newValue;
    }
    
    public void UpdateLivesCountText(int newValue)
    {
        livesCountText.text = newValue.ToString();
    }
}

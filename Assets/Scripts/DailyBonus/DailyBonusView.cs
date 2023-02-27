using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class DailyBonusView : MonoBehaviour
    {
        [SerializeField] private ShowHideAnimator animator;
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private Button claimBtn;

        public TextMeshProUGUI CoinsText => coinsText;
        public Button ClaimButton => claimBtn;

        private bool _currentActiveState = false;
        public bool CurrentActiveState => _currentActiveState;
        
        public void ShowPopup(bool isActive)
        {
            if (isActive == _currentActiveState) return;
        
            if(isActive) animator.PlayAnim(ShowHideAnimState.Show);
            else animator.PlayAnim(ShowHideAnimState.Hide);

            _currentActiveState = isActive;
        }

        public void UpdateCoinsText(long newValue)
        {
            if(newValue < 0) return;
            coinsText.text = $"{newValue} coins";
        }
    }
}
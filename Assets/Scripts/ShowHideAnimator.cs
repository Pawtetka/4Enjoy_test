using UnityEngine;

namespace DefaultNamespace
{
    public class ShowHideAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private ShowHideAnimState currentState = ShowHideAnimState.Idle;
        
        private static readonly int Show = Animator.StringToHash(SHOWTRIGGER);
        private static readonly int Hide = Animator.StringToHash(HIDETRIGGER);

        private const string SHOWTRIGGER = "Show";
        private const string HIDETRIGGER = "Hide";

        public void PlayAnim(ShowHideAnimState newState)
        {
            if (currentState == newState) return;

            switch (newState)
            {
                case ShowHideAnimState.Show:
                    animator.SetTrigger(Show);
                    break;
                case ShowHideAnimState.Hide:
                    animator.SetTrigger(Hide);
                    break;
                default:
                    break;
            }

            currentState = newState;
        }
    }

    public enum ShowHideAnimState
    {
        Idle,
        Show,
        Hide
    }
}
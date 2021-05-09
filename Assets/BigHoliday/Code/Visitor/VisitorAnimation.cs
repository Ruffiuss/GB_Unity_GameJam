using UnityEngine;
using System;


namespace BigHoliday
{
    internal sealed class VisitorAnimation : MonoBehaviour, IChangeableStates
    {
        #region Properties

        public SpriteRenderer SpriteRenderer { get; private set; }

        public bool IsLooped { get; private set; }

        public event Action<AnimState> OnStateChange;

        #endregion


        #region Methods

        public void SetupAnimation(SpriteRenderer spriteRenderer, bool isLooped)
        {
            SpriteRenderer = spriteRenderer;
            IsLooped = isLooped;
        }

        public void ChangeState(AnimState state)
        {
            OnStateChange.Invoke(state);
        }

        #endregion
    }
}

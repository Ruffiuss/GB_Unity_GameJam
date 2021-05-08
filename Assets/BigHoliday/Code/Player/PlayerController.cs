using UnityEngine;
using System;


namespace BigHoliday
{
    internal sealed class PlayerController : IFixedUpdatable, IChangeableStates
    {
        #region Fields

        private GameObject _playerView;
        private Vector3 _leftScale;
        private Vector3 _rightScale;

        private float _movingThreshold = 0.35f;
        private float _walkSpeed = 4.0f;

        #endregion


        #region Properties

        public SpriteRenderer SpriteRenderer { get; private set; }
        public event Action<AnimState> OnStateChange;
        public bool IsLooped { get; private set; }

        #endregion


        #region ClassLifeCycles

        internal PlayerController(GameObject playerView)
        {
            _playerView = playerView;
            _rightScale = playerView.transform.localScale;
            _leftScale = new Vector3(_rightScale.x * -1, _rightScale.y, _rightScale.z);
            _playerView.TryGetComponent<SpriteRenderer>(out var spriteRenderer);
            SpriteRenderer = spriteRenderer;
            IsLooped = true;
        }

        #endregion


        #region Methods

        public void FixedUpdate(float fixedDeltaTime)
        {
            var xAxisInput = Input.GetAxis("Horizontal");
            if (Mathf.Abs(xAxisInput) > _movingThreshold)
            {
                if (OnStateChange != null) OnStateChange.Invoke(AnimState.Walk);
                _playerView.transform.Translate((Vector3.right * fixedDeltaTime * _walkSpeed) * (xAxisInput < 0 ? -1 : 1));
                _playerView.transform.localScale = (xAxisInput < 0 ? _leftScale : _rightScale);
            }
            else if (OnStateChange != null) OnStateChange.Invoke(AnimState.Idle);
        }

        #endregion
    }
}

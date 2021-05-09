using UnityEngine;
using System;
using System.Collections.Generic;


namespace BigHoliday
{
    internal sealed class PlayerController : IUpdatable, IChangeableStates, IContactListener
    {
        #region Fields

        private GameObject _playerView;
        private SpriteRenderer _toolSpriteRenderer;
        private Vector3 _leftScale;
        private Vector3 _rightScale;

        private Dictionary<string, Sprite> _toolSprites;

        private float _movingThreshold = 0.35f;
        private bool _isInToolArea = false;

        #endregion


        #region Properties

        public SpriteRenderer SpriteRenderer { get; private set; }
        public event Action<AnimState> OnStateChange;
        public bool IsLooped { get; private set; }

        #endregion


        #region ClassLifeCycles

        internal PlayerController(GameObject playerView, Dictionary<string, Sprite> toolSprites)
        {
            _playerView = playerView;
            _rightScale = playerView.transform.localScale;
            _leftScale = new Vector3(_rightScale.x * -1, _rightScale.y, _rightScale.z);
            _playerView.TryGetComponent<SpriteRenderer>(out var spriteRenderer);
            SpriteRenderer = spriteRenderer;
            IsLooped = true;

            _playerView.transform.GetChild(0).TryGetComponent<SpriteRenderer>(out var toolSpriteRenderer);
            _toolSpriteRenderer = toolSpriteRenderer;

            _toolSprites = toolSprites;
        }

        #endregion


        #region Methods

        public void Update(float deltaTime)
        {
            Movement(deltaTime);
            Interaction();
        }

        private void Movement(float fixedDeltaTime)
        {
            var xAxisInput = Input.GetAxis("Horizontal");
            if (Mathf.Abs(xAxisInput) > _movingThreshold)
            {
                if (OnStateChange != null) OnStateChange.Invoke(AnimState.Walk);
                _playerView.transform.Translate((Vector3.right * fixedDeltaTime * GameSettings.PLAYER_WALK_SPEED) * (xAxisInput < 0 ? -1 : 1));
                _playerView.transform.localScale = (xAxisInput < 0 ? _leftScale : _rightScale);
            }
            else if (OnStateChange != null) OnStateChange.Invoke(AnimState.Idle);
        }

        private void Interaction()
        {
            if (_isInToolArea)
            {
                if (Input.GetKeyDown(GameSettings.PLAYER_TOOL1))
                {
                    _toolSpriteRenderer.sprite = _toolSprites["key"];                    
                }
                if (Input.GetKeyDown(GameSettings.PLAYER_TOOL2))
                {
                    _toolSpriteRenderer.sprite = _toolSprites["vantuz"];
                }
                if (Input.GetKeyDown(GameSettings.PLAYER_TOOL3))
                {
                    _toolSpriteRenderer.sprite = _toolSprites["paper"];
                }
            }
        }

        public void ChageToolContactState(bool value)
        {
            _isInToolArea = value;
        }

        #endregion
    }
}

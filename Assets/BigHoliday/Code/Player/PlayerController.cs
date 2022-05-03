using UnityEngine;
using System;
using System.Collections.Generic;


namespace BigHoliday
{
    internal sealed class PlayerController : IUpdatable, IChangeableStates, IContactListener, IToiletManager
    {
        #region Fields

        private GameObject _playerProvider;
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
        public byte CurrentTool { get; private set; }
        public event Action<byte> ToolInteract;

        #endregion


        #region ClassLifeCycles

        internal PlayerController(GameObject provider, Dictionary<string, Sprite> toolSprites)
        {
            _playerProvider = provider;

            _rightScale = provider.transform.localScale;
            _leftScale = new Vector3(_rightScale.x * -1, _rightScale.y, _rightScale.z);


            _playerProvider.TryGetComponent<SpriteRenderer>(out var spriteRenderer);
            if (spriteRenderer) SpriteRenderer = spriteRenderer;

            IsLooped = true;

            _playerProvider.transform.GetChild(0).TryGetComponent<SpriteRenderer>(out var toolSpriteRenderer);
            if (toolSpriteRenderer) _toolSpriteRenderer = toolSpriteRenderer;

            _toolSprites = toolSprites;
        }

        #endregion


        #region Methods

        public void Update(float deltaTime)
        {
            Movement(deltaTime);
            Interaction();
        }

        private void Movement(float deltaTime)
        {
            var xAxisInput = Input.GetAxis("Horizontal");
            if (Mathf.Abs(xAxisInput) > _movingThreshold)
            {
                if (OnStateChange != null) OnStateChange.Invoke(AnimState.Walk);
                _playerProvider.transform.Translate((Vector3.right * deltaTime * GameSettings.PLAYER_WALK_SPEED) * (xAxisInput < 0 ? -1 : 1));
                _playerProvider.transform.localScale = (xAxisInput < 0 ? _leftScale : _rightScale);
            }
            else if (OnStateChange != null) OnStateChange.Invoke(AnimState.Idle);
        }

        private void Interaction()
        {            
            if (Input.GetKeyDown(GameSettings.PLAYER_TOOL1))
            {
                if (_isInToolArea)
                {
                    _toolSpriteRenderer.sprite = _toolSprites["ak47"];
                    CurrentTool = 1;
                }
                else ToolInteract.Invoke(CurrentTool);
            }
            if (Input.GetKeyDown(GameSettings.PLAYER_TOOL2))
            {
                if (_isInToolArea)
                {
                    _toolSpriteRenderer.sprite = _toolSprites["bita"];
                    CurrentTool = 2;
                }
                else ToolInteract.Invoke(CurrentTool);
            }
            if (Input.GetKeyDown(GameSettings.PLAYER_TOOL3))
            {
                if (_isInToolArea)
                {
                    _toolSpriteRenderer.sprite = _toolSprites["katana"];
                    CurrentTool = 3;
                }
                else ToolInteract.Invoke(CurrentTool);
            }
        }

        public void ChageToolContactState(bool value)
        {
            _isInToolArea = value;
        }

        #endregion
    }
}

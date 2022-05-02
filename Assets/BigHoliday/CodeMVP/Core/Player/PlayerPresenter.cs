using Features.Tips;
using System;
using UniRx;
using UnityEngine;
using Utils;

namespace Core
{
    public class PlayerPresenter : MonoBehaviour
    {
        #region Fields

        [SerializeField] private AnimatorDictionary _animatorValues;
        [SerializeField] private GameContext _gameContext;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _playerSpriteRenderer;
        [SerializeField] private SpriteRenderer _toolSpriteRenderer;

        private ToolBoxPresenter _currentToolBoxPresenter;
        private ToolPresenter _activeTool = null;
        private Vector3 _leftScale = new Vector3(-1, 1, 0);
        private Vector3 _rightScale = new Vector3(1, 1, 0);
        private float _animCounter;
        private bool _isInteractingTool = false;
        private bool _toolEquipped = false;

        #endregion

        #region Propeties

        public ReactiveProperty<AnimState> CurrentAnimationState = new ReactiveProperty<AnimState>();
        public ReactiveProperty<string> CurrentTip = new ReactiveProperty<string>();

        #endregion

        #region UnityMethods

        private void Awake()
        {
            var playingStream = Observable.EveryUpdate().Where(_ => _gameContext.CurrentGameState.Equals(GameState.Playing));

            playingStream.Subscribe(_ =>
            {
                if (_animCounter > _animatorValues[CurrentAnimationState.Value].Count - 1)
                    _animCounter = 0;
                _playerSpriteRenderer.sprite = _animatorValues[CurrentAnimationState.Value][(int)_animCounter];
                _animCounter += Time.deltaTime * _gameContext.PlayerAnimationSpeed;
            });

            var idleAnimationStream = playingStream.Where(_ => (Input.GetAxis("Horizontal") == 0) && (CurrentAnimationState.Value != AnimState.Idle)).Subscribe(_ => CurrentAnimationState.Value = AnimState.Idle);

            var moveInputStream = playingStream.Where(_ => Input.GetAxis("Horizontal") != 0);

            moveInputStream.Subscribe(_ =>
            {
                CurrentAnimationState.Value = AnimState.Walk;
                transform.Translate((Vector3.right * Time.deltaTime * _gameContext.PlayerMoveSpeed * (Input.GetAxis("Horizontal") < 0 ? -1 : 1)));
                transform.localScale = (Input.GetAxis("Horizontal") < 0 ? _leftScale : _rightScale);
            });

            var playerInteractStream = playingStream.Where(_ => Input.GetKeyDown(_gameContext.PlayerInteractKey));

            //playerInteractStream.Subscribe(_ => Debug.Log($"Is interact:{_isInteractingTool};Have active tool:{_activeTool ?? false};Have presenter{_currentToolBoxPresenter ?? false}"));

            var toolContactStream = playerInteractStream.Where(_ => _isInteractingTool);
            var getToolStream = toolContactStream.Where(_ => !_toolEquipped).Throttle(TimeSpan.FromMilliseconds(10))
                .Subscribe(_=> 
                {
                    _activeTool = _currentToolBoxPresenter.GetTool();
                    _toolSpriteRenderer.sprite = _activeTool.CurrentSprite;
                    _toolSpriteRenderer.enabled = true;
                    _toolEquipped = true;
                });
            var putUpToolStreeam = toolContactStream.Where(_ => _toolEquipped)
                .Subscribe(_ =>
                {
                    if (_currentToolBoxPresenter.PutUpTool(_activeTool))
                    {
                        _toolSpriteRenderer.sprite = null;
                        _toolSpriteRenderer.enabled = false;
                        _activeTool = null;
                        _toolEquipped = false;
                    }
                });
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "tip":
                    CurrentTip.Value = other.GetComponent<TipTrigger>().TipText;
                    break;
                case "tool":
                    _isInteractingTool = true;
                    _currentToolBoxPresenter = other.GetComponent<ToolPresenter>().Presenter;
                    break;
                default:
                    break;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "tool":
                    _isInteractingTool = false;
                    _currentToolBoxPresenter = null;
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}

using System.Collections.Generic;
using UnityEngine;
using System;


namespace BigHoliday
{
    internal sealed class Visitor : MonoBehaviour, IChangeableStates
    {
        #region Fields

        internal Transform SpawnPosition;

        private System.Random _random;
        private Vector3 _targetToilet;
        private SpriteAnimController _animatorController;

        private Vector3 _leftScale = new Vector3(-1, 1, 0);
        private Vector3 _rightScale = new Vector3(1, 1, 0);

        #endregion


        #region Properties

        public SpriteRenderer SpriteRenderer { get; private set; }
        public bool IsLooped { get; private set; }
        public VisitorState CurrentState { get; private set; }
        public event Action<AnimState> OnStateChange;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            IsLooped = true;
            SpriteRenderer = GetComponent<SpriteRenderer>();
            _random = new System.Random();

            var configs = Resources.LoadAll<SpriteAnimatorConfig>(@"AnimationConfigs\Visitor");
            var config = configs[_random.Next(0, configs.Length)];

            _animatorController = new SpriteAnimController(config, this);

            OnStateChange.Invoke(AnimState.Idle);
        }

        private void Update()
        {
            _animatorController.Update(Time.deltaTime);

            switch (CurrentState)
            {
                case VisitorState.Coming:
                    Move(_leftScale, Time.deltaTime);
                    break;
                case VisitorState.Arrived:
                    break;
                case VisitorState.Done:
                    break;
                case VisitorState.Escaped:
                    break;
                default:
                    break;  
            }
        }

        #endregion


        #region Methods

        public void SetupDestination(Vector3 destination)
        {
            _targetToilet = destination;
            CurrentState = VisitorState.Coming;
            OnStateChange.Invoke(AnimState.Walk);
        }

        public void ChangeState(AnimState state)
        {
            OnStateChange.Invoke(state);
        }

        private void Move(Vector3 scaleSide, float deltaTime)
        {
            var distance = Vector2.Distance(transform.position, _targetToilet);
            Debug.Log(distance);
            if (distance > 0.2f)
            {
                transform.Translate((Vector3.right * deltaTime * GameSettings.VISITOR_WALK_SPEED) * scaleSide.x);
                transform.localScale = scaleSide;
            }
            else
            {
                CurrentState = VisitorState.Arrived;
                OnStateChange.Invoke(AnimState.Idle);
            }
        }

        #endregion
    }
}

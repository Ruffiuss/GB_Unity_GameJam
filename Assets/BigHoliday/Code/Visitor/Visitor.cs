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
        private SpriteAnimController _animatorController;
        private Rigidbody2D _rigidbody2D;
        private Vector3 _movingTarget;
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
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _random = new System.Random();

            var configs = Resources.LoadAll<SpriteAnimatorConfig>(@"AnimationConfigs\Visitor");
            var config = configs[_random.Next(0, configs.Length)];

            _animatorController = new SpriteAnimController(config, this);

            OnStateChange.Invoke(AnimState.Idle);

            _rigidbody2D.simulated = false;
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
                    Move(_rightScale, Time.deltaTime);
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
            _movingTarget = destination;
            if (!CurrentState.Equals(VisitorState.Done)) CurrentState = VisitorState.Coming;
            OnStateChange.Invoke(AnimState.Walk);
        }

        public void ChangeState(VisitorState state)
        {
            CurrentState = state;
        }

        private void Move(Vector3 scaleSide, float deltaTime)
        {
            var distance = Vector2.Distance(transform.position, _movingTarget);
            //Debug.DrawRay(transform.position, _movingTarget);
            //Debug.Log($"ScaleVector:{scaleSide}");
            //Debug.Log(distance);
            if (distance > 0.29f)
            {
                transform.Translate((Vector3.right * deltaTime * GameSettings.VISITOR_WALK_SPEED) * scaleSide.x);
                transform.localScale = scaleSide;
                if (_rigidbody2D.simulated == true) _rigidbody2D.simulated = false;
            }
            else if (!CurrentState.Equals(VisitorState.Done))
            {
                _rigidbody2D.simulated = true;
                CurrentState = VisitorState.Arrived;
                OnStateChange.Invoke(AnimState.Idle);
            }
            else
            {
                CurrentState = VisitorState.Escaped;
                //Destroy(this, 1.0f);
            }
        }

        #endregion
    }
}

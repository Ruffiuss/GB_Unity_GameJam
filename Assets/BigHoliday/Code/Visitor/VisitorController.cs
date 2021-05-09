using System;
using System.Collections.Generic;
using UnityEngine;


namespace BigHoliday
{
    public class VisitorController : MonoBehaviour
    {
        #region Fields

        public GameObject _visitorTemplate;
        public Transform _spawnTransform;
        public List<SpriteAnimatorConfig> _animationConfigs;
        public List<Transform> _toiletTrasforms;

        private System.Random _random;
        private Queue<KeyValuePair<VisitorAnimation, SpriteAnimController>> _visitorsQueue;
        private Queue<Transform> _toiletsQueue;
        private KeyValuePair<VisitorAnimation, SpriteAnimController> _activeVisitor;
        private Transform _activeToilet;

        private Vector3 _leftScale = new Vector3(-1, 1, 0);
        private Vector3 _rightScale = new Vector3(1, 1, 0);

        private bool _isVisitorComing = false;
        private bool _isArrived = false;
        private bool _isDone = false;

        #endregion


        #region MyRegion

        private void Awake()
        {
            _random = new System.Random();

            _visitorsQueue = new Queue<KeyValuePair<VisitorAnimation, SpriteAnimController>>();
            _toiletsQueue = new Queue<Transform>(_toiletTrasforms);

            InvokeRepeating("SpawnVisitor", 0, 25);
        }

        #endregion


        #region UnityMethods

        private void Update()
        {
            if (!_isVisitorComing)
            {
                _activeVisitor = _visitorsQueue.Peek();
                _visitorsQueue.Dequeue();
                _activeToilet = _toiletsQueue.Peek();
                _toiletsQueue.Dequeue();
                _isVisitorComing = true;
                _activeVisitor.Key.ChangeState(AnimState.Walk);
            }
            else if(!_isArrived)
            {
                MoveVisitor(_activeVisitor.Key.transform, _activeToilet, Time.deltaTime);
                _activeVisitor.Value.Update(Time.deltaTime);
            }
            else
            {
                _activeVisitor.Key.ChangeState(AnimState.Idle);
            }
        }

        #endregion


        #region Methods

        private void SpawnVisitor()
        {

            var animatipnConfigIndex = _random.Next(0, _animationConfigs.Count);
            var spawnedVisitor = Instantiate(_visitorTemplate, _spawnTransform);

            spawnedVisitor.TryGetComponent<SpriteRenderer>(out var spriteRenderer);
            if (spriteRenderer)
            {
                var animation = spawnedVisitor.AddComponent<VisitorAnimation>();
                animation.SetupAnimation(spriteRenderer, true);
            }
            else throw new Exception($"{spawnedVisitor.name} doesen`t have SpriteRenderer");

            _visitorsQueue.Enqueue(new KeyValuePair<VisitorAnimation, SpriteAnimController>(
                spawnedVisitor.GetComponent<VisitorAnimation>(),
                new SpriteAnimController(_animationConfigs[animatipnConfigIndex], spawnedVisitor.GetComponent<VisitorAnimation>()
                    )));
            spawnedVisitor.GetComponent<VisitorAnimation>().ChangeState(AnimState.Idle);
        }

        private void MoveVisitor(Transform visitorTransform, Transform toiletTransform, float deltaTime)
        {
            var distance = Vector2.Distance(visitorTransform.position, toiletTransform.position);
            if (distance > 0.1f)
            {
                visitorTransform.Translate((Vector3.right * deltaTime * GameSettings.VISITOR_WALK_SPEED) * (_isDone ? 1 : -1));
                visitorTransform.localScale = _isDone ? _rightScale : _leftScale;
            }
            else
            {
                _isArrived = true;
            }
        }


        #endregion
    }
}
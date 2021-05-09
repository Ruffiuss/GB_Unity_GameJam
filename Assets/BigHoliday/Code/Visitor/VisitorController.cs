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

        private System.Random _random;
        private Dictionary<VisitorAnimation, SpriteAnimController> _animationControllers;

        #endregion


        #region MyRegion

        private void Awake()
        {
            _animationControllers = new Dictionary<VisitorAnimation, SpriteAnimController>();
            _random = new System.Random();

            InvokeRepeating("SpawnVisitor", 0, 10);
        }

        #endregion


        #region Methods

        private void SpawnVisitor()
        {
            var animatipnConfigIndex = _random.Next(0, _animationConfigs.Count);
            Debug.Log($"Count:{_animationConfigs.Count}|GeneratedIndex:{animatipnConfigIndex}");
            var spawnedVisitor = Instantiate(_visitorTemplate, _spawnTransform);

            spawnedVisitor.TryGetComponent<SpriteRenderer>(out var spriteRenderer);
            if (spriteRenderer)
            {
                var animation = spawnedVisitor.AddComponent<VisitorAnimation>();
                animation.SetupAnimation(spriteRenderer, true);
            }
            else throw new Exception($"{spawnedVisitor.name} doesen`t have SpriteRenderer");

            _animationControllers.Add(
                spawnedVisitor.GetComponent<VisitorAnimation>(),
                new SpriteAnimController(_animationConfigs[animatipnConfigIndex], spawnedVisitor.GetComponent<VisitorAnimation>()
                    ));
        }

        #endregion
    }
}
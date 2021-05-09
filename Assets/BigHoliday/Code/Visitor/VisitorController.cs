using System;
using System.Collections.Generic;
using UnityEngine;


namespace BigHoliday
{
    public class VisitorController : IUpdatable
    {
        #region Fields   

        private GameObject _visitorTemplate;
        private Transform _spawnTransform;
        private Queue<GameObject> _visitorsQueue;
        private Queue<Transform> _toiletsQueue;

        private float _spawnDelay = 10.0f;
        private float _timePassed = 0.0f;

        #endregion


        #region ClassLifeCycles

        public VisitorController(List<Transform> toiletSpots, GameObject visitor, Transform spawnTransform)
        {
            _spawnTransform = spawnTransform;
            _visitorTemplate = visitor;
            _visitorsQueue = new Queue<GameObject>();
            _toiletsQueue = new Queue<Transform>(toiletSpots);

            SpawnVisitor();
        }

        #endregion


        #region Methods

        public void Update(float deltaTime)
        {
            _timePassed += deltaTime;
            if (_timePassed > _spawnDelay)
            {
                _timePassed = 0.0f;
                SpawnVisitor();
            }
        }

        private void SpawnVisitor()
        {
            if (_visitorsQueue.Count < 20)
            {
                var spawnedVisitor = GameObject.Instantiate(_visitorTemplate, _spawnTransform) as GameObject;
                _visitorsQueue.Enqueue(spawnedVisitor);
                var visitor = spawnedVisitor.AddComponent<Visitor>();
                visitor.SetupDestination(_toiletsQueue.Dequeue());
            }
        }

        #endregion
    }
}
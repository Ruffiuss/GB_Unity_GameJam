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
        private bool _haveFreeSpots = true;

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

                if (_haveFreeSpots) SpawnVisitor();
            }

            foreach (var visitor in _visitorsQueue)
            {
                Debug.Log($"{visitor.GetInstanceID()}-{visitor.GetComponent<Visitor>().CurrentState}");
            }
        }

        private void SpawnVisitor()
        {
            if (_visitorsQueue.Count < 20)
            {
                var spawnedVisitor = GameObject.Instantiate(_visitorTemplate, _spawnTransform) as GameObject;
                _visitorsQueue.Enqueue(spawnedVisitor);
                var visitor = spawnedVisitor.AddComponent<Visitor>();
                if (!_toiletsQueue.Count.Equals(0))
                {
                    visitor.SetupDestination(_toiletsQueue.Dequeue());
                }
                else _haveFreeSpots = false;
            }
        }

        #endregion
    }
}
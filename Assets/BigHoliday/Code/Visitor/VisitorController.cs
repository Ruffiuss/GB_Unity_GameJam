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
        private Queue<Vector3> _toiletsQueue;
        private Dictionary<int, Vector3> _toiletDictionary;

        private float _spawnDelay = 10.0f;
        private float _timePassed = 0.0f;
        private bool _haveFreeSpots = true;

        #endregion


        #region ClassLifeCycles

        public VisitorController(Dictionary<int, Vector3> toiletDictionary, GameObject visitor, Transform spawnTransform)
        {
            _toiletDictionary = toiletDictionary;
            _spawnTransform = spawnTransform;
            _visitorTemplate = visitor;
            _visitorsQueue = new Queue<GameObject>();
            _toiletsQueue = new Queue<Vector3>();

            foreach (var key in _toiletDictionary.Keys)
            {
                _toiletsQueue.Enqueue(_toiletDictionary[key]);
            }
            
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
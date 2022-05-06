using System;
using System.Collections.Generic;
using UnityEngine;


namespace BigHoliday
{
    public class VisitorController : IUpdatable, IVisitorEvents
    {
        #region Fields   

        private GameObject _visitorTemplate;
        private Transform _spawnTransform;
        private Queue<GameObject> _visitorsQueue;
        private Stack<Vector3> _freeToilets;
        private Dictionary<int, Vector3> _toiletDictionary;
        private Stack<Vector3> _busyToilets;

        private float _spawnDelay = 10.0f;
        private float _spawnTimePassed = 0.0f;
        private bool _haveFreeSpots = true;

        #endregion


        #region Properties

        public event Func<int, bool> CheckToilet;

        #endregion


        #region ClassLifeCycles

        public VisitorController(Dictionary<int, Vector3> toiletDictionary, GameObject visitor, Transform spawnTransform)
        {
            _toiletDictionary = toiletDictionary;
            _spawnTransform = spawnTransform;
            _visitorTemplate = visitor;
            _visitorsQueue = new Queue<GameObject>();
            _freeToilets = new Stack<Vector3>();
            _busyToilets = new Stack<Vector3>();

            foreach (var key in _toiletDictionary.Keys)
            {
                _freeToilets.Push(_toiletDictionary[key]);
            }
            
            SpawnVisitor();
        }

        #endregion


        #region Methods

        public void Update(float deltaTime)
        {
            _spawnTimePassed += deltaTime;
            if (_spawnTimePassed > _spawnDelay)
            {
                _spawnTimePassed = 0.0f;

                if (_haveFreeSpots) SpawnVisitor();
            }

            foreach (var visitor in _visitorsQueue)
            {
                // Debug.Log($"{visitor.GetInstanceID()}-{visitor.GetComponent<Visitor>().CurrentState}");
                var visitorsView = visitor.GetComponent<Visitor>();
                switch (visitorsView.CurrentState)
                {
                    case VisitorState.Waiting:
                        break;
                    case VisitorState.Coming:
                        break;
                    case VisitorState.Arrived:
                        if (CheckToilet.Invoke(visitor.GetInstanceID()))
                            VisitorDone(visitor);
                        break;
                    case VisitorState.Done:
                        break;
                    case VisitorState.Escaped:
                        //_freeToilets.Enqueue(_busyToilets.Pop());
                        //_visitorsQueue.Dequeue();
                        break;
                    default:
                        break;
                }
            }
        }

        private void VisitorDone(GameObject visitor)
        {
            visitor.GetComponent<Visitor>().ChangeState(VisitorState.Done);
            visitor.GetComponent<Visitor>().SetupDestination(_spawnTransform.position);
            visitor.GetComponent<SpriteRenderer>().enabled = true;
        }

        private void SpawnVisitor()
        {
            if (_visitorsQueue.Count < 20)
            {
                var spawnedVisitor = GameObject.Instantiate(_visitorTemplate, _spawnTransform) as GameObject;
                _visitorsQueue.Enqueue(spawnedVisitor);
                var visitor = spawnedVisitor.AddComponent<Visitor>();
                if (!_freeToilets.Count.Equals(0))
                {
                    _busyToilets.Push(_freeToilets.Pop());
                    visitor.SetupDestination(_busyToilets.Peek());
                    visitor.OnReleaseToilet += Visitor_OnReleaseToilet;
                }
                else _haveFreeSpots = false;
            }
        }

        private void Visitor_OnReleaseToilet(Vector3 toiletVector)
        {
            _freeToilets.Push(toiletVector);
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;


namespace BigHoliday
{
    public sealed class CollisionHandler : ICollisionHandler
    {
        #region Fileds

        private Dictionary<string, List<ColliderProvider>> _colliderGroups;
        private Dictionary<string, List<IListener>> _groupSubscribers;

        #endregion


        #region ClassLifeCycles

        public CollisionHandler(List<ColliderProvider> colliders)
        {
            _colliderGroups = new Dictionary<string, List<ColliderProvider>>(); 

            foreach (var collider in colliders)
            {
                switch (collider.tag)
                {
                    case "Limit":
                        if (!_colliderGroups.ContainsKey("Limit"))
                        {
                            _colliderGroups.Add("Limit", new List<ColliderProvider>());
                        }
                        _colliderGroups["Limit"].Add(collider);
                        break;
                    case "Toilet":
                        if (!_colliderGroups.ContainsKey("Toilet"))
                        {
                            _colliderGroups.Add("Toilet", new List<ColliderProvider>());
                        }
                        _colliderGroups["Toilet"].Add(collider);
                        break;
                    case "Tools":
                        if (!_colliderGroups.ContainsKey("Tools"))
                        {
                            _colliderGroups.Add("Tools", new List<ColliderProvider>());
                        }
                        _colliderGroups["Tools"].Add(collider);
                        break;
                    default:
                        break;
                }
            }

            _groupSubscribers = new Dictionary<string, List<IListener>>();

            foreach (var key in _colliderGroups.Keys)
            {
                _groupSubscribers.Add(key, new List<IListener>());
            }
        }

        #endregion


        #region Methods

        public void SubscribeOnGroup(string name, IListener listener)
        {
            if (_groupSubscribers.ContainsKey(name))
            {
                _groupSubscribers[name].Add(listener);
            }
            else throw new Exception($"Group {name} not exist");
        }

        public void SubscribeAllColliders()
        {
            foreach (var key in _colliderGroups.Keys)
            {
                foreach (var collider in _colliderGroups[key])
                {
                    //collider.Collision += CheckCollision;
                }
            }
        }

        private void CheckCollision(string colliderTag, string collisionTag)
        {
            if (_groupSubscribers.ContainsKey(colliderTag))
            {
                foreach (var listener in _groupSubscribers[colliderTag])
                {
                    //listener.Notify();
                }
            }
        }

        #endregion
    }
}

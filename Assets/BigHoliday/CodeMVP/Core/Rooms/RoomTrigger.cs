using System;
using UnityEngine;

namespace Core
{
    public class RoomTrigger : MonoBehaviour, IDisposable
    {
        #region Fields

        private Action<RoomTrigger> _onTrigger;

        #endregion

        #region UnityMethods

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
                _onTrigger.Invoke(this);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
                _onTrigger.Invoke(null);
        }

        #endregion

        #region Methods

        public void Init(Action<RoomTrigger> action)
        {
            _onTrigger = action;
        }

        public void Dispose()
        {
            _onTrigger = null;
        }

        #endregion
    }
}

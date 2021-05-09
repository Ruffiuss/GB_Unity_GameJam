using System;
using UnityEngine;
using System.Collections.Generic;


namespace BigHoliday
{
    public sealed class ToiletView : MonoBehaviour
    {
        #region Fields
        
        private float _timeToEvent;
        private System.Random _random;

        #endregion


        #region Properties

        public ToiletStatus Status { get; private set; }
        public event Action<int> ContactedToilet;
        public event Action<ToiletStatus, int> ToiletStatusChange;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _random = new System.Random();
            _timeToEvent = _random.Next(GameSettings.RANDOM_EVENT_MINVALUE, GameSettings.RANDOM_EVENT_MAXVALUE);
            Status = ToiletStatus.Normal;
            InvokeRepeating("ChangeStatus", _timeToEvent, GameSettings.RANDOM_EVENT_REPEAT_RATE);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                ContactedToilet.Invoke(gameObject.GetInstanceID());
            }
            if (collision.CompareTag("Visitor"))
            {
                Debug.Log($"{collision.GetInstanceID()} entred to {gameObject.GetInstanceID()}");
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                ContactedToilet.Invoke(0);
            }
            if (collision.CompareTag("Visitor"))
            {
                Debug.Log($"{collision.GetInstanceID()} escaped from {gameObject.GetInstanceID()}");
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                ContactedToilet.Invoke(gameObject.GetInstanceID());
            }
        }

        #endregion


        #region Methods

        private void ChangeStatus()
        {
            if (Status == ToiletStatus.Normal)
            {
                Status = (ToiletStatus)UnityEngine.Random.Range(1,4);
                ToiletStatusChange.Invoke(Status, gameObject.GetInstanceID());
                //Debug.Log($"{gameObject.name}-{Status}");
            }
            _timeToEvent = UnityEngine.Random.Range(GameSettings.RANDOM_EVENT_MINVALUE, GameSettings.RANDOM_EVENT_MAXVALUE);
        }

        internal void RestoreStatus()
        {
            Status = ToiletStatus.Normal;
            ToiletStatusChange.Invoke(Status, gameObject.GetInstanceID());
        }

        #endregion
    }
}

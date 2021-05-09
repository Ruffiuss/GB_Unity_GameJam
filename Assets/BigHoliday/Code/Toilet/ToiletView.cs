using System;
using UnityEngine;
using System.Collections.Generic;


namespace BigHoliday
{
    public sealed class ToiletView : MonoBehaviour
    {
        #region Fields

        private float _timeToEvent;

        #endregion


        #region Properties

        public ToiletStatus Status { get; private set; }
        public event Action<int> ContactedToilet;
        public event Action<ToiletStatus, int> ToiletStatusChange;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _timeToEvent = UnityEngine.Random.Range(GameSettings.RANDOM_EVENT_MINVALUE, GameSettings.RANDOM_EVENT_MAXVALUE);
            Status = ToiletStatus.Normal;
        }

        private void Update()
        {
            InvokeRepeating("ChangeStatus", _timeToEvent, 10.0f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                ContactedToilet.Invoke(gameObject.GetInstanceID());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                ContactedToilet.Invoke(0);
            }
        }

        #endregion


        #region Methods

        private void ChangeStatus()
        {
            if (Status == ToiletStatus.Normal)
            {
                Status = (ToiletStatus)UnityEngine.Random.Range(1,3);
                ToiletStatusChange.Invoke(Status, gameObject.GetInstanceID());
            }
            _timeToEvent = UnityEngine.Random.Range(GameSettings.RANDOM_EVENT_MINVALUE, GameSettings.RANDOM_EVENT_MAXVALUE);
        }

        #endregion
    }
}

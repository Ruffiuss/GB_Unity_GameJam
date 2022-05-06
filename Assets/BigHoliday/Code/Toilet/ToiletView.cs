using System;
using UnityEngine;
using System.Collections.Generic;


namespace BigHoliday
{
    public sealed class ToiletView : MonoBehaviour
    {
        #region Fields

        private int _visitorAwaiting;

        #endregion


        #region Properties

        public ToiletStatus Status { get; private set; }
        public event Action<int, string> ContactedToilet;
        public event Action<ToiletStatus, int> ToiletStatusChange;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            Status = ToiletStatus.Normal;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                ContactedToilet.Invoke(gameObject.GetInstanceID(), "Player");
            }
            if (collision.CompareTag("Visitor"))
            {
                ChangeStatus();
                //Debug.Log($"ToiletSee-{collision.name}:{collision.gameObject.GetInstanceID()}");
                if (Status.Equals(ToiletStatus.Normal))
                {
                    ContactedToilet.Invoke(collision.gameObject.GetInstanceID(), "Visitor");
                }
                else
                {
                    _visitorAwaiting = collision.gameObject.GetInstanceID();
                }
                //Debug.Log($"{collision.gameObject.GetInstanceID()} entred to {gameObject.GetInstanceID()}");
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                ContactedToilet.Invoke(0, "Player");
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
                ContactedToilet.Invoke(gameObject.GetInstanceID(), "Player");
            }
        }

        #endregion


        #region Methods

        public void ChangeStatus()
        {
            if (Status == ToiletStatus.Normal)
            {
                Status = (ToiletStatus)UnityEngine.Random.Range(1,4);
                ToiletStatusChange.Invoke(Status, gameObject.GetInstanceID());
                //Debug.Log($"{gameObject.name}-{Status}");
            }
        }

        internal void RestoreStatus()
        {
            Status = ToiletStatus.Normal;
            ToiletStatusChange.Invoke(Status, gameObject.GetInstanceID());
            ContactedToilet.Invoke(_visitorAwaiting, "Visitor");
        }

        #endregion
    }
}

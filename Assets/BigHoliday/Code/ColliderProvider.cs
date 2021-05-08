using System;
using UnityEngine;


namespace BigHoliday
{
    public class ColliderProvider : MonoBehaviour
    {
        #region Properties

        public event Action<GameObject, string> Collision;

        #endregion


        #region UnityMethods

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var contactedTag = collision.tag;
            Collision.Invoke(gameObject, contactedTag);
        }

        #endregion
    }
}
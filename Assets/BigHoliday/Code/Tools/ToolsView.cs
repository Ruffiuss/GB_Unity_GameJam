using System;
using System.Collections.Generic;
using UnityEngine;


namespace BigHoliday
{
    internal sealed class ToolsView : MonoBehaviour
    {
        #region Fields

        internal event Action<bool> IsContact;

        #endregion


        #region UnityMethods

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                IsContact.Invoke(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                IsContact.Invoke(false);
            }
        }

        #endregion
    }
}

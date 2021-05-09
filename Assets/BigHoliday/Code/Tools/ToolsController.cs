using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace BigHoliday
{
    internal sealed class ToolsController
    {
        #region Fields

        internal Dictionary<string, Sprite> Tools;
        internal Text _toolsTip;

        private IContactListener _listener;

        #endregion


        #region ClassLifeCycles

        internal ToolsController(ToolsView toolsView, IContactListener listener, Text toolsTip)
        {
            _toolsTip = toolsTip;
            _toolsTip.enabled = false;

            _toolsTip.text = GameSettings.TOOLS_TIP_TEXT;
            toolsView.IsContact += ChangeContactState;
            _listener = listener;
        }

        #endregion


        #region Methods

        private void ChangeContactState(bool isContact)
        {
            if (isContact) _toolsTip.enabled = true;
            else _toolsTip.enabled = false;
            _listener.ChageToolContactState(isContact);
        }

        #endregion
    }
}

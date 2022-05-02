using System;
using System.Linq;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Core
{
    public class ToolBoxPresenter : MonoBehaviour, IDisposable
    {
        #region Fields

        [SerializeField] private List<ToolPresenter> _toolPresenters;
        private Dictionary<ToolType, bool> _toolAvailabilityMap = new Dictionary<ToolType, bool>();
        private ToolType _selectedTool = ToolType.None;

        #endregion

        #region Properties



        #endregion

        #region UnityMethods

        private void Awake()
        {
            foreach (var trigger in _toolPresenters)
            {
                trigger.Init(this, SelectTool);
            }
            for (int i = 1; i < Enum.GetValues(typeof(ToolType)).Length -1; i++)
            {
                _toolAvailabilityMap.Add((ToolType)i, true);
            }
        }

        private void OnDestroy()
        {
            Dispose();
        }

        #endregion

        #region Methods

        private void SelectTool(ToolType toolType)
        {
            _selectedTool = toolType;
        }

        public ToolPresenter GetTool()
        {
            if (!_selectedTool.Equals(ToolType.None))
            {
                _toolAvailabilityMap[_selectedTool] = false;
                var tool = _toolPresenters.Where(t => t.CurrentToolType.Equals(_selectedTool)).First();
                tool.HideTool();
                return tool;
            }
            else return null;
        }

        public bool PutUpTool(ToolPresenter tool)
        {
            if (_selectedTool.Equals(tool.CurrentToolType))
            {
                _toolAvailabilityMap[tool.CurrentToolType] = true;
                tool.ShowTool();
                return true;
            }
            else return false;
        }

        public void Dispose()
        {
            foreach (var trigger in _toolPresenters)
            {
                trigger.Dispose();
            }
        }

        #endregion
    }
}

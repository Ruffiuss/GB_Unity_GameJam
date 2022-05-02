using System;
using UnityEngine;

namespace Core
{
    public class ToolPresenter : MonoBehaviour, IDisposable
    {
        #region Fields

        public ToolType CurrentToolType;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Action<ToolType> _onSelected;
        private ToolBoxPresenter _currentPresenter;

        #endregion

        #region Properties

        public ToolBoxPresenter Presenter => _currentPresenter;
        public Sprite CurrentSprite => _spriteRenderer.sprite;

        #endregion

        #region UnityMethods


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
                _onSelected?.Invoke(CurrentToolType);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
                _onSelected?.Invoke(ToolType.None);
        }

        #endregion

        #region Methods

        public void Init(ToolBoxPresenter toolsPresenter, Action<ToolType> action)
        {
            _currentPresenter = toolsPresenter;
            _onSelected = action;
        }

        public void HideTool() => _spriteRenderer.enabled = false;
        public void ShowTool() => _spriteRenderer.enabled = true;

        public void Dispose()
        {
            _currentPresenter = null;
            _onSelected = null;
        }

        #endregion
    }
}

using UnityEngine;
using System;


namespace BigHoliday
{
    internal sealed class PlayerController : IFixedUpdatable
    {
        #region Fields

        private GameObject _playerView;
        private Vector3 _leftScale;
        private Vector3 _rightScale;

        private float _movingThreshold = 0.001f;
        private float _walkSpeed = 5.0f;
        private bool _goesSideway;

        #endregion


        #region ClassLifeCycles

        internal PlayerController(GameObject playerView)
        {
            _playerView = playerView;
            _rightScale = playerView.transform.localScale;
            _leftScale = new Vector3(_rightScale.x * -1, _rightScale.y, _rightScale.z);
        }

        #endregion


        #region Methods

        public void FixedUpdate(float fixedDeltaTime)
        {
            var xAxisInput = Input.GetAxis("Horizontal");
            if (Mathf.Abs(xAxisInput) > _movingThreshold)
            {
                _playerView.transform.Translate((Vector3.right * fixedDeltaTime * _walkSpeed) * (xAxisInput < 0 ? -1 : 1));
                _playerView.transform.localScale = (xAxisInput < 0 ? _leftScale : _rightScale);
            }
        }

        #endregion
    }
}

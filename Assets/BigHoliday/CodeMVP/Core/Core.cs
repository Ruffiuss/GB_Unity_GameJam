using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Utils;

namespace Core
{
    public class Core : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform _rootUI;
        [SerializeField] private Canvas _canvasUI;
        [SerializeField] private TipsPresenter _tipsPresenter;
        private PlayerEvents _playerEvents = new PlayerEvents();

        #endregion

        #region UnityMethods

        void Start()
        {
            _tipsPresenter.SubscribeFlow(_playerEvents.TipsFlow);
        }

        #endregion

        #region Methods



        #endregion
    }
}

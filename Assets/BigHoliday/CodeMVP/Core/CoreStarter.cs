using UnityEngine;
using Utils;

namespace Core
{
    public class CoreStarter : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform _rootUI;
        [SerializeField] private Canvas _canvasUI;
        [SerializeField] private GameContext _gameProcess;
        [SerializeField] private TipsPresenter _tipsPresenter;
        [SerializeField] private PlayerPresenter _playerPresenter;

        #endregion

        #region UnityMethods

        void Start()
        {
            _tipsPresenter.SubscribeFlow(_playerPresenter.CurrentTip);            
        }

        #endregion

        #region Methods



        #endregion
    }
}

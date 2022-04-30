using UnityEngine;
using Utils;

namespace Core
{
    public class CoreStarter : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform _rootUI;
        [SerializeField] private Canvas _canvasUI;
        [SerializeField] private TipsPresenter _tipsPresenter;
        [SerializeField] private PlayerPresenter _playerPresenter;

        #endregion

        #region Properties

        public GameState CurrentGameState;

        #endregion

        #region UnityMethods

        void Start()
        {
            CurrentGameState = GameState.Playing;
            _tipsPresenter.SubscribeFlow(_playerPresenter.CurrentTip);

            
        }

        #endregion

        #region Methods



        #endregion
    }
}

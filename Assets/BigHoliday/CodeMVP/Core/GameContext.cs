using UnityEngine;
using Utils;

namespace Core
{
    public class GameContext : MonoBehaviour
    {
        #region Fields

        [Range(0, 10)] public float PlayerMoveSpeed;
        [Range(10, 100)] public int PlayerAnimationSpeed;
        public KeyCode PlayerInteractKey = KeyCode.Z;

        [SerializeField] private GameState _currentGameState;

        #endregion

        #region Properties

        public GameState CurrentGameState => _currentGameState;

        #endregion

        #region Methods

        public void StartGame() => _currentGameState = GameState.Playing;
        public void PauseGame() => _currentGameState = GameState.Paused;

        #endregion
    }
}

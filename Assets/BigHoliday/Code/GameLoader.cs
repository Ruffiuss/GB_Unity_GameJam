using UnityEngine;


namespace BigHoliday
{
    internal sealed class GameLoader : MonoBehaviour
    {
        #region Fields

        [SerializeField] internal GameObject Player;
        [SerializeField] internal string AnimationConfigsPath = "AnimationConfigs";
        [SerializeField] internal string PlayerAnimationConfigPath;

        private SpriteAnimController _playerAnimController;
        private ResourceLoader _resourceLoader;
        private Controllers _controllers;

        private float _deltaTime;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _resourceLoader = new ResourceLoader();
            _controllers = new Controllers();

            _playerAnimController = new SpriteAnimController(_resourceLoader.LoadAnimConfig(AnimationConfigsPath + @"\" + PlayerAnimationConfigPath));
            _playerAnimController.StartAnimation(Player.GetComponent<SpriteRenderer>(), AnimState.Idle, true);
            _controllers.AddController(_playerAnimController);
        }

        private void Update()
        {
            _deltaTime = Time.deltaTime;
            _controllers.Update(_deltaTime);
        }

        #endregion
    }
}

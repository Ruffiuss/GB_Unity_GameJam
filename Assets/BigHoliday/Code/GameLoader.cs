using System.Collections.Generic;
using UnityEngine;


namespace BigHoliday
{  
    internal sealed class GameLoader : MonoBehaviour
    {
        #region Fields

        [SerializeField] internal GameObject Player;
        [SerializeField] internal string AnimationConfigsPath = "AnimationConfigs";
        [SerializeField] internal string PlayerAnimationConfigPath;
        [SerializeField] internal List<ColliderProvider> Colliders;

        private ResourceLoader _resourceLoader;
        private Controllers _controllers;
        private SpriteAnimController _playerAnimController;
        private PlayerController _playerController;

        private float _deltaTime;
        private float _fixedDeltaTime;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _resourceLoader = new ResourceLoader();
            _controllers = new Controllers();

            _playerController = new PlayerController(Player);
            _controllers.AddController(_playerController);

            _playerAnimController = new SpriteAnimController(_resourceLoader.LoadAnimConfig(AnimationConfigsPath + @"\" + PlayerAnimationConfigPath), _playerController);
            _controllers.AddController(_playerAnimController);
        }

        private void Update()
        {
            _deltaTime = Time.deltaTime;
            _controllers.Update(_deltaTime);
        }

        private void FixedUpdate()
        {
            _fixedDeltaTime = Time.fixedDeltaTime;
            _controllers.FixedUpdate(_fixedDeltaTime);
        }

        #endregion
    }
}

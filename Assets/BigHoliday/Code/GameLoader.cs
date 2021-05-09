using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace BigHoliday
{  
    internal sealed class GameLoader : MonoBehaviour
    {
        #region Fields

        [SerializeField] internal GameObject Player;
        [SerializeField] internal string AnimationConfigsPath = "AnimationConfigs";
        [SerializeField] internal string PlayerAnimationConfigPath;
        [SerializeField] internal ToolsView ToolsView;
        [SerializeField] internal Text ToolsTip;
        [SerializeField] internal List<string> ToolSpriteNames;
        [SerializeField] internal List<GameObject> Toilets;
        [SerializeField] internal List<Transform> ToiletSpots;
        [SerializeField] internal Transform VisitorSpawnTransform;

        private ResourceLoader _resourceLoader;
        private Controllers _controllers;
        private SpriteAnimController _playerAnimController;
        private PlayerController _playerController;
        private ToolsController _toolsController;
        private ToiletController _toiletController;
        private VisitorController _visitorController;

        private float _deltaTime;
        private float _fixedDeltaTime;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _resourceLoader = new ResourceLoader();
            _controllers = new Controllers();

            var toolSprites = new Dictionary<string, Sprite>();
            foreach (var spriteName in ToolSpriteNames)
            {
                toolSprites.Add(spriteName, _resourceLoader.LoadSprite(spriteName));
            }

            _playerController = new PlayerController(Player, toolSprites);
            _controllers.AddController(_playerController);

            _playerAnimController = new SpriteAnimController(_resourceLoader.LoadAnimConfig(AnimationConfigsPath + @"\" + PlayerAnimationConfigPath), _playerController);
            _controllers.AddController(_playerAnimController);

            _toolsController = new ToolsController(ToolsView, _playerController, ToolsTip);

            _toiletController = new ToiletController(_playerController, Toilets);

            _visitorController = new VisitorController(ToiletSpots, _resourceLoader.LoadPrefab("Visitor"), VisitorSpawnTransform);
            _controllers.AddController(_visitorController);
        }

        private void Update()
        {
            _deltaTime = Time.deltaTime;
            _controllers.Update(_deltaTime);
        }

        //private void FixedUpdate()
        //{
        //    _fixedDeltaTime = Time.fixedDeltaTime;
        //    _controllers.FixedUpdate(_fixedDeltaTime);
        //}

        #endregion
    }
}

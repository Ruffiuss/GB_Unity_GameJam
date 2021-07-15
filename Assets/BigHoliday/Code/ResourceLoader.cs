using UnityEngine;
using System.Collections.Generic;


namespace BigHoliday
{
    internal sealed class ResourceLoader
    {
        #region Fields

        private Dictionary<string, SpriteAnimatorConfig> _configs;

        #endregion


        #region ClasLifeCycles

        internal ResourceLoader()
        {
            _configs = new Dictionary<string, SpriteAnimatorConfig>();
        }

        #endregion


        #region Methods

        internal SpriteAnimatorConfig LoadAnimConfig(string path)
        {
            if (_configs.ContainsKey(path))
            {
                return _configs[path];
            }
            else
            {
                var config = Resources.Load<SpriteAnimatorConfig>(path);
                if (config)
                {
                    _configs.Add(path, config);
                    return _configs[path];
                }
                else throw new System.Exception($"Config does not exist at {path}");
            }
        }

        internal GameObject LoadPrefab(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            if (prefab)
            {
                return prefab;
            }
            else throw new System.Exception("Prefab not founded");
        }

        internal Sprite LoadSprite(string path)
        {
            var sprite = Resources.Load<Sprite>(path);
            if (sprite)
            {
                return sprite;
            }
            else throw new System.Exception("Sprite not founded");
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;


namespace BigHoliday
{
    internal sealed class SpriteAnimController
    {
        #region Fields

        private SpriteAnimatorConfig _config;
        private Dictionary<SpriteRenderer, Animation> _activeAnimation;

        #endregion


        #region ClassLifeCycles

        internal SpriteAnimController(SpriteAnimatorConfig config)
        {
            _config = config;
            _activeAnimation = new Dictionary<SpriteRenderer, Animation>();
            
        }

        #endregion


        #region Methods

        internal void StartAnimation(SpriteRenderer spriteRenderer, AnimState track, bool isLoop, float speed = 10.0f)
        {
            if (_activeAnimation.TryGetValue(spriteRenderer, out var animation))
            {
                animation.is
            }
        }

        #endregion
    }
}

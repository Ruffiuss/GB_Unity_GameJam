using System;
using System.Collections.Generic;
using UnityEngine;


namespace BigHoliday
{
    internal sealed class SpriteAnimController : IUpdatable
    {
        #region Fields

        private SpriteAnimatorConfig _config;
        private Dictionary<SpriteRenderer, Animation> _activeAnimations;

        #endregion


        #region ClassLifeCycles

        internal SpriteAnimController(SpriteAnimatorConfig config)
        {
            _config = config;
            _activeAnimations = new Dictionary<SpriteRenderer, Animation>();
            
        }

        #endregion


        #region Methods

        internal void StartAnimation(SpriteRenderer spriteRenderer, AnimState state, bool isLoop, float speed = 10.0f)
        {
            if (_activeAnimations.TryGetValue(spriteRenderer, out var animation))
            {
                animation.IsLooped = isLoop;
                animation.AnimSpeed = speed;
                animation.IsSleeps = false;

                if (animation.State != state)
                {
                    animation.State = state;
                    animation.Sprites = _config.Sequence.Find(sequence => sequence.State == state).Sprites;
                    animation.Counter = 0.0f;
                }
            }
            else
            {
                _activeAnimations.Add(spriteRenderer, new Animation()
                {
                    State = state,
                    Sprites = _config.Sequence.Find(sequence => sequence.State == state).Sprites,
                    IsLooped = isLoop,
                    AnimSpeed = speed
                });
            }
        }

        internal void StopAnimation(SpriteRenderer sprite)
        {
            if (_activeAnimations.ContainsKey(sprite))
            {
                _activeAnimations.Remove(sprite);
            }
        }

        public void Update(float deltaTime)
        {
            foreach (var animation in _activeAnimations)
            {
                animation.Value.Animate(deltaTime);

                if (animation.Value.Counter < animation.Value.Sprites.Count)
                {
                    animation.Key.sprite = animation.Value.Sprites[(int)animation.Value.Counter];
                }
            }
        }

        #endregion
    }
}

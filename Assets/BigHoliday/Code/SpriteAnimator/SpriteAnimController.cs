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
        private IChangeableStates _animationTarget;
        private AnimState _currentState;

        private float _animationSpeed;

        #endregion


        #region ClassLifeCycles

        internal SpriteAnimController(SpriteAnimatorConfig config, IChangeableStates animTarget)
        {
            _config = config;
            _activeAnimations = new Dictionary<SpriteRenderer, Animation>();
            _animationTarget = animTarget;
            _animationTarget.OnStateChange += TrackCurrentState;
            StartAnimation();
        }

        #endregion


        #region Methods

        private void TrackCurrentState(AnimState state)
        {
            _currentState = state;
            StartAnimation();
        }

        internal void StartAnimation()
        {
            if (_activeAnimations.TryGetValue(_animationTarget.SpriteRenderer, out var animation))
            {
                animation.IsLooped = _animationTarget.IsLooped;
                animation.IsSleeps = false;

                if (animation.State != _currentState)
                {
                    animation.State = _currentState;
                    animation.Sprites = _config.Sequence.Find(sequence => sequence.State == _currentState).Sprites;
                    animation.AnimSpeed = _config.Sequence.Find(sequence => sequence.State == _currentState).AnimationSpeed;
                    animation.Counter = 0.0f;
                }
            }
            else
            {
                _activeAnimations.Add(_animationTarget.SpriteRenderer, new Animation()
                {
                    State = _currentState,
                    Sprites = _config.Sequence.Find(sequence => sequence.State == _currentState).Sprites,
                    IsLooped = _animationTarget.IsLooped,
                    AnimSpeed = _config.Sequence.Find(sequence => sequence.State == _currentState).AnimationSpeed
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

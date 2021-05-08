using System;
using System.Collections.Generic;
using UnityEngine;


namespace BigHoliday
{
    internal sealed class Animation
    {
        #region Fields

        internal List<Sprite> Sprites;
        internal AnimState State;

        internal float AnimSpeed = 10.0f;
        internal float Counter = 0.0f;
        internal bool IsLooped = true;
        internal bool IsSleeps = false;

        #endregion


        #region Methods

        internal void Animate(float deltaTime)
        {
            if (IsSleeps) return;
            Counter += deltaTime * AnimSpeed;

            if (IsLooped)
            {
                while (Counter > Sprites.Count)
                {
                    Counter -= Sprites.Count;
                }
            }
            else if (Counter > Sprites.Count)
            {
                Counter = Sprites.Count;
                IsSleeps = true;
            }
        }

        #endregion
    }
}

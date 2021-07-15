using System;
using UnityEngine;


namespace BigHoliday
{
    internal interface IChangeableStates
    {
        SpriteRenderer SpriteRenderer { get; }
        event Action<AnimState> OnStateChange;
        bool IsLooped { get; }
    }
}
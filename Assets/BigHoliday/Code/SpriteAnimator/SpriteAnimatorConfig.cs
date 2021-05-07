using System;
using System.Collections.Generic;
using UnityEngine;


namespace BigHoliday
{
    [CreateAssetMenu(fileName = "SpriteAnimatorConfig", menuName = "Configs/Animator", order = 1)]
    public sealed class SpriteAnimatorConfig : ScriptableObject
    {
        public List<SpriteSequence> Sequence = new List<SpriteSequence>();
    }
}
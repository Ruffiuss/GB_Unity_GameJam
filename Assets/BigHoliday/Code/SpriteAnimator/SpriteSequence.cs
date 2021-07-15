using System;
using System.Collections.Generic;
using UnityEngine;


namespace BigHoliday
{
    [Serializable]
    public class SpriteSequence
    {
        public AnimState State;
        public float AnimationSpeed;
        public List<Sprite> Sprites = new List<Sprite>();
    }
}
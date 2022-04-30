using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public class AnimatorDictionary : SerializableDictionary<AnimState, List<Sprite>, SpritesStorage> { }
}

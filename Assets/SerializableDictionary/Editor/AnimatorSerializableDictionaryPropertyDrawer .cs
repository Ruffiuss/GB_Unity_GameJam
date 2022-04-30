using UnityEditor;
using Utils;

namespace Assets.SerializableDictionary.Editor
{
    [CustomPropertyDrawer(typeof(AnimatorDictionary))]
    public class AnimatorSerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }
}

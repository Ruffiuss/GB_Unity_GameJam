using UnityEngine;


namespace BigHoliday
{
    internal sealed class ResourceLoader
    {
        #region Methods

        internal GameObject LoadPrefab(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            if (prefab)
            {
                return prefab;
            }
            else throw new System.Exception("Prefab not founded");
        }

        #endregion
    }
}

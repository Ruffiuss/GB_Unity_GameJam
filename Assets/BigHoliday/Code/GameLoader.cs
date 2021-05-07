using UnityEngine;


namespace BigHoliday
{
    internal sealed class GameLoader : MonoBehaviour
    {
        #region Fields

        [SerializeField] internal GameObject Player;

        private ResourceLoader _resourceLoader;

        #endregion


        #region ClassLifeCycles

        internal GameLoader()
        {
            _resourceLoader = new ResourceLoader();
        }

        #endregion


        #region UnityMethods

        private void Update()
        {
            
        }

        #endregion
    }
}

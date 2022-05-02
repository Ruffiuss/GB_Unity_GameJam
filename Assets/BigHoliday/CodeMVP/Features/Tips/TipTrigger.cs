using UnityEngine;

namespace Features.Tips
{
    public class TipTrigger : MonoBehaviour
    {
        #region Fields

        [SerializeField] private string _tipText;
        private bool _isShowed = false;

        #endregion

        #region Properites

        public string TipText
        {
            get
            {
                if (!_isShowed)
                {
                    _isShowed = true;
                    return _tipText;
                }
                else return "";
            }
            
        }

        #endregion
    }
}

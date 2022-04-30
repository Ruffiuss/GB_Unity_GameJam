using UnityEngine;

namespace Features.Tips
{
    public class TipTrigger : MonoBehaviour
    {
        #region Fields

        [SerializeField] private string _tipText;

        #endregion

        #region Properites

        public string TipText => _tipText;

        #endregion
    }
}

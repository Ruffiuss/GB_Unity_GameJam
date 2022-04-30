using Features.Tips;
using UniRx;
using UnityEngine;
using Utils;

namespace Core
{
    public class PlayerPresenter : MonoBehaviour
    {
        #region Fields

        [SerializeField] private AnimatorDictionary _values;

        #endregion

        #region Propeties

        public ReactiveProperty<AnimState> CurrentState = new ReactiveProperty<AnimState>();
        public ReactiveProperty<string> CurrentTip = new ReactiveProperty<string>();

        #endregion

        #region UnityMethods

        private void Awake()
        {

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "tip":
                    CurrentTip.Value = other.GetComponent<TipTrigger>().TipText;
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}

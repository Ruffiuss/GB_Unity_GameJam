using System;
using UnityEngine;
using System.Collections.Generic;


namespace BigHoliday
{
    internal sealed class ToiletController
    {
        #region Fields

        private IToiletManager _toiletManager;
        private Dictionary<ToiletView, GameObject> _toilets;
        private Dictionary<int, SpriteRenderer> _toiletNotiffications;

        private int _currentPlayerToiletID;

        #endregion


        #region ClassLifeCycles

        internal ToiletController(IToiletManager toiletManager, List<GameObject> toilets)
        {
            _toilets = new Dictionary<ToiletView, GameObject>(); 
            _toiletNotiffications = new Dictionary<int, SpriteRenderer>();
            _toiletManager = toiletManager;
            _toiletManager.ToolInteract += PlayerInteraction;

            foreach (var toilet in toilets)
            {
                toilet.TryGetComponent<ToiletView>(out var toiletView);
                if (toiletView)
                {
                    _toilets.Add(toiletView, toilet);
                    toiletView.ContactedToilet += PlayerOnToilet;
                    toiletView.ToiletStatusChange += ChangeStatus;
                }


                toilet.transform.GetChild(0).TryGetComponent<SpriteRenderer>(out var spriteRenderer);
                if (spriteRenderer)
                {
                    spriteRenderer.sprite = Resources.Load<Sprite>("world_bubble");
                    spriteRenderer.enabled = false;
                    _toiletNotiffications.Add(toilet.GetInstanceID(), spriteRenderer);
                }
            }
        }

        #endregion


        #region Methods

        private void PlayerOnToilet(int ID)
        {
            _currentPlayerToiletID = ID;
        }

        private void PlayerInteraction(byte toolID)
        {

        }

        private void ChangeStatus(ToiletStatus status, int ID)
        {
            if (_toiletNotiffications.ContainsKey(ID))
            {
                _toiletNotiffications[ID].enabled = true;
            }
        }

        #endregion
    }
}

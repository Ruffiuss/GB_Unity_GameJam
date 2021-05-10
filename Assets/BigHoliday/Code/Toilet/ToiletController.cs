using System;
using UnityEngine;
using System.Collections.Generic;


namespace BigHoliday
{
    internal sealed class ToiletController
    {
        #region Fields

        private IToiletManager _toiletManager;
        private IVisitorEvents _visitorEvents;
        private Dictionary<ToiletView, GameObject> _toilets;
        private Dictionary<int, List<SpriteRenderer>> _toiletNotiffications;
        private Dictionary<int, ToiletView> _toiletViews;
        private List<int> _approwedVisitors;

        private int _currentPlayerToiletID;
        private Sprite _statusSprite;

        #endregion


        #region ClassLifeCycles

        internal ToiletController(IToiletManager toiletManager, List<GameObject> toilets)
        {
            _toilets = new Dictionary<ToiletView, GameObject>();
            _toiletNotiffications = new Dictionary<int, List<SpriteRenderer>>();
            _toiletViews = new Dictionary<int, ToiletView>();
            _approwedVisitors = new List<int>();

            _toiletManager = toiletManager;
            _toiletManager.ToolInteract += PlayerInteraction;

            foreach (var toilet in toilets)
            {
                toilet.TryGetComponent<ToiletView>(out var toiletView);
                if (toiletView)
                {
                    _toilets.Add(toiletView, toilet);
                    _toiletViews.Add(toilet.GetInstanceID(), toiletView);
                    toiletView.ContactedToilet += ToiletContacted;
                    toiletView.ToiletStatusChange += ChangeStatus;
                }

                for (int i = 0; i < toilet.transform.childCount; i++)
                {
                    toilet.transform.GetChild(i).TryGetComponent<SpriteRenderer>(out var spriteRenderer);
                    if (spriteRenderer)
                    {
                        spriteRenderer.sprite = Resources.Load<Sprite>("world_bubble");
                        spriteRenderer.enabled = false;

                        if (!_toiletNotiffications.ContainsKey(toilet.GetInstanceID()))
                        {
                            _toiletNotiffications.Add(toilet.GetInstanceID(), new List<SpriteRenderer>());
                        }
                        _toiletNotiffications[toilet.GetInstanceID()].Add(spriteRenderer);
                    }
                }
            }
        }

        #endregion


        #region Methods

        public void AddVisitorEvents(IVisitorEvents visitorEvents)
        {
            _visitorEvents = visitorEvents;
            _visitorEvents.CheckToilet += RequestToilet;
        }

        private bool RequestToilet(int visitorID)
        {
            Debug.Log($"{visitorID} requested");
            if (_approwedVisitors.Contains(visitorID))
            {
                Debug.Log($"{visitorID} can in");
                return true;
            }
            else
            {
                Debug.Log($"{visitorID} cant");
                return false;
            }
        }

        private void ToiletContacted(int ID, string tag)
        {
            switch (tag)
            {
                case "Player":
                    _currentPlayerToiletID = ID;
                    break;
                case "Visitor":
                    Debug.Log($"Added {ID}");
                    _approwedVisitors.Add(ID);
                    break;
                default:
                    break;
            }
            //Debug.Log(_currentPlayerToiletID);
        }

        private void PlayerInteraction(byte toolID)
        {
            if (!_currentPlayerToiletID.Equals(0))
            {
                var activeToiletStatus = _toiletViews[_currentPlayerToiletID].Status;
                switch (toolID)
                {
                    case 1:
                        if (activeToiletStatus.Equals(ToiletStatus.Broken)) _toiletViews[_currentPlayerToiletID].RestoreStatus();
                        break;
                    case 2:
                        if (activeToiletStatus.Equals(ToiletStatus.Dirty)) _toiletViews[_currentPlayerToiletID].RestoreStatus();
                        break;
                    case 3:
                        if (activeToiletStatus.Equals(ToiletStatus.Empty)) _toiletViews[_currentPlayerToiletID].RestoreStatus();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ChangeStatus(ToiletStatus status, int ID)
        {
            switch (status)
            {
                case ToiletStatus.Normal:
                    break;
                case ToiletStatus.Dirty:
                    _statusSprite = Resources.Load<Sprite>("poop");
                    break;
                case ToiletStatus.Broken:
                    _statusSprite = Resources.Load<Sprite>("broken");
                    break;
                case ToiletStatus.Empty:
                    _statusSprite = Resources.Load<Sprite>("paper_ended");
                    break;
                default:
                    break;
            }
            if (_toiletNotiffications.ContainsKey(ID))
            {
                if (!status.Equals(ToiletStatus.Normal))
                {
                    _toiletNotiffications[ID][0].enabled = true;
                    _toiletNotiffications[ID][1].sprite = _statusSprite;
                    _toiletNotiffications[ID][1].enabled = true;
                }
                else
                {
                    _toiletNotiffications[ID][0].enabled = false;
                    _toiletNotiffications[ID][1].enabled = false;
                }
            }
        }

        #endregion
    }
}

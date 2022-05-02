using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class RoomsPresenter : MonoBehaviour, IDisposable
    {
        #region Fields

        [SerializeField] private List<RoomTrigger> _rooms;
        private Dictionary<RoomTrigger, RoomState> _roomsStateMap = new Dictionary<RoomTrigger, RoomState>();
        private RoomTrigger _selectedRoom = null;

        #endregion

        #region UnityMethods

        private void Awake()
        {
            foreach (var room in _rooms)
            {
                room.Init(SelectRoom);
                _roomsStateMap.Add(room, RoomState.Complete);
            }
        }

        private void OnDestroy()
        {
            Dispose();
        }

        #endregion

        #region Methods

        private void SelectRoom(RoomTrigger room)
        {
            _selectedRoom = room;
        }

        public void Dispose()
        {
            foreach (var room in _rooms)
            {
                room.Dispose();
            }
            _rooms.Clear();
        }

        #endregion
    }
}

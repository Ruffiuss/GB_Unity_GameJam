using System.Collections.Generic;


namespace BigHoliday
{
    internal class Controllers : IUpdatable
    {
        #region Fields

        private List<IUpdatable> _updatableControllers;

        #endregion


        #region ClassLifeCycles

        internal Controllers()
        {
            _updatableControllers = new List<IUpdatable>();
        }

        #endregion


        #region Methods

        public void Update(float deltaTime)
        {
            foreach (var controller in _updatableControllers)
            {
                controller.Update(deltaTime);
            }
        }

        internal void AddController<T>(T controller)
        {
            if (controller is IUpdatable updatableController)
            {
                _updatableControllers.Add(updatableController);
            }
        }

        #endregion
    }
}
using System.Collections.Generic;


namespace BigHoliday
{
    internal class Controllers : IUpdatable, IFixedUpdatable
    {
        #region Fields

        private List<IUpdatable> _updatableControllers;
        private List<IFixedUpdatable> _fixedUpdatableControllers;

        #endregion


        #region ClassLifeCycles

        internal Controllers()
        {
            _updatableControllers = new List<IUpdatable>();
            _fixedUpdatableControllers = new List<IFixedUpdatable>();
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

        public void FixedUpdate(float fixedDeltaTime)
        {
            foreach (var controller in _fixedUpdatableControllers)
            {
                controller.FixedUpdate(fixedDeltaTime);
            }
        }

        internal void AddController<T>(T controller)
        {
            if (controller is IUpdatable updatableController)
            {
                _updatableControllers.Add(updatableController);
            }
            if (controller is IFixedUpdatable fixedUpdatableController)
            {
                _fixedUpdatableControllers.Add(fixedUpdatableController);
            }
        }

        #endregion
    }
}
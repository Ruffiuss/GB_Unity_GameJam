namespace BigHoliday
{
    internal interface ICollisionHandler
    {
        void SubscribeOnGroup(string name, IListener listener);
    }
}
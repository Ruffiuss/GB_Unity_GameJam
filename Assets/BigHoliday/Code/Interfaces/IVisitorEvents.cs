using System;


namespace BigHoliday
{
    public interface IVisitorEvents
    {
        event Func<int, bool> CheckToilet;
    }
}

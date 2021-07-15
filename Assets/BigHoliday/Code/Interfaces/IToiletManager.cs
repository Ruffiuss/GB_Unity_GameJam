using System;


namespace BigHoliday
{
    public interface IToiletManager
    {
        event Action<byte> ToolInteract;
    }
}

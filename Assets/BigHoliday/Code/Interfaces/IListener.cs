using UnityEngine;


namespace BigHoliday
{
    public interface IListener
    {
        void Notify(Collider2D collider, Collision2D collision);
    }
}

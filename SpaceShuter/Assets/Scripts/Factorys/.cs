using UnityEngine;

namespace LiveToday
{
    public interface IFactory<T> where T : MonoBehaviour
    {
        T NewInstance();
    }
}
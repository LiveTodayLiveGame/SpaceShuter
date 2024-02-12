using System;

namespace LiveToday
{
    public interface IStorageService
    {
        void Save(string key, object data, Action<bool> callBack = null);
        void Load<T>(string key, Action<T> callBack);
    }
}

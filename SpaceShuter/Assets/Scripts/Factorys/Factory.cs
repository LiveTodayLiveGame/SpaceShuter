using UnityEngine;

namespace LiveToday
{
    public class Factory
    {
        private readonly PrefabsProvider _provider; 

        public Factory(PrefabsProvider provider)
        {
            _provider = provider;
        }

        public Object NewInstance(ref string fileName, ref Transform parent )
        {
            var prefab = _provider.GetPrefab(ref fileName);
            var newObject = Object.Instantiate(prefab, parent);
            return newObject;
        }

        
    }
}

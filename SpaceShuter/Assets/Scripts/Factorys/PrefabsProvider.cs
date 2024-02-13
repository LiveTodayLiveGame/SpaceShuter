using UnityEngine;

namespace LiveToday
{
    public class PrefabsProvider
    {
        public Object GetPrefab(ref string fileName)
        {
            var prefab = Resources.Load<Object>("Prefabs/" + fileName);
            return prefab;
        }
    }
}
using System.Collections.Generic;

namespace Game
{
    public class PoolEntry<T>
    {
        public string Id;
        public T Value;
    }

    public class PoolGeneric<T>
    {
        private readonly List<PoolEntry<T>> _available = new List<PoolEntry<T>>();
        private readonly List<PoolEntry<T>> _inUse = new List<PoolEntry<T>>();

        public PoolEntry<T> GetOrCreate(string id)
        {
            if (_available.Count > 0)
            {
                for (var i = _available.Count - 1; i > 0 ; i--)
                {
                    if (_available[i].Id == id)
                    {
                        var obj = _available[i];
                        _available.RemoveAt(i);
                        _inUse.Add(obj);
                        return obj;
                    }
                }
            }

            var newObj = new PoolEntry<T>
            {
                Id = id
            };
            _inUse.Add(newObj);
            
            return newObj;
        }

        public void InUseToAvailable(PoolEntry<T> poolEntry)
        {
            _inUse.Remove(poolEntry);
            _available.Add(poolEntry);
        }
    }
}

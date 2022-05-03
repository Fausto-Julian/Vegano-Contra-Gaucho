using System;
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
        private List<PoolEntry<T>> availables = new List<PoolEntry<T>>();
        private List<PoolEntry<T>> inUse = new List<PoolEntry<T>>();

        public PoolEntry<T> GetorCreate(string id)
        {
            if (availables.Count > 0)
            {
                for (var i = 0; i < availables.Count; i++)
                {
                    if (availables[i].Id == id)
                    {
                        var obj = availables[i];
                        availables.RemoveAt(i);
                        inUse.Add(obj);
                        return obj;
                    }
                }
            }

            var newObj = new PoolEntry<T>
            {
                Id = id
            };
            
            inUse.Add(newObj);
            return newObj;
        }

        public void InUseToAvailable(PoolEntry<T> poolEntry)
        {
            inUse.Remove(poolEntry);
            availables.Add(poolEntry);
        }
    }
}

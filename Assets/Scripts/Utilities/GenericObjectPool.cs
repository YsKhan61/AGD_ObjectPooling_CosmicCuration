using System.Collections.Generic;

namespace CosmicCuration.Utilities
{ 
    public abstract class GenericObjectPool<T> where T : class
    {
        private List<PooledObject<T>> pooledObjects = new List<PooledObject<T>>();
        
        /// <summary>
        /// Get an object from the pool. If there are no available objects, create a new one.
        /// </summary>
        /// <returns></returns>
        public T GetObject()
        {
            if (pooledObjects.Count > 0)
            {
                PooledObject<T> pooledObject = pooledObjects.Find(item => !item.IsUsed);
                if (pooledObject != null)
                {
                    pooledObject.IsUsed = true;
                    return pooledObject.Object;
                }
            }
            return CreateNewPooledObject();
        }

        /// <summary>
        /// Return an object to the pool.
        /// </summary>
        /// <param name="pooledObject"></param>
        public void ReturnObject(T pooledObject)
        {
            PooledObject<T> pooledObjectToReturn = pooledObjects.Find(e => e.Object.Equals(pooledObject));
            pooledObjectToReturn.IsUsed = false;
        }

        private T CreateNewPooledObject()
        {
            PooledObject<T> newPooledObject = new() { Object = CreateObject()};
            newPooledObject.IsUsed = true;
            pooledObjects.Add(newPooledObject);
            return newPooledObject.Object;
        }

        protected abstract T CreateObject();

        public class PooledObject<U>
        {
            public U Object;
            public bool IsUsed;
        }
    }
}

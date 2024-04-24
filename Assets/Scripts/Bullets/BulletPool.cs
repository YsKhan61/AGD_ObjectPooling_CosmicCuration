using System.Collections.Generic;

namespace CosmicCuration.Bullets
{
    public class BulletPool
    {
        private BulletView bulletView;
        private BulletScriptableObject bulletScriptableObject;
        private List<PooledBullet> pooledBullets = new List<PooledBullet>();

        public BulletPool(BulletView bulletPrefab, BulletScriptableObject bulletScriptableObject)
        {
            bulletView = bulletPrefab;
            this.bulletScriptableObject = bulletScriptableObject;
            
        }

        /// <summary>
        /// Get a bullet from the pool
        /// </summary>
        /// <returns></returns>
        public BulletController GetBullet()
        {
            // Check if there are any bullets in the pool that are not being used, if so, return one of them
            if (pooledBullets.Count > 0)
            {
                foreach (var pooledBullet in pooledBullets)
                {
                    if (!pooledBullet.IsUsed)
                    {
                        pooledBullet.IsUsed = true;
                        return pooledBullet.Bullet;
                    }
                }
            }

            // If there are no bullets in the pool, create a new pooled bulet and return it's bullet.
            PooledBullet newPooledBullet = CreateNewPooledBullet();
            newPooledBullet.IsUsed = true;
            return newPooledBullet.Bullet;
        }

        /// <summary>
        /// Return a bullet to the pool
        /// </summary>
        /// <param name="bullet"></param>
        public void ReturnBullet(BulletController bullet)
        {
            foreach (var pooledBullet in pooledBullets)
            {
                if (pooledBullet.Bullet == bullet)
                {
                    pooledBullet.IsUsed = false;
                    return;
                }
            }
        }

        private PooledBullet CreateNewPooledBullet()
        {
            PooledBullet pooledBullet = new() { IsUsed = false, Bullet = CreateBullet() };
            pooledBullets.Add(pooledBullet);
            return pooledBullet;
        }

        private BulletController CreateBullet()
        {
            return new BulletController(bulletView, bulletScriptableObject);
        }

        public class PooledBullet
        {
            public bool IsUsed;
            public BulletController Bullet;
        }
    }
}

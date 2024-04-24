using System.Collections.Generic;

namespace CosmicCuration.Bullets
{
    public class BulletPool
    {
        private BulletView bulletView;
        private BulletScriptableObject bulletScriptableObject;
        private List<PooledBullet> pooledBullets;

        public BulletPool(BulletView bulletPrefab, BulletScriptableObject bulletScriptableObject)
        {
            bulletView = bulletPrefab;
            this.bulletScriptableObject = bulletScriptableObject;
            
        }

        public class PooledBullet
        {
            public bool IsUsed;
            public BulletController Bullet;
        }
    }
}

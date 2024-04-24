using CosmicCuration.Utilities;


namespace CosmicCuration.Bullets
{
    public class BulletPool : GenericObjectPool<BulletController>
    {
        private BulletView bulletPrefab;
        private BulletScriptableObject bulletSO;

        public BulletPool(BulletView bulletPrefab, BulletScriptableObject bulletSO)
        {
            this.bulletPrefab = bulletPrefab;
            this.bulletSO = bulletSO;
        }

        protected override BulletController CreateObject() => CreateBullet();

        private BulletController CreateBullet() => new BulletController(bulletPrefab, bulletSO);

        public class PooledBullet
        {
            public BulletController Bullet;
            public bool isUsed;
        }
    }
}
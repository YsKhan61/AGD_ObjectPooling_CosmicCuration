using System.Collections.Generic;

namespace CosmicCuration.Enemy
{
    public class EnemyPool
    {
        private EnemyView enemyPrefab;
        private EnemyScriptableObject enemyScriptableObject;
        private List<PooledEnemy> pooledEnemies = new List<PooledEnemy>();

        public EnemyPool(EnemyView enemyPrefab, EnemyScriptableObject enemyScriptableObject)
        {
            this.enemyPrefab = enemyPrefab;
            this.enemyScriptableObject = enemyScriptableObject;
            
        }

        /// <summary>
        /// Get a bullet from the pool
        /// </summary>
        /// <returns></returns>
        public EnemyController GetEnemy()
        {
            // Check if there are any bullets in the pool that are not being used, if so, return one of them
            if (pooledEnemies.Count > 0)
            {
                foreach (var pooledEnemy in pooledEnemies)
                {
                    if (!pooledEnemy.IsUsed)
                    {
                        pooledEnemy.IsUsed = true;
                        return pooledEnemy.Enemy;
                    }
                }
            }

            // If there are no bullets in the pool, create a new pooled bulet and return it's bullet.
            PooledEnemy newPooledBullet = CreateNewPooledEnemy();
            newPooledBullet.IsUsed = true;
            return newPooledBullet.Enemy;
        }

        /// <summary>
        /// Return a bullet to the pool
        /// </summary>
        /// <param name="enemy"></param>
        public void ReturnEnemy(EnemyController enemy)
        {
            foreach (var pooledBullet in pooledEnemies)
            {
                if (pooledBullet.Enemy == enemy)
                {
                    pooledBullet.IsUsed = false;
                    return;
                }
            }
        }

        private PooledEnemy CreateNewPooledEnemy()
        {
            PooledEnemy pooledEnemy = new() { IsUsed = false, Enemy = CreateEnemy() };
            pooledEnemies.Add(pooledEnemy);
            return pooledEnemy;
        }

        private EnemyController CreateEnemy()
        {
            return new EnemyController(enemyPrefab, enemyScriptableObject.enemyData);
        }

        public class PooledEnemy
        {
            public bool IsUsed;
            public EnemyController Enemy;
        }
    }
}

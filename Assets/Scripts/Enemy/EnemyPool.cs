using CosmicCuration.Utilities;


namespace CosmicCuration.Enemy
{
    public class EnemyPool : GenericObjectPool<EnemyController>
    {
        private EnemyView enemyPrefab;
        private EnemyData enemyData;


        public EnemyPool(EnemyView enemyPrefab, EnemyData enemyData)
        {
            this.enemyPrefab = enemyPrefab;
            this.enemyData = enemyData;
        }

        protected override EnemyController CreateObject() => CreateEnemy();
        private EnemyController CreateEnemy() => new EnemyController(enemyPrefab, enemyData);
    }
}

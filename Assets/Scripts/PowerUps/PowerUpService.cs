using System;
using UnityEngine;

namespace CosmicCuration.PowerUps
{
    public class PowerUpService
    {
        private PowerUpScriptableObject powerUpScriptableObject;
        private bool isSpawning;
        private float spawnTimer;
        private PowerUpPool powerUpPool;

        public PowerUpService(PowerUpScriptableObject powerUpScriptableObject)
        {
            this.powerUpScriptableObject = powerUpScriptableObject;
            spawnTimer = this.powerUpScriptableObject.spawnRate;
            isSpawning = true;
            powerUpPool = new PowerUpPool();
        }

        public void Update()
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                SpawnPowerUps();
                ResetSpawnTimer();
            }
        }

        public void ReturnPowerUpToPool(PowerUpController powerUp) => powerUpPool.ReturnItem(powerUp);

        private void ResetSpawnTimer() => spawnTimer = powerUpScriptableObject.spawnRate;

        private void SpawnPowerUps()
        {
            if (isSpawning)
            {
                // Select a random powerup type (Shield/RapidFire/DoubleTurret).
                PowerUpType randomPowerUp = (PowerUpType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(PowerUpType)).Length);

                // Fetch the corresponding PowerUpController
                PowerUpController powerUp = FetchPowerUp(randomPowerUp);

                // Configure the PowerUp to be spawned.
                powerUp.Configure(CalculateRandomSpawnPosition());
            }
        }

        private PowerUpController FetchPowerUp(PowerUpType typeToFetch)
        {
            PowerUpData fetchedData = powerUpScriptableObject.powerUpData.Find(item => item.powerUpType == typeToFetch);

            switch (typeToFetch)
            {
                case PowerUpType.Shield:
                    return powerUpPool.GetPowerUp<Shield>(fetchedData);
                case PowerUpType.DoubleTurret:
                    return powerUpPool.GetPowerUp<DoubleTurret>(fetchedData);
                case PowerUpType.RapidFire:
                    return powerUpPool.GetPowerUp<RapidFire>(fetchedData);
                default:
                    throw new Exception($"Failed to Create PowerUpController for: {typeToFetch}");
            }
        }

        private Vector2 CalculateRandomSpawnPosition()
        {
            // Get the boundaries of the visible game screen
            float minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            float maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            float minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
            float maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

            // Generate random values for X and Y coordinates within the screen boundaries
            float randomX = UnityEngine.Random.Range(minX, maxX);
            float randomY = UnityEngine.Random.Range(minY, maxY);

            // Return the calculated random spawn position
            return new Vector2(randomX, randomY);
        }

        public void SetPowerUpSpawning(bool setSpawningActive) => isSpawning = setSpawningActive;
    } 
}
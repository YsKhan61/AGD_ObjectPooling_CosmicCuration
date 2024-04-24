using UnityEngine;
using CosmicCuration.Player;
using System.Threading.Tasks;

namespace CosmicCuration.PowerUps
{
    public class PowerUpController : IPowerUp
    {
        private PowerUpView powerUpView;
        private float activeDuration;
        private bool isActive;

        public PowerUpController(PowerUpData powerUpData)
        {
            powerUpView = Object.Instantiate(powerUpData.powerUpPrefab);
            powerUpView.SetController(this);
            activeDuration = powerUpData.activeDuration;
        }

        public void Configure(Vector2 spawnPosition)
        {
            isActive = false;
            powerUpView.gameObject.SetActive(true);
            powerUpView.transform.position = spawnPosition;
        }

        public async void StartTimer()
        {
            if (isActive)
            {
                await Task.Delay(Mathf.RoundToInt(activeDuration * 1000));
                Deactivate();
            }
        }

        public void PowerUpTriggerEntered(GameObject collidedObject)
        {
            if (collidedObject.GetComponent<PlayerView>() != null)
                Activate();
        }

        public virtual void Activate()
        {
            isActive = true;
            powerUpView.gameObject.SetActive(false);
            StartTimer();
        }

        public virtual void Deactivate()
        {
            isActive = false;
            GameService.Instance.GetPowerUpService().ReturnPowerUpToPool(this);
        }
    } 
}
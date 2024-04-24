using UnityEngine;

namespace CosmicCuration.VFX
{
    public abstract class VFXController
    {
        private VFXView vfxView;

        public VFXController(VFXView vfxPrefab)
        {
            vfxView = Object.Instantiate(vfxPrefab);
            vfxView.SetController(this);
        }

        public void Configure(Vector2 spawnPosition) => vfxView.ConfigureAndPlay(spawnPosition);
    } 
}
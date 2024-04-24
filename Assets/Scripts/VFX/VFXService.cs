using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.VFX
{
    public class VFXService
    {
        private VFXPool vfxPool;

        public VFXService(VFXScriptableObject vfxScriptableObject)
        {
            vfxPool = new VFXPool(vfxScriptableObject);
        }

        public void PlayVFXAtPosition(VFXType type, Vector2 spawnPosition)
        {
            VFXController vfxController = vfxPool.GetVFX(type);
            vfxController.Configure(spawnPosition);
        }

        public void ReturnToPool(VFXController vfxController)
        {
            vfxPool.ReturnItem(vfxController);
        }
    } 
}
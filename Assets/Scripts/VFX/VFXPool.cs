using CosmicCuration.Utilities;
using System;


namespace CosmicCuration.VFX
{
    public class VFXPool : GenericObjectPool<VFXController>
    {
        private VFXScriptableObject vfxDataSO;

        public VFXPool(VFXScriptableObject vfxDataSO)
        {
            this.vfxDataSO = vfxDataSO;
        }

        public VFXController GetVFX(VFXType type)
        {
            switch (type)
            {
                case VFXType.PlayerExplosion:
                    return GetItem<PlayerExplosion>();
                case VFXType.EnemyExplosion:
                    return GetItem<EnemyExplosion>();
                case VFXType.BulletHitExplosion:
                    return GetItem<BulletHitExplosion>();
                default:
                    throw new NotImplementedException("VFXType not implemented");
            }
        }

        protected override VFXController CreateItem<T>()
        {
            if (typeof(T) == typeof(PlayerExplosion))
            {
                return new PlayerExplosion(vfxDataSO.GetVFXData(VFXType.PlayerExplosion).prefab);
            }
            else if (typeof(T) == typeof(EnemyExplosion))
            {
                return new EnemyExplosion(vfxDataSO.GetVFXData(VFXType.EnemyExplosion).prefab);
            }
            else if (typeof(T) == typeof(BulletHitExplosion))
            {
                return new BulletHitExplosion(vfxDataSO.GetVFXData(VFXType.BulletHitExplosion).prefab);
            }
            else
            {
                throw new System.NotImplementedException("PowerUp type not implemented");
            }
        }
    }
}
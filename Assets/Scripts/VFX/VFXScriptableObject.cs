using System;
using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.VFX
{
    [CreateAssetMenu(fileName = "VFXScriptableObject", menuName = "ScriptableObjects/VFXScriptableObject")]
    public class VFXScriptableObject : ScriptableObject
    {
        public List<VFXData> vfxData;

        public VFXData GetVFXData(VFXType type)
        {
            return vfxData.Find(vfx => vfx.type == type);
        }
    }

    [Serializable]
    public class VFXData
    {
        public VFXType type;
        public VFXView prefab;
    }
}
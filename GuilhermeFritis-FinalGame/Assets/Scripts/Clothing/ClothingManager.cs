using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;

namespace Clothing
{
    public enum ClothingType
    {
        SPEED,
        JUMP_HEIGHT,
        STRENGTH
    }

    public class ClothingManager : Singleton<ClothingManager>
    {
        public List<ClothingSetup> clothingSetups;

        public ClothingSetup GetSetupByType(ClothingType clothingType)
        {
            return clothingSetups.Find(i => i.clothingType == clothingType);
        }
    }

    [System.Serializable]
    public class ClothingSetup
    {
        public ClothingType clothingType;
        public Texture2D texture;
    }
}

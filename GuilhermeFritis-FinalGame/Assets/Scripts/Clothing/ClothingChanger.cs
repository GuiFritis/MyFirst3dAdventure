using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clothing
{    
    public class ClothingChanger : MonoBehaviour
    {
        public SkinnedMeshRenderer mesh;
        public Texture2D texture;

        [SerializeField]
        private string _shaderIdName = "_EmissionMap";
        private Texture2D _defaultTexture;
        private ClothingType _clothingType = ClothingType.NONE;

        void Awake()
        {
            _defaultTexture = (Texture2D) mesh.materials[0].GetTexture(_shaderIdName);
        }

        public void ChangeTexture(ClothingSetup setup)
        {
            _clothingType = setup.clothingType;
            if(setup.texture != null)
            {
                mesh.materials[0].SetTexture(_shaderIdName, setup.texture);
            }
        }

        public void ResetTexture()
        {
            _clothingType = ClothingType.NONE;
            mesh.materials[0].SetTexture(_shaderIdName, _defaultTexture);
        }

        public ClothingType GetClothingType()
        {
            return _clothingType;
        }
    }
}

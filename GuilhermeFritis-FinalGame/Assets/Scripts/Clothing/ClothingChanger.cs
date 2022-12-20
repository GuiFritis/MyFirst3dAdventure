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

        void Awake()
        {
            _defaultTexture = (Texture2D) mesh.materials[0].GetTexture(_shaderIdName);
        }

        private void ChangeTexture()
        {
            mesh.materials[0].SetTexture(_shaderIdName, texture);
        }

        public void ChangeTexture(ClothingSetup setup)
        {
            mesh.materials[0].SetTexture(_shaderIdName, setup.texture);
        }

        public void ResetTexture()
        {
            mesh.materials[0].SetTexture(_shaderIdName, _defaultTexture);
        }
    }
}

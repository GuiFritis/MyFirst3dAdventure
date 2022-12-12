using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothChanger : MonoBehaviour
{
    public SkinnedMeshRenderer mesh;
    public Texture2D texture;

    [SerializeField]
    private string _shaderIdName = "_EmissionMap";

    private void ChangeTexture()
    {
        mesh.materials[0].SetTexture(_shaderIdName, texture);
    }
}

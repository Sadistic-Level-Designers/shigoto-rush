using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureFlipbook : MonoBehaviour
{
    public Texture[] textures;

    public string propertyName;
    public Material material;

    public float flipSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        int i = Mathf.RoundToInt( Time.time * flipSpeed ) % textures.Length;
        material.SetTexture(propertyName, textures[i]);
    }
}

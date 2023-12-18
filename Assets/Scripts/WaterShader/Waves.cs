using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

    //Wave strength
    [SerializeField, Tooltip("Defines the strength of the waves.")]
    private float waveStrength = 1.0f;

    //Wave speed
    [SerializeField, Tooltip("Defines the speed of the waves.")]
    private float waveSpeed = 1.0f;

    // Waves direction in degrees.
    [SerializeField, Tooltip("Defines the direction the waves are going by degree.")]
    private float wavesDirection = 0f;

    // Texture scroll speed.
    [SerializeField, Tooltip("Defines the texture scroll speed.")]
    private float textureScrollSpeed = 0.5f;

    private void Start()
    {
        // Getting references to components.
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        // Scrolling water texture
        float xSpeed = Mathf.Sin(wavesDirection * Mathf.Deg2Rad);
        float zSpeed = Mathf.Cos(wavesDirection * Mathf.Deg2Rad);
        meshRenderer.material.mainTextureOffset += new Vector2(xSpeed, zSpeed) * textureScrollSpeed * Time.deltaTime;

        //Getting references
        var mesh = meshFilter.mesh;
        var verts = mesh.vertices;

        //Changing vertice elevation
        for (int i = 0; i < verts.Length; i++)
        {
            float xOffset = verts[i].x * xSpeed;
            float zOffset = verts[i].z * zSpeed;
            float elevation = Mathf.Sin(xOffset + zOffset + Time.time * waveSpeed) * waveStrength;

            verts[i].y = elevation;
        }
        //Applying changes to the mesh
        mesh.vertices = verts;
    }
}

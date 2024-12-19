using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandFill : MonoBehaviour
{
    float timeInMins = 2f;
    float maxSize = 2.5f;
    Mesh mesh;
    Vector3[] meshVertices;
    public float fillSpeed;
    public float tickDeformation;
    float timer = 0f;
    public float interval = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshVertices = mesh.vertices;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval) 
        {
            incrementSand();
            timer = 0f;
        }
    }

    private void incrementSand()
    {
        for (int i = 0; i < meshVertices.Length; i++)
        {
            meshVertices[i].y += Random.Range(-tickDeformation / 2, tickDeformation) * fillSpeed;
            //meshVertices[i].y += Mathf.PerlinNoise(meshVertices[i].x, meshVertices[i].z) * fillSpeed * tickDeformation;
        }
        mesh.vertices = meshVertices;
    }
}

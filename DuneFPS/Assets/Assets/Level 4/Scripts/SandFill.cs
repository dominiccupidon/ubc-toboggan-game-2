using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandFill : MonoBehaviour
{
    public float timeInMins = 2f;
    float maxSize = 2.5f;
    Mesh mesh;
    Vector3[] meshVertices;
    float fillSpeed = 0.01f;
    float tickDeformation = 1f;
    float timer = 0f;
    float interval = 0.1f;

                //0.01, 1, 0.1 = 92
                //0.02, 1, 0.1 = 43
                //0.01, 2, 0.1 = 39
                //0.01, 1, 0.2 = 177
                
    // Start is called before the first frame update
    void Start()
    {
        float multiplier = 92f / (timeInMins * 60);
        tickDeformation *= multiplier;
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
            if (meshVertices[i].y < maxSize)
            {
                meshVertices[i].y += Random.Range(-tickDeformation / 2, tickDeformation) * fillSpeed;
            }
            else 
            {
                Debug.Log("max reached after " + Time.time);
            }
        }
        mesh.vertices = meshVertices;
    }
}

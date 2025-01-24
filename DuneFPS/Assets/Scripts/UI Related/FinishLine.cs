using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float floatAmplitude = 0.5f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
    }
}

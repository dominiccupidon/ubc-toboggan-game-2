using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Door : MonoBehaviour
{
    private Animator animator;
    private float deltaX = -18;
    private float time = 4;
    private float speed;
    private Vector3 endPos;
    private bool isMoving = false;
    private bool hasMoved = false;
    // Start is called before the first frame update
    void Start()
    {
        endPos = transform.position;
        endPos.x += deltaX;
        speed = Mathf.Abs(deltaX) / time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) { open(); }

        if (isMoving) 
        { 
            float step = Time.deltaTime * speed;
            transform.position = Vector3.MoveTowards(transform.position, endPos, step);
            if (transform.position.x <= endPos.x) 
            { 
                isMoving = false;
            }
        }
    }

    public void open() 
    {
        if (!hasMoved) 
        {
            isMoving = true;
            hasMoved = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float speed = 2f; //scrolling speed
    private float width; //Width of background

    void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        //Move background to left
        transform.position += Vector3.left * speed * Time.deltaTime;

        //If moves off-screen then reposition
        if (transform.position.x <= -width)
        {
            transform.position += new Vector3(width * 2, 0, 0);
        }
    }
}
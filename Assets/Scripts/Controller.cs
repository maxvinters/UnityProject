using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField]
    int Speed = 10;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        if (Input.GetKey(KeyCode.D))
            rb.velocity = new Vector2(Speed, rb.velocity.y);
        else if (Input.GetKey(KeyCode.A))
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
        if (rb.velocity.x > 10)
            GetComponent<SpriteRenderer>().flipX = false;
        else if (GetComponent<Rigidbody2D>().velocity.x < -10)
            GetComponent<SpriteRenderer>().flipX = true;
        if (rb.velocity.x < -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (rb.velocity.x > 1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }


        
    }
    
    
}

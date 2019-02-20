using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsScript : MonoBehaviour {

    Rigidbody2D rb;
    MovementScript mov;

    bool onStairs;

    [SerializeField]
    Joystick joystick;

    [SerializeField]
    float speed;
    float gravScale;

    void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
        mov = GetComponent<MovementScript>();
        gravScale = rb.gravityScale;
	}
	
	void Update () 
    {
		if(onStairs)
        {
            rb.velocity = new Vector2(joystick.Horizontal, joystick.Vertical) * speed;
        }
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.CompareTag("Stairs"))
        {
            onStairs = true;
            rb.gravityScale = 0;
            mov.inOtherMovement = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Stairs"))
        {
            onStairs = false;
            rb.gravityScale = gravScale;
            mov.inOtherMovement = false;
        }
    }

}

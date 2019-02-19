using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintScript : MonoBehaviour {


    MovementScript mov;
    Rigidbody2D rb;
    [SerializeField]
    Joystick joystick;
    [SerializeField]
    float MaxSpeed;
    [SerializeField]
    float deceleration;
    bool inAction;
    bool stop;
    int direction;
    

    void Start()
    {
        inAction = false;
        stop = false;
        mov = GetComponent<MovementScript>();
        rb = GetComponent<Rigidbody2D>();
        direction = 1;
    }


    void Update()
    {
        if (inAction)
        {
            Vector2 axis = new Vector2(joystick.Horizontal, joystick.Vertical);
            if(Vector2.Distance(Vector2.zero, axis) < 0.85f || Mathf.Abs(axis.y) > 0.35f)
            {
                inAction = false;
                stop = true;
            }
            else
                rb.velocity = new Vector2(axis.x * MaxSpeed, rb.velocity.y);
        }
        if(stop)
        {
            rb.velocity = new Vector2(rb.velocity.x - rb.velocity.x * deceleration, rb.velocity.y);
            if (Mathf.Abs(rb.velocity.x) < 0.3f)
            {
                CapsuleCollider2D col = GetComponent<CapsuleCollider2D>();
                col.direction = CapsuleDirection2D.Vertical;
                col.size = new Vector2(col.size.y, col.size.x);
                stop = false;
                mov.inOtherMovement = false;
            }
        }
    }

    public void StartSprint(bool isRight)
    {
        if (isRight)
            direction = 1;
        else
            direction = -1;
        CapsuleCollider2D col = GetComponent<CapsuleCollider2D>();
        col.direction = CapsuleDirection2D.Horizontal;
        col.size = new Vector2(col.size.y, col.size.x);
        inAction = true;
    }
}

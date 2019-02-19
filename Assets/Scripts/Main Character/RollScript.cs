using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollScript : MonoBehaviour {

    MovementScript mov;
    Rigidbody2D rb;
    [SerializeField]
    float MaxSpeed;
    [SerializeField]
    float acceleration;
    bool inAction;
    bool stage2;
    int direction;


    void Start () 
    {
        inAction = false;
        stage2 = false;
        mov = GetComponent<MovementScript>();
        rb = GetComponent<Rigidbody2D>();
        direction = 1;
	}
	
	
	void Update ()
    {
		if(inAction)
        {
            if (!stage2)
            {
                rb.velocity = new Vector2(rb.velocity.x + (MaxSpeed-Mathf.Abs(rb.velocity.x)) * acceleration * direction, rb.velocity.y);
                stage2 = Mathf.Abs(rb.velocity.x) > MaxSpeed - 0.1f;
            }
            else
            {

                rb.velocity = new Vector2(rb.velocity.x - (MaxSpeed - Mathf.Abs(rb.velocity.x)) * acceleration * direction, rb.velocity.y);
                if (Mathf.Abs(rb.velocity.x) < 0.5f)
                {
                    mov.inOtherMovement = inAction = false;
                    CapsuleCollider2D col = GetComponent<CapsuleCollider2D>();
                    col.direction = CapsuleDirection2D.Vertical;
                    col.size = new Vector2(col.size.y, col.size.x);
                    stage2 = false;
                }

            }
        }
    }

    public void StartRoll(bool isRight)
    {
        if (isRight)
            direction = 1;
        else
            direction = -1;
        CapsuleCollider2D col = GetComponent<CapsuleCollider2D>();
        col.direction = CapsuleDirection2D.Horizontal;
        col.size = new Vector2(col.size.y, col.size.x);
        inAction = true;
        rb.velocity = new Vector2(rb.velocity.x * 0.3f, 0);
    }
}

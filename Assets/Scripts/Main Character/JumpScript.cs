using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour {

    MovementScript mov;
    Rigidbody2D rb;
    Animator anim;
    [SerializeField]
    LayerMask mask;
    public bool isGrounded;
    [SerializeField]
    int jumpPower;
    [Header("X/Y ratio")]
    [SerializeField]
    Vector2 JumpDirection;
    [SerializeField]
    Transform legs;
    public float RayLen;

	void Start ()
    {
        mov = GetComponent<MovementScript>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        JumpDirection = JumpDirection.normalized;
        isGrounded=true;
    }

	void FixedUpdate ()
    {

        if(!isGrounded)
        {
            //Debug.DrawLine(legs.position, Vector2.down, Color.red, RayLen);
            RaycastHit2D hit = Physics2D.Raycast(legs.position, Vector2.down, RayLen, mask.value);
            if (hit)
            {
                Debug.DrawLine(legs.position, hit.transform.position, Color.red);
                //Debug.Log("Grounded");
                isGrounded = true;
                mov.inOtherMovement = false;
            }

        }
    }

    public void StartJump(int state) //1-up and rigth, 2- up, 3-up and left
    {
        rb.velocity = Vector2.zero; //???
        //RaycastHit2D hit = Physics2D.Raycast(legs.position, Vector2.down, RayLen, mask.value);
        if (isGrounded)
        {
            if (state == 2)
            {
                //anim.Play();

                rb.AddForce(Vector2.up * jumpPower);
            }
            else if (state == 1)
            {
                //anim.Play();
                rb.AddForce(JumpDirection * jumpPower);
            }
            else if (state == 3)
            {
                //anim.Play();
                rb.AddForce(JumpDirection * new Vector2(-1, 1) * jumpPower);
            }

            Invoke("ChangeGround", 0.8f);
        }
        else mov.inOtherMovement = false;
    }

    void ChangeGround()
    {
        isGrounded = false;
    }
}

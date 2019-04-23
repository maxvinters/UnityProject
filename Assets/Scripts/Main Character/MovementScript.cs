using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

    Animator anim;
    Rigidbody2D rb;
    CapsuleCollider2D col;

    JumpScript jmp;
    SprintScript sprint;
    RollScript roll;

    [SerializeField]
    float Speed;
    [SerializeField]
    float SneakSpeed;
    public Joystick joystick;
    public DragonBones.UnityArmatureComponent animobj;
    int lastBorderState; //0-right,1-right jump,2-jump,3-left jump,4-left,5-left roll,6-down,7-right roll
    float lastBorderTime;
    //[HideInInspector]
    public bool inOtherMovement;
    
    float Ysize;
    [SerializeField]
    bool isSitting;



	void Start () 
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jmp = GetComponent<JumpScript>();
        roll = GetComponent<RollScript>();
        sprint = GetComponent<SprintScript>();
        col = GetComponent<CapsuleCollider2D>();
        //animobj = this.gameObject.GetComponentInChildren<DragonBones.UnityArmatureComponent>();
        Ysize = col.size.y;
        isSitting = false;
        inOtherMovement = false;


	}


    void FixedUpdate()
    {
        if (!inOtherMovement)
        {
            Vector2 axis = new Vector2(joystick.Horizontal, joystick.Vertical);
            int state = BorderState(axis);
            if (isSitting && state < 5)
            {
                col.size = new Vector2(col.size.x, Ysize);
                isSitting = false;

            }
            if (Vector2.Distance(Vector2.zero, axis) > 0.85f) // if joystick in border
            {
                Debug.Log(axis);
                
                if (lastBorderState == state && (Time.time - lastBorderTime) > 0.1f && (Time.time - lastBorderTime) < 0.5f && lastBorderTime != 0) //if double click in same direction
                {

                    if (state == 0)
                    {
                        inOtherMovement = true;
                        sprint.StartSprint(true);

                    }
                    else if (state == 4)
                    {
                        inOtherMovement = true;
                        sprint.StartSprint(false);
                    }
                    if (state == 7)
                    {
                        col.size = new Vector2(col.size.x, Ysize);
                        inOtherMovement = true;
                        roll.StartRoll(true);

                    }

                    if (state == 5)
                    {
                        col.size = new Vector2(col.size.x, Ysize);
                        inOtherMovement = true;
                        roll.StartRoll(false);

                    }

                    lastBorderTime = Time.time;
                    lastBorderState = state;
                    return;
                }
                lastBorderTime = Time.time;
                lastBorderState = state;


                if (state > 0 && state < 4) // if state - jump
                {
                    inOtherMovement = true;
                    jmp.StartJump(state);
                    return;
                }


                if(state > 4  && !isSitting)
                {
                    rb.velocity = new Vector2(0,rb.velocity.y);
                    isSitting = true;
                    col.size = new Vector2(col.size.x, col.size.x);
                }



            }

            if (!isSitting)
            {

                rb.velocity = new Vector2(axis.x * Speed, rb.velocity.y);
                //rb.AddForce(new Vector2(axis.x * Speed, rb.velocity.y));
                //transform.Translate(new Vector3(axis.x * Speed*0.01f, rb.velocity.y, 0));
            }
            if (isSitting && (state == 5 || state == 7))
                rb.velocity = new Vector2(axis.x * SneakSpeed, rb.velocity.y);
        }

    }




    int BorderState(Vector2 axis)
    {
        if (axis.x > 0)
            animobj.armature.flipX = false;
        else if(axis.x < 0)
        {
            animobj.armature.flipX = true;
        }
        if (Mathf.Abs(axis.y) < 0.35)
        {
            if (axis.x > 0)
                return 0;
            return 4;
        }
        if(Mathf.Abs(axis.x) < 0.35)
        {
            if (axis.y > 0)
                return 2;
            return 6;
        }
        if (axis.x > 0)
        {
            if (axis.y > 0)
                return 1;
            return 7;
        }
        if (axis.y > 0)
            return 3;

        return 5;
    }
}



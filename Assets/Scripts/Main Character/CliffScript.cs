using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffScript : MonoBehaviour {


    MovementScript mov;
    Animator anim;
    Rigidbody2D rb;
    [SerializeField]
    CapsuleCollider2D coll;

    bool OnCliff;



    void Start () 
    {
        anim = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
        mov = GetComponentInParent<MovementScript>();
        OnCliff = false;

    }
	

	void Update ()
    {
        if(OnCliff && Input.GetKeyDown(KeyCode.S))
        {
            rb.simulated = true;
            coll.enabled = false;
            Invoke("NewCliff", 1f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("RCliffTag"))
        {
            rb.velocity = Vector2.zero;
            rb.simulated = false;
            mov.inOtherMovement = true;
            OnCliff = true;

        }
        if (col.gameObject.CompareTag("LCliffTag"))
        {
            rb.velocity = Vector2.zero;
            rb.simulated = false;
            mov.inOtherMovement = true;
            OnCliff = true;
        }

    }

    void NewCliff()
    {
        coll.enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour {

    MovementScript mov;
    Rigidbody2D rb;
    CapsuleCollider2D col;
    bool isHiding;
    bool inShadow;
    GameObject shadow;
    [SerializeField]
    Joystick joystick;

    void Start ()
    {
        mov = GetComponent<MovementScript>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
    }
	
	
	void Update ()
    {
        if (inShadow)
        {
            Vector2 axis = new Vector2(joystick.Horizontal, joystick.Vertical);

            if(Input.GetKeyDown(KeyCode.Space) && !isHiding && !mov.inOtherMovement)
            {
                isHiding = true;
                mov.inOtherMovement = true;
                col.enabled = false;
                rb.simulated = false;
                shadow.GetComponent<EnvShadowScript>().isBusy = true;
                transform.position = shadow.transform.GetChild(0).transform.position;
                GetComponentInChildren<SpriteRenderer>().sortingOrder=0;
                return;
            }
            if ((Input.GetKeyDown(KeyCode.Space) && isHiding) || (Vector2.Distance(Vector2.zero, axis) > 0.70f && Mathf.Abs(axis.y) < 0.3f && isHiding))
            {
                isHiding = mov.inOtherMovement = false;
                col.enabled = true;
                rb.simulated = true;
                shadow.GetComponent<EnvShadowScript>().isBusy = false;
                GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Shadow"))
        {
            inShadow = true;
            shadow = trig.gameObject;

        }
    }
    void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Shadow") && !isHiding)
        {
            inShadow = false;

        }
    }

}

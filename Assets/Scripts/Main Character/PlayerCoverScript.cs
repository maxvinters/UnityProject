using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoverScript : MonoBehaviour {

    MovementScript mov;
    Rigidbody2D rb;
    CapsuleCollider2D col;
    bool isHiding;
    bool inCover;
    GameObject Cover;

    [SerializeField]
    Joystick joystick;

    void Start()
    {
        mov = GetComponent<MovementScript>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
    }


    void Update()
    {
        if (inCover)
        {
            Vector2 axis = new Vector2(joystick.Horizontal, joystick.Vertical);

            if (Input.GetKeyDown(KeyCode.Space) && !isHiding && !mov.inOtherMovement)
            {
                isHiding = true;
                mov.inOtherMovement = true;
                if (Cover.transform.position.x < transform.position.x)
                    GetComponentInParent<Transform>().position = Cover.transform.GetChild(0).transform.position;
                else
                    GetComponentInParent<Transform>().position = Cover.transform.GetChild(1).transform.position;
                Cover.GetComponent<CoverScript>().Enablehitbox();
                return;
            }
            if ((Input.GetKeyDown(KeyCode.Space) && isHiding) || (Vector2.Distance(Vector2.zero, axis) > 0.70f && Mathf.Abs(axis.y) < 0.3f && isHiding))
            {
                Cover.GetComponent<CoverScript>().Disablehitbox();
                isHiding = mov.inOtherMovement = false;

            }
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Cover"))
        {
            inCover = true;
            Cover = trig.gameObject;

        }
    }
    void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Cover") && !isHiding)
        {
            inCover = false;

        }
    }

}

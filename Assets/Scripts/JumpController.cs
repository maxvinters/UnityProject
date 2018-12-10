using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour {

    Rigidbody2D rb;
    [SerializeField]
    float jumpForce = 50f;
    [SerializeField]
    GameObject JumpRing;
    float JumpRad = 0.26f;
    float LerpSpeed = 25f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update ()
    {
        if (rb.velocity.y < -400)
            rb.velocity = new Vector2(rb.velocity.x, -400);
        if (Input.GetKey(KeyCode.Space) && CheckGround())
        {
            JumpRing.transform.localScale = new Vector3(Mathf.Lerp(JumpRing.transform.localScale.x, JumpRad, Time.deltaTime* LerpSpeed), Mathf.Lerp(JumpRing.transform.localScale.y, JumpRad, Time.deltaTime* LerpSpeed), 1);
        }
        else if(JumpRing.transform.localScale.x>0.001f)
            JumpRing.transform.localScale = new Vector3(Mathf.Lerp(JumpRing.transform.localScale.x, 0f, Time.deltaTime * LerpSpeed), Mathf.Lerp(JumpRing.transform.localScale.y, 0f, Time.deltaTime * LerpSpeed), 1);
        if (Input.GetKeyUp(KeyCode.Space) && CheckGround())
        {
            float g = 4 * 9.82f;
            if (Vector3.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10)) < 2)
            {
                int sign = 1;
                float Hmax = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
                if (Hmax < 0)
                {
                    Hmax *= -1;
                    sign *= 1;
                }
                float Length = (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x)*2;
                float alpha = Mathf.Atan2(4 * Hmax, Length);
                float ForcMag = Mathf.Sqrt(Hmax * 2 * g / Mathf.Pow(Mathf.Sin(alpha), 2));
                Vector2 Force = new Vector2(ForcMag *sign * Mathf.Cos(alpha), ForcMag * Mathf.Sin(alpha));
                rb.velocity = Force;
            }
            else
            {
                int sign = 1;
                Vector2 delta = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y).normalized * 2;
                float Hmax = delta.y;
                if (Hmax < 0)
                {
                    Hmax *= -1;
                    sign *= 1;
                }
                float Length = delta.x * 2;
                float alpha = Mathf.Atan2(4 * Hmax, Length);
                float ForcMag = Mathf.Sqrt(Hmax * 2 * g / Mathf.Pow(Mathf.Sin(alpha), 2));
                Vector2 Force = new Vector2(ForcMag * sign * Mathf.Cos(alpha), ForcMag * Mathf.Sin(alpha));
                rb.velocity = Force;
            }
        }
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -10, 10), rb.velocity.y);
    }


    bool CheckGround()
    {
        RaycastHit2D[] tmp = Physics2D.RaycastAll(transform.position, Vector2.down, 0.51f);
        Debug.DrawLine(transform.position, transform.position + new Vector3(0, -0.51f, 0), Color.red);
        if (tmp.Length > 1)
        {
            if (tmp[1].collider.CompareTag("Ground"))
                return true;
            else
                return false;
        }
        else
            return false;
    }
}

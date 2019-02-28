using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeScript : MonoBehaviour {

    BattleFieldScript bfs;
    ShootingScript shs;
    LegsScript lgs;
    Rigidbody2D rb;

    [SerializeField]
    Sprite wtf;

    [SerializeField]
    Transform eye;

    [SerializeField]
    float thinkTime;

    [SerializeField]
    LayerMask mask;

    [SerializeField]
    float range;

    bool isRight;

	void Start ()
    {
        isRight = false;
        bfs = GetComponent<BattleFieldScript>();
        shs = GetComponent<ShootingScript>();
        lgs = GetComponent<LegsScript>();
        rb = GetComponent<Rigidbody2D>();


    }

    void Update ()
    {
        if(Mathf.Abs(rb.velocity.x) > 0.1f)
            isRight = rb.velocity.x > 0;
        RaycastHit2D[] hits = new RaycastHit2D[6];
        if (isRight)
        {

            hits[0] = Physics2D.Raycast(eye.position, Vector2.right, range, mask);
            hits[1] = Physics2D.Raycast(eye.position, Vector2.right + new Vector2(0, 0.15f), range, mask);
            hits[2] = Physics2D.Raycast(eye.position, Vector2.right + new Vector2(0, -0.15f), range, mask);
            hits[3] = Physics2D.Raycast(eye.position, Vector2.right + new Vector2(0, 0.30f), range, mask);
            hits[4] = Physics2D.Raycast(eye.position, Vector2.right + new Vector2(0, -0.30f), range, mask);
            hits[5] = Physics2D.Raycast(eye.position, Vector2.right + new Vector2(0, -0.95f), range / 4, mask);
            Debug.DrawRay(eye.position, Vector2.right * range, Color.red);
            Debug.DrawRay(eye.position, (Vector2.right + new Vector2(0, -0.15f)) * range, Color.red);
            Debug.DrawRay(eye.position, (Vector2.right + new Vector2(0, 0.15f))* range, Color.red);
            Debug.DrawRay(eye.position, (Vector2.right + new Vector2(0, -0.30f)) * range, Color.red);
            Debug.DrawRay(eye.position, (Vector2.right + new Vector2(0, 0.30f)) * range, Color.red);
            Debug.DrawRay(eye.position, (Vector2.right + new Vector2(0, -0.95f)) * range / 4, Color.red);
        }
        else
        {
            hits[0] = Physics2D.Raycast(eye.position, Vector2.left, range, mask);
            hits[1] = Physics2D.Raycast(eye.position, Vector2.left + new Vector2(0, 0.15f), range, mask);
            hits[2] = Physics2D.Raycast(eye.position, Vector2.left + new Vector2(0, -0.15f), range, mask);
            hits[3] = Physics2D.Raycast(eye.position, Vector2.left + new Vector2(0, 0.30f), range, mask);
            hits[4] = Physics2D.Raycast(eye.position, Vector2.left + new Vector2(0, -0.30f), range, mask);
            hits[5] = Physics2D.Raycast(eye.position, Vector2.left + new Vector2(0, -0.95f), range / 4, mask);
            Debug.DrawRay(eye.position, Vector2.left * range, Color.red);
            Debug.DrawRay(eye.position, (Vector2.left + new Vector2(0, -0.15f)) * range, Color.red);
            Debug.DrawRay(eye.position, (Vector2.left + new Vector2(0, 0.15f)) * range, Color.red);
            Debug.DrawRay(eye.position, (Vector2.left + new Vector2(0, -0.30f)) * range, Color.red);
            Debug.DrawRay(eye.position, (Vector2.left + new Vector2(0, 0.30f)) * range, Color.red);
            Debug.DrawRay(eye.position, (Vector2.left + new Vector2(0, -0.95f)) * range / 4, Color.red);
        }
        foreach (RaycastHit2D hit in hits)
        {
            if (hit && hit.collider.CompareTag("Player"))
                StartCoroutine(BigBrotherWatchingYou());
        }
    }


    IEnumerator BigBrotherWatchingYou()
    {
        GetComponent<SpriteRenderer>().sprite = wtf;
        Destroy(lgs);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(thinkTime);
        bfs.enabled = true;
        shs.enabled = true;
        transform.GetChild(2).gameObject.SetActive(false);
        enabled = false;

    }
}

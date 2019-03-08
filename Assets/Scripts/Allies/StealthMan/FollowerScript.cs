using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerScript : MonoBehaviour {

    [SerializeField]
    GameObject player;

    [SerializeField]
    float offset;

    [SerializeField]
    float speed;

    [SerializeField]
    LayerMask coverMask;

    [SerializeField]
    LayerMask shadowMask;

    [SerializeField]
    float Range;
    [SerializeField]
    GameObject myCover;
    Rigidbody2D rb;
    Vector3 targetpos;
    bool onPosition;
    bool onTarget;
    bool inCover;

	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
        onPosition = false;
        onTarget = false;
        InvokeRepeating("TargetUpdate", Random.Range(min: 0, max: 1f), 0.5f);
        NewTarget();
    }
	
	
	void FixedUpdate () 
    {
        if (onTarget && !onPosition && Mathf.Abs(transform.position.x - targetpos.x) < 0.5f && !myCover)
        {
            rb.velocity = Vector2.zero;
            onPosition = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("CoverTag") && !inCover && col.gameObject.Equals(myCover))
        {
            inCover = true;
            myCover.transform.parent.GetComponent<CoverScript>().isBusy = true;
            rb.velocity = Vector2.zero;
        }
        if(col.CompareTag("Shadow") && !inCover && col.gameObject.Equals(myCover))
        {
            print("Chik");
            inCover = true;
            myCover.GetComponent<EnvShadowScript>().isBusy = true;
            rb.velocity = Vector2.zero;
            rb.simulated = false;
            transform.position = myCover.transform.GetChild(0).transform.position;
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            GetComponent<CapsuleCollider2D>().enabled = false;
        }

    }

    void NewTarget()
    {

        inCover = false;
        myCover = FindCover();
        if (myCover)
        {
            rb.velocity = new Vector2(myCover.transform.position.x - transform.position.x, 0).normalized * speed;
            onPosition = false;
            return;
        }
        onPosition = false;
        targetpos = player.transform.position - new Vector3(Mathf.Sign(player.transform.position.x - transform.position.x) * offset*0.6f, 0);
        rb.velocity = new Vector2(targetpos.x - transform.position.x, 0).normalized * speed;
        onTarget = true;
    }

    void TargetUpdate()
    {
        bool isBusy;
        if (myCover && myCover.CompareTag("Shadow"))
            isBusy = myCover.GetComponent<EnvShadowScript>().isBusy;
        else if (myCover)
            isBusy = myCover.transform.parent.GetComponent<CoverScript>().isBusy;
        else
            isBusy = false;
        if (onTarget && (Vector2.Distance(targetpos, player.transform.position) > offset * 1.1f) || 
            myCover && !inCover && isBusy)
        {
            if (myCover)
                ClearMyCover();
            NewTarget();
        }
    }

    GameObject FindCover()
    {
        if (myCover)
            ClearMyCover();
        List<GameObject> Covers = new List<GameObject>();
        Collider2D[] covcoliders = Physics2D.OverlapCircleAll(transform.position, Range * 1.2f, coverMask);

        foreach (Collider2D col in covcoliders)
        {
            if (Mathf.Abs(col.transform.position.y - transform.position.y) < 4 && !col.GetComponent<CoverScript>().isBusy && (A_Between_B_and_C(player.transform.position.x, transform.position.x, col.transform.position.x)
                || Mathf.Abs(player.transform.position.x - col.transform.position.x) < 5f) && Mathf.Abs(transform.position.x - col.transform.position.x)>1.4f)
                Covers.Add(col.gameObject);
        }
        Covers.Sort((x, y) => Vector2.Distance(x.transform.position, transform.position).CompareTo(Vector2.Distance(y.transform.position, transform.position)));
        for (int i = 0; i < Covers.Count; i++)
        {
            if (Vector2.Distance(Covers[i].transform.position, player.transform.position) < offset*1.5f )

            {
                onTarget = true;
                targetpos = Covers[i].transform.position;
                if (transform.position.x < Covers[i].transform.position.x)
                    return Covers[i].transform.GetChild(1).gameObject;
                else
                    return Covers[i].transform.GetChild(0).gameObject;
            }
        }
        List<GameObject> Shadows = new List<GameObject>();
        Collider2D[] shadowcolliders = Physics2D.OverlapCircleAll(transform.position, Range * 1.2f, shadowMask);
        foreach (Collider2D col in shadowcolliders)
        {
            if (Mathf.Abs(col.transform.position.y - transform.position.y) < 8f  && !col.GetComponent<EnvShadowScript>().isBusy && (A_Between_B_and_C(player.transform.position.x, transform.position.x, col.transform.position.x)
                || Mathf.Abs(player.transform.position.x - col.transform.position.x) < 5f) && Mathf.Abs(transform.position.x - col.transform.position.x) > 1.4f)
                Shadows.Add(col.gameObject);
        }
        Shadows.Sort((x, y) => Vector2.Distance(x.transform.position, transform.position).CompareTo(Vector2.Distance(y.transform.position, transform.position)));
        for (int i = 0; i < Shadows.Count; i++)
        {
            if (Vector2.Distance(Shadows[i].transform.position, player.transform.position) < offset * 1.5f)

            {
                onTarget = true;
                targetpos = Shadows[i].transform.position;
                return Shadows[i].gameObject;
            }
        }
        return null;
    }

    void ClearMyCover()
    {
        if(myCover.CompareTag("Shadow"))
        {
            rb.simulated = true;
            GetComponent<SpriteRenderer>().sortingOrder = 2;
            GetComponent<CapsuleCollider2D>().enabled = true;
            myCover.GetComponent<EnvShadowScript>().isBusy = true;
        }
        else
            myCover.transform.parent.GetComponent<CoverScript>().isBusy = false; 
    }

    public static bool A_Between_B_and_C(float A, float B, float C) // is A between B and C in this axis
    {
        return Mathf.Approximately(Mathf.Sign(A - B), Mathf.Sign(C - A));
    }



}

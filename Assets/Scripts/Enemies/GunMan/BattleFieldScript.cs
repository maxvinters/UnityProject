using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldScript : MonoBehaviour {
    
    Rigidbody2D rb;

    public GameObject player;
    [SerializeField]
    LayerMask mask;

    [SerializeField]
    float Range;
    [SerializeField]
    float Speed;


    bool withoutCover;
    public bool inCover;
    bool findCover;
    [SerializeField]
    GameObject myCover;


    void Start()
    {
        inCover = false;
        findCover = false;
        withoutCover = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (withoutCover)
        {
            List<GameObject> Covers = new List<GameObject>();
            Collider2D[] covcoliders = Physics2D.OverlapCircleAll(transform.position, Range * 1.2f, mask);

            foreach (Collider2D col in covcoliders)
            {
                if (Mathf.Abs(col.transform.position.y - transform.position.y) < 4 && Vector2.Distance(transform.position, player.transform.position) > Vector2.Distance(col.transform.position, player.transform.position)
                        && Mathf.Approximately(Mathf.Sign(col.transform.position.x - transform.position.x), Mathf.Sign(player.transform.position.x - transform.position.x))&& !col.GetComponent<CoverScript>().isBusy)
                    Covers.Add(col.gameObject);
            }

            Covers.Sort((x, y) => Vector2.Distance(x.transform.position, transform.position).CompareTo(Vector2.Distance(y.transform.position, transform.position)));
            if (Covers.Count > 0 && Vector2.Distance(Covers[0].transform.position, transform.position) < Vector2.Distance(player.transform.position, transform.position)
                && Vector2.Distance(Covers[0].transform.position, player.transform.position) > 3f && Vector2.Distance(Covers[0].transform.position, player.transform.position) < Range)
            {
                ClearMyCover();
                if (player.transform.position.x < Covers[0].transform.position.x)
                    Covers[0].transform.GetChild(0).GetComponent<CircleCollider2D>().enabled = true;
                else
                    Covers[0].transform.GetChild(1).GetComponent<CircleCollider2D>().enabled = true;
                myCover = Covers[0];
                inCover = false;
                findCover = true;
                rb.velocity = new Vector2(myCover.transform.position.x - transform.position.x, 0).normalized * Speed;
            }

        }

        if (inCover && Vector2.Distance(myCover.transform.position, player.transform.position) > Range)
        {
            ClearMyCover();
            inCover = false;
            findCover = false;
        }

        if (!inCover && !findCover)
        {
            myCover = FindMyCover();
            if (!myCover)
            {
                withoutCover = true;
                myCover = new GameObject();
                findCover = true;
                myCover.transform.position = player.transform.position + new Vector3(transform.position.x > player.transform.position.x ? Range - 3f : -Range + 3f, 0);
                myCover.AddComponent<CircleCollider2D>().radius = 0.06f;
                myCover.GetComponent<CircleCollider2D>().isTrigger = true;
                myCover.tag = "CoverTag";
                myCover.name = "Temporary";
            }
            rb.velocity = new Vector2(myCover.transform.position.x - transform.position.x, 0).normalized * Speed;
        }

        if(inCover && Vector2.Distance(transform.position,player.transform.position) < 3f)
        {
            inCover = false;
            myCover = FindCoverBack();
            if(!myCover)
            {
                withoutCover = true;
                myCover = new GameObject();
                findCover = true;
                myCover.transform.position = player.transform.position + new Vector3(transform.position.x > player.transform.position.x ? 6f : -6f, 0);
                myCover.AddComponent<CircleCollider2D>().radius = 0.06f;
                myCover.GetComponent<CircleCollider2D>().isTrigger = true;
                myCover.tag = "CoverTag";
                myCover.name = "Temporary";
            }
            rb.velocity = new Vector2(myCover.transform.position.x - transform.position.x, 0).normalized * Speed;

        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("CoverTag") && !inCover && col.gameObject.Equals(myCover))
        {
            inCover = true;
            if(!withoutCover)
                myCover.transform.parent.GetComponent<CoverScript>().isBusy = true;
            rb.velocity = Vector2.zero;
        }
    }   

    GameObject FindMyCover()
    {

        if (myCover)
            ClearMyCover();
        List<GameObject> Covers = new List<GameObject>();
        Collider2D[] covcoliders = Physics2D.OverlapCircleAll(transform.position, Range * 1.2f, mask);

        foreach (Collider2D col in covcoliders)
        {
            if (Mathf.Abs(col.transform.position.y - transform.position.y) < 4 && Vector2.Distance(transform.position, player.transform.position) > Vector2.Distance(col.transform.position, player.transform.position)
                    && Mathf.Approximately(Mathf.Sign(col.transform.position.x - transform.position.x), Mathf.Sign(player.transform.position.x - transform.position.x)) && !col.GetComponent<CoverScript>().isBusy)
                Covers.Add(col.gameObject);
        }

        Covers.Sort((x, y) => Vector2.Distance(x.transform.position, transform.position).CompareTo(Vector2.Distance(y.transform.position, transform.position)));
        for (int i = 0; i < Covers.Count; i++)
        {
            if (Vector2.Distance(Covers[i].transform.position, player.transform.position) < Range && Mathf.Approximately(Mathf.Sign(player.transform.position.x - Covers[i].transform.position.x), Mathf.Sign(player.transform.position.x - transform.position.x)))
            // if can shoot from it && player and cover in same direction
            {
                findCover = true;
                Covers[i].GetComponent<CoverScript>().isBusy = true;
                if (player.transform.position.x < Covers[i].transform.position.x)
                    return Covers[i].transform.GetChild(0).gameObject;
                else
                    return Covers[i].transform.GetChild(1).gameObject;
            }
        }
        return null;
    }

    GameObject FindCoverBack()
    {
        if (myCover)
            ClearMyCover();
        List<GameObject> Covers = new List<GameObject>();
        Collider2D[] covcoliders = Physics2D.OverlapCircleAll(transform.position, Range * 1.2f, mask);

        foreach (Collider2D col in covcoliders)
        {
            if (Mathf.Abs(col.transform.position.y - transform.position.y) < 4 && Vector2.Distance(transform.position, player.transform.position) < Vector2.Distance(col.transform.position, player.transform.position)
                && !Mathf.Approximately(Mathf.Sign(col.transform.position.x-transform.position.x), Mathf.Sign(player.transform.position.x - transform.position.x)) && !col.GetComponent<CoverScript>().isBusy)
                Covers.Add(col.gameObject);
        }
        Covers.Sort((x, y) => Vector2.Distance(x.transform.position, transform.position).CompareTo(Vector2.Distance(y.transform.position, transform.position)));
        if (Covers.Count > 0)
        {
            if (Vector2.Distance(Covers[0].transform.position, player.transform.position) < Range)
            {
                Covers[0].GetComponent<CoverScript>().isBusy = true;
                if (player.transform.position.x < Covers[0].transform.position.x)
                    return Covers[0].transform.GetChild(0).gameObject;
                else
                    return Covers[0].transform.GetChild(1).gameObject;
                    
            }
        }
        return null;
    }

    void ClearMyCover()
    {
        if (!withoutCover)
        {
            myCover.transform.parent.GetComponent<CoverScript>().isBusy = false;
        }
        else
        {
            withoutCover = false;
            Destroy(myCover);
        }
    }

}

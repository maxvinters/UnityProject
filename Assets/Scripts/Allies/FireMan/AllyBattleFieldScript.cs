using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBattleFieldScript : MonoBehaviour {

    [SerializeField]
    GameObject enemy;

    [SerializeField]
    float speed;

    [SerializeField]
    LayerMask coverMask;

    [SerializeField]
    LayerMask enemyMask;

    [SerializeField]
    float Range;
    [SerializeField]
    GameObject myCover;
    Rigidbody2D rb;
    Vector3 targetpos;
    bool onTarget;
    bool inCover;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        onTarget = false;
        InvokeRepeating("CoverUpdate", Random.Range(0f, 1f), Random.Range(0.1f, 0.2f));
    }

    void FixedUpdate()  
    {
        if(!enemy)
        {
            List<GameObject> enemies = new List<GameObject>();
            Collider2D[] enemiesCol = Physics2D.OverlapBoxAll(transform.position, new Vector2(Range * 1.5f, 5), 0, enemyMask.value);
            foreach (Collider2D col in enemiesCol)
                enemies.Add(col.gameObject);
            enemies.Sort((x, y) => Vector2.Distance(x.transform.position, transform.position).CompareTo(Vector2.Distance(y.transform.position, transform.position)));
            if(enemies.Count>0)
                enemy = enemies[0];
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("CoverTag") && !inCover && col.gameObject.Equals(myCover))
        {
            inCover = true;
            myCover.GetComponentInParent<CoverScript>().isBusy = true;
            rb.velocity = Vector2.zero;
        }
    }

    GameObject FindMyCover()
    {
        if (myCover)
            ClearMyCover();
        List<GameObject> Covers = new List<GameObject>();
        Collider2D[] covcoliders = Physics2D.OverlapCircleAll(transform.position, Range * 1.2f, coverMask);
        foreach (Collider2D col in covcoliders)
        {
            if (!col.GetComponent<CoverScript>().isBusy && A_Between_B_and_C(col.transform.position.x, transform.position.x, enemy.transform.position.x) && Vector2.Distance(col.transform.position,enemy.transform.position)<Range)
                Covers.Add(col.gameObject);
        }
        Covers.Sort((x, y) => Vector2.Distance(x.transform.position, transform.position).CompareTo(Vector2.Distance(y.transform.position, transform.position)));
        foreach(GameObject a in Covers)
            print(a.name);
        for (int i = 0; i < Covers.Count; i++)
        {
            if (Vector2.Distance(Covers[i].transform.position, enemy.transform.position) < Range * 1.5f)
            {
                onTarget = true;
                targetpos = Covers[i].transform.position;
                if (enemy.transform.position.x > Covers[i].transform.position.x)
                    return Covers[i].transform.GetChild(1).gameObject;
                else
                    return Covers[i].transform.GetChild(0).gameObject;
            }
        }
        return null;
    }



    void ClearMyCover()
    {
        myCover.transform.parent.GetComponent<CoverScript>().isBusy = false;
        inCover = false;
    }
    void CoverUpdate()
    {
        if (enemy && (!onTarget || Vector2.Distance(transform.position, enemy.transform.position) > Range * 1.1f || (!inCover && myCover.GetComponentInParent<CoverScript>().isBusy)))
        {
            myCover = FindMyCover();
            if (myCover)
            {
                rb.velocity = new Vector2(myCover.transform.position.x - transform.position.x, 0).normalized * speed;
            }
        }
    }
    public static bool A_Between_B_and_C(float A, float B, float C) // is A between B and C in this axis
    {
        return Mathf.Approximately(Mathf.Sign(A - B), Mathf.Sign(C - A));
    }
}

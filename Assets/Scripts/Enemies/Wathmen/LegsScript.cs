using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsScript : MonoBehaviour {

    Rigidbody2D rb;
    [SerializeField]
    GameObject Neck;

    [SerializeField]
    Transform[] waysPos;

    [SerializeField]
    float[] waysTime;

    WayPoint[] wayPoints = new WayPoint[3];

    [SerializeField]
    float speed;
    bool isRight;
    int currentWay;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        currentWay = 0;

        for (int i=0;i<waysTime.Length;i++)
        {
            wayPoints[i] = new WayPoint(waysPos[i].position, waysTime[i]);
        }
        StartCoroutine(GoToPoint());
    }

    IEnumerator GoToPoint()
    {
        rb.velocity = new Vector2(Mathf.Sign(wayPoints[currentWay].position.x - transform.position.x), 0) * speed;
        if (isRight ^ wayPoints[currentWay].position.x - transform.position.x > 0)
            Flip();
        yield return new WaitUntil(() => Mathf.Abs(transform.position.x - wayPoints[currentWay].position.x) < 0.5f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(wayPoints[currentWay].restTime);
        currentWay++;
        if (currentWay == wayPoints.Length)
            currentWay = 0;
        StartCoroutine(GoToPoint());
    }


    void Flip()
    {
        isRight = !isRight;
        GetComponent<SpriteRenderer>().flipX ^= true;
        Neck.transform.localPosition = new Vector2(Neck.transform.localPosition.x * (-1), Neck.transform.localPosition.y); 
    }
}

public class WayPoint
{
    public Vector3 position;
    public float restTime;

    public WayPoint(Vector3 pos,float time)
    {
        position = pos;
        restTime = time;
    }
}

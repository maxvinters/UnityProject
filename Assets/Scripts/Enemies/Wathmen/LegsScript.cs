using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsScript : MonoBehaviour {

    Rigidbody2D rb;

    [SerializeField]
    Transform[] waysPos;

    [SerializeField]
    float[] waysTime;

    WayPoint[] wayPoints = new WayPoint[3];

    [SerializeField]
    float speed;

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
        GetComponent<SpriteRenderer>().flipX = wayPoints[currentWay].position.x - transform.position.x > 0;
        yield return new WaitUntil(() => Vector2.Distance(transform.position, wayPoints[currentWay].position) < 1f);
        rb.velocity = rb.velocity * 0.7f;
        yield return new WaitUntil(() => Vector2.Distance(transform.position, wayPoints[currentWay].position) < 0.85f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(wayPoints[currentWay].restTime);
        currentWay++;
        if (currentWay == wayPoints.Length)
            currentWay = 0;
        StartCoroutine(GoToPoint());
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

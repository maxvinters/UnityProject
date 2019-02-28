using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {


    bool rightStop, leftStop;
    bool downStop;
    Vector3 offset;
    [SerializeField]
    GameObject player;
    Collider2D collider;
	void Start ()
    {
        offset = transform.position - player.transform.position;
        rightStop = false;
        leftStop = false;
	}
	
	void Update ()
    {
		if(!(rightStop ||leftStop) || (rightStop && player.transform.position.x<transform.position.x) || (leftStop && player.transform.position.x > transform.position.x) )
        {
            rightStop = false;
            leftStop = false;
            transform.position =  new Vector3(player.transform.position.x + offset.x,transform.position.y, transform.position.z);
        }
        if (!downStop)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y + offset.y, transform.position.z);
        }
        else if (downStop && collider.transform.position.y > player.transform.position.y)
        {
            //offset = transform.position - player.transform.position;
            downStop = false;
        }
        else if (downStop && player.transform.position.y > transform.position.y)
        {
            offset.y = transform.position.y - player.transform.position.y;
            downStop = false;
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("CamWall"))
        {
            if (col.transform.position.x > transform.position.x)
                rightStop = true;
            else if (col.transform.position.x < transform.position.x)
                leftStop = true;
        }
        if (col.CompareTag("CamFloor"))
        {
            downStop = true;
            collider = col;
        }
   }
}

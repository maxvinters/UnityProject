using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {


    bool rightStop, leftStop;
    bool downStop;
    public DragonBones.UnityArmatureComponent animobj;
    Vector3 offset;
    Vector3 stopPos;
    public float speed;
    public float distance;
    int offs=1;
    //public int off
    [SerializeField]
    GameObject player;
    Collider2D collider;
    public Camera camera;
    public BoxCollider2D boxCollider;
    void Start ()
    {
        camera = this.GetComponent<Camera>();
        boxCollider = this.transform.GetComponent<BoxCollider2D>();
        //boxCollider.size.Set(camera.aspect * 2f * camera.orthographicSize, 2f * camera.orthographicSize);
        boxCollider.size = new Vector2(camera.aspect * 2f * camera.orthographicSize, 2f * camera.orthographicSize);
        offset = transform.position - player.transform.position;
        rightStop = false;
        leftStop = false;
	}
	
	void Update ()
    {
        

        

        if (!(rightStop ||leftStop) || (rightStop && transform.position.x- player.transform.position.x>=distance) || (leftStop && player.transform.position.x - transform.position.x>=distance) )
        {
            //if (rightStop)
            //    offs = -1;
            //if (leftStop)
            //    offs = 1;
            rightStop = false;
            leftStop = false;
            if (animobj.armature.flipX)
                offs = -1;
            else
                offs = 1;
            transform.position=Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + offset.x*offs, transform.position.y, transform.position.z), speed);
            //transform.position =  new Vector3(player.transform.position.x + offset.x,transform.position.y, transform.position.z);
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
            if (col.transform.position.x >= transform.position.x)
            {
                rightStop = true;
                Debug.Log("Right Stop");

                //stopPos = transform.position;
            }
            else if (col.transform.position.x <= transform.position.x)
            {
                leftStop = true;
                Debug.Log("Left Stop");
                //stopPos = transform.position;
                //offset = transform.position + player.transform.position;
            }
        }
        if (col.CompareTag("CamFloor"))
        {
            downStop = true;
            collider = col;
        }
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour {


    int lastTouchCount;
    bool canShoot;

    [SerializeField]
    float reload;


    [SerializeField]
    Transform ShotPos;

    [SerializeField]
    int damage;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    float bulletSpeed;


    void Start ()
    {
        canShoot = true;
	}
	

	void Update ()
    {
        if (Input.touchCount > lastTouchCount && lastTouchCount == 0 && canShoot && (Input.GetTouch(0).position.x > 540 ||   Input.GetTouch(0).position.y > 540))
            StartCoroutine(Shot());
    }


    void LateUpdate()
    {
        lastTouchCount = Input.touchCount;

    }
    IEnumerator Shot()
    {
        canShoot = false;
        GameObject bull = Instantiate(bullet, ShotPos.position,Quaternion.identity);
        bull.GetComponent<Rigidbody2D>().velocity = (Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) + new Vector3(0, 0, 10) - transform.position).normalized * bulletSpeed;
        bull.GetComponent<BulletScript>().friendly = true;
        bull.GetComponent<BulletScript>().damage = damage; 
        yield return new WaitForSeconds(reload);
        canShoot = true;
    }
}

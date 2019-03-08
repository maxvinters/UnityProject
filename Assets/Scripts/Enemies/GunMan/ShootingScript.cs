using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour {


    BattleFieldScript bfs;
    SpriteRenderer sr;

    [SerializeField]
    Sprite Hide;

    [SerializeField]
    Sprite Show;

    int currentAmmo;

    [SerializeField]
    int maxAmmo;

    bool reloading;

    [SerializeField]
    float frequency;

    [SerializeField]
    float fullReloadTime;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    float bulletSpeed;

    [SerializeField]
    int damage;

    [SerializeField]
    Transform shotPoint;

    [SerializeField]
    GameObject player;

    bool canShoot;

    void Start()
    {
        bfs = GetComponent<BattleFieldScript>();
        sr = GetComponent<SpriteRenderer>();
        canShoot = true;
        sr.sprite = Show;
        currentAmmo = maxAmmo;
    }


    void Update()
    {

        if (bfs.inCover && canShoot)
        {
            if (currentAmmo == 0)
                StartCoroutine(LongReload());
            else
            {
                Shoot();
                StartCoroutine(ShortReload());
            }
            
        }
    }

    IEnumerator ShortReload()
    {
        canShoot = false;
        currentAmmo--;
        yield return new WaitForSeconds(frequency);
        canShoot = true;
    }
    IEnumerator LongReload()
    {
        canShoot = false;
        sr.sprite = Hide;
        yield return new WaitForSeconds(fullReloadTime);
        sr.sprite = Show;
        currentAmmo = maxAmmo;
        canShoot = true;
    }
    void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, shotPoint.position, shotPoint.rotation) as GameObject;
        Vector2 direction = player.transform.position-newBullet.transform.position;
        newBullet.GetComponent<Rigidbody2D>().velocity = (direction + new Vector2(Random.Range(-1.3f,1.3f), Random.Range(-1.3f, 1.3f))).normalized * bulletSpeed;
        newBullet.GetComponent<BulletScript>().friendly = false;
        newBullet.GetComponent<BulletScript>().damage = damage;
        Destroy(newBullet, 2f);
    }
}

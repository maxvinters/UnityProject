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
    Transform shotPoint;

    bool canShoot;

    void Start()
    {
        print("Sish");
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
        GameObject newBullet = Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        int direction = shotPoint.position.x > bfs.player.transform.position.x ? -1 : 1;
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction, Random.Range(-0.05f,0.05f)).normalized * bulletSpeed;
        Destroy(newBullet, 2f);
    }
}

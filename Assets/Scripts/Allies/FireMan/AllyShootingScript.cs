using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyShootingScript : MonoBehaviour {

    AllyBattleFieldScript abfs;

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
    GameObject enemy;

    bool canShoot;

    void Start () 
    {
        abfs = GetComponent<AllyBattleFieldScript>();
        canShoot = true;
        currentAmmo = maxAmmo;
    }


    void Update()
    {
        enemy = abfs.enemy;
        if (abfs.inCover && canShoot)
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
        yield return new WaitForSeconds(fullReloadTime);
        currentAmmo = maxAmmo;
        canShoot = true;
    }
    void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, shotPoint.position, shotPoint.rotation) as GameObject;
        Vector2 direction = enemy.transform.position - newBullet.transform.position;
        newBullet.GetComponent<Rigidbody2D>().velocity = (direction + new Vector2(Random.Range(-1.3f, 1.3f), Random.Range(-1.3f, 1.3f))).normalized * bulletSpeed;
        newBullet.GetComponent<BulletScript>().friendly = true;
        newBullet.GetComponent<BulletScript>().damage = damage;
        Destroy(newBullet, 2f);
    }
}

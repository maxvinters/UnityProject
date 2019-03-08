using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public int damage;
    public bool friendly; //if true - shoot by friends, false - by enemies

    void OnTriggerEnter2D(Collider2D col)
    {
        if(friendly && col.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
        else if(!friendly && col.CompareTag("Player"))
        {
            col.GetComponent<PlayerHealth>().GetDamage(damage);
            gameObject.SetActive(false);
        }

    }



}

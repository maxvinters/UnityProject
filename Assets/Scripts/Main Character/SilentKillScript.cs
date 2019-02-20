using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SilentKillScript : MonoBehaviour {

    GameObject target;
    [SerializeField]
    Button kill;

	void Start ()
    {
    
	}
	

	void Update ()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Neck"))
        {
            target = col.transform.parent.gameObject;
            kill.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Neck"))
        {
            target = null;
            kill.gameObject.SetActive(false);
        }
    }



    public void SilentKill()
    {
        Destroy(target);
    }


}

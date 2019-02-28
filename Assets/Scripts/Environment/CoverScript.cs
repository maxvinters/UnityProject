using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverScript : MonoBehaviour {

    [SerializeField]
    BoxCollider2D hitbox;

    public bool isBusy;

	void Start () 
    {
        isBusy = false;
	}
	
	void Update ()
    {
		
	}

    public void Enablehitbox()
    {
        hitbox.enabled = true;
    }
    public void Disablehitbox()
    {
        isBusy = false;
        hitbox.enabled = false;
    }
}

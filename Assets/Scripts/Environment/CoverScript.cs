using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverScript : MonoBehaviour {

    [SerializeField]
    BoxCollider2D hitbox;

	void Start () 
    {
		
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
        hitbox.enabled = false;
    }
}

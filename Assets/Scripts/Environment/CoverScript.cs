using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverScript : MonoBehaviour {


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
        isBusy = true;
    }
    public void Disablehitbox()
    {
        isBusy = false;
    }
}

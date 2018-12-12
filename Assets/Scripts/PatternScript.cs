using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternScript : MonoBehaviour {

    [SerializeField]
    GameObject Pointer;

    public GameObject[] Dots;


    void OnEnable()
    {
        transform.position = Pointer.transform.position;
    }
	
    void OnDisable()
    {
        foreach (GameObject Dot in Dots)
            Dot.SetActive(false);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternScript : MonoBehaviour {

    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject Pointer;
    Vector3 offset = new Vector3(-1, -2, 0);


    void Awake()
    {
        Pointer.SetActive(true);
    }
	

	void FixedUpdate ()
    {
        transform.position = player.transform.position + offset;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DarknessScript : MonoBehaviour {

    [SerializeField]
    Image floor0;

    [SerializeField]
    Image floor1;

    [SerializeField]
    GameObject player;

    [SerializeField]
    float dist;

	void Start () 
    {

	}
	


	void Update () 
    {
        float deltaY = Mathf.Clamp(transform.position.y - player.transform.position.y,-dist, dist) / dist;
        floor0.color = new Color(0, 0, 0, Mathf.Clamp01(-deltaY));
        floor1.color = new Color(0, 0, 0, Mathf.Clamp01(deltaY));
    }



}

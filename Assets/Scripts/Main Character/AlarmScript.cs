using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmScript : MonoBehaviour {

    [SerializeField]
    GameObject[] allies;

    public delegate void MethodContainer();

    public event MethodContainer Alarm;

    void Start () 
    {
        foreach (GameObject ally in allies)
            Alarm += ally.GetComponent<ChangeStateScript>().ToFireMode;
	}
	
    public void MakeAlarm()
    {
        Alarm();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateScript : MonoBehaviour {
    [SerializeField]
    bool inFireMode;

    float lastEnemyTime;
    AllyBattleFieldScript abfs;
	void Start ()
    {
        abfs = GetComponent<AllyBattleFieldScript>();
        inFireMode = false;
	}
	
	
	void Update ()
    {
		if(inFireMode)
        {
            if (abfs.enemy)
                lastEnemyTime = Time.time;
            if(Time.time - lastEnemyTime > 5f)
            {
                lastEnemyTime = 0;
                ToFollowMode();
            }
        }
    }

    public void ToFireMode()
    {
        inFireMode = true;
        GetComponent<FollowerScript>().enabled = false;
        GetComponent<AllyShootingScript>().enabled = true;
        GetComponent<AllyBattleFieldScript>().enabled = true;
    }

    void ToFollowMode()
    {
        inFireMode = false;
        GetComponent<FollowerScript>().enabled = true;
        GetComponent<AllyShootingScript>().enabled = false;
        GetComponent<AllyBattleFieldScript>().enabled = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCubeGen : MonoBehaviour {

    int count = 0;

    public Transform attackCube;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        count++;

        if(count%75 == 0)
        {
            Instantiate(attackCube, this.transform.position, this.transform.rotation, null);
        }

	}
}

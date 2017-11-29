using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    Transform Tr;
    CameraTarget camTr;
	// Use this for initialization
	void Start ()
    {
        camTr = GetComponent<CameraTarget>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (GetComponent<CameraTarget>() != null)
            this.transform.position = new Vector3(camTr.trg.position.x,camTr.trg.position.y,transform.position.z);


	}
}

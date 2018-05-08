using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform trg;
    Transform gObject;
    Transform _startGameObj;
    TurnsSystem turns;
	// Use this for initialization
	void Start ()
    {
		gObject = GameObject.Find("MotherOfEverything").transform.Find("Manager");
        _startGameObj = GameObject.Find("MotherOfEverything").transform.Find("Start");
        turns = gObject.GetComponent<TurnsSystem>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        try
        {
           Transform tr = GameObject.Find("AttackCube(Clone)").transform;
            if (tr != null)
            {
                trg = tr;
                return;
            }
        }catch(Exception e)
        {

        }

        if (turns.gameObject.activeInHierarchy)
        {
            trg = turns.getActivePlayer().transform;
        }
        else
            trg = _startGameObj;
    }
}

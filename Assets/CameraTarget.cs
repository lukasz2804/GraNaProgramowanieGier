using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform trg;
    GameObject gObject;
    TurnsSystem turns;
	// Use this for initialization
	void Start ()
    {
        gObject = GameObject.FindGameObjectWithTag("Player");
        turns = gObject.GetComponent<TurnsSystem>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        trg = turns.getActivePlayer().transform;
	}
}

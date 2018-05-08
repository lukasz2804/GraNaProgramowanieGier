using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionSwitcher : MonoBehaviour {


    public BasicMove BasicMove;
	public AttackCubeShoter AttackCubeShoter;



	public void Switch()
	{
        AttackCubeShoter.enabled = !AttackCubeShoter.enabled;
        BasicMove.Switch();
        
	}

    // Use this for initialization
    void Start () {
        //BasicMoverByRotate.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

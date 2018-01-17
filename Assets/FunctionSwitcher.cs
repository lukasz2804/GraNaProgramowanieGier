using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionSwitcher : MonoBehaviour {


	public BasicMoverByRotate BasicMoverByRotate;
	public AttackCubeShoter AttackCubeShoter;
    public BasicMoveByDoubleJump BasicMoveByDoubleJump;
    public BasicMoveByJumping BasicMoveByJumping;
    public BasicMoveByRotateAndJump BasicMoveByRotateAndJump;
    public BasicMoveByWheel BasicMoveByWheel;


	public void Switch()
	{
        GameObject.FindGameObjectWithTag("Player").GetComponent<BasicMoverByRotate>().enabled = !GameObject.FindGameObjectWithTag("Player").GetComponent<BasicMoverByRotate>().enabled;

       // BasicMoverByRotate.enabled = !BasicMoverByRotate.enabled;
		//AttackCubeShoter.enabled = !AttackCubeShoter.enabled;
	}
    void SwitchDoubleJump()
    {
        BasicMoveByDoubleJump.enabled = !BasicMoveByDoubleJump.enabled;
        AttackCubeShoter.enabled = !AttackCubeShoter.enabled;
    }
    void SwitchJump()
    {
        BasicMoveByJumping.enabled = !BasicMoveByJumping.enabled;
        AttackCubeShoter.enabled = !AttackCubeShoter.enabled;
    }
    void SwitchRotateNJump()
    {
        BasicMoveByRotateAndJump.enabled = !BasicMoveByRotateAndJump.enabled;
        AttackCubeShoter.enabled = !AttackCubeShoter.enabled;
    }
    void SwitchWheel()
    {
        BasicMoveByWheel.enabled = !BasicMoveByWheel.enabled;
        AttackCubeShoter.enabled = !AttackCubeShoter.enabled;
    }
    // Use this for initialization
    void Start () {
        //BasicMoverByRotate.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

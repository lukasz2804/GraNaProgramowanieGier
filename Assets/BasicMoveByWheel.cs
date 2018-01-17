using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveByWheel : MonoBehaviour
{



    public WheelJoint2D backwheel;

    JointMotor2D motorBack;

    public float speedB;

    public float speedF;

    public float torqueB;

    public bool TractionBack = true;

    public float carRotationSpeed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxisRaw("Vertical") > 0)
        {

            if (TractionBack)
            {
                motorBack.motorSpeed = speedF * -1;
                motorBack.maxMotorTorque = torqueB;
                backwheel.motor = motorBack;

            }

        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {


            if (TractionBack)
            {
                motorBack.motorSpeed = speedB * -1;
                motorBack.maxMotorTorque = torqueB;
                backwheel.motor = motorBack;

            }
        }
        else
        {
            backwheel.useMotor = false;
        }




        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            GetComponent<Rigidbody2D>().AddTorque(carRotationSpeed * Input.GetAxisRaw("Horizontal") * -1);
        }

    }
}

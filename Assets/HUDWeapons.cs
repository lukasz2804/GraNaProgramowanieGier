using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDWeapons : MonoBehaviour {

    public Sprite[] weapons;

    public Image weaponUI;

    public Text degreeUI;

    private int curr=0, degreeAngle = 90;

    private int degreeStep = 10;

    private int weaponsCunt = 3;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (degreeAngle >= 360)
                degreeAngle = 0;
            else
                degreeAngle = degreeAngle + degreeStep;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (degreeAngle <= 0)
                degreeAngle = 360;
            else
                degreeAngle = degreeAngle - degreeStep;
        }

        degreeUI.text = "" + degreeAngle;

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(curr >= weaponsCunt-1)
                curr = 0; 
            else
                curr++;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (curr <= 0)
                curr = weaponsCunt-1;
            else
                curr--;
        }

        weaponUI.sprite = weapons[curr];
        
    }
}

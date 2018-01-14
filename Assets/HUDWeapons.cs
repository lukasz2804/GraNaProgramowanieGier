using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDWeapons : MonoBehaviour {

    public Sprite[] weapons;

    public Image weaponUI;

    private int curr=0;

    private int weaponsCunt = 3;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
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

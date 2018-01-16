using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnsSystem : MonoBehaviour {
    // Use this for initialization
    bool waitActive;
	bool timerIsOn;
    BasicMoverByRotate scriptToAccess;
    Transform objectToAccess;
    Players players;
	Transform activePlayer, manager;
    int time;
    Coroutine waiter;
	public Text text;

	public Camera camera1;
	public Camera camera2;
	public Camera main;

    void Start () {
		main.enabled = false;
		camera2.enabled = false;
		camera1.enabled = true;

		objectToAccess = GameObject.FindGameObjectWithTag("Player").transform;
        activePlayer = objectToAccess;
        manager = GameObject.Find("MotherOfEverything").transform.Find("Manager");
        players = manager.GetComponent<Players>();
        time = 20;
        text.text = time.ToString();
        waitActive = false;
    }

    void Update () {
        timerIsOn = true;
        if (!waitActive)
        {
            if (timerIsOn && main.enabled == true) 
			{
				waiter = StartCoroutine (Wait ());
                activePlayer.GetComponent<FunctionSwitcher>().Switch();
                activePlayer =players.getNext().transform;
                activePlayer.GetComponent<FunctionSwitcher>().Switch();
            }
        }
    }

    IEnumerator Wait()
    {
        waitActive = true;
        while (time > 0) {
            yield return new WaitForSeconds(1.0f);
            time--;
            text.text = time.ToString();
        }
        time = 20;
        waitActive = false;
    }

    internal GameObject getActivePlayer()
    {
		return activePlayer.gameObject;
    }
    internal void setChangePlayer(bool ch)
    {
        StopCoroutine(waiter);
        waiter = StartCoroutine(Wait());
        activePlayer.GetComponent<FunctionSwitcher>().Switch();
        activePlayer = players.getNext().transform;
        activePlayer.GetComponent<FunctionSwitcher>().Switch();
        time = 20;

    }
}

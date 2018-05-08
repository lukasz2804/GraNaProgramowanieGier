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
    public int timeForTurn;
    Coroutine waiter;
	public Text text;

	public Camera camera1;
	public Camera camera2;
	public Camera main;

    void Start () {
		main.enabled = true;
		camera2.enabled = false;
		camera1.enabled = false;
        objectToAccess = GameObject.Find("MotherOfEverything").transform.Find("Player1List").GetChild(0);
        activePlayer = objectToAccess;
        activePlayer.GetComponent<FunctionSwitcher>().Switch();
        manager = GameObject.Find("MotherOfEverything").transform.Find("Manager");
        players = manager.GetComponent<Players>();
        time = timeForTurn;
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
        time = timeForTurn;
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
        if (activePlayer != null) { 
        activePlayer.GetComponent<FunctionSwitcher>().Switch();
        }
        activePlayer = players.getNext().transform;
        activePlayer.GetComponent<FunctionSwitcher>().Switch();
        time = timeForTurn;

    }


    internal void setPlayerNull()
    {
        StopCoroutine(waiter);
        waiter = StartCoroutine(Wait());
        activePlayer.GetComponent<FunctionSwitcher>().Switch();
        activePlayer = null;
       
    }

}

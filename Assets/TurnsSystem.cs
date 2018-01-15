using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnsSystem : MonoBehaviour {
    // Use this for initialization
    bool waitActive;
    BasicMoverByRotate scriptToAccess;
    GameObject objectToAccess;
    Players players;
    GameObject activePlayer;
    int time;
    Coroutine waiter;

    void Start () {
        objectToAccess = GameObject.FindGameObjectWithTag("Player");
        activePlayer = objectToAccess;
        scriptToAccess = objectToAccess.GetComponent<BasicMoverByRotate>();
        players = objectToAccess.GetComponent<Players>();
        time = 20;
        GameObject.FindGameObjectWithTag("text").GetComponent<Text>().text = time.ToString();
        waitActive = false;
    }

    void Update () {
        if (!waitActive)
        {
            waiter = StartCoroutine(Wait());
            Destroy(activePlayer.GetComponent<AttackCubeShoter>());
            activePlayer=players.getNext();
            activePlayer.AddComponent<AttackCubeShoter>();
        }
    }

    IEnumerator Wait()
    {
        waitActive = true;
        while (time > 0) {
            yield return new WaitForSeconds(1.0f);
            time--;
            GameObject.FindGameObjectWithTag("text").GetComponent<Text>().text = time.ToString();
        }
        time = 20;
        waitActive = false;
    }

    internal GameObject getActivePlayer()
    {
        return activePlayer;
    }
    internal void setChangePlayer(bool ch)
    {
        StopCoroutine(waiter);
        waiter = StartCoroutine(Wait());
        Destroy(activePlayer.GetComponent<AttackCubeShoter>());
        activePlayer = players.getNext();
        activePlayer.AddComponent<AttackCubeShoter>();
        time = 20;

    }
}

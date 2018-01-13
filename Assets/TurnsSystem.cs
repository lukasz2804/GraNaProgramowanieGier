using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnsSystem : MonoBehaviour {
    // Use this for initialization
    bool waitActive;
    BasicMoverByRotate scriptToAccess;
    GameObject objectToAccess;
    Players players;
    GameObject activePlayer;

    void Start () {
        objectToAccess = GameObject.FindGameObjectWithTag("Player");
        activePlayer = objectToAccess;
        scriptToAccess = objectToAccess.GetComponent<BasicMoverByRotate>();
        players = objectToAccess.GetComponent<Players>();

        waitActive = false;
    }

    void Update () {
        if (!waitActive)
        {
            StartCoroutine(Wait());
            Destroy(activePlayer.GetComponent<AttackCubeShoter>());
            activePlayer=players.getNext();
            activePlayer.AddComponent<AttackCubeShoter>();
        }
    }

    IEnumerator Wait()
    {
        waitActive = true;
        yield return new WaitForSeconds(20.0f);
        waitActive = false;
    }

    internal GameObject getActivePlayer()
    {
        return activePlayer;
    }
}

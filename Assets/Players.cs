using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Players : MonoBehaviour {
	public Transform player1ListTr;
	public Transform player2ListTr;
    public int player1Counter;
    public int player2Counter;
    public List<GameObject> player1List; 
    public List<GameObject> player2List;

    public Transform Head1Prefab;
    public Transform Head2Prefab;
    public Transform Head3Prefab;
    public Transform Head4Prefab;
    public Transform Head5Prefab;

    GameObject P1ListRoot;
    GameObject P2ListRoot;

    int indexP1;
    int indexP2;
    bool playerOneMove = true;

    // Use this for initialization
    void Start () {
        indexP1 = 0;
        indexP2 = 0;
        player1List = new List<GameObject>();
        player2List = new List<GameObject>();
        GameObject playerRobot = GameObject.FindGameObjectWithTag("Player");
        GameObject objToSpawn;

        P1ListRoot = GameObject.Find("Player1List");
        P2ListRoot = GameObject.Find("Player2List");


        for(int i=0;i<P1ListRoot.transform.childCount;i++)
        {
            player1List.Add(P1ListRoot.transform.GetChild(i).gameObject);
            P1ListRoot.transform.GetChild(i).gameObject.AddComponent<DamageManagerSc>();
        }

        for (int i = 0; i < P2ListRoot.transform.childCount; i++)
        {
            player2List.Add(P2ListRoot.transform.GetChild(i).gameObject);
            P2ListRoot.transform.GetChild(i).gameObject.AddComponent<DamageManagerSc>();
        }
    }
    internal GameObject getNext()
    {
        if (playerOneMove)
        {
            playerOneMove = false;
            indexP1++;
            indexP1 = indexP1 % player1List.Count;
            return player1List[indexP1];
        }
        else
        {
            playerOneMove = true;
            indexP2++;
            indexP2 = indexP2 % player2List.Count;
            return player2List[indexP2];

        }
    }

    internal void dealDMG(int dmg)
    {
        for (int i = 0; i < P1ListRoot.transform.childCount; i++)
        {
            if (player1List[i].active)
            {
                player1List[i].GetComponent<DamageManagerSc>().basicLife -= dmg;
                if(player1List[i].GetComponent<DamageManagerSc>().basicLife <= 0)
                {
                    player1List[i].SetActive(false);
                }
            }
        }

        for (int i = 0; i < P2ListRoot.transform.childCount; i++)
        {
            if (player2List[i].active)
            {
                player2List[i].GetComponent<DamageManagerSc>().basicLife -= dmg;
                if (player2List[i].GetComponent<DamageManagerSc>().basicLife <= 0)
                {
                    player2List[i].SetActive(false);
                }
            }

        }

    }


}

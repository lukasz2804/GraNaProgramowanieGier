using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Players : MonoBehaviour {
    public int player1Counter;
    public int player2Counter;
    public List<GameObject> player1List; 
    public List<GameObject> player2List;
    int indexP1;
    int indexP2;
    bool playerOneMove = true;

    // Use this for initialization
    void Start () {
        indexP1 = 0;
        indexP2 = 0;
        player1List = new List<GameObject>();
        player2List = new List<GameObject>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject objToSpawn;
        
        if (player1Counter > 0) {
            player1List.Add(player);
        for (int i = 0; i < player1Counter-1; i++)
        {
            objToSpawn = new GameObject("PlayerOne"+i);
            //Add Components
            objToSpawn.AddComponent<Rigidbody2D>();
            objToSpawn.AddComponent<BoxCollider2D>();
            objToSpawn.AddComponent<SpriteRenderer>();
            objToSpawn.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BasicHead");
            objToSpawn.transform.position = player.transform.position;
            player1List.Add(objToSpawn);
            
        }
        }



        for (int i = 0; i < player2Counter; i++)
        {
            objToSpawn = new GameObject("PlayerTwo" + i);
            //Add Components
            objToSpawn.AddComponent<Rigidbody2D>();
            objToSpawn.AddComponent<BoxCollider2D>();
            objToSpawn.AddComponent<SpriteRenderer>();
            objToSpawn.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BasicHeadOther");
            objToSpawn.transform.position = player.transform.position;
            player2List.Add(objToSpawn);
        }



    }

    internal GameObject getNext()
    {
        if (playerOneMove)
        {
            playerOneMove = false;
            indexP1++;
            indexP1 = indexP1 % player1Counter;
            return player1List[indexP1];
        }
        else
        {
            playerOneMove = true;
            indexP2++;
            indexP2 = indexP2 % player2Counter;
            return player2List[indexP2];

        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

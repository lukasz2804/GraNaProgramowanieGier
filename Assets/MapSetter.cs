using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetter : MonoBehaviour {
    Points ptSc;
    Triangulator trSc;
    // Use this for initialization
    void Start()
    {
        
        ptSc = GetComponent<Points>();
        Debug.Log("Waiting for princess to be rescued..."+ptSc.name);
        trSc = GetComponent<Triangulator>();
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        while (!ptSc.isReady) { yield return null; }

        trSc.ApplyNewPointsFromLst(ptSc.convertToInxCyclLst(0));
    }

    void Update()
    {
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvexChecker : MonoBehaviour {


    public Transform []points;

    private Vector2 Mirred;
	// Use this for initialization
	void Start () {
        Mirred = Vector2.down;
	}


    private bool IsConvex()
    {
        
        Vector2 prev = points[0].position;
        Vector2 trg = points[1].position;
        Vector2 nxt = points[2].position;

        //trg = trg - prev;

        Vector2 d1 = trg - prev;
        d1.Normalize();

        Vector2 d2 = nxt - trg;
        d2.Normalize();

        Vector2 n2 = new Vector2(-d2.y, d2.x);

        // Gizmos.DrawLine(trg, nxt);
        // Gizmos.DrawLine(trg, prev);
        // Gizmos.DrawLine(trg, n2 + trg);

        Mirred = n2 + trg;

        if (Vector2.Dot(d1, n2) <= 0f)
        {
            Debug.Log("Is Convex");
        }
        else
        {
            Debug.Log("isnt");
        }

        return (Vector2.Dot(d1, n2) <= 0f);
    }
    // Update is called once per frame
    void Update () {


        IsConvex();
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(points[1].position, points[2].position);
         Gizmos.DrawLine(points[1].position, points[0].position);
         Gizmos.DrawLine(points[1].position, Mirred );
    }

}

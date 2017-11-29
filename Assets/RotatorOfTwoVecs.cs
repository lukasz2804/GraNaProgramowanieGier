using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorOfTwoVecs : MonoBehaviour {


    public Transform one;
    public Transform two;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        two.position = RotateVecPFromI(one.position,1.0f, two.position); 

	}

    void OnDrawGizmos()
    {
     
        Gizmos.DrawLine(one.position, two.position);
         
        
    }


    Vector2 RotateVecPFromI(Vector2 i, float angle, Vector2 p)
    {
        float s = Mathf.Sin(Mathf.Deg2Rad * angle);
        float c = Mathf.Cos(Mathf.Deg2Rad * angle);

        // translate point back to origin:
        p.x -= i.x;
        p.y -= i.y;

        // rotate point
        float xnew = p.x * c - p.y * s;
        float ynew = p.x * s + p.y * c;

        // translate point back:
        p.x = xnew + i.x;
        p.y = ynew + i.y;
        return p;
    }

}

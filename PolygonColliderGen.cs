using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonColliderGen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PolygonCollider2D polyCol = GetComponent<PolygonCollider2D>();
        Triangulator sc = GetComponent<Triangulator>();
        if (sc != null && polyCol != null)
        {
            polyCol.points = sc.GetVertices2d();
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

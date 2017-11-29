using System.Collections;
using System.Collections.Generic;
using UnityEngine;








public class TriangleChecker : MonoBehaviour {

    

    public Transform Atr, Btr, Ctr;
    public Transform TrgTr;
    public Vertex A;
    public Vertex B;
    public Vertex C;

    void Start()
    {
        A = new Vertex(Atr.position, 1);
        B = new Vertex(Btr.position, 2);
        C = new Vertex(Ctr.position, 3);
    }

    public bool ContainsPoint(Vertex point)
    {
        //return true if the point to test is one of the vertices
        if (point.Equals(A) || point.Equals(B) || point.Equals(C))
            return true;

        bool oddNodes = false;

        if (CheckPointToSegment(C, A, point))
            oddNodes = !oddNodes;
        if (CheckPointToSegment(A, B, point))
            oddNodes = !oddNodes;
        if (CheckPointToSegment(B, C, point))
            oddNodes = !oddNodes;

        return oddNodes;
    }


    static bool CheckPointToSegment(Vertex sA, Vertex sB, Vertex point)
    {
        if ((sA.Position.y < point.Position.y && sB.Position.y >= point.Position.y) ||
            (sB.Position.y < point.Position.y && sA.Position.y >= point.Position.y))
        {
            float x =
                sA.Position.x +
                (point.Position.y - sA.Position.y) /
                (sB.Position.y - sA.Position.y) *
                (sB.Position.x - sA.Position.x);

            if (x < point.Position.x)
                return true;
        }

        return false;
    }


    // Use this for initialization
    
	
	// Update is called once per frame
	void Update () {
		
        if(ContainsPoint(new Vertex(TrgTr.position,12)))
        {
            Debug.Log("triangle contains the point");
            Debug.Log(TrgTr);
        }

	}


    void OnDrawGizmos()
    {
        Gizmos.DrawLine(Atr.position, Btr.position);
        Gizmos.DrawLine(Btr.position, Ctr.position);
        Gizmos.DrawLine(Ctr.position, Atr.position);
    }

}

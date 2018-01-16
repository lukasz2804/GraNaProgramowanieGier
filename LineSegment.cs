using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IntersectionWithVert
{
    public Vertex vert;
    public Vector2 intersection;
    public IntersectionWithVert(Vertex v,Vector2 inters)
    {
        vert = v;
        intersection = inters;
    }
}



public class LineSegmentInnerWithIntersect : LineSegment
    {
   public bool isGoingIn;
    public Vector2 intersection;
    public LinkedListNode<Vertex> aLink;
    public LinkedListNode<Vertex> bLink;
    public LineSegmentInnerWithIntersect(Vertex a,Vertex b,LinkedListNode<Vertex> aL,LinkedListNode<Vertex> bL,Vector2 intersect,bool isInner)
        : base(a,b)
    {
        aLink = aL;
        bLink = bL;
        isGoingIn = isInner;
        intersection = intersect;
    }
}


public class LineSegmentWithAngle : LineSegment
{
    public float angle;
    public LineSegmentWithAngle(Vertex a,Vertex b)
        :base(a,b)
    {
        angle = Angle(a.Position - b.Position);
    }
    private float Angle(Vector2 v)
    {
        float dx = v.x;
        float dy = v.y;
        float r = Mathf.Sqrt(dx * dx + dy * dy);
        float angle = Mathf.Atan2(dy, dx);
        //angle -= base_angle;
        if (angle < 0) angle += Mathf.PI * 2;
        return angle;
    }
}

public class LineSegment {


    public Vertex A;
    public Vertex B;

    public LineSegment(Vertex a, Vertex b)
    {
        A = a;
        B = b;
    }

    public LineSegment()
    {
        A = new Vertex();
        B = new Vertex();
    }

    public float? IntersectsWithRay(Vector2 origin, Vector2 direction)
    {
        float largestDistance = Mathf.Max(A.Position.x - origin.x, B.Position.x - origin.x) * 2f;
        LineSegment raySegment = new LineSegment(new Vertex(origin, 0), new Vertex(origin + (direction * largestDistance), 0));

        Vector2? intersection = FindIntersection(this, raySegment);
        float? value = null;

        if (intersection != null)
            value = Vector2.Distance(origin, intersection.Value);

        return value;
    }

    public static Vector2? FindIntersection(LineSegment a, LineSegment b)
    {

       // Vector2 s = new Vector2();
      //  s.

        float x1 = a.A.Position.x;
        float y1 = a.A.Position.y;
        float x2 = a.B.Position.x;
        float y2 = a.B.Position.y;
        float x3 = b.A.Position.x;
        float y3 = b.A.Position.y;
        float x4 = b.B.Position.x;
        float y4 = b.B.Position.y;

        float denom = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);

        float uaNum = (x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3);
        float ubNum = (x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3);

        float ua = uaNum / denom;
        float ub = ubNum / denom;

        if (Mathf.Clamp(ua, 0f, 1f) != ua || Mathf.Clamp(ub, 0f, 1f) != ub)
            return null;

        return a.A.Position + (a.B.Position - a.A.Position) * ua;

    }
    void Start () {
		
	}
	void Update () {
		
	}
}
    // Use this for initialization
	
	// Update is called once per frame


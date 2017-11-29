using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestPointFromPolygon : MonoBehaviour {


    public Transform polygon;

    public float dmgRadius;

    private Vector2 target;
    private List<Vector2> circleIntesections;

    private Transform[] pointsTab;
	// Use this for initialization
	void Start ()
    {
        target = this.transform.position;
        circleIntesections = new List<Vector2>();

        if(polygon == null)
        {
            polygon = GameObject.Find("PolygonTerrain").transform;
        }

        if(polygon.GetComponent<Triangulator>()!=null)
        {
            Triangulator trScript = polygon.GetComponent<Triangulator>();
            pointsTab = trScript.GetPoints();

        }
	
        
	}




    private double FindDistanceToSegment(
    Vector2 pt, Vector2 p1, Vector2 p2, out Vector2 closest)
    {
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        if ((dx == 0) && (dy == 0))
        {
            // It's a point not a line segment.
            closest = p1;
            dx = pt.x - p1.x;
            dy = pt.y - p1.y;
            return Mathf.Sqrt(dx * dx + dy * dy);
        }

        // Calculate the t that minimizes the distance.
        float t = ((pt.x - p1.x) * dx + (pt.x- p1.y) * dy) /
            (dx * dx + dy * dy);

        // See if this represents one of the segment's
        // end points or a point in the middle.
        if (t < 0)
        {
            closest = new Vector2(p1.x, p1.y);
            dx = pt.x - p1.x;
            dy = pt.y - p1.y;
        }
        else if (t > 1)
        {
            closest = new Vector2(p2.x, p2.y);
            dx = pt.x - p2.x;
            dy = pt.y - p2.y;
        }
        else
        {
            closest = new Vector2(p1.x + t * dx, p1.y + t * dy);
            dx = pt.x - closest.x;
            dy = pt.y - closest.y;
        }

        return Mathf.Sqrt(dx * dx + dy * dy);
    }

    Transform GetClosest(Transform []points)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = this.transform.position;
        foreach (Transform potentialTarget in points)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }


    int GetClosestPointIdx(IndexableCyclicalLinkedList<Vertex> lst, Vector2 fromP)
    {

        float closestDistanceSqr = Mathf.Infinity;
        Vector2 currentPosition = fromP;
       // int i = 0;
        int tarIdx = 0;
        for (var i = 0; i < lst.Count; i++)
        {
            Vector3 directionToTarget = lst[i].Value.Position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                //  bestTarget = potentialTarget;
                tarIdx = lst.IndexOf(lst[i].Value);
            }
       //     i++;
        }
        return tarIdx;
    }




    LinkedListNode<Vertex> GetClosestPointVert(IndexableCyclicalLinkedList<Vertex> lst, Vector2 fromP)
    {

        float closestDistanceSqr = Mathf.Infinity;
        
        Vector2 currentPosition = fromP;
        LinkedListNode<Vertex> trg = null;
        // int i = 0;
        int tarIdx = 0;
        for (var i = 0; i < lst.Count; i++)
        {
            Vector3 directionToTarget = lst[i].Value.Position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                //  bestTarget = potentialTarget;
                trg= lst[i];
               
            }
            //     i++;
        }
        return trg;
    }


    int GetClosestPointIdx(Transform []points,Vector2 fromP)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromP;
        int i = 0;
        int tarIdx = 0;
        foreach (Transform potentialTarget in points)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
                tarIdx = i;
            }
            i++;
        }
        return tarIdx;
    }

    bool FirstIsCloserThenSecond(Vector2 src1,Vector2 src2,Vector2 trg)
    {
        Vector3 directionToTarget = trg - src1;
        float dSqrToTarget = directionToTarget.sqrMagnitude;
        if (directionToTarget.magnitude > (trg - src2).magnitude)
        {
            return false;
        }
        else
            return true;
    }

    // Update is called once per frame
    void Update ()
    {
        target = GetClosest(pointsTab).position;
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(target, new Vector2(1,1));


        UnityEditor.Handles.DrawWireDisc(this.transform.position, this.transform.forward, dmgRadius);
        //Gizmos.DrawSphere(this.transform.position, dmgRadius);

        /*
        for (int i=0;i<circleIntesections.Count;i++)
        {
            Gizmos.DrawCube(circleIntesections[i], new Vector2(0.3f, 0.3f));
        }
        */
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

    private float AngleBetweenVectorsDe(Vector2 v1, Vector2 v2)
    {
        v1.Normalize();
        v2.Normalize();
        float dot = Vector2.Dot(v1, v2);
        float det = v1.x * v2.y - v1.y * v2.x;
        return Mathf.Rad2Deg * Mathf.Atan2(det, dot);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       ContactPoint2D contact =  collision.contacts[0];
      //  Debug.Break();
        Triangulator trianSc = collision.gameObject.transform.GetComponent<Triangulator>();

        IndexableCyclicalLinkedList<Vertex> lst = trianSc.GetVertLst();

       

        int prevIdx; 
        int pointIdx = GetClosestPointIdx(lst, contact.point);
        LinkedListNode<Vertex> prev, nxt, ctr, closest;
        ctr = GetClosestPointVert(lst, contact.point);
        closest = ctr;
       // prev = ctr.Previous;
        if (ctr.Next != null)
            nxt = ctr.Next;
        else
            nxt = lst.First;
        prevIdx = pointIdx - 1;

        //co gdy circledmg zakrewa wszystkie punkty?....

        //

        //to teraz maja idx bierzesz dwie intersekcje a potem dla tych intersekcji sprawdzasz dystans do segmentow
        //dzieki temu bedzieszi wiedzil jaki odcienek dzielic.

        if (prevIdx < 0)
            prevIdx += pointsTab.Length;

        Vector2 []intersection;
        //form first line seg
        intersection = null; 



        List<Vertex> VertToRemoveLst;
        VertToRemoveLst = new List<Vertex>();
        int someIdx = lst.Count;
            LinkedListNode<Vertex> prevT, nxtT, ctrT;
        ctrT = ctr;
        int a = 0;
       

       // return;
        bool areWeOnBeginning = true;
        while (1 == 1)//going counter clock wise by vertexes
        {

            string temp = "we check now ctr:" + ctr.Value.Index.ToString() + "on " + ctr.Value.Position.ToString() + " and " + nxt.Value.Index.ToString() + "on " + nxt.Value.Position.ToString();
            string temp1 = "czy odleglosc od contact point :" + contact.point.ToString() + " do " + ctr.Value.Position.ToString() + " = " + Distance(contact.point, ctr.Value.Position).ToString();
            float ctr_next_radius = (float)Distance(ctr.Value.Position, nxt.Value.Position) + dmgRadius;
            string temp2 = "jest wieksza od dystansy z ctr do nxt plus dmgRadius" + ctr_next_radius.ToString();

            if (ctr == closest && areWeOnBeginning)
            { 
                areWeOnBeginning = false;
            }

            if (Distance(contact.point, ctr.Value.Position) > Distance(ctr.Value.Position, nxt.Value.Position) + dmgRadius)//if to far to calc
            {

            }
            else
            {
                intersection = Intersection(contact.point.x, contact.point.y, dmgRadius, ctr.Value.Position, nxt.Value.Position);
                if (Distance(ctr.Value.Position, contact.point) < dmgRadius)
                {
                    VertToRemoveLst.Add(ctr.Value);
                }
                if (intersection != null)
                {
                    if (intersection.Length == 1)
                    {
                        lst.AddBefore(nxt, new LinkedListNode<Vertex>(new Vertex(intersection[0], someIdx++)));
                        if(Distance(nxt.Value.Position, contact.point) < dmgRadius)
                        {
                            Vector2[] circleIntersection;
                            circleIntersection = new Vector2[2];
                            Vector2 tempRotatedVec = intersection[0];
                            bool weHaveSecondCircleIntersect = false;
                         //   float tempAngle = AngleBetweenVectorsDe(nxt.Value.Position, ctr.Value.Position);
                            while(!weHaveSecondCircleIntersect)// check is have lenght already?
                            {
                                circleIntersection[0] = intersection[0];
                                RotateVecPFromI(contact.point, 20, tempRotatedVec);
                                //kuzwa nie da rady tak trzeba wyliczyc wszystkie intersekcje i zapameitywac gdzie byly z jakimi odcinkami i potem na podstawie
                                //tych info dodawac te vertexy.
                                //patrzysz jeszcze czy to jest intersekcja wejsciowa czy wyjsciowa.
                                //mozna dwie listy bo potem tylko szukasz intersekcji wyjsciowych.
                                //to lecisz to tych intesekcjach z odcinkami wedle clockwise i bierzesz pierwsza ktora jest pod wzgledem wielkosci konta.
                                //czyli wyjsciowa intersekcja odcninkowa z najmniejszym katem clockwise co do obecnej wejsciowej.
                            }
                            //addbefore nxt until in
                        }
                        //  if (Distance(ctr.Value.Position, contact.point) < dmgRadius)
                        //   {
                        //      VertToRemoveLst.Add(ctr.Value);
                        // kiedy remove bo jak mamy kilka vertow np 3 w kole to jak?
                        // rozkmin ten usuwa tylko dwoch.
                        //   }
                        // VertToRemoveLst.Add(ctr.Value);
                        //  break;
                    }
                    else
                    {
                        if (FirstIsCloserThenSecond(intersection[0], intersection[1], nxt.Value.Position))
                        {
                            lst.AddBefore(nxt, new LinkedListNode<Vertex>(new Vertex(intersection[0], someIdx++)));
                            lst.AddBefore(nxt.Previous, new LinkedListNode<Vertex>(new Vertex(intersection[1], someIdx++)));
                            //  areWeHaveTwoIntesect = true;
                            //  break;
                        }
                        else
                        {
                            lst.AddBefore(nxt, new LinkedListNode<Vertex>(new Vertex(intersection[1], someIdx++)));
                            lst.AddBefore(nxt.Previous, new LinkedListNode<Vertex>(new Vertex(intersection[0], someIdx++)));
                            // areWeHaveTwoIntesect = true;
                            // break;
                        }
                    }

                }
                else
                {
                    // i tak trzbea leciec po wszystkich bo jak beda plaskie polygony to bedzie uj
                    // a jak lecisz po wszystkich to wtedy nie mozesz breakowac jak juz masz intersekcje z dmgRadius.

                }
                
            }

            ctr = nxt;

            if (nxt.Next == null)
            {
                nxt = lst.First;
            }
            else
            {
                nxt = nxt.Next;
            }

            if (ctr == closest && !areWeOnBeginning)
            {
                break;
            }


            //ctr = closest;
        }

        /*
        while(1==1 && !areWeHaveTwoIntesect)
        {
            intersection = Intersection(contact.point.x, contact.point.y, dmgRadius, ctr.Value.Position, prev.Value.Position);

            if (intersection != null)
            {
                if (intersection.Length == 1)
                {
                    lst.AddAfter(prev, new LinkedListNode<Vertex>(new Vertex(intersection[0], someIdx++)));
                    VertToRemoveLst.Add(ctr.Value);
                    break;
                }
                else
                {
                    if (FirstIsCloserThenSecond(intersection[0], intersection[1], nxt.Value.Position))
                    {
                        lst.AddAfter(prev, new LinkedListNode<Vertex>(new Vertex(intersection[0], someIdx++)));
                        lst.AddAfter(prev.Next, new LinkedListNode<Vertex>(new Vertex(intersection[1], someIdx++)));
                        areWeHaveTwoIntesect = true;
                        break;
                    }
                    else
                    {
                        lst.AddAfter(prev, new LinkedListNode<Vertex>(new Vertex(intersection[1], someIdx++)));
                        lst.AddAfter(prev.Next, new LinkedListNode<Vertex>(new Vertex(intersection[0], someIdx++)));
                        areWeHaveTwoIntesect = true;
                        break;
                    }
                }

            }
            else
            {
                ctr = prev;
                prev = prev.Previous;
                VertToRemoveLst.Add(ctr.Value);
            }
        }

        */

        //we must do this i one direction
        //

        for(int i=0;i<VertToRemoveLst.Count;i++)
        {
            lst.Remove(VertToRemoveLst[i]);
        }



        /*
        intersection = Intersection(contact.point.x, contact.point.y, dmgRadius, ctr.Value.Position, nxt.Value.Position);

        for(int i=0;i<intersection.Length;i++)
        {
            circleIntesections.Add(intersection[i]);
        }


        intersection = Intersection(contact.point.x, contact.point.y, dmgRadius, ctr.Value.Position, nxt.Value.Position);

        for (int i = 0; i < intersection.Length; i++)
        {
            circleIntesections.Add(intersection[i]);
        }

        */
        //lecisz po next od near i dozycasz do wyrzucenia.
        //
        //lst.

        trianSc.ApplyNewPointsFromLst(lst);

        Destroy(gameObject);

    }


   // public 

    public Vector2 MidPointOfLineSeg(Vector2 p1,Vector2 p2)
    {
        return new Vector2((p1.x + p2.x) / 2, (p1.y + p2.y) / 2);
    }

    public Vector2 []Intersection(float cx, float cy, float radius,
            Vector2 lineStart, Vector2 lineEnd)
    {

        //Vector2 []intersection = new Vector2[2];
        Vector2 intersection1;
        Vector2 intersection2;
        int intersections = FindLineCircleIntersections(cx, cy, radius, lineStart, lineEnd, out intersection1, out intersection2);

        

        if (intersections == 1)
        {
            Vector2 []inter;
            inter = new Vector2[1];
            inter[0] = intersection1;
            return inter; 
        }
        if( intersections == 2)
        {
            // są to intesekcje tylko ze lini z odcinka a nie odcinka wiec trzeba sprawdzic dystansy.
            
            Vector2 mid = MidPointOfLineSeg(lineStart, lineEnd);
            double distFromMid = Distance(lineEnd, mid);
            if(Distance(mid,intersection1)< distFromMid &&  Distance(mid,intersection2)< distFromMid)
            {
                Vector2[] inter;
                inter = new Vector2[2];
                inter[0] = intersection1;
                inter[1] = intersection2;
                return inter;
            }
            else
            {
                if (Distance(mid, intersection1) < distFromMid)
                {
                    Vector2[] inter;
                    inter = new Vector2[1];
                    inter[0] = intersection1;
                    return inter;
                }
                if (Distance(mid, intersection2) < distFromMid)
                {
                    Vector2[] inter;
                    inter = new Vector2[1];
                    inter[0] = intersection2;
                    return inter;
                }
                else
                {
                    return null;
                }
            }

           
           // return inter;
        }
        else
        {
            return null;// no intersections at all

        }
        
     

    }

    private double Distance(Vector2 p1, Vector2 p2)
    {
        return Mathf.Sqrt(Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.y - p1.y, 2));
    }

    // Find the points of intersection.
    private int FindLineCircleIntersections(float cx, float cy, float radius,
        Vector2 point1, Vector2 point2, out Vector2 intersection1, out Vector2 intersection2)
    {
        float dx, dy, A, B, C, det, t;

        dx = point2.x - point1.x;
        dy = point2.y - point1.y;

        A = dx * dx + dy * dy;
        B = 2 * (dx * (point1.x - cx) + dy * (point1.y - cy));
        C = (point1.x - cx) * (point1.x - cx) + (point1.y - cy) * (point1.y - cy) - radius * radius;

        det = B * B - 4 * A * C;
        if ((A <= 0.0000001) || (det < 0))
        {
            // No real solutions.
            intersection1 = new Vector2(float.NaN, float.NaN);
            intersection2 = new Vector2(float.NaN, float.NaN);
            return 0;
        }
        else if (det == 0)
        {
            // One solution.
            t = -B / (2 * A);
            intersection1 = new Vector2(point1.x + t * dx, point1.y + t * dy);
            intersection2 = new Vector2(float.NaN, float.NaN);
            return 1;
        }
        else
        {
            // Two solutions.
            t = (float)((-B + Mathf.Sqrt(det)) / (2 * A));
            intersection1 = new Vector2(point1.x + t * dx, point1.y + t * dy);
            t = (float)((-B - Mathf.Sqrt(det)) / (2 * A));
            intersection2 = new Vector2(point1.x + t * dx, point1.y + t * dy);
            return 2;
        }
    }

    private int FindLineCircleIntersections(float cx, float cy, float radius,
        Vector2 point1, Vector2 point2, Vector2 []intersection)
    {
        float dx, dy, A, B, C, det, t;

        dx = point2.x - point1.x;
        dy = point2.y - point1.y;

        A = dx * dx + dy * dy;
        B = 2 * (dx * (point1.x - cx) + dy * (point1.y - cy));
        C = (point1.x - cx) * (point1.x - cx) + (point1.y - cy) * (point1.y - cy) - radius * radius;

        det = B * B - 4 * A * C;
        if ((A <= 0.0000001) || (det < 0))
        {
            // No real solutions.
            intersection = null;
            return 0;
        }
        else if (det == 0)
        {
            // One solution.
            t = -B / (2 * A);
            intersection[0] = new Vector2(point1.x + t * dx, point1.y + t * dy);
            
            return 1;
        }
        else
        {
            // Two solutions.
            t = (float)((-B + Mathf.Sqrt(det)) / (2 * A));
            intersection[0] = new Vector2(point1.x + t * dx, point1.y + t * dy);
            t = (float)((-B - Mathf.Sqrt(det)) / (2 * A));
            intersection[1] = new Vector2(point1.x + t * dx, point1.y + t * dy);
            return 2;
        }
    }





}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineFuctions
{

    static public int GetClosestPointIdx(IndexableCyclicalLinkedList<Vertex> lst, Vector2 fromP)
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

    static public LinkedListNode<Vertex> GetClosestPointVert(IndexableCyclicalLinkedList<Vertex> lst, Vector2 fromP)
    {

        float closestDistanceSqr = Mathf.Infinity;

        Vector2 currentPosition = fromP;
        LinkedListNode<Vertex> trg = null;
        // int i = 0;
        
        for (var i = 0; i < lst.Count; i++)
        {
            Vector3 directionToTarget = lst[i].Value.Position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                //  bestTarget = potentialTarget;
                trg = lst[i];

            }
            //     i++;
        }
        return trg;
    }

    static public bool FirstIsCloserThenSecond(Vector2 src1, Vector2 src2, Vector2 trg)
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
    
    static public Vector2 RotateVecPFromI(Vector2 i, float angle, Vector2 p)
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


    static public float AngleBetweenVectorsDe(Vector2 v1, Vector2 v2)
    {
        v1.Normalize();
        v2.Normalize();
        float dot = Vector2.Dot(v1, v2);
        float det = v1.x * v2.y - v1.y * v2.x;
        return Mathf.Rad2Deg * Mathf.Atan2(det, dot);


    }

    static public Vector2[] Intersection(float cx, float cy, float radius,
            Vector2 lineStart, Vector2 lineEnd)
    {

        //Vector2 []intersection = new Vector2[2];
        Vector2 intersection1;
        Vector2 intersection2;
        int intersections = FindLineCircleIntersections(cx, cy, radius, lineStart, lineEnd, out intersection1, out intersection2);



        if (intersections == 1)
        {
            Vector2[] inter;
            inter = new Vector2[1];
            inter[0] = intersection1;
            return inter;
        }
        if (intersections == 2)
        {
            // są to intesekcje tylko ze lini z odcinka a nie odcinka wiec trzeba sprawdzic dystansy.

            Vector2 mid = MidPointOfLineSeg(lineStart, lineEnd);
            double distFromMid = Distance(lineEnd, mid);
            if (Distance(mid, intersection1) < distFromMid && Distance(mid, intersection2) < distFromMid)
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

   static public float Angle(Vector2 v)
    {
        Vector2 o = new Vector2(0, 0);
        //  o = points[0].position - points[1].position;
        float dx = v.x - o.x;
        float dy = v.y - o.y;
        float r = Mathf.Sqrt(dx * dx + dy * dy);
        float angle = Mathf.Atan2(dy, dx);
        //angle -= base_angle;
        if (angle < 0) angle += Mathf.PI * 2;
        return angle;
    }

    static public int FindLineCircleIntersections(float cx, float cy, float radius,
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



    static public double Distance(Vector2 p1, Vector2 p2)
    {
        return Mathf.Sqrt(Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.y - p1.y, 2));
    }


    static public Vector2 MidPointOfLineSeg(Vector2 p1, Vector2 p2)
    {
        return new Vector2((p1.x + p2.x) / 2, (p1.y + p2.y) / 2);
    }

    static public int Get_line_intersection(float p0_x, float p0_y, float p1_x, float p1_y,
   float p2_x, float p2_y, float p3_x, float p3_y, out float i_x, out float i_y)
    {
        float s1_x, s1_y, s2_x, s2_y;
        s1_x = p1_x - p0_x; s1_y = p1_y - p0_y;
        s2_x = p3_x - p2_x; s2_y = p3_y - p2_y;
        i_x = 0;
        i_y = 0;
        float s, t;
        s = (-s1_y * (p0_x - p2_x) + s1_x * (p0_y - p2_y)) / (-s2_x * s1_y + s1_x * s2_y);
        t = (s2_x * (p0_y - p2_y) - s2_y * (p0_x - p2_x)) / (-s2_x * s1_y + s1_x * s2_y);

        if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
        {
            // Collision detected

            i_x = p0_x + (t * s1_x);

            i_y = p0_y + (t * s1_y);
            return 1;
        }

        return 0; // No collision
    }





}


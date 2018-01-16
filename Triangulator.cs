using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;






public class Triangulator : MonoBehaviour
{


    static readonly IndexableCyclicalLinkedList<Vertex> polygonVertices = new IndexableCyclicalLinkedList<Vertex>();
    static readonly IndexableCyclicalLinkedList<Vertex> earVertices = new IndexableCyclicalLinkedList<Vertex>();
    static readonly CyclicalList<Vertex> convexVertices = new CyclicalList<Vertex>();
    static readonly CyclicalList<Vertex> reflexVertices = new CyclicalList<Vertex>();

    static readonly IndexableCyclicalLinkedList<Vertex> listOfPoints = new IndexableCyclicalLinkedList<Vertex>();



    public Transform []points;
    public List<Transform> neighbours;
    public Vector2 boundingBoxUpLf;
    public Vector2 boundingBoxDwRg;
    int[] idxOfTriangles;


    /// <summary>
    /// Modify points list by neighbour collision if collision circle intesect with bounding box of this
    /// </summary>
    public void InitNeighboursWithCollision(Vector2 pt, float radius)
    {
        for(int i=0;i<neighbours.Count;i++)
        {


            Triangulator scTr = neighbours[i].GetComponent<Triangulator>();
            float up = scTr.boundingBoxUpLf.y;
            float dw = scTr.boundingBoxDwRg.y;
            float rg = scTr.boundingBoxDwRg.x;
            float lf = scTr.boundingBoxUpLf.x;
                //    if (RectA.Left < RectB.Right && RectA.Right > RectB.Left &&
    // RectA.Top > RectB.Bottom && RectA.Bottom < RectB.Top)

            if (lf < pt.x +radius && rg > pt.x - radius && up > pt.y - radius && dw < pt.y + radius)
            {
                scTr.InitCollision(pt, radius);
              //  Debug.Log("neighbour with number:" + i.ToString() + " overlaps");
            }
        }
       // if()
    }



    

    public void InitCollision(Vector2 pt,float dmgRadius)
    {
        

        IndexableCyclicalLinkedList<Vertex> lst = GetVertLst();
        


        int prevIdx;
        int pointIdx = LineFuctions.GetClosestPointIdx(lst, pt);
        LinkedListNode<Vertex> prev, nxt, ctr, closest;
        ctr = LineFuctions.GetClosestPointVert(lst, pt);
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

        

        Vector2[] intersection;
        //form first line seg
        intersection = null;



        List<Vertex> VertToRemoveLst;
        VertToRemoveLst = new List<Vertex>();
        int someIdx = lst.Count;
        LinkedListNode<Vertex> prevT, nxtT, ctrT;
        ctrT = ctr;
       // int a = 0;

        List<LineSegmentWithAngle> lineSegsOuter;
        List<LineSegmentInnerWithIntersect> intersectionLineSeg;
        List<IntersectionWithVert> intersctionVertLst;
        intersctionVertLst = new List<IntersectionWithVert>();
        lineSegsOuter = new List<LineSegmentWithAngle>();
        intersectionLineSeg = new List<LineSegmentInnerWithIntersect>();


        // return;
        bool areWeOnBeginning = true;
        while (1 == 1)//going counter clock wise by vertexes
        {

            string temp = "we check now ctr:" + ctr.Value.Index.ToString() + "on " + ctr.Value.Position.ToString() + " and " + nxt.Value.Index.ToString() + "on " + nxt.Value.Position.ToString();
            string temp1 = "czy odleglosc od contact point :" + pt.ToString() + " do " + ctr.Value.Position.ToString() + " = " + LineFuctions.Distance(pt, ctr.Value.Position).ToString();
            float ctr_next_radius = (float)LineFuctions.Distance(ctr.Value.Position, nxt.Value.Position) + dmgRadius;
            string temp2 = "jest wieksza od dystansy z ctr do nxt plus dmgRadius" + ctr_next_radius.ToString();

            if (ctr == closest && areWeOnBeginning)
            {
                areWeOnBeginning = false;
            }

            if (LineFuctions.Distance(pt, ctr.Value.Position) > LineFuctions.Distance(ctr.Value.Position, nxt.Value.Position) + dmgRadius)//if to far to calc
            {

            }
            else
            {
                intersection = LineFuctions.Intersection(pt.x, pt.y, dmgRadius, ctr.Value.Position, nxt.Value.Position);
                if (LineFuctions.Distance(ctr.Value.Position, pt) < dmgRadius)
                {
                    VertToRemoveLst.Add(ctr.Value);

                }
                if (intersection != null)
                {
                    if (intersection.Length == 1)
                    {
                        //  lst.AddBefore(nxt, new LinkedListNode<Vertex>(new Vertex(intersection[0], someIdx++)));
                        if (LineFuctions.Distance(nxt.Value.Position, pt) < dmgRadius)
                        {
                            //odcinek wchodzacy w dmgRadius push.
                            intersectionLineSeg.Add(new LineSegmentInnerWithIntersect(ctr.Value, nxt.Value, ctr, nxt, intersection[0], true));
                            //zapamietac intersekcje  wychodzącę.
                            //pod koniec alg lecisz od wychodzacej dopuki nie masz intersekcji. 
                            //powinno dzialac dobrze x171113
                            Vector2[] circleIntersection;
                            circleIntersection = new Vector2[2];
                            Vector2 tempRotatedVec = intersection[0];
                            bool weHaveSecondCircleIntersect = false;
                            //   float tempAngle = AngleBetweenVectorsDe(nxt.Value.Position, ctr.Value.Position);
                            //  while(!weHaveSecondCircleIntersect)// check is have lenght already?
                            //   {
                            //      circleIntersection[0] = intersection[0];
                            //      RotateVecPFromI(contact.point, 20, tempRotatedVec);
                            //kuzwa nie da rady tak trzeba wyliczyc wszystkie intersekcje i zapameitywac gdzie byly z jakimi odcinkami i potem na podstawie
                            //tych info dodawac te vertexy.
                            //patrzysz jeszcze czy to jest intersekcja wejsciowa czy wyjsciowa.
                            //mozna dwie listy bo potem tylko szukasz intersekcji wyjsciowych.
                            //to lecisz to tych intesekcjach z odcinkami wedle clockwise i bierzesz pierwsza ktora jest pod wzgledem wielkosci konta.
                            //czyli wyjsciowa intersekcja odcninkowa z najmniejszym katem clockwise co do obecnej wejsciowej.
                            //  }
                            //addbefore nxt until in
                        }
                        else
                        {
                            lineSegsOuter.Add(new LineSegmentWithAngle(ctr.Value, nxt.Value));
                            // intersectionLineSeg.Add(new LineSegmentInnerWithIntersect(ctr.Value, nxt.Value,intersection[0], false));
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
                        if (LineFuctions.FirstIsCloserThenSecond(intersection[0], intersection[1], nxt.Value.Position))
                        {
                            //     lst.AddBefore(nxt, new LinkedListNode<Vertex>(new Vertex(intersection[0], someIdx++)));
                            intersectionLineSeg.Add(new LineSegmentInnerWithIntersect(ctr.Value, nxt.Value, ctr, nxt, intersection[1], true));
                            lineSegsOuter.Add(new LineSegmentWithAngle(ctr.Value, nxt.Value));    // previus ctr to nxt

                            //       lst.AddBefore(nxt.Previous, new LinkedListNode<Vertex>(new Vertex(intersection[1], someIdx++)));
                            //  areWeHaveTwoIntesect = true;
                            //  break;
                        }
                        else
                        {
                            //     lst.AddBefore(nxt, new LinkedListNode<Vertex>(new Vertex(intersection[1], someIdx++)));
                            //      lst.AddBefore(nxt.Previous, new LinkedListNode<Vertex>(new Vertex(intersection[0], someIdx++)));
                            intersectionLineSeg.Add(new LineSegmentInnerWithIntersect(ctr.Value, nxt.Value, ctr, nxt, intersection[0], true));
                            lineSegsOuter.Add(new LineSegmentWithAngle(ctr.Value, nxt.Value));
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

        


        
        while (intersectionLineSeg.Count > 0)
        {
            LineSegmentInnerWithIntersect trg = intersectionLineSeg[0];
            float angleOftrg = LineFuctions.Angle(trg.A.Position - trg.B.Position);
            float angleOfOuter = LineFuctions.Angle(lineSegsOuter[0].A.Position - lineSegsOuter[0].B.Position);
            LinkedListNode<Vertex> tmp;


            bool weHaveIntersect = false;
            Vector2 intersecVector = trg.intersection;
            Vector2 ctrV;
            ctrV = intersecVector;
            float basicAngle = LineFuctions.Angle(trg.intersection - pt);
            foreach (LineSegmentWithAngle i in lineSegsOuter)
            {
                i.angle -= basicAngle;
                if (i.angle < 0)
                    i.angle += Mathf.PI * 2;
            }
            //float tempAngle = AngleBetweenVectorsDe(nxt.Value.Position, ctr.Value.Position);
            //  objListOrder.Sort((x, y) => x.OrderDate.CompareTo(y.OrderDate));
            lineSegsOuter.OrderByDescending(x => x.angle);
            float crossingX, crossingY;
            // lineSegsOuter.Sort()
            tmp = lst.AddAfter(trg.aLink, new Vertex(trg.intersection, someIdx++));
            ctrV = LineFuctions.RotateVecPFromI(pt, -2, ctrV);
            while (!weHaveIntersect)// check is have lenght already?
            {
                Vector2 nxtV;

                LineSegmentWithAngle nearestLineSeg = lineSegsOuter[0];
                nxtV = LineFuctions.RotateVecPFromI(pt, -30, ctrV); // czy obraca w dobra strone?
                if (LineFuctions.Get_line_intersection(ctrV.x, ctrV.y, nxtV.x, nxtV.y, nearestLineSeg.A.Position.x,
                    nearestLineSeg.A.Position.y, nearestLineSeg.B.Position.x, nearestLineSeg.B.Position.y, out crossingX, out crossingY) == 0)
                {
                    ctrV = nxtV;
                    nxtV = LineFuctions.RotateVecPFromI(pt, -30, ctrV);
                    tmp = lst.AddAfter(tmp, new Vertex(ctrV, someIdx++));

                }
                else
                {
                    tmp = lst.AddAfter(tmp, new Vertex(new Vector2(crossingX, crossingY), someIdx++));
                    weHaveIntersect = true;
                    intersectionLineSeg.RemoveAt(0);
                }

                //gizmosLineSegs.Add(new LineSegment(new Vertex(ctrV,/*idx*/ 1000), new Vertex(nxtV,/*idx*/ 1000)));
                // lst.ad


                //kuzwa nie da rady tak trzeba wyliczyc wszystkie intersekcje i zapameitywac gdzie byly z jakimi odcinkami i potem na podstawie
                //tych info dodawac te vertexy.
                //patrzysz jeszcze czy to jest intersekcja wejsciowa czy wyjsciowa.
                //mozna dwie listy bo potem tylko szukasz intersekcji wyjsciowych.
                //to lecisz to tych intesekcjach z odcinkami wedle clockwise i bierzesz pierwsza ktora jest pod wzgledem wielkosci konta.
                //czyli wyjsciowa intersekcja odcninkowa z najmniejszym katem clockwise co do obecnej wejsciowej.
            }
        }




        for (int i = 0; i < VertToRemoveLst.Count; i++)
        {
            lst.Remove(VertToRemoveLst[i]);
        }



        for (int i = 0; i < lst.Count; i++)
        {
            Debug.Log(lst[i].Value);

        }



        ApplyNewPointsFromLst(lst);

    }




    private void Awake()
    {
       // neighbours = new List<Transform>();
        float xMin, xMax, yMin, yMax;
        xMin = 3000.0f;
        xMax = -3000.0f;
        yMin = 3000.0f;
        yMax = -3000.0f;
        Vector2 tmp;
        for(int i =0;i<points.Length;i++)
        {
            tmp = points[i].position;
            if(tmp.x < xMin)
            {
                xMin = tmp.x;
            }
            if(tmp.x > xMax)
            {
                xMax = tmp.x;
            }
            if(tmp.y < yMin)
            {
                yMin = tmp.y;
            }
            if(tmp.y > yMax)
            {
                yMax = tmp.y;
            }
        }
        boundingBoxUpLf = new Vector2(xMin, yMax);
        boundingBoxDwRg = new Vector2(xMax, yMin);
    }

    /// <summary>
    /// Triangulates a 2D polygon produced the indexes required to render the points as a triangle list.
    /// </summary>
    /// <param name="inputVertices">The polygon vertices in counter-clockwise winding order.</param>
    /// <param name="desiredWindingOrder">The desired output winding order.</param>
    /// <param name="outputVertices">The resulting vertices that include any reversals of winding order and holes.</param>
    /// <param name="indices">The resulting indices for rendering the shape as a triangle list.</param>
    /// 
    public static void Triangulate(
        Vector2[] inputVertices,
        WindingOrder desiredWindingOrder,
        out Vector2[] outputVertices,
        out int[] indices)
    {
        

        List<Triangle> triangles = new List<Triangle>();

        //make sure we have our vertices wound properly
    //    if (DetermineWindingOrder(inputVertices) == WindingOrder.Clockwise)
   //         outputVertices = ReverseWindingOrder(inputVertices);
   //     else
            outputVertices = (Vector2[])inputVertices.Clone();

        //clear all of the lists
        polygonVertices.Clear();
        earVertices.Clear();
        convexVertices.Clear();
        reflexVertices.Clear();

        //generate the cyclical list of vertices in the polygon
        for (int i = 0; i < outputVertices.Length; i++)
            polygonVertices.AddLast(new Vertex(outputVertices[i], i));

        //categorize all of the vertices as convex, reflex, and ear
        FindConvexAndReflexVertices();
        FindEarVertices();

        //clip all the ear vertices
        while (polygonVertices.Count > 3 && earVertices.Count > 0)
            ClipNextEar(triangles);

        //if there are still three points, use that for the last triangle
        if (polygonVertices.Count == 3)
            triangles.Add(new Triangle(
                polygonVertices[0].Value,
                polygonVertices[1].Value,
                polygonVertices[2].Value));

        //add all of the triangle indices to the output array
        indices = new int[triangles.Count * 3];

        //move the if statement out of the loop to prevent all the
        //redundant comparisons
        if (desiredWindingOrder == WindingOrder.CounterClockwise)
        {
            for (int i = 0; i < triangles.Count; i++)
            {
                indices[(i * 3)] = triangles[i].A.Index;
                indices[(i * 3) + 1] = triangles[i].B.Index;
                indices[(i * 3) + 2] = triangles[i].C.Index;
            }
        }
        else
        {
            for (int i = 0; i < triangles.Count; i++)
            {
                indices[(i * 3)] = triangles[i].C.Index;
                indices[(i * 3) + 1] = triangles[i].B.Index;
                indices[(i * 3) + 2] = triangles[i].A.Index;
            }
        }
    }

    

    

    /// <summary>
    /// Cuts a hole into a shape.
    /// </summary>
    /// <param name="shapeVerts">An array of vertices for the primary shape.</param>
    /// <param name="holeVerts">An array of vertices for the hole to be cut. It is assumed that these vertices lie completely within the shape verts.</param>
    /// <returns>The new array of vertices that can be passed to Triangulate to properly triangulate the shape with the hole.</returns>
    public static Vector2[] CutHoleInShape(Vector2[] shapeVerts, Vector2[] holeVerts)
    {
        

        //make sure the shape vertices are wound counter clockwise and the hole vertices clockwise
        shapeVerts = EnsureWindingOrder(shapeVerts, WindingOrder.CounterClockwise);
        holeVerts = EnsureWindingOrder(holeVerts, WindingOrder.Clockwise);

        //clear all of the lists
        polygonVertices.Clear();
        earVertices.Clear();
        convexVertices.Clear();
        reflexVertices.Clear();

        //generate the cyclical list of vertices in the polygon
        for (int i = 0; i < shapeVerts.Length; i++)
            polygonVertices.AddLast(new Vertex(shapeVerts[i], i));

        CyclicalList<Vertex> holePolygon = new CyclicalList<Vertex>();
        for (int i = 0; i < holeVerts.Length; i++)
            holePolygon.Add(new Vertex(holeVerts[i], i + polygonVertices.Count));



        FindConvexAndReflexVertices();
        FindEarVertices();

        //find the hole vertex with the largest X value
        Vertex rightMostHoleVertex = holePolygon[0];
       
        foreach (Vertex v in holePolygon)
            if (v.Position.x > rightMostHoleVertex.Position.x)
                rightMostHoleVertex = v;

        Debug.Log("rightMost to cut out is " + rightMostHoleVertex);
        Debug.Log("rightMost to cut out isdskdl;skdl;ksal;dkl;sakd;lksa;ldk;sakd;lsakdl;ksa;ldfdgdfgfdsgfdgfdgdgfd\n \n\n\n " );

        //construct a list of all line segments where at least one vertex
        //is to the right of the rightmost hole vertex with one vertex
        //above the hole vertex and one below
        List<LineSegment> segmentsToTest = new List<LineSegment>();
        for (int i = 0; i < polygonVertices.Count; i++)
        {
            Vertex a = polygonVertices[i].Value;
            Vertex b = polygonVertices[i + 1].Value;

            if ((a.Position.x > rightMostHoleVertex.Position.x || b.Position.x > rightMostHoleVertex.Position.x) &&
                ((a.Position.y >= rightMostHoleVertex.Position.y && b.Position.y <= rightMostHoleVertex.Position.y) ||
                (a.Position.y <= rightMostHoleVertex.Position.y && b.Position.y >= rightMostHoleVertex.Position.y)))
                segmentsToTest.Add(new LineSegment(a, b));
        }

        //now we try to find the closest intersection point heading to the right from
        //our hole vertex.
        float? closestPoint = null;
        LineSegment closestSegment = new LineSegment();
        foreach (LineSegment segment in segmentsToTest)
        {
            float? intersection = segment.IntersectsWithRay(rightMostHoleVertex.Position, new Vector2(1f,0f));
            if (intersection != null)
            {
                if (closestPoint == null || closestPoint.Value > intersection.Value)
                {
                    closestPoint = intersection;
                    closestSegment = segment;
                }
            }
        }

        //if closestPoint is null, there were no collisions (likely from improper input data),
        //but we'll just return without doing anything else
        if (closestPoint == null)
            return shapeVerts;

        //otherwise we can find our mutually visible vertex to split the polygon
        Vector2 I = rightMostHoleVertex.Position + new Vector2(1f,0f) * closestPoint.Value;
        Vertex P = (closestSegment.A.Position.x> closestSegment.B.Position.x)
            ? closestSegment.A
            : closestSegment.B;

        //construct triangle MIP
        Triangle mip = new Triangle(rightMostHoleVertex, new Vertex(I, 1), P);

        //see if any of the reflex vertices lie inside of the MIP triangle
        List<Vertex> interiorReflexVertices = new List<Vertex>();
        foreach (Vertex v in reflexVertices)
            if (mip.ContainsPoint(v))
                interiorReflexVertices.Add(v);

        //if there are any interior reflex vertices, find the one that, when connected
        //to our rightMostHoleVertex, forms the line closest to Vector2.UnitX
        if (interiorReflexVertices.Count > 0)
        {
            float closestDot = -1f;
            foreach (Vertex v in interiorReflexVertices)
            {
                //compute the dot product of the vector against the UnitX
                Vector2 d = v.Position - rightMostHoleVertex.Position;
                d.Normalize();
                float dot = Vector2.Dot(new Vector2(1f,0), d);

                //if this line is the closest we've found
                if (dot > closestDot)
                {
                    //save the value and save the vertex as P
                    closestDot = dot;
                    P = v;
                }
            }
        }

        //now we just form our output array by injecting the hole vertices into place
        //we know we have to inject the hole into the main array after point P going from
        //rightMostHoleVertex around and then back to P.
        int mIndex = holePolygon.IndexOf(rightMostHoleVertex);
        int injectPoint = polygonVertices.IndexOf(P);

        
        for (int i = mIndex; i <= mIndex + holePolygon.Count; i++)
        {
            
            polygonVertices.AddAfter(polygonVertices[injectPoint++], holePolygon[i]);
        }
        polygonVertices.AddAfter(polygonVertices[injectPoint], P);



        //finally we write out the new polygon vertices and return them out
        Vector2[] newShapeVerts = new Vector2[polygonVertices.Count];
        for (int i = 0; i < polygonVertices.Count; i++)
            newShapeVerts[i] = polygonVertices[i].Value.Position;

        return newShapeVerts;
    }

   

    

    /// <summary>
    /// Ensures that a set of vertices are wound in a particular order, reversing them if necessary.
    /// </summary>
    /// <param name="vertices">The vertices of the polygon.</param>
    /// <param name="windingOrder">The desired winding order.</param>
    /// <returns>A new set of vertices if the winding order didn't match; otherwise the original set.</returns>
    public static Vector2[] EnsureWindingOrder(Vector2[] vertices, WindingOrder windingOrder)
    {
        
        if (DetermineWindingOrder(vertices) != windingOrder)
        {
            
            return ReverseWindingOrder(vertices);
        }

        
        return vertices;
    }

    
    /// <summary>
    /// Reverses the winding order for a set of vertices.
    /// </summary>
    /// <param name="vertices">The vertices of the polygon.</param>
    /// <returns>The new vertices for the polygon with the opposite winding order.</returns>
    public static Vector2[] ReverseWindingOrder(Vector2[] vertices)
    {
       
        Vector2[] newVerts = new Vector2[vertices.Length];



        newVerts[0] = vertices[0];
        for (int i = 1; i < newVerts.Length; i++)
            newVerts[i] = vertices[vertices.Length - i];



        return newVerts;
    }

    

    /// <summary>
    /// Determines the winding order of a polygon given a set of vertices.
    /// </summary>
    /// <param name="vertices">The vertices of the polygon.</param>
    /// <returns>The calculated winding order of the polygon.</returns>
    public static WindingOrder DetermineWindingOrder(Vector2[] vertices)
    {
        int clockWiseCount = 0;
        int counterClockWiseCount = 0;
        Vector2 p1 = vertices[0];

        for (int i = 1; i < vertices.Length; i++)
        {
            Vector2 p2 = vertices[i];
            Vector2 p3 = vertices[(i + 1) % vertices.Length];

            Vector2 e1 = p1 - p2;
            Vector2 e2 = p3 - p2;

            if (e1.x * e2.y - e1.y * e2.x >= 0)
                clockWiseCount++;
            else
                counterClockWiseCount++;

            p1 = p2;
        }

        return (clockWiseCount > counterClockWiseCount)
            ? WindingOrder.Clockwise
            : WindingOrder.CounterClockwise;
    }

   

   

    private static void ClipNextEar(ICollection<Triangle> triangles)
    {
        //find the triangle


        Vertex ear = earVertices[0].Value;
        Vertex prev = polygonVertices[polygonVertices.IndexOf(ear) - 1].Value;
        Vertex next = polygonVertices[polygonVertices.IndexOf(ear) + 1].Value;
        triangles.Add(new Triangle(ear, next, prev));
        Debug.Log("we clip ear of" + ear + next + prev);

        //remove the ear from the shape
        earVertices.RemoveAt(0);
        polygonVertices.RemoveAt(polygonVertices.IndexOf(ear));
       

        //validate the neighboring vertices
        ValidateAdjacentVertex(prev);
        ValidateAdjacentVertex(next);

        //write out the states of each of the lists

    }

   

    private static void ValidateAdjacentVertex(Vertex vertex)
    {
        

        if (reflexVertices.Contains(vertex))
        {
            if (IsConvex(vertex))
            {
                reflexVertices.Remove(vertex);
                convexVertices.Add(vertex);
                
            }
            else
            {
                
            }
        }

        if (convexVertices.Contains(vertex))
        {
            bool wasEar = earVertices.Contains(vertex);
            bool isEar = IsEar(vertex);

            if (wasEar && !isEar)
            {
                earVertices.Remove(vertex);
               
            }
            else if (!wasEar && isEar)
            {
                earVertices.AddFirst(vertex);
                
            }
            else
            {
                
            }
        }
    }

   

    private static void FindConvexAndReflexVertices()
    {
        for (int i = 0; i < polygonVertices.Count; i++)
        {
            Vertex v = polygonVertices[i].Value;

            if (IsConvex(v))
            {
                Debug.Log(v.Index + "is Convex");
                convexVertices.Add(v);
                
            }
            else
            {

                Debug.Log(v.Index + "is Reflex");
                reflexVertices.Add(v);
               
            }
        }
    }

   

    private static void FindEarVertices()
    {
        for (int i = 0; i < convexVertices.Count; i++)
        {
            Vertex c = convexVertices[i];

            if (IsEar(c))
            {
                Debug.Log("ear is " + c.Index);
                earVertices.AddLast(c);
               
            }
        }
    }

    

    private static bool IsEar(Vertex c)
    {
        Vertex p = polygonVertices[polygonVertices.IndexOf(c) - 1].Value;
        Vertex n = polygonVertices[polygonVertices.IndexOf(c) + 1].Value;


        Debug.Log("is"+ c +"with" + p + " "+ n +"is ear" );


        foreach (Vertex t in reflexVertices)
        {
            if (t.Equals(p) || t.Equals(c) || t.Equals(n))
                continue;

            if (Triangle.ContainsPoint(p, c, n, t))
            {
                Debug.Log("for :" + t + "triangle contains: " + p + " " + c + " " + n);
                return false;
            }
        }

        return true;
    }


    private static float AngleBetweenVectors(Vector2 v1, Vector2 v2)
    {
        return Mathf.Atan2(v2.y - v1.y, v2.x - v1.x);
    }

    private static float AngleBetweenVectorsDe(Vector2 v1, Vector2 v2)
    {
        v1.Normalize();
        v2.Normalize();
        float dot = Vector2.Dot(v1, v2);
        float det = v1.x * v2.y - v1.y * v2.x;
        return Mathf.Rad2Deg * Mathf.Atan2(det, dot);


    }



    private static bool IsConvex(Vertex c)
    {
        Vertex p = polygonVertices[polygonVertices.IndexOf(c) - 1].Value;
        Vertex n = polygonVertices[polygonVertices.IndexOf(c) + 1].Value;

        Vector2 d1 = p.Position - c.Position;
        d1.Normalize();
        Vector2 d2 = n.Position - c.Position;
        d2.Normalize();
        Vector2 n2 = new Vector2(-d2.y, d2.x);
        string tmp;
        tmp = AngleBetweenVectorsDe(d1, d2).ToString() + "of vec2:" + d1.ToString() + " and " + d2.ToString();
        float angleD1 =  AngleBetweenVectorsDe(Vector2.up, d1);
      //  if (angleD1 < 0) angleD1 += 360;
        float angleD2 =  AngleBetweenVectorsDe(Vector2.up, d2);
      //  if (angleD2 < 0) angleD2 += 360;
        tmp = (angleD1.ToString() +"  " +angleD2.ToString() + "of vec2:" + d1.ToString() + " and " + d2.ToString());
        Debug.Log(tmp);
        //rage because byl counterclockwise zamiast clockwise i na zle verty sie patrzylo.

        return AngleBetweenVectorsDe(d1, d2) < 0f;
    }

   
    private static bool IsReflex(Vertex c)
    {
        return !IsConvex(c);
    }


    public void ApplyNewPointsFromLst(IndexableCyclicalLinkedList<Vertex> lst)
    {
        Transform[]newPoints;
        newPoints = new Transform[lst.Count];
        Vector2 []tab;
        tab = new Vector2[lst.Count];
        Transform tempObj = points[0];
        for(int i=0;i<lst.Count;i++)
        {
            newPoints[i] = Instantiate(tempObj, lst[i].Value.Position, tempObj.rotation,gameObject.transform);
            newPoints[i].name = "pt"+ i.ToString();
            tab[i] = lst[i].Value.Position;


        }
        for(int i=0;i<points.Length;i++)
        {
            Destroy(points[i].gameObject);
        }

        Vector2[] tmpTab;
        tmpTab = new Vector2[lst.Count];
        /*
        for (int i = 0,j= lst.Count-1 ; i < lst.Count;j--, i++)
        {
            tmpTab[j] = tab[i];
 


        }
        tab = tmpTab;

    */
       // points.
        points = newPoints;
        Vector2[] tabOut;
        tabOut = new Vector2[lst.Count];
        int[] idxs;
      //  if(lst.Count>2)
       //     idxs = new int[(lst.Count -2) *3];
    

        Triangulate(tab, WindingOrder.Clockwise, out tabOut, out idxs);

        for (int i = 0; i < lst.Count; i++)
        {
            points[i].position = tabOut[i];


        }
        idxOfTriangles = idxs;
      //  tab = 
        
        

        Navigator navi;
        navi = GetComponent<Navigator>();
        if (navi != null)
        {
            navi.RunMeshAndCollidUpdaters();
        }


    }


    void Start()
    {
        Vector2 []tab;
        Vector2 []tabOut;
        int []idxs;
        
        tab = new Vector2[points.Length];
        for(int i=0;i<points.Length;i++)
        {
            tab[i] = points[i].position;
        }

        tabOut = new Vector2[points.Length];

        Triangulate(tab, WindingOrder.Clockwise,out tabOut,out idxs); 

        for(int i=0;i<idxs.Length/3;i++)
        {
            Debug.Log("triagnle " + i + "idxs: " + idxs[(i * 3)] + " " + idxs[(i * 3) + 1] + " " + idxs[(i * 3) + 2]);
        }
        idxOfTriangles = idxs;



        for(int i=0;i<tabOut.Length;i++)
        {
            listOfPoints.AddLast(new Vertex(tabOut[i],i));
        }



        for(int i=0;i < tabOut.Length;i++)
        {
            Debug.Log(tabOut[i].ToString() + "tabOut pos");
        }
        Debug.Log(tabOut.Length + "tabOut len");
        Debug.Log(points.Length + "points in triang len");
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying )
        {
            for (int j = 0; j < idxOfTriangles.Length; j += 3)
            {
                Gizmos.DrawLine(points[idxOfTriangles[j]].position, points[idxOfTriangles[j + 1]].position);
                Gizmos.DrawLine(points[idxOfTriangles[j + 1]].position, points[idxOfTriangles[j + 2]].position);
                Gizmos.DrawLine(points[idxOfTriangles[j + 2]].position, points[idxOfTriangles[j]].position);
            }
        }
        else
        {
            for(int j=0;j< points.Length; j++)
            {
                Gizmos.DrawLine(points[j].position, points[(j+1) % points.Length].position);
            }
        }

        
    }

    public int []GetTriangulatorIdxs()
    {
        if(idxOfTriangles != null)
        {
            return idxOfTriangles;
        }
        return null;
    }

    public int GetNumberOfPoints()
    {
        return points.Length;
    }

    public Vector3 []GetVertices()
    {
        Vector3[] VerTab;
        VerTab = new Vector3[points.Length];
        for(int i = 0;i < points.Length;i++)
        {
            VerTab[i] = new Vector3(points[i].position.x,points[i].position.y,0);
        }
        return VerTab;
    }

    public Vector2 []GetVertices2d()
    {
        Vector2[] VerTab;
        VerTab = new Vector2[points.Length];
        for(int i=0;i < points.Length;i++)
        {
            VerTab[i] = new Vector2(points[i].position.x, points[i].position.y);
        }
         return VerTab;
    }

    public Transform []GetPoints()
    {
        return points;
    }


    /*
    public void UpdatePolygonByList(IndexableCyclicalLinkedList<Vertex> lst)
    {
        Vector2[] tab;
        Vector2[] tabOut;
        int[] idxs;

        

        tab = new Vector2[points.Length];
        for (int i = 0; i < lst.Count; i++)
        {
            tab[i] = lst[i].Value.Position;
        }

        tabOut = new Vector2[points.Length];
        Triangulate(tab, WindingOrder.Clockwise, out tabOut, out idxs);
        idxOfTriangles = idxs;



    }
    */
    public IndexableCyclicalLinkedList<Vertex> GetVertLst()
    {
        IndexableCyclicalLinkedList<Vertex> lst;
        lst = new IndexableCyclicalLinkedList<Vertex>();
        for(int i=0;i < points.Length;i++)
        {
            lst.AddLast(new Vertex(points[i].position, i));
        }
        return lst;
    }

}

/// <summary>
/// Specifies a desired winding order for the shape vertices.
/// </summary>
public enum WindingOrder
{
    Clockwise,
    CounterClockwise
}
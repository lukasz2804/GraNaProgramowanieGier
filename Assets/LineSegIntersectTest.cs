using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class LineSegIntersectTest : MonoBehaviour {


    public Transform firstA;
    public Transform firstB;
    public Transform secondA;
    public Transform secondB;

    public Transform []points;
    public float[] angles;

    private Vector2 intersect;
	// Use this for initialization
	void Start ()
    {
        angles = new float[points.Length / 2];
        for(int i = 0;i<points.Length/2;i++)
        {
            angles[i] = Mathf.Rad2Deg*Angle(points[i * 2].position - points[(i * 2) + 1].position);
        }
        intersect = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        int tmp = get_line_intersection(firstA.position, firstB.position, secondA.position, secondB.position, out intersect);
        if(tmp == 0)
        {
            intersect = this.transform.position;
        }
	}

    int get_line_intersection(float p0_x, float p0_y, float p1_x, float p1_y,
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

    float Angle(Vector2 v)
    {
        Vector2 o =new Vector2(0,0);
      //  o = points[0].position - points[1].position;
        float dx = v.x - o.x;
        float dy = v.y - o.y;
        float r = Mathf.Sqrt(dx * dx + dy * dy);
        float angle = Mathf.Atan2(dy, dx);
       //angle -= base_angle;
        if (angle < 0) angle += Mathf.PI * 2;
        return angle;
    }


    int get_line_intersection(Vector2 p0, Vector2 p1,
    Vector2 p2, Vector2 p3,out Vector2 pInter)
    {
        float s1_x, s1_y, s2_x, s2_y;
        s1_x = p1.x - p0.x; s1_y = p1.y - p0.y;
        s2_x = p3.x - p2.x; s2_y = p3.y - p2.y;
        pInter = new Vector2();
        float s, t;
        s = (-s1_y * (p0.x - p2.x) + s1_x * (p0.y - p2.y)) / (-s2_x * s1_y + s1_x * s2_y);
        t = (s2_x * (p0.y - p2.y) - s2_y * (p0.x - p2.x)) / (-s2_x * s1_y + s1_x * s2_y);

        if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
        {
            // Collision detected

            pInter.x = p0.x + (t * s1_x);

            pInter.y = p0.y + (t * s1_y);
            return 1;
        }

        return 0; // No collision
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawLine(firstA.position, firstB.position);
            Gizmos.DrawLine(secondA.position, secondB.position);
            Gizmos.DrawCube(intersect, new Vector2(0.4f, 0.4f));
            for(int i = 0; i< points.Length; i+=2)
            {
                Gizmos.DrawLine(points[i].position, points[i + 1].position);
                Gizmos.DrawCube(points[i].position, new Vector2(0.3f, 0.3f));
            }

        }
        else
        {
           
            Gizmos.DrawLine(firstA.position, firstB.position);
            Gizmos.DrawLine(secondA.position, secondB.position);
            for (int i = 0; i < points.Length; i += 2)
            {
                Gizmos.DrawLine(points[i].position, points[i + 1].position);
                Gizmos.DrawCube(points[i].position, new Vector2(0.3f, 0.3f));
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class LineDrawerWithHull : MonoBehaviour {




    public Transform []points;
    private MeshFilter mf;
    private Mesh mesh;
    public Vector3[] vertices;


    public Vector3 Vector3RotateByZ(Vector3 vec, float _angle)
    {
        
        float _cos = Mathf.Cos(_angle * Mathf.Deg2Rad);
        float _sin = Mathf.Sin(_angle * Mathf.Deg2Rad);

        float _x2 = vec.x * _cos - vec.y * _sin;
        float _y2 = vec.x * _sin + vec.y * _cos;

        return new Vector3(_x2, _y2, vec.z);
    }

    // Use this for initialization
    void Start ()
    {
        if (Application.isPlaying)
        {
            mf = GetComponent<MeshFilter>();
            mesh = mf.mesh;
        }
        vertices = new Vector3[points.Length * 4];

        Vector2[] uvs = new Vector2[points.Length * 4];

        

        for(int i=0,j=0;i<points.Length;i++,j+=4)
        {
            vertices[j] = points[i].position;
            vertices[j+ 1] = points[(i+1)% points.Length].position;
            vertices[j + 2] = vertices[j] - vertices[j + 1];
            vertices[j + 2] = Vector3RotateByZ( vertices[j + 2], 90).normalized/5;            
            vertices[j + 3] = vertices[j + 2] + vertices[j + 1];
            vertices[j + 2] = vertices[j + 2] + vertices[j];

            uvs[j] =new Vector2(0, 1);
            uvs[j+1] = new Vector2(0, 0);
            uvs[j+2] = new Vector2(1, 1);
            uvs[j+3] = new Vector2(1, 0);
        }

        int[] triangles = new int[points.Length * 6];
        for(int i=0,j=0,k=0;i<points.Length;i++,j+=4,k+=6)
        {
            triangles[k] = j +2;
            triangles[k + 1] = j;
            triangles[k + 2] = j + 3;
            triangles[k + 3] = j + 1;
            triangles[k + 4] = j + 3;
            triangles[k + 5] = j;

        }

        // mesh.Clear();
        if (Application.isPlaying)
        {
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            UnityEditor.MeshUtility.Optimize(mesh);
            mesh.RecalculateNormals();
        }




    }
	
	// Update is called once per frame
	void Update ()
    {


       // Debug.Log(Vector2.Dot(new Vector2(0.8f, -0.5f), new Vector2(-0.7f, 0.8f)));
    }

    void OnDrawGizmos()
    {
        /*
        if (Application.isPlaying && 1 == 2)
        {
            for (int j = 0; j < points.Length * 4; j += 4)
            {
                Gizmos.DrawLine(vertices[j], vertices[j + 1]);
                Gizmos.DrawLine(vertices[j + 1], vertices[j + 3]);
                Gizmos.DrawLine(vertices[j], vertices[j + 2]);
            }
        }
        */
    }

}

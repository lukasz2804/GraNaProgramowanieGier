using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPoly : MonoBehaviour {

	// Use this for initialization
	void Start () {


        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh;

        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-1,1,1),
            new Vector3(1,1,1),
            new Vector3(-1,-1,1),
            new Vector3(1,-1,1)
        };

        int[] triangles = new int[]
        {
            2,0,3,1,3,0
        };

        Vector2[] uvs = new Vector2[]
        {
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0)
        };

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        UnityEditor.MeshUtility.Optimize(mesh);
        mesh.RecalculateNormals();



	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

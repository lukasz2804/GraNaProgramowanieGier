using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh_updater : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateMesh()
    {
        Triangulator sc = GetComponent<Triangulator>();
        if (sc != null)
        {
            int numberOfPoints = sc.GetNumberOfPoints();
            Vector3[] vertices;
            MeshFilter mf;
            Mesh mesh;
            mf = GetComponent<MeshFilter>();
            mesh = mf.mesh;
            vertices = new Vector3[numberOfPoints];

            Vector2[] uvs = new Vector2[numberOfPoints];

            //  int[] triangles = new int[(numberOfPoints - 2) * 3];
            int[] triangles = new int[(numberOfPoints) * 3];
            triangles = sc.GetTriangulatorIdxs();

            mesh.vertices = sc.GetVertices();
            mesh.triangles = triangles;
            mesh.uv = MyVector3Extension.ToVector2Array(mesh.vertices);
            UnityEditor.MeshUtility.Optimize(mesh);
            mesh.RecalculateNormals();

            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                Debug.Log("generator vert:" + mesh.vertices[i]);
            }
            Debug.Log(mesh.vertices.Length.ToString() + " mesh.ver len");

        }
    }
}

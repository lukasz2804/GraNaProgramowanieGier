using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawerIsolate : MonoBehaviour
{
    public Material mat;
    public Vector3 first;
    public Vector3 second;
    public Transform []points;
    
    private Vector3 startVertex;
    private Vector3 mousePos;
    private Camera cam;


    public Matrix4x4 matrixV;



    public Vector3[] pointsToRender;


    private void Start()
    {
        cam = GetComponent<Camera>();
        for(int i=0;i<points.Length;i++)
        {
            pointsToRender[i] = cam.projectionMatrix.MultiplyVector(points[i].position);
        }

        matrixV = cam.projectionMatrix;
    }


    void Update()
    {

        mousePos = Input.mousePosition;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space");
            startVertex = new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0);
        }





    }
    void OnPostRender()
    {
        if (!mat)
        {
            Debug.LogError("Please Assign a material on the inspector");
            return;
        }

        GL.PushMatrix();
        mat.SetPass(0);
        //GL.LoadOrtho();
        GL.MultMatrix(transform.localToWorldMatrix);
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        for (int i = 0; i < points.Length; i+=2)
        {
            GL.Vertex(new Vector3(points[i].position.x - transform.position.x, points[i].position.y - transform.position.y, 0));
            GL.Vertex(new Vector3(points[i+1].position.x - transform.position.x, points[i+1].position.y - transform.position.y, 0));
        }
      //  GL.Vertex(second);
       // GL.Vertex(new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0));
        GL.End();
        GL.PopMatrix();


    }
    void Example()
    {
        startVertex = this.transform.position;
    }
}
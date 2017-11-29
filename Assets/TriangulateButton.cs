
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Triangulator))]
public class TriangulateButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Triangulator myScript = (Triangulator)target;
        if (GUILayout.Button("Build Object"))
        {
            IndexableCyclicalLinkedList<Vertex> lst;
            lst = new IndexableCyclicalLinkedList<Vertex>();
            for(int i=0;i<myScript.points.Length;i++)
            {
                lst.AddFirst(new Vertex(myScript.points[i].position,i));
            }
            myScript.ApplyNewPointsFromLst(lst);
        }
    }
}
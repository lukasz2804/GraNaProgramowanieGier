using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour {




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RunMeshAndCollidUpdaters()
    {
        PolygonColliderUpdater polyUpdate = GetComponent<PolygonColliderUpdater>();
        Mesh_updater meshUpdate = GetComponent<Mesh_updater>();
        if (meshUpdate != null && polyUpdate != null)
        {
            polyUpdate.UpdateColliderPoints();
            meshUpdate.UpdateMesh();
        }
    }
}

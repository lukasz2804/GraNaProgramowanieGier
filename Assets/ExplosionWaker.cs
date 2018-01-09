using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionWaker : MonoBehaviour {

    public Transform explosTr;

    private void Awake()
    {
        GameObject variableForPrefab = (GameObject)Resources.Load("prefabs/Explosion", typeof(GameObject));
        //  explosTr = GameObject.Find("Explosion").transform;
        explosTr = variableForPrefab.transform;
        explosTr.position += new Vector3(1, 0, 0);
    }

    public void WakeExplo(Vector2 pt, float radius)
    {       
        if (explosTr != null)
        {
            explosTr.transform.localScale = new Vector3(radius, radius);
            Instantiate(explosTr, new Vector3(pt.x, pt.y), this.transform.rotation, null);
        }
     
    }
    // Use this for initialization
    void Start ()
    {
      
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}

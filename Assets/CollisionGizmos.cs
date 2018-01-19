using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionGizmos : MonoBehaviour {


    public bool IsDebugMode;
	// Use this for initialization
    Vector2 []gizmosDrawTabColl;
    int numberOfGimzos;
	void Start () {

        gizmosDrawTabColl = new Vector2[10];

	}



    void OnCollisionEnter2D(Collision2D coll)
    {
        if (IsDebugMode)
        {
            int i = 0;

            foreach (ContactPoint2D contact in coll.contacts)
            {
                numberOfGimzos = coll.contacts.Length;
                Debug.DrawRay(contact.point, contact.normal, Color.white);
                Debug.Log(contact.point.ToString() + contact.normal.ToString());

                gizmosDrawTabColl[i] = contact.point;
                i++;
                gizmosDrawTabColl[i] = contact.normal;
                i++;

            }

            Debug.Log(" ");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (IsDebugMode)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                Debug.DrawRay(contact.point, contact.normal, Color.white);
                Debug.Log(contact.point.ToString() + contact.normal.ToString());
            }
        }
        
    }

    void OnDrawGizmos()
    {
        
        if(Application.isPlaying && IsDebugMode)
        {
            for(int i=0;i<numberOfGimzos*2;i+=2)
            {
                Gizmos.DrawLine(gizmosDrawTabColl[i], gizmosDrawTabColl[i + 1]+ gizmosDrawTabColl[i]);
            }
        }
    }

        // Update is called once per frame
        void Update () {
		
	}
}

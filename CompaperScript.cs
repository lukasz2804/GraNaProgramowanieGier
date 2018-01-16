using UnityEngine;
using System.Collections;



[ExecuteInEditMode]
public class CompaperScript : MonoBehaviour
{

    float AngleBetweenVectors(Vector2 v1,Vector2 v2)
    {
        return Mathf.Atan2(v2.y - v1.y, v2.x - v1.x);
    }
    


    public Transform []tab;

    Vector2[] vecTab;



	// Use this for initialization
	void Start ()
    {
        vecTab = new Vector2[tab.Length];


        for (int i = 0; i < tab.Length; i++)
        {
            vecTab[i] = tab[i].position;
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < tab.Length; i++)
        {
            vecTab[i] = tab[i].position;
        }

        Vector2 V1;
        Vector2 V2;

        if (Application.isPlaying)
        {
            V1 =vecTab[1] - vecTab[0];
            V2 = vecTab[2] - vecTab[0];
            //Debug.Log(V1.ToString() + V2.ToString());
            Debug.Log((Mathf.Rad2Deg *AngleBetweenVectors(Vector2.up, V2)).ToString()  + " " + (Mathf.Rad2Deg * AngleBetweenVectors(Vector2.up, V1)).ToString() );

          //  Debug.Log(Mathf.Rad2Deg * AngleBetweenVectors(Vector2.up, V1));


        }

    }
}

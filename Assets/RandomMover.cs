using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMover : MonoBehaviour {

    public float radius;

    private float basicX, basicY;
	// Use this for initialization
	void Start () {
        InvokeRepeating("MoveRand", 2f, 2f);  //1s delay, repeat every 1s
        basicX = this.transform.position.x;
        basicY = this.transform.position.y;
	}

    // Update is called once per frame
    private void MoveRand()
    {
        float xDiff, yDiff;
        xDiff = Random.Range(-radius, radius);
        yDiff = Random.Range(-radius, radius);
        this.transform.position = new Vector2(basicX + xDiff, basicY + yDiff);
    }

    void Update ()
    {
       

	}
}

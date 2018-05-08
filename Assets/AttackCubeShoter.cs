using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCubeShoter : MonoBehaviour {

    public float degreeAngle;

    public int typeOfWeapon;

    public Transform tr;

    public float maximumPower;

    public float powerMultiplier = 20;

    float power;

    private IEnumerator coroutine;

    bool isShotting;


    private void Awake()
    {
        GameObject variableForPrefab = (GameObject)Resources.Load("prefabs/AttackCube", typeof(GameObject));
        //  explosTr = GameObject.Find("Explosion").transform;
        tr = variableForPrefab.transform;
    }
    // Use this for initialization
    void Start ()
    {
        isShotting = false;
        coroutine = Fire(0.5f);
    }
    private IEnumerator Fire(float waitTime)
    {
        isShotting = true;
        
        Transform tmp;
            Vector2 trg;
            trg = LineFuctions.RotateVecPFromI(Vector2.zero, degreeAngle, Vector2.up);
            tmp = Instantiate(tr, this.transform.position + new Vector3(trg.x*2, trg.y*2), this.transform.rotation, null);
        tmp.GetComponent<Rigidbody2D>().velocity = new Vector2(trg.x * powerMultiplier, trg.y * powerMultiplier);
        GameObject.Find("MotherOfEverything").transform.Find("Manager").GetComponent<TurnsSystem>().setPlayerNull();
        yield return new WaitUntil(() => tmp == null);
        GameObject.Find("MotherOfEverything").transform.Find("Manager").GetComponent<Players>().dealDMG(51);

 
        GameObject.Find("MotherOfEverything").transform.Find("Manager").GetComponent<TurnsSystem>().setChangePlayer(true);
        isShotting = false;
        
    }
 
	
	// Update is called once per frame
	void Update ()
    {
        power = Input.GetAxis("Fire1");
        if(power > 0.1f && !isShotting)
        {
            
            //isShotting = true;
            StartCoroutine(Fire(1.0f));

        }

    }
}

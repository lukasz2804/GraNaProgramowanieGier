using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCubeShoter : MonoBehaviour {

    private float degreeAngle=90;
    private int degreeStep = 10;

    public int typeOfWeapon;

    public Transform tr;

    public float maximumPower;

    public float powerMultiplier = 20;

    private string prefix = "prefabs/";

    private string sufix = "AttackBomb";

    private int weaponsCunt = 3;

    float power;

    private IEnumerator coroutine;

    bool isShotting;

    private int selectedWeapon = 1;


    private void Awake()
    {
        GameObject variableForPrefab = (GameObject)Resources.Load(prefix + sufix, typeof(GameObject));
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
         //   Rigidbody2D rg = tr.GetComponent<Rigidbody2D>();
          //  trg = trg *powerMultiplier;
        //    rg.velocity = trg;
       // rg.velocity = new Vector2(trg.x * powerMultiplier, trg.y * powerMultiplier);
            tmp = Instantiate(tr, this.transform.position + new Vector3(trg.x*2, trg.y*2), this.transform.rotation, null);
        tmp.GetComponent<Rigidbody2D>().velocity = new Vector2(trg.x * powerMultiplier, trg.y * powerMultiplier);
        yield return new WaitForSeconds(waitTime);
        GameObject.Find("MotherOfEverything").transform.Find("Manager").GetComponent<TurnsSystem>().setChangePlayer(true);
        isShotting = false;
        
           
            //  print("WaitAndPrint " + Time.time);
        
    }
 
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (degreeAngle >= 360)
                degreeAngle = 0;
            else
                degreeAngle = degreeAngle + degreeStep;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (degreeAngle <= 0)
                degreeAngle = 360;
            else
                degreeAngle = degreeAngle - degreeStep;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (selectedWeapon >= weaponsCunt-1)
                selectedWeapon = 0;
            else
                selectedWeapon++;       
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (selectedWeapon <= 0)
                selectedWeapon =  weaponsCunt - 1;
            else
                selectedWeapon--;
        }

        switch (selectedWeapon)
        {
            case 1:
                sufix = "AttackBomb";
                break;
            case 2:
                sufix = "AttackGrenade";
                break;
            default:
                sufix = "AttackMultiGrenade";
                break;
        }

        GameObject variableForPrefab = (GameObject)Resources.Load(prefix + sufix, typeof(GameObject));
        tr = variableForPrefab.transform;

        power = Input.GetAxis("Fire1");
        if(power > 0.1f && !isShotting)
        {
            
            //isShotting = true;
            StartCoroutine(Fire(1.0f));

        }

    }
}

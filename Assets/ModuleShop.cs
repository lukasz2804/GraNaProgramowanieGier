using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleShop : MonoBehaviour
{

    public int WhichPlayer;
    public int Coins;
    public string Title;
    public Text TitleText;
    public Transform polygon1;
    public Transform polygon2;
    public Transform polygon3;
    public Transform list1;
    public Transform list2;
    public Transform Manager;
    public Text Text;
    public Transform Mother;
    public Transform BasicHeadPrefab;
    public Transform JumpingHeadPrefab;
    public Transform DoubleJumpHeadPrefab;
    public Transform JumpAndRotHeadPrefab;
    public Transform WheelingHeadPrefab;

    private int _whichLevel;
    public Transform []LevelSrcs;
    public Transform []LevelTrgs;

    public void SetLevelDecission(int i)
    {
        Mother = GameObject.Find("MotherOfEverything").transform;
        polygon1 = Mother.Find("Polygons1");
        Manager = Mother.Find("Manager");
        polygon2 = Mother.Find("Polygons2");
        polygon3 = Mother.Find("Polygons3");
        list1 = Mother.Find("Player1List");
        list2 = Mother.Find("Player2List");


        _whichLevel = i;
        StartCoroutine(WaitForEndOfFrame());
        Title = string.Format("player {0} have {1} coins...", WhichPlayer, Coins);
        Title = Title + i.ToString();
        if(i == 1)
        {
            polygon2.gameObject.SetActive(false);
            polygon3.gameObject.SetActive(false);
        }
        else if (i == 2)
        {
            polygon1.gameObject.SetActive(false);
            polygon3.gameObject.SetActive(false);
        }
        else if (i == 3)
        {
            polygon1.gameObject.SetActive(false);
            polygon2.gameObject.SetActive(false);
        }
        
    }


    public void CreateRobotOnListTr(Transform trLst,Transform robotToCreate)
    {
        Vector2 robotSpawnPos = LevelTrgs[_whichLevel].position - LevelSrcs[_whichLevel].position;
        robotSpawnPos = (Vector2)LevelSrcs[_whichLevel].position + robotSpawnPos * Random.Range(0f, 1f);
        Instantiate(robotToCreate, robotSpawnPos, this.transform.rotation, trLst);

    }

    public void PlayerPick(int headIdx)
    {
        if((BasicHeads)headIdx == BasicHeads.BasicHead)
        {
            if(Coins - 120 < 0)
            {
                return;
            }
            Coins -= 120;
            if (WhichPlayer == 1)
            {
                CreateRobotOnListTr(list1, BasicHeadPrefab);
            }
            else
            {
                CreateRobotOnListTr(list2, BasicHeadPrefab);
            }
        }
        else if((BasicHeads)headIdx == BasicHeads.BasicHeadJump)
        {
            if (Coins - 100 < 0)
            {
                return;
            }
            Coins -= 100;
            if (WhichPlayer == 1)
            {
                CreateRobotOnListTr(list1, JumpingHeadPrefab);
            }
            else
            {
                CreateRobotOnListTr(list2, JumpingHeadPrefab);
            }
        }
        else if((BasicHeads)headIdx == BasicHeads.BasicHeadDoubleJump)
        {
            if (Coins - 150 < 0)
            {
                return;
            }
            Coins -= 150;
            if (WhichPlayer == 1)
            {
                CreateRobotOnListTr(list1, DoubleJumpHeadPrefab);
            }
            else
            {
                CreateRobotOnListTr(list2, DoubleJumpHeadPrefab);
            }
        }
        else if((BasicHeads)headIdx == BasicHeads.BasicHeadRotAndJump)
        {
            if (Coins - 170 < 0)
            {
                return;
            }
            Coins -= 170;
            if (WhichPlayer == 1)
            {
                CreateRobotOnListTr(list1, JumpAndRotHeadPrefab);
            }
            else
            {
                CreateRobotOnListTr(list2, JumpAndRotHeadPrefab);
            }
        }
        else if((BasicHeads)headIdx == BasicHeads.BasicHeadWheel)
        {
            if (Coins - 60 < 0)
            {
                return;
            }
            Coins -= 60;
            if (WhichPlayer == 1)
            {
                CreateRobotOnListTr(list1, WheelingHeadPrefab);
            }
            else
            {
                CreateRobotOnListTr(list2, WheelingHeadPrefab);
            }
        }
        ApplyNewCoins();
        if(Coins < 60)
        {
            EndOfPlayerShop();
        }
    }

    void ApplyNewCoins()
    {
        Title = string.Format("player {0} have {1} coins...", WhichPlayer, Coins);
        TitleText.text = Title;
    }

    public void EndOfPlayerShop()
    {
        if (WhichPlayer == 1)
        {
            Coins = 600;
            WhichPlayer = 2;
            Title = string.Format("player {0} have {1} coins...", WhichPlayer, Coins);
            TitleText.text = Title;
        }
        else
        {
            EndOfShopping();
        }
    }

    void EndOfShopping()
    {
        this.gameObject.SetActive(false);
        Manager.gameObject.SetActive(true);
    }

    IEnumerator WaitForEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
    }

	// Use this for initialization
	void Start ()
    {
      //  Title = string.Format("player {0} have {1} coins...", WhichPlayer,Coins);
        TitleText.text = Title;
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}

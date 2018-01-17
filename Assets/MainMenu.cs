using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour {

	public bool isStart;
	public bool isSound;
	public bool isQuit;
	public bool Level1;
	public bool Level2;
	public bool Level3;
	public bool toMainMenu;

	public Camera camera1;
	public Camera camera2;
	public Camera main;

	private bool muted = false;
	public Transform polygon1;
	public Transform polygon2;
	public Transform polygon3;
	public Transform list1;
	public Transform list2;
	public Transform Manager;
	public Text Text;
	public Transform Mother;

	public AudioClip sound;
	private AudioSource source { get { return GetComponent<AudioSource> ();}}

	void DisableAllChild(Transform tr)
	{
		
	}

	void Start() {
		Mother = GameObject.Find ("MotherOfEverything").transform;
		main = Mother.Find ("Main Camera").transform.GetComponent<Camera> ();
		polygon1 = Mother.Find("Polygons");
		Manager = Mother.Find("Manager");
		polygon2 = Mother.Find("Polygons2");
		polygon3 = Mother.Find("Polygons3");
		list1 = Mother.Find("Player1List");
		list2 = Mother.Find("Player2List");
		Text = GameObject.Find ("Canvas").transform.GetChild (0).GetComponent<Text> ();

		/*
		polygon1.gameObject.SetActive (false);
		polygon2.gameObject.SetActive (false);
		polygon3.gameObject.SetActive (false);
		Manager.gameObject.SetActive (false);
		Text.gameObject.SetActive (false);
		*/
		Text.text = "wychylylybym";



		gameObject.AddComponent<AudioSource> ();
		source.clip = sound;
		source.playOnAwake = true;
		source.Play();
		source.loop = true;
		camera1.enabled = true;
		camera2.enabled = false;

	}
	void OnMouseUp(){
		if(isStart)
		{
			TextMesh t = GetComponent<TextMesh> ();
			t.color = Color.black;
			camera2.enabled = true;
			camera1.enabled = false;
			// Application.LoadLevel(1);
		}
		if (isSound) {
			TextMesh t = GetComponent<TextMesh> ();
			if (!muted) {
				t.text = "Sound Off";
				AudioListener.volume = 0.0f;
				muted = true;
			} else {		    
				t.text = "Sound On";
				AudioListener.volume = 1.0f;
				muted = false;
			}
			t.color = Color.black;

		}
		if (isQuit)
		{
			TextMesh t = GetComponent<TextMesh> ();
			t.color = Color.black;
			Application.Quit();
		}
		if (Level1) {
			main.enabled = true;
			camera2.enabled = false;
			camera1.enabled = false;
			polygon1.gameObject.SetActive (true);
			Manager.gameObject.SetActive (true);
			Text.gameObject.SetActive (true);

			// state = true;

		}
		if (Level2) {
			main.enabled = true;
			camera2.enabled = false;
			camera1.enabled = false;
			polygon2.gameObject.SetActive (true);
			Manager.gameObject.SetActive (true);
			Text.gameObject.SetActive (true);
		}
		if (Level3) {
			main.enabled = true;
			camera2.enabled = false;
			camera1.enabled = false;
			polygon3.gameObject.SetActive (true);
			Manager.gameObject.SetActive (true);
			Text.gameObject.SetActive (true) ;
		}
		if (toMainMenu) {
			camera2.enabled = false;
			camera1.enabled = true;

		}
	} 
}

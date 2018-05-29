using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraSound : MonoBehaviour {
	public AudioClip sound;
	private AudioSource source { get { return GetComponent<AudioSource> ();}}

	// Use this for initialization
	void Start () {
		source.clip = sound;
		source.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

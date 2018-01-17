using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour {

	public AudioClip sound;
	private AudioSource source { get { return GetComponent<AudioSource> ();}}

	void Start(){
		gameObject.AddComponent<AudioSource> ();
		source.clip = sound;
		source.playOnAwake = false;
	}

	void OnMouseEnter(){
		TextMesh t = GetComponent<TextMesh> ();
		t.color = Color.red;
		source.PlayOneShot (sound);
	}

	void OnMouseExit() {
		TextMesh t = GetComponent<TextMesh> ();
		t.color = Color.white;
	}
}

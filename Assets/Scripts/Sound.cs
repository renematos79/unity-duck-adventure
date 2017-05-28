using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {

	public AudioSource Audio;
	private UnityEngine.UI.Toggle SoundManager;
	private UnityEngine.UI.Text SoundText;

	// Use this for initialization
	void Start () {
		SoundManager = GetComponent<UnityEngine.UI.Toggle> ();
		SoundText = SoundManager.GetComponentInChildren<UnityEngine.UI.Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnSoundClick(){
		if (SoundManager.isOn == true) {
			Audio.Play ();
			SoundText.text = "Sound Off";
		} else {
			Audio.Stop ();
			SoundText.text = "Sound On";
		}
	}
}
